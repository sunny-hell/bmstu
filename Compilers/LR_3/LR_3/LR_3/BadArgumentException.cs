using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_3
{
    class BadArgumentException: Exception
    {
        public BadArgumentException(String s):base(s){}
        public BadArgumentException() : base() { }
    }
}
