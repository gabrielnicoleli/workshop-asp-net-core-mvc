using System;

namespace SalesWebMVC.Services.Exceptions
{
    public class NotFoundExceptions : ApplicationException
    {
        public NotFoundExceptions(string message) : base(message)
        {
        }
    }
}
