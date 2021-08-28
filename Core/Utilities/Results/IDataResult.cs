using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Burada artık işlem sonucu, mesaj döndürebileceğiz
    public interface IDataResult<T>:IResult //Mesajlar da içereceği için IResult verdik bir daha mesaj yazmadık
    {
        T Data { get; }
    }
}
