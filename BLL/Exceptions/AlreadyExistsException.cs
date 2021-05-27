using System;

namespace back_community_report.BLL.Exceptions
{
    public class  AlreadyExistsException : ApplicationException
    {
        public AlreadyExistsException(string message) : base(message)
        {
        }
    }
}