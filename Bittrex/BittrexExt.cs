//=================================================================================
//
//  Created by: MrYukonC
//  Created on: 27 OCT 2017
//
//=================================================================================
//
// MIT License
//
// Copyright (c) 2017 MrYukonC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//=================================================================================

using System;
using System.Collections.Generic;
using System.Threading;

namespace MYC
{
    public class BittrexExt : Bittrex
    {
        //==========================================================
        public BittrexExt( String APIKey, String APISecret ) : base( APIKey, APISecret ) {}


        //==========================================================
        public static Boolean GetIsCurrencyValid( String Currency )
        {
            return !String.IsNullOrEmpty( Currency ) && Currency.Length == 3;
        }


        //==========================================================
        public static Boolean GetIsMarketValid( Bittrex B, String Market )
        {
            if( String.IsNullOrEmpty( Market ) || Market.Length != 7 )
                return false;

            return B.GetOrderBook( Market, BittrexOrderBook.Type.Buy, 1 ).Success;
        }


        //==========================================================
        public void AutoSell( String Market, String SellCurrency, String BuyCurrency, Double MinSellThresh = 0.001 )
        {
            const Int32 SleepMS = 1000;

            if( !GetIsCurrencyValid( SellCurrency ) )
                return;

            if( !GetIsCurrencyValid( BuyCurrency ) )
                return;

            if( !GetIsMarketValid( this, Market ) )
                return;

            //String Market = BuyCurrency.ToUpper() + "-" + SellCurrency.ToUpper();            

            String SellOrderUuid = String.Empty;

            while( true )
            {
                BittrexResult<BittrexBalance> SellBalance = base.GetBalance( SellCurrency );

                if( !SellBalance.Success )
                {
                    Console.WriteLine( SellBalance.Message );
                    break;
                }

                if( SellBalance.Result.Available < MinSellThresh )
                {
                    Console.WriteLine( String.Format( "{0:0.00000000} {1} does not meet the specified minimum sell amount threshold of {2} {3}", SellBalance.Result.Available, SellCurrency, MinSellThresh, SellCurrency ) );
                    break;
                }

                Console.WriteLine( String.Format( "{0} balance: {1:0.00000000}", SellCurrency, SellBalance.Result.Available ) );

                if( !String.IsNullOrEmpty( SellOrderUuid ) )
                    base.Cancel( SellOrderUuid );
                    
                SellOrderUuid = String.Empty;

                BittrexResult<BittrexOrderBook> OrderBook = base.GetOrderBook( Market, BittrexOrderBook.Type.Buy, 1 );

                if( !OrderBook.Success )
                {
                    Console.WriteLine( OrderBook.Message );
                    //Thread.Sleep( SleepMS );
                    //continue;
                    break;
                }

                BittrexOrderBook.Entry BestOffer = OrderBook.Result.Buy[ 0 ];

                Double SellAmount = Math.Min( SellBalance.Result.Available, BestOffer.Quantity );

                Console.WriteLine( String.Format( "Attempt to sell {0:0.00000000} {1} @ {2:0.00000000} {3}/{4}", SellAmount, SellCurrency, BestOffer.Rate, SellCurrency, BuyCurrency ) );

                BittrexResult<BittrexUuid> OrderUuid = base.SellLimit( Market, SellAmount, BestOffer.Rate );

                if( !OrderUuid.Success )
                {
                    Console.WriteLine( OrderUuid.Message );
                    //continue;
                    break;
                }

                SellOrderUuid = OrderUuid.Result.Uuid;

                Thread.Sleep( SleepMS );
            }
        }


        //==========================================================
        public void AutoBuy( String Market, String SellCurrency, String BuyCurrency, Double MinSellThresh = 0.001 )
        {
            const Int32 SleepMS = 1000;

            if( !GetIsCurrencyValid( SellCurrency ) )
                return;

            if( !GetIsCurrencyValid( BuyCurrency ) )
                return;

            if( !GetIsMarketValid( this, Market ) )
                return;

            String BuyOrderUuid = String.Empty;

            while( true )
            {
                BittrexResult<BittrexBalance> SellBalance = base.GetBalance( SellCurrency );

                if( !SellBalance.Success )
                {
                    Console.WriteLine( SellBalance.Message );
                    break;
                }

                if( SellBalance.Result.Available < MinSellThresh )
                {
                    Console.WriteLine( String.Format( "{0:0.00000000} {1} does not meet the specified minimum sell amount threshold of {2} {3}", SellBalance.Result.Available, SellCurrency, MinSellThresh, SellCurrency ) );
                    break;
                }

                Console.WriteLine( String.Format( "{0} balance: {1:0.00000000}", SellCurrency, SellBalance.Result.Available ) );

                if( !String.IsNullOrEmpty( BuyOrderUuid ) )
                    base.Cancel( BuyOrderUuid );
                    
                BuyOrderUuid = String.Empty;

                BittrexResult<BittrexOrderBook> OrderBook = base.GetOrderBook( Market, BittrexOrderBook.Type.Sell, 1 );

                if( !OrderBook.Success )
                {
                    Console.WriteLine( OrderBook.Message );
                    //Thread.Sleep( SleepMS );
                    //continue;
                    break;
                }

                BittrexOrderBook.Entry BestOffer = OrderBook.Result.Sell[ 0 ];

                Double BuyAmount = Math.Min( BestOffer.Quantity, SellBalance.Result.Available / BestOffer.Rate );

                BuyAmount = Math.Truncate( BuyAmount * 10 ) / 10;

                Console.WriteLine( String.Format( "Attempt to buy {0:0.00000000} {1} @ {2:0.00000000} {3}/{4} for a total of {5:0.000000000000} {6}", BuyAmount, BuyCurrency, BestOffer.Rate, SellCurrency, BuyCurrency, BuyAmount * BestOffer.Rate, SellCurrency ) );

                BittrexResult<BittrexUuid> OrderUuid = base.BuyLimit( Market, BuyAmount, BestOffer.Rate );

                if( !OrderUuid.Success )
                {
                    Console.WriteLine( OrderUuid.Message );
                    //continue;
                    break;
                }

                BuyOrderUuid = OrderUuid.Result.Uuid;

                Thread.Sleep( SleepMS );
            }
        }


        //==========================================================
        public void WithdrawAll( String Currency, String DestAddress, Double MinAmountThresh = 0.1 )
        {
            if( !GetIsCurrencyValid( Currency ) )
                return;

            BittrexResult<BittrexBalance> Balance = base.GetBalance( Currency );

            if( !Balance.Success )
            {
                Console.WriteLine( Balance.Message );
                return;
            }

            Console.WriteLine( "{0} balance: {1:0.00000000}", Currency, Balance.Result.Available );

            if( Balance.Result.Available >= MinAmountThresh )
                base.Withdraw( Currency, Balance.Result.Available, DestAddress );
        }
    }
}