using SQLite;

namespace MaxWell.Services
{
	public interface ISQLite
	{
		SQLiteAsyncConnection GetConnection();
	}
}

