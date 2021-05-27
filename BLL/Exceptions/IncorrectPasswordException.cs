using System;

namespace back_community_report.BLL.Exceptions
{
    public class  IncorrectPasswordException : ApplicationException
    {
        public IncorrectPasswordException(string message) : base(message)
        {
        }
    }
}