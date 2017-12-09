//=================================================================================
//
//  Created by: MrYukonC
//  Created on: 26 OCT 2017
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

namespace MYC
{
    public abstract class Signer
    {
        public String APIKey        { get; private set; }
        public String APISecret     { get; private set; }

        public String APICallFinal  { get; private set; }
        public String APICallSigned { get; private set; }


        //==========================================================
        public abstract void Sign( String APICall, String APIArgs, String InAPIKey, String InAPISecret );


        //==========================================================
        protected  void SignInternal( String InAPIKey, String InAPISecret, String InAPICallFinal, String InAPISigned )
        {
            APIKey          = InAPIKey;
            APISecret       = InAPISecret;
            APICallFinal    = InAPICallFinal;
            APICallSigned   = InAPISigned;
        }
    }
}