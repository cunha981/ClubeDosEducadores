using Domain;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoCompilado.RF.WindowsForms
{
    public partial class UnidadesForm : Form
    {
        private ModelDomain<Unidade> _unidadeDomain;
        private static IDictionary<int, string> _unidades;

        public int IdSelecionado { get; private set; }
        public string UnidadeSelecionada { get; set; }
        public bool UnidadeRegistrada { get; set; } //Unidades que já tem vagas no banco

        public UnidadesForm()
        {
            InitializeComponent();
            _unidadeDomain = this.Get<ModelDomain<Unidade>>();

            if (_unidades == null || _unidades.Count == 0)
            {
                var ano = DateTime.Now.Year;
                var mes = DateTime.Now.Month;

                _unidades = _unidadeDomain.Get().Where(a => !a.Vagas.Any(b => b.Data.Year == ano && b.Data.Month == mes))
                                .ToDictionary(a => a.Id, a => a.Id + " - " + a.TipoUnidade.Tipo + " - " + a.Nome);
            }

            Listar();

        }

        private void textBoxFiltro_TextChanged(object sender, EventArgs e)
        {
            Listar();
        }

        private void listViewUnidades_DoubleClick(object sender, EventArgs e)
        {
            var item = listViewUnidades.SelectedItems[0];

            DialogResult = DialogResult.OK;
            IdSelecionado = (int) item.Tag;
            UnidadeSelecionada = item.Text;
            UnidadeRegistrada = checkBoxUnidadeSalva.Checked;

            if(UnidadeRegistrada)
            {
                Thread thread = new Thread(AtualizarLista);
                thread.Start();
            }

            Close();
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            var ano = DateTime.Now.Year;
            var mes = DateTime.Now.Month;

            _unidades = _unidadeDomain.Get().Where(a => !a.Vagas.Any(b => b.Data.Year == ano && b.Data.Month == mes))
                            .ToDictionary(a => a.Id, a => a.Id + " - " + a.TipoUnidade.Tipo + " - " + a.Nome);

            Listar();
        }

        private void Listar()
        {
            var list = _unidades.Where(a => a.Value.ToLower().Contains(textBoxFiltro.Text))
                                                        .Select(a => new ListViewItem { Text = a.Value, Tag = a.Key }).ToArray();

            listViewUnidades.Items.Clear();
            listViewUnidades.Items.AddRange(list);
        }

        private void checkBoxUnidadeSalva_CheckedChanged(object sender, EventArgs e)
        {
            textBoxFiltro.Text = "";

            if(checkBoxUnidadeSalva.Checked)
            {
                var ano = DateTime.Now.Year;
                var mes = DateTime.Now.Month;

                _unidades = _unidadeDomain.Get().Where(a => a.Vagas.Any(b => b.Data.Year == ano && b.Data.Month == mes))
                            .ToDictionary(a => a.Id, a => a.Id + " - " + a.TipoUnidade.Tipo + " - " + a.Nome);

                Listar();
                return;
            }

            AtualizarLista();
        }
    }


}
