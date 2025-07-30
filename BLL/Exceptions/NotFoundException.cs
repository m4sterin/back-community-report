using System;

namespace back_community_report.BLL.Exceptions
{
    public class  NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

}