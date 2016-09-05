using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.ViewModel.ViewModel.Exceptions
{
    public class ConnectionProblemsException : Exception
    {
        public ConnectionProblemsException()
            : base()
        {
        }

        public ConnectionProblemsException(string message)
            : base(message)
        {
        }

        public ConnectionProblemsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
