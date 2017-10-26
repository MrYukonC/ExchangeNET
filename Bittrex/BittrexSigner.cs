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
using System.Security.Cryptography;

namespace MYC
{
    public class BittrexSigner : Signer
    {
        //==========================================================
        public BittrexSigner( String APICall, String APIArgs, String APIKey, String APISecret )
        {
            Sign( APICall, APIArgs, APIKey, APISecret );
        }


        //==========================================================
        public override void Sign( String APICall, String APIArgs, String APIKey, String APISecret )
        {
            base.APICallFinal = APICall + "?";
            
            if( !String.IsNullOrEmpty( APIArgs ) )
                base.APICallFinal += APIArgs + "&";

            base.APICallFinal += "apikey=" + APIKey + "&nonce=" + DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // https://stackoverflow.com/questions/8063004/what-is-the-net-equivalent-of-the-php-function-hash-hmac
            // https://msdn.microsoft.com/en-us/library/system.security.cryptography.hmacsha512(v=vs.110).aspx
            HMACSHA512  SHA512Hash = new HMACSHA512( System.Text.Encoding.UTF8.GetBytes( APISecret ) );
            Byte[]      HashResult = SHA512Hash.ComputeHash( System.Text.Encoding.UTF8.GetBytes( APICallFinal ) );

            // https://stackoverflow.com/questions/623104/byte-to-hex-string
            base.APICallSigned = BitConverter.ToString( HashResult ).Replace( "-", String.Empty );
        }
    }
}