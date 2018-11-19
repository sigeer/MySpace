using System;

namespace Utility.Exceptions
{
    public class SigeerException : Exception
    {
        public string[] Params{get;set;}
        public SigeerException(string message,string[] p = null) : base(message)
        {
            Params = p;
        }
    }
}