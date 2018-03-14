namespace Model
{
    public class PerguntaFrequente : IKey
    {
        public int Id { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public string Tags { get; set; }
    }
}
