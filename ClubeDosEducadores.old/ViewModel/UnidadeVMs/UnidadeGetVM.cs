namespace ViewModel.UnidadeVMs
{
    public class UnidadeGetVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string TelefonePrincipal { get; set; }
        public string TelefoneSecundario { get; set; }
        public int? DificilAcesso { get; set; }
        public string Endereco { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Tipo { get; set; }
        public string TipoDescricao { get; set; }
        public double? Distancia { get; set; }
        public string Url { get; set; }
    }
}
