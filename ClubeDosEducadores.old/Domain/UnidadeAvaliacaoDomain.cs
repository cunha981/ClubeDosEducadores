using Helper;
using Helper.CacheHelper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModel.UnidadeAvaliacaoVMs;

namespace Domain
{
    public class UnidadeAvaliacaoDomain : ModelDomain<UnidadeAvaliacao>
    {
        public UnidadeAvaliacao GetByUnidade(int unidadeId)
        {
            return Data.SingleOrDefault(a => a.UnidadeId == unidadeId && a.FuncionarioId == _funcionarioProvider.User.Id);
        }

        public IEnumerable<UnidadeAvaliacao> GetAllByUnidade(int id)
        {
            return _cacheManager.Get($"unidadeAvaliacao-{id}", 60, () =>
            {
                return Data.Where(a => a.UnidadeId == id).ToList();
            });
        }

        public UnidadeAvaliacao Save(UnidadeAvaliacaoPostVM vm)
        {
            var model = Data.SingleOrDefault(a => a.UnidadeId == vm.UnidadeId && a.FuncionarioId == _funcionarioProvider.User.Id);

            if(model == null)
            {
                model = vm.ConvertTo<UnidadeAvaliacao>();
                model.FuncionarioId = _funcionarioProvider.User.Id;
                Data.Add(model);
            }
            else
            {
                model.DtAvaliacao = DateTime.Now;
                model.Nota = vm.Nota;
                model.Comentario = vm.Comentario;
            }

            SaveChanges();
            return model;
        }
    }
}
