using PokePoke.Business.Interfaces;
using PokePoke.Business.Response;

namespace PokePoke.Services
{
    public class PokemonApi : IPokemonAPI
    {
        public async Task<PokemonResponse> GetPokemon(string name)
        {
           
                using (var client = new HttpClient())
                {
                    var url = $"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}/";
                    HttpResponseMessage result = await client.GetAsync(url);
                    var jsonString = await result.Content.ReadAsStringAsync();
                    int responseCode = (int)result.StatusCode;
                    var pokemon = Newtonsoft.Json.JsonConvert.DeserializeObject<PokemonResponse>(jsonString);
                    return pokemon;
                }
        }
    }
}
