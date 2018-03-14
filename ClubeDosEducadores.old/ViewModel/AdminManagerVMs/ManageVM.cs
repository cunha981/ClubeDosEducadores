using Helper;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel.AdminManagerVMs
{
    public class ManageVM
    {
        public int UsuarioCount { get; set; }
        public int PermutaCount { get; set; }
        public int EventoCount { get; set; }
        public int PerguntaFrequenteCount { get; set; }
        public IEnumerable<UsuarioManageVM> Usuarios { get; set; }
        public IEnumerable<LabelValueVM> FuncionariosCountByCargoJson { get; set; }

        public ManageVM(IQueryable<UsuarioFuncionario> usuarios, IQueryable<Permuta> permutas, 
            IQueryable<Evento> eventos, IQueryable<PerguntaFrequente> perguntasFrequentes, IQueryable<Cargo> cargos)
        {
            UsuarioCount = usuarios.Count();
            PermutaCount = permutas.Count();
            EventoCount = eventos.Count();
            PerguntaFrequenteCount = perguntasFrequentes.Count();

            FuncionariosCountByCargoJson = cargos.Where(a => a.Funcionarios.Count > 0).Select(a => new LabelValueVM
            {
                label = a.Abreviacao,
                value = a.Funcionarios.Count + ""
            }).ToArray();

            var usuariosQuery = usuarios.OrderByDescending(a => a.DtAtividade);

            var ontem = DateTime.Now.AddDays(-1);

            Usuarios = usuariosQuery.Where(a => a.DtAtividade >= ontem).Select(a => new UsuarioManageVM
            {
                DtAtividade = a.DtAtividade,
                Email = a.Email,
                Nome = a.Funcionario.Nome,
                Cargo = a.Funcionario.Cargo.Abreviacao
            }).AsEnumerable();
        }
    }
}
