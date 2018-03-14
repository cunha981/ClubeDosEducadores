namespace Model
{
    public class PermutaRegiaoUnidade
    {
        public int RegiaoUnidadeId { get; set; }
        public virtual RegiaoUnidade RegiaoUnidade { get; set; }

        public int PermutaId { get; set; }
        public virtual Permuta Permuta { get; set; }
    }
}
