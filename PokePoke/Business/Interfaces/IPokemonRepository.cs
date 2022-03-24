using PokePoke.Business.DTO;
using PokePoke.Business.Models;

namespace PokePoke.Business.Interfaces
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<CollectionDTO>> GetCollectionByUserId(int userId);
        Task<bool> UpdatePokemons(int userIdFirst, int userIdSecond, List<int> pokemonIdFirst, List<int> pokemonIdSecond);
        Task<IEnumerable<User>> GetUserAll(); 
    }
}
