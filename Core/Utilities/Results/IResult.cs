using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç
    public interface IResult
    {
        bool Success { get; } //Sadece okuma yapacak get ile. Yapmak istediğin işlem başarılı yani true
        string Message { get; }//Yapmak istediğin işlem için mesaj
    }
}
