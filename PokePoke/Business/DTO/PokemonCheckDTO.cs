using PokePoke.Business.Response;

namespace PokePoke.Business.DTO
{
    public class PokemonCheckDTO
    {
        public List<PokemonResponse> PokemonDTOX { get; set; }
        public List<PokemonResponse> PokemonDTOY { get; set; }
        public bool Check { get; set; }
    }
}
