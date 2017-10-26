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
using System.Net.Http.Headers;

namespace MYC
{
    public class BittrexHttpClient
    {
        System.Net.Http.HttpClient m_HttpClient;


        //==========================================================
        public BittrexHttpClient( String URL )
        {
            m_HttpClient = new System.Net.Http.HttpClient();
            m_HttpClient.BaseAddress = new Uri( URL );
        }


        //==========================================================
        public Task<HttpResponseMessage> Get( String APICall, String APICallSigned = null )
        {
            m_HttpClient.DefaultRequestHeaders.Accept.Clear();
            m_HttpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

            if( !String.IsNullOrEmpty( APICallSigned ) )
            {
                // https://stackoverflow.com/questions/35907642/custom-header-to-httpclient-request
                m_HttpClient.DefaultRequestHeaders.Clear();
                m_HttpClient.DefaultRequestHeaders.Add( "apisign", APICallSigned );
            }

            return m_HttpClient.GetAsync( APICall );
        }
    }
}