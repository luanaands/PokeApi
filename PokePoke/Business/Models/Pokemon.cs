using MongoDB.Entities;

namespace PokePoke.Business.Models
{
    public class Pokemon : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseExperience { get; set; }
        public int Height { get; set; }
        public bool IsDefault { get; set; }
        public int Weight { get; set; }
    }
}
