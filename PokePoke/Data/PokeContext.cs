using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using PokePoke.Business.Settings;

namespace PokePoke.Data
{
    public class PokeContext : IDisposable
    {

        private MySqlConnection mySqlConnection;
        private readonly PokeSettings _pokeSettings;

        public PokeContext(PokeSettings pokeSettings)
        {
            this._pokeSettings = pokeSettings;
            this.mySqlConnection = new MySqlConnection(_pokeSettings.ConnectionString);
        }

        public MySqlConnection GetConnection()
        {
            try
            {
                if (this.mySqlConnection.State == System.Data.ConnectionState.Closed
              || this.mySqlConnection.State == System.Data.ConnectionState.Broken)
                {
                     this.mySqlConnection.Open();
                }
                return this.mySqlConnection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose()
        {
            this.mySqlConnection.Close();
        }
    }
}
