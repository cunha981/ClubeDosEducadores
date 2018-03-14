namespace ViewModel.RemocaoVMs
{
    public class VagaRemocaoVM
    {
        public int Id { get; set; }
        public int CargoId { get; set; }
        public int UnidadeId { get; set; }
        public string Unidade { get; set; }
        public string Endereco { get; set; }
        public double? Distancia { get; set; }
        public int? DificilAcesso { get; set; }
    }
}
