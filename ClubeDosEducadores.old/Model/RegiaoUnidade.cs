using System.Collections.Generic;

namespace Model
{
    public class RegiaoUnidade : IKey
    {
        public int Id { get; set; }
        public string Regiao { get; set; }
        public virtual ICollection<Unidade> Unidades { get; set; }
        public virtual ICollection<PermutaRegiaoUnidade> PermutasRegiaoUnidade { get; set; }
    }
}
