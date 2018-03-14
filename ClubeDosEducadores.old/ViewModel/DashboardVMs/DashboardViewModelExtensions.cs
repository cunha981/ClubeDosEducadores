using Model;
using System.Collections.Generic;
using System.Linq;
using ViewModel.PermutaVMs;
using ViewModel.UsuarioVMs;

namespace ViewModel.DashboardVMs
{
    public static class DashboardViewModelExtensions
    {
        public static DashboardVM ToVM(this IEnumerable<Permuta> permutas, FuncionarioOnline user)
        {
            return new DashboardVM
            {
                Permutas = permutas.Take(10).ToListVM(user)
            };
        }
    }
}
