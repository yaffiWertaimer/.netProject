using AutoMapper;
using Subsciber.CORE.interfaceBL;
using Subsciber.CORE.interfaceDAL;
using Subsciber.CORE.model;
using Subscriber.DAL;

namespace Subscriber.WebApi.Config
{
    public static class Configuration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICardSubscriberRepository, CardSubscriberRepository>();
           
            services.AddScoped<ICardSubscriberService, CardSubscriberService>();
           
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WWProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


        }
    }
}
