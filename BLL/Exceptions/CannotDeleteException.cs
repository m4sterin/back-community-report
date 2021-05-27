using System;

namespace back_community_report.BLL.Exceptions
{
    public class  CannotDeleteException : ApplicationException
    {
        public CannotDeleteException(string message) : base(message)
        {
        }
    }
}