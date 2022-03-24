using Microsoft.Extensions.Options;
using Moq;
using PokePoke.Business.Interfaces;
using PokePoke.Business.Response;
using PokePoke.Business.Settings;
using PokePoke.UseCases;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PokePokeTest
{
    public class PokemonTest
    {
        [Fact]
        public async void VerificaATrocaJustaComSucesso()
        {
            //Arrange 
            Moq.Mock<IPokemonAPI> mock = new Moq.Mock<IPokemonAPI>();
            Moq.Mock<IPokemonRepository> mock2 = new Moq.Mock<IPokemonRepository>();
            var someOptions= new PokeSettings() { ToClose = 20, ConnectionString=""};
            PokemonResponse pokemonResponse = new PokemonResponse { Name = "Venusaur", BaseExperience = 3 };
            List<string> junior = new List<string> {  "Charmander", "Charmeleon" };
            List<string> otavio = new List<string> {  "Squirtle", "Wartortle" };
            mock.Setup(x => x.GetPokemon(It.IsAny<string>())).Returns(Task.FromResult(pokemonResponse));
           
            //Atc
            PokemonUseCase pokeUse = new(mock.Object, someOptions, mock2.Object);

            var verifica = await pokeUse.CheckChange(junior, otavio);

            //Assert
            Assert.True(verifica.Check);
        }
    }
}