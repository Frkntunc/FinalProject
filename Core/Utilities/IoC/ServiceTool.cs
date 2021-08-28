using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services) //.Net servislerini al ve build et
            //burası injectionları oluşturmamıza yarıyor. İstediğimiz herhangibir interface'nin servisteki karşılığını bu tool ile alabiliriz
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
