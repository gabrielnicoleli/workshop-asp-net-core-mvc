using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services.Exceptions
{
    public class DbConcourenceExceptions : ApplicationException
    {

        public DbConcourenceExceptions(string message) : base(message)
        {

        }
    }
}
