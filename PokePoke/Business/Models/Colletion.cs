namespace PokePoke.Business.Models
{
    public class Colletion
    {
        public int Id { get; set; }
        public Pokemon Pokemon { get; set; }
        public User User { get; set; }
        public int UserId {get;set;}
        public int PokemonId { get; set; }
    }
}
