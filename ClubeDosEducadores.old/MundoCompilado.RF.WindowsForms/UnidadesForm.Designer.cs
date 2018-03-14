namespace MundoCompilado.RF.WindowsForms
{
    partial class UnidadesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnidadesForm));
            this.listViewUnidades = new System.Windows.Forms.ListView();
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxFiltro = new System.Windows.Forms.TextBox();
            this.buttonAtualizar = new System.Windows.Forms.Button();
            this.checkBoxUnidadeSalva = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listViewUnidades
            // 
            this.listViewUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewUnidades.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
            this.listViewUnidades.Location = new System.Drawing.Point(12, 47);
            this.listViewUnidades.Name = "listViewUnidades";
            this.listViewUnidades.Size = new System.Drawing.Size(373, 230);
            this.listViewUnidades.TabIndex = 0;
            this.listViewUnidades.TileSize = new System.Drawing.Size(373, 30);
            this.listViewUnidades.UseCompatibleStateImageBehavior = false;
            this.listViewUnidades.View = System.Windows.Forms.View.Details;
            this.listViewUnidades.DoubleClick += new System.EventHandler(this.listViewUnidades_DoubleClick);
            // 
            // columnHeader
            // 
            this.columnHeader.Text = "Unidade";
            this.columnHeader.Width = 373;
            // 
            // textBoxFiltro
            // 
            this.textBoxFiltro.Location = new System.Drawing.Point(12, 13);
            this.textBoxFiltro.Name = "textBoxFiltro";
            this.textBoxFiltro.Size = new System.Drawing.Size(293, 20);
            this.textBoxFiltro.TabIndex = 1;
            this.textBoxFiltro.TextChanged += new System.EventHandler(this.textBoxFiltro_TextChanged);
            // 
            // buttonAtualizar
            // 
            this.buttonAtualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAtualizar.Location = new System.Drawing.Point(311, 10);
            this.buttonAtualizar.Name = "buttonAtualizar";
            this.buttonAtualizar.Size = new System.Drawing.Size(75, 23);
            this.buttonAtualizar.TabIndex = 2;
            this.buttonAtualizar.Text = "Atualizar";
            this.buttonAtualizar.UseVisualStyleBackColor = true;
            this.buttonAtualizar.Click += new System.EventHandler(this.buttonAtualizar_Click);
            // 
            // checkBoxUnidadeSalva
            // 
            this.checkBoxUnidadeSalva.AutoSize = true;
            this.checkBoxUnidadeSalva.Location = new System.Drawing.Point(12, 290);
            this.checkBoxUnidadeSalva.Name = "checkBoxUnidadeSalva";
            this.checkBoxUnidadeSalva.Size = new System.Drawing.Size(162, 17);
            this.checkBoxUnidadeSalva.TabIndex = 3;
            this.checkBoxUnidadeSalva.Text = "Exibir unidades já registradas";
            this.checkBoxUnidadeSalva.UseVisualStyleBackColor = true;
            this.checkBoxUnidadeSalva.CheckedChanged += new System.EventHandler(this.checkBoxUnidadeSalva_CheckedChanged);
            // 
            // UnidadesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 319);
            this.Controls.Add(this.checkBoxUnidadeSalva);
            this.Controls.Add(this.buttonAtualizar);
            this.Controls.Add(this.textBoxFiltro);
            this.Controls.Add(this.listViewUnidades);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UnidadesForm";
            this.Text = "Unidades";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewUnidades;
        private System.Windows.Forms.ColumnHeader columnHeader;
        private System.Windows.Forms.TextBox textBoxFiltro;
        private System.Windows.Forms.Button buttonAtualizar;
        private System.Windows.Forms.CheckBox checkBoxUnidadeSalva;
    }
}