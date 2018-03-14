using Domain;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MundoCompilado.RF.WindowsForms
{
    public partial class MainForm : Form
    {
        private int _unidadeId;
        private string _unidadeFormatada;
        private bool _unidadeRegistrada;

        private ICollection<NumericUpDown> _vagas = new List<NumericUpDown>();

        private ModelDomain<Cargo> _cargoDomain;
        private ModelDomain<VagaRemocao> _vagaDomain;

        public string UnidadeFormatada
        {
            get
            {
                return _unidadeFormatada;
            }
            private set
            {
                if (value == null)
                {
                    _unidadeId = 0;
                    _unidadeFormatada = "Clique aqui para selecionar a unidade";
                    _unidadeRegistrada = false;
                    linkLabelUnidade.Text = _unidadeFormatada;
                    return;
                }

                _unidadeFormatada = value;
                linkLabelUnidade.Text = value;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            _cargoDomain = this.Get<ModelDomain<Cargo>>();
            _vagaDomain = this.Get<ModelDomain<VagaRemocao>>();

            foreach (var cargo in _cargoDomain.Get())
            {
                addCargo(cargo.Id, cargo.Abreviacao);
            }
        }

        private void linkLabelUnidade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (UnidadesForm form = new UnidadesForm())
            {
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    _unidadeId = form.IdSelecionado;
                    UnidadeFormatada = form.UnidadeSelecionada;
                    _unidadeRegistrada = form.UnidadeRegistrada;

                    ExibirVagas();
                }
            }
        }

        private void addCargo(int id, string nome)
        {
            var label = new Label();
            var numericUpDown = new NumericUpDown();

            _vagas.Add(numericUpDown);

            label.AutoSize = true;
            label.Location = new Point(70, _vagas.Count * 48 + 7);
            label.Size = new Size(250, 13);
            label.Text = nome;

            numericUpDown.Location = new Point(13, _vagas.Count * 48 + 5);
            numericUpDown.Size = new Size(46, 20);
            numericUpDown.Tag = id;
            numericUpDown.Minimum = -999;
            numericUpDown.Maximum = 999;
            numericUpDown.Value = 0;

            Controls.Add(label);
            Controls.Add(numericUpDown);
        }

        private void ExibirVagas()
        {
            if (_unidadeRegistrada)
            {
                var ano = DateTime.Now.Year;
                var mes = DateTime.Now.Month;

                var vagas = _vagaDomain.Get()
                                    .Where(a =>
                                        a.UnidadeId == _unidadeId
                                        && a.Data.Year == ano
                                        && a.Data.Month == mes);

                foreach (var vaga in vagas)
                {
                    var vagaTextBox = _vagas.SingleOrDefault(a => a.Tag.Equals(vaga.CargoId));
                    if (vagaTextBox != null)
                    {
                        vagaTextBox.Value = vaga.Vagas;
                    }
                }
            }
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (_unidadeId == 0)
            {
                MessageBox.Show("Unidade não seleciona!");
                return;
            }

            var ano = DateTime.Now.Year;
            var mes = DateTime.Now.Month;

            if (_unidadeRegistrada)
            {
                AtualizarVagas(mes, ano);
            }
            else
            {
                if (_vagaDomain.Get()
                    .Any(a => a.UnidadeId == _unidadeId && a.Data.Year == ano && a.Data.Month == mes))
                {
                    MessageBox.Show("Já foram cadastradas as vagas para esta unidade!");
                    return;
                }
                CadastrarVagas();
            }

            UnidadeFormatada = null;
            MessageBox.Show("Vagas cadastradas com sucesso!");
        }

        private void CadastrarVagas()
        {
            foreach (var vagaTextBox in _vagas)
            {
                _vagaDomain.Save(new VagaRemocao
                {
                    CargoId = (int)vagaTextBox.Tag,
                    Data = DateTime.Now.Date,
                    UnidadeId = _unidadeId,
                    Vagas = int.Parse(vagaTextBox.Text)
                });

                vagaTextBox.Text = "0";
            }
        }

        private void AtualizarVagas(int mes, int ano)
        {
            var cargos = _vagas.Select(a => (int)a.Tag);

            var vagasRemocao = _vagaDomain.Get()
                .Where(a => cargos.Contains(a.CargoId)
                            && a.UnidadeId == _unidadeId
                            && a.Data.Year == ano
                            && a.Data.Month == mes);

            foreach (var vagaView in _vagas)
            {
                var cargoId = (int)vagaView.Tag;
                var vagas = (int)vagaView.Value;

                var vagaRemocao = vagasRemocao.SingleOrDefault(a => a.CargoId == cargoId);

                if (vagaRemocao != null)
                {
                    vagaRemocao.Vagas = vagas;
                }
                else
                {
                    vagaRemocao = new VagaRemocao
                    {
                        CargoId = cargoId,
                        Data = DateTime.Now.Date,
                        UnidadeId = _unidadeId,
                        Vagas = vagas
                    };
                }

                vagaView.Value = 0;
                _vagaDomain.Save(vagaRemocao);
            }
        }
    }
}
