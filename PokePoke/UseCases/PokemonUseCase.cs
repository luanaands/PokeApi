using Microsoft.Extensions.Options;
using PokePoke.Business.DTO;
using PokePoke.Business.Interfaces;
using PokePoke.Business.Response;
using PokePoke.Business.Settings;

namespace PokePoke.UseCases
{
    public class PokemonUseCase : IPokemonUseCase
    {
        private readonly IPokemonAPI _pokemonAPI;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly PokeSettings _pokeSettings;
        public PokemonUseCase(IPokemonAPI pokemonAPI, PokeSettings settings, IPokemonRepository pokemonRepository)
        {
            _pokemonAPI = pokemonAPI;
            _pokeSettings = settings;
            _pokemonRepository = pokemonRepository;
        }
        public async Task<bool> DoChange(List<CollectionDTO> collectionDTOs)
        {
            var change = false;
            var treinadoresCollection = collectionDTOs.GroupBy(x => x.UserId).Select(x => new { userId = x.Key, pokemon = x.ToList() }).ToArray();

            if (treinadoresCollection.Length != 2)
            {
                throw new Exception("Só é permitido a troca entre dois usuarios");
            }
            var userIdFirst = treinadoresCollection[0].userId;
            var userIdSecond = treinadoresCollection[1].userId;
            var user1 = treinadoresCollection[0].pokemon.Select(x => x.Name).ToList();
            var user2 = treinadoresCollection[1].pokemon.Select(x => x.Name).ToList();
            var pokemonsFirst = treinadoresCollection[0].pokemon.Select(x => x.PokemonId).ToList();
            var pokemonsSecond = treinadoresCollection[1].pokemon.Select(x => x.PokemonId).ToList();
            var PokemonCheckDTO = await this.CheckChange(user1, user2);
            if(PokemonCheckDTO.Check)
            {
               change = await _pokemonRepository.UpdatePokemons(userIdFirst, userIdSecond, pokemonsFirst, pokemonsSecond);
            }
            else
            {
                throw new Exception("Troca Não Permitida");
            }
          
            return change;
        }

        public async Task<PokemonCheckDTO> CheckChange(List<string> pokemoX, List<string> pokemoY)
        {
            List<PokemonResponse> pokemonsx = new List<PokemonResponse>();
            List<PokemonResponse> pokemonsy = new List<PokemonResponse>();
            int totalBaseExperienceX = 0;
            int totalBaseExperienceY = 0;
            foreach (var pokemon in pokemoX)
            {
               var pokemom = await _pokemonAPI.GetPokemon(pokemon);
               totalBaseExperienceX += pokemom.BaseExperience;
                pokemonsx.Add(pokemom);
            }
            foreach (var pokemon in pokemoY)
            {
                var pokemom = await _pokemonAPI.GetPokemon(pokemon);
                totalBaseExperienceY += pokemom.BaseExperience;
                pokemonsy.Add(pokemom);
            }

            PokemonCheckDTO pokemonCheckDTO = new PokemonCheckDTO
            {
                Check = (Math.Abs(totalBaseExperienceX - totalBaseExperienceY) <= _pokeSettings.ToClose) ? true : false,
                PokemonDTOX = pokemonsx,
                PokemonDTOY = pokemonsy

            };

            return pokemonCheckDTO;
        }
    }
}
