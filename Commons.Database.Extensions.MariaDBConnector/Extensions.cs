using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Database.Extensions.MariaDBConnector
{
    public static class Extensions
    {
        public static IServiceCollection AddMariaDBConnector(this IServiceCollection services)
        {
            DbProviderFactories.RegisterFactory("MySqlConnector", MySqlConnector.MySqlConnectorFactory.Instance);
            return services;
        }
    }
}
