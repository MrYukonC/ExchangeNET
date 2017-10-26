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
using System.Text;
using Newtonsoft.Json;

namespace MYC
{
    public class BittrexMarketSummary
    {
        [JsonProperty]
        public String MarketName { get; set; }

        [JsonProperty]
        public Double High { get; private set; }

        [JsonProperty]
        public Double Low { get; private set; }

        [JsonProperty]
        public Double Ask { get; private set; }

        [JsonProperty]
        public Double Bid { get; private set; }

        [JsonProperty]
        public Int32 OpenBuyOrders { get; private set; }

        [JsonProperty]
        public Int32 OpenSellOrders { get; private set; }

        [JsonProperty]
        public Double Volume { get; private set; }

        [JsonProperty]
        public Double Last { get; private set; }

        [JsonProperty]
        public Double BaseVolume { get; private set; }

        [JsonProperty]
        public Double PrevDay { get; private set; }

        [JsonProperty]
        public DateTime Created { get; private set; }

        [JsonProperty]
        public DateTime TimeStamp { get; private set; }

        public override String ToString()
        {
            StringBuilder SB = new StringBuilder();

            SB.AppendFormat( "{0,-17} {1,-40}\n", "MarketName:", MarketName );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "Created:", Created );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "High:", High );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "Low:", Low );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "Ask:", Ask );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "Bid:", Bid );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "Last:", Last );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "BaseVolume:", BaseVolume );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "OpenBuyOrders:", OpenBuyOrders );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "OpenSellOrders:", OpenSellOrders );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "TimeStamp:", TimeStamp );

            return SB.ToString();
        }
    }
}