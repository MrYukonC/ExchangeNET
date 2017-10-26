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
    public class BittrexTrade
    {
        [JsonProperty]
        public Int32 Id { get; private set; }

        [JsonProperty]
        public DateTime TimeStamp { get; private set; }

        [JsonProperty]
        public Double Quantity { get; private set; }

        [JsonProperty]
        public Double Price { get; private set; }

        [JsonProperty]
        public Double Total { get; private set; }

        [JsonProperty]
        public String FillType { get; private set; }

        [JsonProperty]
        public String OrderType { get; private set; }

        public override String ToString()
        {
            StringBuilder SB = new StringBuilder();

            SB.AppendFormat( "{0,-17} {1,-40}\n", "Id:", Id );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "TimeStamp:", TimeStamp );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "Quantity:", Quantity );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "Price:", Price );
            SB.AppendFormat( "{0,-17} {1,-40:0.00000000}\n", "Total:", Total );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "FillType:", FillType );
            SB.AppendFormat( "{0,-17} {1,-40}\n", "OrderType:", OrderType );
            
            return SB.ToString();
        }
    }
}