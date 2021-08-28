using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
       
        public Result(bool success, string message):this(success)//this demek kendi constructor'ını çalıştıştırı
                                                    //Yani burası çalışınca aşağıdaki tek parametreli constructor da çalışacak
        {
            Message = message; //set ettik
        }
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; } //get'ler read only ama sadece constructor içinde set edilebilir
    }
}
