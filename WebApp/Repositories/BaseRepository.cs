using System.Data;

namespace WebApp.Repositories
{
    public class BaseRepository
    {
        //protected string connectionString;
        protected IDbConnection connection;
        public BaseRepository(IDbConnection connection)
        {
            this.connection = connection;
        }
    }
}
