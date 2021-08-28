using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool //static olma nedeni tek bir instance oluşturulur ve bir daha new lemeye gerek olmaz
    {
        public static void Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity); //context generic'teki product'ı tuttu
            var result = validator.Validate(context);//productValidator.Validate'ine product gönderdik onu doğrula dedik
            if (!result.IsValid) //eğer sonuc not is valid yani doğru değil ise hata fırlat
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
