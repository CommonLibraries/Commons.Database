using System.Data;

namespace Commons.Database.Extensions;
public static class DbCommandExtensions
{
    public static void AddParameter(this IDbCommand dbCommand, string name, object value) {
        var parameter = dbCommand.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        dbCommand.Parameters.Add(parameter);
    }
}
