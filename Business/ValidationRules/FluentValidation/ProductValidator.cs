using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() //kurallar constructor içine yazılır
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p=>p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A ile Başlamalı"); 
            //startwihtA üzerine gelip generate method dedik ve kendi kuralımızı koyduk
        }

        private bool StartWithA(string arg) //arg bizim gönderdiğimiz parametre yani productname
        {
            return arg.StartsWith("A"); 
        }
    }
}
