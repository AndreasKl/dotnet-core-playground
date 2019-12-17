using System.Data.Common;

namespace PostgresWithEFAndDapper.Infrastructure
{
    public interface IDbConnectionProvider
    {
        DbConnection Connection();
    }
}