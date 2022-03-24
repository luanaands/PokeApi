using PokePoke.Business.Interfaces;
using PokePoke.Data;
using PokePoke.Data.Repository;
using PokePoke.Services;
using PokePoke.UseCases;

namespace PokePoke
{
    public class Ioc
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<PokeContext>();
            services.AddScoped<IPokemonUseCase, PokemonUseCase>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IPokemonAPI, PokemonApi>();
        }
    }
}
