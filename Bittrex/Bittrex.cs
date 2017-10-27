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
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;

namespace MYC
{
    public class Bittrex
    {
        static readonly String API_URL = "https://bittrex.com/api/";
        static readonly String API_VER = "v1.1";

        private String              m_APIKey;
        private String              m_APISecret;

        private BittrexHttpClient   m_HttpClient;


        //==========================================================
        public Bittrex( String APIKey, String APISecret )
        {
            m_APIKey        = APIKey;
            m_APISecret     = APISecret;
            m_HttpClient    = new BittrexHttpClient( API_URL );
        }


        //==========================================================
        protected BittrexMsg GetInternal<T>( String APICall, String APICallSigned = null ) where T : BittrexMsg, new()
        {
            try
            {
                Task<HttpResponseMessage> ResponseTask = m_HttpClient.Get( APICall, APICallSigned );

                if( ResponseTask == null )
                    return new T().Init( false, "Failed to get response task." );

                ResponseTask.Wait();

                HttpResponseMessage ResponseMsg = ResponseTask.Result;

                if( ResponseMsg.IsSuccessStatusCode == false )
                    return new T().Init( false, ResponseMsg.ReasonPhrase.ToString() );

                return ResponseMsg.Content.ReadAsAsync<T>().Result;
            }
            catch( Exception E )
            {
                return new T().Init( false, E.Message );
            }
        }


        //==========================================================
        public BittrexResult<List<BittrexMarket>> GetMarkets()
        {
            return GetInternal<BittrexResult<List<BittrexMarket>>>( API_URL + API_VER + "/public/getmarkets" ) as BittrexResult<List<BittrexMarket>>;
        }


        //==========================================================
        public BittrexResult<List<BittrexCurrency>> GetCurrencies()
        {
            return GetInternal<BittrexResult<List<BittrexCurrency>>>( API_URL + API_VER + "/public/getcurrencies" ) as BittrexResult<List<BittrexCurrency>>;
        }


        //==========================================================
        public BittrexResult<BittrexTicker> GetTicker( String Market )
        {
            return GetInternal<BittrexResult<BittrexTicker>>( API_URL + API_VER + "/public/getticker?market=" + Market ) as BittrexResult<BittrexTicker>;
        }


        //==========================================================
        public BittrexResult<List<BittrexMarketSummary>> GetMarketSummaries()
        {
            return GetInternal<BittrexResult<List<BittrexMarketSummary>>>( API_URL + API_VER + "/public/getmarketsummaries" ) as BittrexResult<List<BittrexMarketSummary>>;
        }


        //==========================================================
        public BittrexResult<List<BittrexMarketSummary>> GetMarketSummary( String Market )
        {
            return GetInternal<BittrexResult<List<BittrexMarketSummary>>>( API_URL + API_VER + "/public/getmarketsummary?market=" + Market ) as BittrexResult<List<BittrexMarketSummary>>;
        }


        //==========================================================
        // This is BS Bittrex...
        // Either return an array or an object for _all_ options.
        // Not one or the other depending on which option is passed in.
        public BittrexResult<BittrexOrderBook> GetOrderBook( String Market, BittrexOrderBook.Type OrderBookType, Int32 Depth )
        {
            Depth = Math.Min( 100, Math.Max( 1, Depth ) );

            String APICall = API_URL + API_VER + "/public/getorderbook?market=" + Market + "&type=" + OrderBookType.ToString().ToLower() + "&depth=" + Depth;

            if( OrderBookType == BittrexOrderBook.Type.Buy )
            {
                BittrexResult<List<BittrexOrderBook.Entry>> Result = GetInternal<BittrexResult<List<BittrexOrderBook.Entry>>>( APICall ) as BittrexResult<List<BittrexOrderBook.Entry>>;

                return new BittrexResult<BittrexOrderBook>( Result.Success, Result.Message, new BittrexOrderBook( Result.Success ? Result.Result : new List<BittrexOrderBook.Entry>(), new List<BittrexOrderBook.Entry>() ) );
            }
            else if( OrderBookType == BittrexOrderBook.Type.Sell )
            {
                BittrexResult<List<BittrexOrderBook.Entry>> Result = GetInternal<BittrexResult<List<BittrexOrderBook.Entry>>>( APICall ) as BittrexResult<List<BittrexOrderBook.Entry>>;

                return new BittrexResult<BittrexOrderBook>( Result.Success, Result.Message, new BittrexOrderBook( new List<BittrexOrderBook.Entry>(), Result.Success ? Result.Result : new List<BittrexOrderBook.Entry>() ) );
            }
            
            return GetInternal<BittrexResult<BittrexOrderBook>>( APICall ) as BittrexResult<BittrexOrderBook>;
        }


        //==========================================================
        public BittrexResult<List<BittrexTrade>> GetMarketHistory( String Market )
        {
            return GetInternal<BittrexResult<List<BittrexTrade>>>( API_URL + API_VER + "/public/getmarkethistory?market=" + Market ) as BittrexResult<List<BittrexTrade>>;
        }


        //==========================================================
        public BittrexResult<BittrexUuid> BuyLimit( String Market, Double Quantity, Double Rate )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/market/buylimit", "market=" + Market + "&quantity=" + Quantity + "&rate=" + Rate, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<BittrexUuid>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<BittrexUuid>;
        }


        //==========================================================
        public BittrexResult<BittrexUuid> SellLimit( String Market, Double Quantity, Double Rate )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/market/selllimit", "market=" + Market + "&quantity=" + Quantity + "&rate=" + Rate, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<BittrexUuid>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<BittrexUuid>;
        }


        //==========================================================
        public BittrexResult<String> Cancel( String OrderUuid )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/market/cancel", "uuid=" + OrderUuid, m_APIKey, m_APISecret );

             return GetInternal<BittrexResult<BittrexUuid>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<String>;
        }


        //==========================================================
        public BittrexResult<List<BittrexOrder1>> GetOpenOrders( String Market )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/account/getopenorders", "market=" + Market, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<List<BittrexOrder1>>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<List<BittrexOrder1>>;
        }


        //==========================================================
        public BittrexResult<List<BittrexBalance>> GetBalances()
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/account/getbalances", null, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<List<BittrexBalance>>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<List<BittrexBalance>>;
        }


        //==========================================================
        public BittrexResult<BittrexBalance> GetBalance( String Currency )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/account/getbalance", "currency=" + Currency, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<BittrexBalance>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<BittrexBalance>;
        }


        //==========================================================
        public BittrexResult<BittrexAddress> GetDepositAddress( String Currency )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/account/getdepositaddress", "currency=" + Currency, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<BittrexAddress>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<BittrexAddress>;
        }


        //==========================================================
        public BittrexResult<BittrexUuid> Withdraw( String Currency, Double Quantity, String Address, String PaymentId = null )
        {
            String APICall = API_URL + API_VER + "/account/withdraw";
            String APIArgs = "currency=" + Currency + "&quantity=" + Quantity + "&address=" + Address;

            if( !String.IsNullOrEmpty( PaymentId ) )
                APIArgs += "&paymentid=" + PaymentId;

            Signer S = new BittrexSigner( APICall, APIArgs, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<BittrexUuid>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<BittrexUuid>;
        }


        //==========================================================
        public BittrexResult<BittrexOrder2> GetOrder( String OrderUuid )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/account/getorder", "uuid=" + OrderUuid, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<BittrexOrder2>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<BittrexOrder2>;
        }


        //==========================================================
        public BittrexResult<List<BittrexOrder1>> GetOrderHistory( String Market = null )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/account/getorderhistory", String.IsNullOrEmpty( Market ) ? null : "market=" + Market, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<List<BittrexOrder1>>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<List<BittrexOrder1>>;
        }


        //==========================================================
        public BittrexResult<List<BittrexWithdrawal>> GetWithdrawalHistory( String Currency = null )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/account/getwithdrawalhistory", String.IsNullOrEmpty( Currency ) ? null : "currency=" + Currency, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<List<BittrexWithdrawal>>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<List<BittrexWithdrawal>>;
        }


        //==========================================================
        public BittrexResult<List<BittrexDeposit>> GetDepositHistory( String Currency = null )
        {
            Signer S = new BittrexSigner( API_URL + API_VER + "/account/getdeposithistory", String.IsNullOrEmpty( Currency ) ? null : "currency=" + Currency, m_APIKey, m_APISecret );

            return GetInternal<BittrexResult<List<BittrexDeposit>>>( S.APICallFinal, S.APICallSigned ) as BittrexResult<List<BittrexDeposit>>;
        }
    }
}