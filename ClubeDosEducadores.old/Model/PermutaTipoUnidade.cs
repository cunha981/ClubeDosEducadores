namespace Model
{
    public class PermutaTipoUnidade
    {
        public int TipoUnidadeId { get; set; }
        public virtual TipoUnidade TipoUnidade { get; set; }

        public int PermutaId { get; set; }
        public virtual Permuta Permuta { get; set; }
    }
}
