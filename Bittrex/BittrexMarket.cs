//=================================================================================
//
//  Created by: MrYukonC
//  Created on: 22 OCT 2017
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
using System.Text;
using Newtonsoft.Json;

namespace MYC
{
    public class BittrexMarket
    {
        [JsonProperty]
        public String MarketName { get; private set; }

        [JsonProperty]
        public String MarketCurrency { get; private set; }

        [JsonProperty]
        public String MarketCurrencyLong { get; private set; }

        [JsonProperty]
        public String BaseCurrency { get; private set; }

        [JsonProperty]
        public String BaseCurrencyLong { get; private set; }

        [JsonProperty]
        public Double MinTradeSize { get; private set; }
        
        [JsonProperty]
        public Boolean IsActive { get; private set; }

        [JsonProperty]
        public DateTime Created { get; private set; }

        public override String ToString()
        {
            StringBuilder SB = new StringBuilder();

            SB.AppendFormat( "{0,-17} {1,-40}\n", "MarketName:", MarketName );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "MarketCurrency:", MarketCurrency );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "MarketCurrencyLong:", MarketCurrencyLong );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "BaseCurrency:", BaseCurrency );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "BaseCurrencyLong:", BaseCurrencyLong );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "IsActive:", IsActive );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "MinTradeSize:", MinTradeSize );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "IsActive:", IsActive );
            
            return SB.ToString();
        }
    }
}