using System;

namespace back_community_report.BLL.Exceptions
{
    public class  PasswordLengthException : ApplicationException
    {
        public PasswordLengthException(string message) : base(message)
        {
        }
    }
}