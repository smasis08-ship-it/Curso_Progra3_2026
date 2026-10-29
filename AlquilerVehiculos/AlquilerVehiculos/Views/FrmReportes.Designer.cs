namespace AlquilerVehiculos
{
    partial class FrmReportes
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabReportes = new System.Windows.Forms.TabControl();
            this.tabMasAlquilados = new System.Windows.Forms.TabPage();
            this.tabIngresos = new System.Windows.Forms.TabPage();
            this.tabHistorial = new System.Windows.Forms.TabPage();
            this.dgvMasAlquilados = new System.Windows.Forms.DataGridView();
            this.btnActualizarMas = new System.Windows.Forms.Button();
            this.lblDesdeIng = new System.Windows.Forms.Label();
            this.dtpDesdeIng = new System.Windows.Forms.DateTimePicker();
            this.lblHastaIng = new System.Windows.Forms.Label();
            this.dtpHastaIng = new System.Windows.Forms.DateTimePicker();
            this.btnConsultarIng = new System.Windows.Forms.Button();
            this.dgvIngresos = new System.Windows.Forms.DataGridView();
            this.lblTotalIngresos = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.lblResumenCliente = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabReportes.SuspendLayout();
            this.tabMasAlquilados.SuspendLayout();
            this.tabIngresos.SuspendLayout();
            this.tabHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasAlquilados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngresos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Location = new System.Drawing.Point(-2, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 73);
            this.panel1.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(361, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "Reportes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(273, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alquiler de Vehiculos";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabReportes
            // 
            this.tabReportes.Controls.Add(this.tabMasAlquilados);
            this.tabReportes.Controls.Add(this.tabIngresos);
            this.tabReportes.Controls.Add(this.tabHistorial);
            this.tabReportes.Location = new System.Drawing.Point(12, 95);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.SelectedIndex = 0;
            this.tabReportes.Size = new System.Drawing.Size(776, 345);
            this.tabReportes.TabIndex = 10;
            // 
            // tabMasAlquilados
            // 
            this.tabMasAlquilados.Controls.Add(this.dgvMasAlquilados);
            this.tabMasAlquilados.Controls.Add(this.btnActualizarMas);
            this.tabMasAlquilados.Location = new System.Drawing.Point(4, 22);
            this.tabMasAlquilados.Name = "tabMasAlquilados";
            this.tabMasAlquilados.Padding = new System.Windows.Forms.Padding(3);
            this.tabMasAlquilados.Size = new System.Drawing.Size(768, 319);
            this.tabMasAlquilados.TabIndex = 0;
            this.tabMasAlquilados.Text = "Vehículos más alquilados";
            this.tabMasAlquilados.UseVisualStyleBackColor = true;
            // 
            // tabIngresos
            // 
            this.tabIngresos.Controls.Add(this.lblDesdeIng);
            this.tabIngresos.Controls.Add(this.dtpDesdeIng);
            this.tabIngresos.Controls.Add(this.lblHastaIng);
            this.tabIngresos.Controls.Add(this.dtpHastaIng);
            this.tabIngresos.Controls.Add(this.btnConsultarIng);
            this.tabIngresos.Controls.Add(this.dgvIngresos);
            this.tabIngresos.Controls.Add(this.lblTotalIngresos);
            this.tabIngresos.Location = new System.Drawing.Point(4, 22);
            this.tabIngresos.Name = "tabIngresos";
            this.tabIngresos.Padding = new System.Windows.Forms.Padding(3);
            this.tabIngresos.Size = new System.Drawing.Size(768, 319);
            this.tabIngresos.TabIndex = 0;
            this.tabIngresos.Text = "Ingresos por período";
            this.tabIngresos.UseVisualStyleBackColor = true;
            // 
            // tabHistorial
            // 
            this.tabHistorial.Controls.Add(this.lblCliente);
            this.tabHistorial.Controls.Add(this.cbCliente);
            this.tabHistorial.Controls.Add(this.dgvHistorial);
            this.tabHistorial.Controls.Add(this.lblResumenCliente);
            this.tabHistorial.Location = new System.Drawing.Point(4, 22);
            this.tabHistorial.Name = "tabHistorial";
            this.tabHistorial.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistorial.Size = new System.Drawing.Size(768, 319);
            this.tabHistorial.TabIndex = 0;
            this.tabHistorial.Text = "Historial por cliente";
            this.tabHistorial.UseVisualStyleBackColor = true;
            // 
            // dgvMasAlquilados
            // 
            this.dgvMasAlquilados.AllowUserToAddRows = false;
            this.dgvMasAlquilados.AllowUserToDeleteRows = false;
            this.dgvMasAlquilados.BackgroundColor = System.Drawing.Color.White;
            this.dgvMasAlquilados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMasAlquilados.Location = new System.Drawing.Point(6, 6);
            this.dgvMasAlquilados.Name = "dgvMasAlquilados";
            this.dgvMasAlquilados.MultiSelect = false;
            this.dgvMasAlquilados.ReadOnly = true;
            this.dgvMasAlquilados.RowHeadersVisible = false;
            this.dgvMasAlquilados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMasAlquilados.Size = new System.Drawing.Size(756, 275);
            this.dgvMasAlquilados.TabIndex = 11;
            // 
            // btnActualizarMas
            // 
            this.btnActualizarMas.Location = new System.Drawing.Point(6, 288);
            this.btnActualizarMas.Name = "btnActualizarMas";
            this.btnActualizarMas.Size = new System.Drawing.Size(120, 25);
            this.btnActualizarMas.TabIndex = 12;
            this.btnActualizarMas.Text = "Actualizar";
            this.btnActualizarMas.UseVisualStyleBackColor = true;
            this.btnActualizarMas.Click += new System.EventHandler(this.btnActualizarMas_Click);
            // 
            // lblDesdeIng
            // 
            this.lblDesdeIng.AutoSize = true;
            this.lblDesdeIng.Location = new System.Drawing.Point(6, 10);
            this.lblDesdeIng.Name = "lblDesdeIng";
            this.lblDesdeIng.Size = new System.Drawing.Size(30, 13);
            this.lblDesdeIng.TabIndex = 13;
            this.lblDesdeIng.Text = "Desde";
            // 
            // dtpDesdeIng
            // 
            this.dtpDesdeIng.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesdeIng.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesdeIng.Location = new System.Drawing.Point(6, 28);
            this.dtpDesdeIng.Name = "dtpDesdeIng";
            this.dtpDesdeIng.Size = new System.Drawing.Size(130, 22);
            this.dtpDesdeIng.TabIndex = 14;
            // 
            // lblHastaIng
            // 
            this.lblHastaIng.AutoSize = true;
            this.lblHastaIng.Location = new System.Drawing.Point(150, 10);
            this.lblHastaIng.Name = "lblHastaIng";
            this.lblHastaIng.Size = new System.Drawing.Size(30, 13);
            this.lblHastaIng.TabIndex = 15;
            this.lblHastaIng.Text = "Hasta";
            // 
            // dtpHastaIng
            // 
            this.dtpHastaIng.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHastaIng.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHastaIng.Location = new System.Drawing.Point(150, 28);
            this.dtpHastaIng.Name = "dtpHastaIng";
            this.dtpHastaIng.Size = new System.Drawing.Size(130, 22);
            this.dtpHastaIng.TabIndex = 16;
            // 
            // btnConsultarIng
            // 
            this.btnConsultarIng.Location = new System.Drawing.Point(295, 27);
            this.btnConsultarIng.Name = "btnConsultarIng";
            this.btnConsultarIng.Size = new System.Drawing.Size(110, 25);
            this.btnConsultarIng.TabIndex = 17;
            this.btnConsultarIng.Text = "Consultar";
            this.btnConsultarIng.UseVisualStyleBackColor = true;
            this.btnConsultarIng.Click += new System.EventHandler(this.btnConsultarIng_Click);
            // 
            // dgvIngresos
            // 
            this.dgvIngresos.AllowUserToAddRows = false;
            this.dgvIngresos.AllowUserToDeleteRows = false;
            this.dgvIngresos.BackgroundColor = System.Drawing.Color.White;
            this.dgvIngresos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIngresos.Location = new System.Drawing.Point(6, 62);
            this.dgvIngresos.Name = "dgvIngresos";
            this.dgvIngresos.MultiSelect = false;
            this.dgvIngresos.ReadOnly = true;
            this.dgvIngresos.RowHeadersVisible = false;
            this.dgvIngresos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIngresos.Size = new System.Drawing.Size(756, 220);
            this.dgvIngresos.TabIndex = 18;
            // 
            // lblTotalIngresos
            // 
            this.lblTotalIngresos.AutoSize = true;
            this.lblTotalIngresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalIngresos.Location = new System.Drawing.Point(6, 290);
            this.lblTotalIngresos.Name = "lblTotalIngresos";
            this.lblTotalIngresos.Size = new System.Drawing.Size(10, 17);
            this.lblTotalIngresos.TabIndex = 19;
            this.lblTotalIngresos.Text = "";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(6, 10);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(42, 13);
            this.lblCliente.TabIndex = 20;
            this.lblCliente.Text = "Cliente";
            // 
            // cbCliente
            // 
            this.cbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(6, 28);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(300, 23);
            this.cbCliente.TabIndex = 21;
            this.cbCliente.SelectionChangeCommitted += new System.EventHandler(this.cbCliente_SelectionChangeCommitted);
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(6, 62);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.MultiSelect = false;
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(756, 220);
            this.dgvHistorial.TabIndex = 22;
            // 
            // lblResumenCliente
            // 
            this.lblResumenCliente.AutoSize = true;
            this.lblResumenCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumenCliente.Location = new System.Drawing.Point(6, 290);
            this.lblResumenCliente.Name = "lblResumenCliente";
            this.lblResumenCliente.Size = new System.Drawing.Size(10, 17);
            this.lblResumenCliente.TabIndex = 23;
            this.lblResumenCliente.Text = "";
            // 
            // FrmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabReportes);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "FrmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.FrmReportes_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabReportes.ResumeLayout(false);
            this.tabMasAlquilados.ResumeLayout(false);
            this.tabMasAlquilados.PerformLayout();
            this.tabIngresos.ResumeLayout(false);
            this.tabIngresos.PerformLayout();
            this.tabHistorial.ResumeLayout(false);
            this.tabHistorial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasAlquilados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngresos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabReportes;
        private System.Windows.Forms.TabPage tabMasAlquilados;
        private System.Windows.Forms.TabPage tabIngresos;
        private System.Windows.Forms.TabPage tabHistorial;
        private System.Windows.Forms.DataGridView dgvMasAlquilados;
        private System.Windows.Forms.Button btnActualizarMas;
        private System.Windows.Forms.Label lblDesdeIng;
        private System.Windows.Forms.DateTimePicker dtpDesdeIng;
        private System.Windows.Forms.Label lblHastaIng;
        private System.Windows.Forms.DateTimePicker dtpHastaIng;
        private System.Windows.Forms.Button btnConsultarIng;
        private System.Windows.Forms.DataGridView dgvIngresos;
        private System.Windows.Forms.Label lblTotalIngresos;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Label lblResumenCliente;
    }
}