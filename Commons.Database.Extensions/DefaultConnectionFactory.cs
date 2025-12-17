using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Database.ConnectionFactory
{    
    public class DefaultConnectionFactory : IConnectionFactory
    {
        private readonly IDictionary<string, DatabaseContextOptions> databaseContexts;

        private IDbConnection? connection;

        public DefaultConnectionFactory(IDictionary<string, DatabaseContextOptions> databaseContexts)
        {
            this.databaseContexts = databaseContexts;
        }

        private IDbConnection CreateConnection(string? databaseContextKey = null)
        {
            var contextKey = databaseContextKey ?? string.Empty;
            if (!this.databaseContexts.TryGetValue(contextKey, out var databaseContext))
            {
                throw new KeyNotFoundException($"Database context with key '{contextKey}' was not found.");
            }

            var invariantName = databaseContext.InvariantName;
            var connectionString = databaseContext.ConnectionString;

            var dbProviderFactory = DbProviderFactories.GetFactory(invariantName);
            var connection = dbProviderFactory.CreateConnection() ??
                throw new NullReferenceException("Created connection is null.");
            connection.ConnectionString = connectionString;
            return connection;
        }

        public IDbConnection Open(string? databaseContextKey = null)
        {
            if (this.connection is null)
            {
                this.connection = this.CreateConnection(databaseContextKey);
                this.connection.Open();
            }

            return this.connection;
        }

        public async Task<IDbConnection> OpenAsync(string? databaseContextKey = null,CancellationToken cancellationToken = default)
        {
            if (this.connection is null)
            {
                if (this.CreateConnection(databaseContextKey) is not DbConnection connection)
                {
                    throw new InvalidCastException($"The created connection does not inherit {nameof(DbConnection)} class.");
                }
                this.connection = connection;

                await connection.OpenAsync(cancellationToken);
            }

            return this.connection;
        }
    }
}
