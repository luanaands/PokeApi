using Dapper;
using PokePoke.Business.DTO;
using PokePoke.Business.Interfaces;
using PokePoke.Business.Models;

namespace PokePoke.Data.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private PokeContext context;

        public PokemonRepository(PokeContext context)
        {
            this.context = context;
        }
   
        public async Task<bool> UpdatePokemons(int userIdFirst, int userIdSecond, List<int> pokemonIdFirst, List<int> pokemonIdSecond)
        {
            var bd =  await context.GetConnection();

            using(var tran =  bd.BeginTransaction())
            {
                try
                {
                    var results = 0;
                    var results2 = 0;
                    foreach(var pokemon in pokemonIdFirst)
                    {
                        var parans = new
                        {
                            UserId = userIdSecond,
                            pokemonId = pokemon,
                            UserIdOld = userIdFirst
                        };

                        var query = $"Update Collection set UserId = @UserId where UserId = @userIdOld and pokemonId = @pokemonId";

                        var result = await bd.ExecuteAsync(query, parans);
                        results = results + result;
                    }

                    foreach (var pokemon in pokemonIdSecond)
                    {
                        var parans = new
                        {
                            UserId = userIdFirst,
                            pokemonId = pokemon,
                            UserIdOld = userIdSecond
                        };

                        var query = $"Update Collection set UserId = @UserId where UserId = @userIdOld and pokemonId = @pokemonId";
                        var result = await bd.ExecuteAsync(query, parans);
                        results2 = results2 + result;
                    }
                   
                   if ( results ==  pokemonIdFirst.Count() && results2 == pokemonIdSecond.Count()) {
                        tran.Commit();
                        return true;
                    }else {
                        tran.Rollback();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public async Task<IEnumerable<User>> GetUserAll()
        {
            var bd = await context.GetConnection();
          
            var query = $"select u.Id , u.Name from User as u";

            var result = await bd.QueryAsync<User>(query);
            return result;
        }

        public async Task<IEnumerable<CollectionDTO>> GetCollectionByUserId(int userId)
        {
            var bd = await context.GetConnection();
            var parans = new
            {
                userId 
            };

            var query = $"select c.Id, c.UserId, c.PokemonId, p.Name, p.url from Collection as c" +
                $" inner join Pokemon as p on c.PokemonId = p.Id " +
                $"inner join User as u on u.Id = c.UserId " +
                $"where c.UserId = @userId";

            var result = await bd.QueryAsync<CollectionDTO>(query, parans);
            return result;
        }
    }
}
