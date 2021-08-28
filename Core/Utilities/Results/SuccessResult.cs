using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message):base(true,message)//Burası çalışınca base yani resulttaki 2 mesaj alan yeri de çalıştır dedik
        {

        }

        public SuccessResult():base(true)
        {

        }
    }
}
