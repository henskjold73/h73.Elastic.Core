using System;

namespace h73.Elastic.Core.Exceptions
{
    public class AmbiguousQueryException : Exception
    {
        private readonly string _msg;
        private const string MsgPrefix = "What do you mean?! Query does not make any sense!";
        public AmbiguousQueryException(){}
        public AmbiguousQueryException(string msg)
        {
            _msg = msg;
        }

        public override string Message => $"{MsgPrefix} {_msg}".Trim();
    }
}