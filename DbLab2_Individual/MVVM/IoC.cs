using DbLab2_Individual.Models.FirstDatabase;
using DbLab2_Individual.Models.SecondDatabase;
using DbLab2_Individual.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DbLab2_Individual.MVVM
{
    public class IoC
    {
        private static IServiceProvider provider;

        public static void Init()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient(typeof(MainViewModel));
            services.AddTransient(typeof(CreateRelationViewModel));
            services.AddTransient(typeof(RequestsViewModel));
            services.AddTransient(typeof(ChartViewModel));
            //services.AddDbContext<DbContext, Database_ex3_var1Context>();
            services.AddDbContext<DbContext, DataBaseFirst_Ex2Context>();
            services.AddSingleton(typeof(Messenger));

            provider = services.BuildServiceProvider();

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
