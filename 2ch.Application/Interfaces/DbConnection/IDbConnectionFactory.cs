using System.Data;

namespace _2ch.Application.Interfaces.DbConnection
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
