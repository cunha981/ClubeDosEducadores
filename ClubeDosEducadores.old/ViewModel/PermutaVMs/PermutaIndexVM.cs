using PagedList;

namespace ViewModel.PermutaVMs
{
    public class PermutaIndexVM
    {
        public IPagedList<PermutaListVM> Permutas { get; set; }
        public PermutaGetVM Permuta { get; set; }
    }
}
