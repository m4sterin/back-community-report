using System;

namespace back_community_report.BLL.Exceptions
{
    public class  ObrigatoryFieldNotNullException : ApplicationException
    {
        public ObrigatoryFieldNotNullException(string message) : base(message)
        {
        }
    }
}