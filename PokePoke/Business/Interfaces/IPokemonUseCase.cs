using PokePoke.Business.DTO;

namespace PokePoke.Business.Interfaces
{
    public interface IPokemonUseCase
    {
        Task<bool> DoChange(List<CollectionDTO> collectionDTOs);
        Task<PokemonCheckDTO> CheckChange(List<string> pokemoX, List<string> pokemoY);
    }
}
