using DB_Lab1.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lab1.MVVM
{
    public class IoCContainer
    {
        private static IServiceProvider provider;

        public static void Init()
        {
            IServiceCollection services = new ServiceCollection();            

            services.AddTransient(typeof(MainViewModel));
            services.AddTransient(typeof(ViewModelSignUp));
            services.AddTransient(typeof(ViewModelSignIn));
            services.AddTransient(typeof(ViewModelDataBase));
            services.AddTransient(typeof(MyDataBase));
            services.AddSingleton(typeof(Messenger));

            provider = services.BuildServiceProvider();

            // Кинет ошибку в случае, если в классе будет единственный конструктор с параметрами, который не добавлен в зависимости
            // Регистрация Singletone
            foreach (ServiceDescriptor serv in services)
            {
                if (serv.Lifetime != ServiceLifetime.Singleton)
                {
                    continue;
                }

                provider.GetRequiredService(serv.ServiceType);
            }
        }

        public static T Resolve<T>()
        {
            return provider.GetRequiredService<T>();
        }
    }
}
