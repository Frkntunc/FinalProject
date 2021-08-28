using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)//Burası çalışınca base yani resulttaki 2 mesaj alan yeri de çalıştır dedik
        {

        }

        public ErrorResult() : base(false)
        {

        }
    }
}
