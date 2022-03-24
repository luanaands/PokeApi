namespace PokePoke.Business.Settings
{
    public record PokeSettings
    {
        public string ConnectionString { get; set; }
        public int ToClose { get; set; }
    }
}
