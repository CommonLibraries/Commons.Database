using System.Data;

namespace Commons.Database.ConnectionFactory
{
    public interface IConnectionFactory
    {
        IDbConnection Open(string? databaseContextKey = null);

        Task<IDbConnection> OpenAsync(string? databaseContextKey = null, CancellationToken cancellationToken = default);
    }
}
