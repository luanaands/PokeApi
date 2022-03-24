using PokePoke.Business.Response;

namespace PokePoke.Business.Interfaces
{
    public interface IPokemonAPI
    {
        Task<PokemonResponse> GetPokemon(string name);
    }
}
