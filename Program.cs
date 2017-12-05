//=================================================================================
//
//  Created by: MrYukonC
//  Created on: 21 OCT 2017
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
    public enum Exchange
    {
        Unknown,
        Bittrex,
        Poloniex
    }

    class Program
    {
        // static Exchange s_Exchange  = Exchange.Unknown;
        // static String   s_APIKey    = String.Empty;
        // static String   s_APISecret = String.Empty;


        //==========================================================
        static void Main( String[] Args )
        {
            // Check the following out for parsing command line args:
            // https://blog.terribledev.io/Parsing-cli-arguments-in-dotnet-core-Console-App/

            BittrexExt B = new BittrexExt( "BITTREX_API_KEY_HERE", "BITTREX_API_SECRET_HERE" );
            
            B.AutoSell( "BTC-ZEC", "ZEC", "BTC" );
            B.WithdrawAll( "ETH", "YOUR_DESTINATION_ADDRESS_HERE" );

            //TestGetMarkets( B );
            //TestGetCurrencies( B );
            //TestGetTicker( B, "ETH-ZEC" );
            //TestGetMarketSummaries( B );
            //TestGetMarketSummary( B, "ETH-ZEC" );
            //TestGetOrderBook( B, "ETH-ZEC", BittrexOrderBook.Type.Both, 5 );
            //TestGetMarketHistory( B, "ETH-ZEC" );
            //TestGetBalances( B );
            //TestGetBalance( B, "ZEC" );
            //TestGetBalance( B, "ETH" );
            //TestGetOpenOrders( B, "ETH-ZEC" );
            //TestGetOrderHistory( B, "ETH-ZEC" );
            //TestGetWithdrawalHistory( B, "ETH" );
            //TestGetDepositHistory( B, "ZEC" );
            //TestGetDepositAddress( B, "ZEC" );
            //TestGetDepositAddress( B, "ETH" );
        }


        //==========================================================
        static Boolean GetIsResultValid( BittrexMsg Msg )
        {
            if( !Msg.Success )
                Console.WriteLine( Msg.Message );

            return Msg.Success;
        }


        //==========================================================
        static void TestGetMarkets( Bittrex B )
        {
            BittrexResult<List<BittrexMarket>> Markets = B.GetMarkets();

            if( GetIsResultValid( Markets ) )
                foreach( BittrexMarket BM in Markets.Result )
                    Console.WriteLine( BM.ToString() );
        }


        //==========================================================
        static void TestGetCurrencies( Bittrex B )
        {
            BittrexResult<List<BittrexCurrency>> Currencies = B.GetCurrencies();

            if( GetIsResultValid( Currencies ) )
                foreach( BittrexCurrency BC in Currencies.Result )
                    Console.WriteLine( BC.ToString() );
        }


        //==========================================================
        static void TestGetTicker( Bittrex B, String Market )
        {
            BittrexResult<BittrexTicker> BT = B.GetTicker( Market );

                if( GetIsResultValid( BT ) )
                    Console.WriteLine( BT.Result.ToString() );
        }


        //==========================================================
        static void TestGetMarketSummaries( Bittrex B )
        {
            BittrexResult<List<BittrexMarketSummary>> MarketSummaries = B.GetMarketSummaries();

            if( GetIsResultValid( MarketSummaries ) )
                foreach( BittrexMarketSummary BMS in MarketSummaries.Result )
                    Console.WriteLine( BMS.ToString() );
        }


        //==========================================================
        static void TestGetMarketSummary( Bittrex B, String Market )
        {
            BittrexResult<List<BittrexMarketSummary>> BMS = B.GetMarketSummary( Market );

            if( GetIsResultValid( BMS ) )
                Console.WriteLine( BMS.Result[ 0 ].ToString() );
        }


        //==========================================================
        static void TestGetOrderBook( Bittrex B, String Market, BittrexOrderBook.Type OrderBookType, Int32 Depth )
        {
            BittrexResult<BittrexOrderBook> BOB = B.GetOrderBook( Market, OrderBookType, Depth );

            if( GetIsResultValid( BOB ) )
                Console.WriteLine( BOB.Result.ToString() );
        }


        //==========================================================
        static void TestGetMarketHistory( Bittrex B, String Market )
        {
            BittrexResult<List<BittrexTrade>> MarketHistory = B.GetMarketHistory( Market );

            if( GetIsResultValid( MarketHistory ) )
                foreach( BittrexTrade BT in MarketHistory.Result )
                    Console.WriteLine( BT.ToString() );
        }


        //==========================================================
        static void TestGetBalances( Bittrex B )
        {
            BittrexResult<List<BittrexBalance>> Balances = B.GetBalances();

            if( GetIsResultValid( Balances ) )
                foreach( BittrexBalance BB in Balances.Result )
                    Console.WriteLine( BB.ToString() );
        }


        //==========================================================
        static void TestGetBalance( Bittrex B, String Currency )
        {
            BittrexResult<BittrexBalance> BB = B.GetBalance( Currency );

            if( GetIsResultValid( BB ) )
                Console.WriteLine( BB.Result.ToString() );
        }


        //==========================================================
        static void TestGetOpenOrders( Bittrex B, String Market )
        {
            BittrexResult<List<BittrexOrder1>> Orders = B.GetOpenOrders( Market );

            if( GetIsResultValid( Orders ) )
                foreach( BittrexOrder1 BO1 in Orders.Result )
                    Console.WriteLine( BO1.ToString() );
        }


        //==========================================================
        static void TestGetOrderHistory( Bittrex B, String Market )
        {
            BittrexResult<List<BittrexOrder1>> Orders = B.GetOrderHistory( Market );

            if( GetIsResultValid( Orders ) )
                foreach( BittrexOrder1 BO1 in Orders.Result )
                    Console.WriteLine( BO1.ToString() );
        }


        //==========================================================
        static void TestGetWithdrawalHistory( Bittrex B, String Currency )
        {
            BittrexResult<List<BittrexWithdrawal>> Withdrawals = B.GetWithdrawalHistory( Currency );

            if( GetIsResultValid( Withdrawals ) )
                foreach( BittrexWithdrawal BW in Withdrawals.Result )
                    Console.WriteLine( BW.ToString() );
        }


        //==========================================================
        static void TestGetDepositHistory( Bittrex B, String Currency )
        {
            BittrexResult<List<BittrexDeposit>> Deposits = B.GetDepositHistory( Currency );

            if( GetIsResultValid( Deposits ) )
                foreach( BittrexDeposit BD in Deposits.Result )
                    Console.WriteLine( BD.ToString() );
        }


        //==========================================================
        static void TestGetDepositAddress( Bittrex B, String Currency )
        {
            BittrexResult<BittrexAddress> BA = B.GetDepositAddress( Currency );

            if( GetIsResultValid( BA ) )
                Console.WriteLine( BA.Result.ToString() );
        }
    }
}
