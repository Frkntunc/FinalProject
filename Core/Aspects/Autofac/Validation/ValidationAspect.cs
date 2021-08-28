using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)//Attribute'da type ile geçmek zorundayız
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen validatortype bir Ivalidator değilse
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//reflection - çalışma anında productvalidator'ın bir instance'sını oluştur
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //productvalidator'ın base tipini bul. onun da generic argümanını bul - Product
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // validatorun tipine eşit olan metodun parametrelerini bul
            foreach (var entity in entities) //her birini tek tek gez
            {
                ValidationTool.Validate(validator, entity); //validationtool kullanarak validate et
            }
        }
    }
}
