using System.Data;

namespace _2ch.Application.DbConnection
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
