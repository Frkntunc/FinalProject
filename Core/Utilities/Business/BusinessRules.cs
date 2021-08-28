using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics) // params kullandığımızda istediğimiz kadar parametreyi buraya gönderebiliriz. 
                                                           //c# gönderdiklerimizi IResult arrayi olan logics içine atıyor
        {
            foreach (var logic in logics) //parametreyle gönderdiğimiz iş kurallarından
            {
                if (!logic.Success) //başarısız olanı business'a söylüyoruz
                {
                    return logic; //kurala uymayanı döndürüyoruz
                }
            }
            return null;
        }
    }
}
