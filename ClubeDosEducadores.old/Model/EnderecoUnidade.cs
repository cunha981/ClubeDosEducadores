namespace Model
{
    public class EnderecoUnidade : IKey
    {
        public int Id { get; set; }
        public string EnderecoCompleto { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public virtual Unidade Unidade { get; set; }
    }
}