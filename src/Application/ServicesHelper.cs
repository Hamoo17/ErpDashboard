using Microsoft.Extensions.DependencyInjection;

namespace ErpDashboard.Application
{
    public static class ServicesHelper
    {
        public static ServiceProvider provider;


        public static T GetService<T>()
        {

            return (T)provider.GetService(typeof(T));
        }
    }
}
