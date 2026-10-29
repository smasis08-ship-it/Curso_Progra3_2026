namespace AlquilerVehiculos
{
    partial class FrmDevoluciones
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvAlquileres = new System.Windows.Forms.DataGridView();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpDevolucion = new System.Windows.Forms.DateTimePicker();
            this.lblDanio = new System.Windows.Forms.Label();
            this.nudDanio = new System.Windows.Forms.NumericUpDown();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcionDanio = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lblRetraso = new System.Windows.Forms.Label();
            this.lblAviso = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlquileres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDanio)).BeginInit();
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
            this.label5.Location = new System.Drawing.Point(341, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "Devoluciones";
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
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(13, 95);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(180, 13);
            this.lblInfo.TabIndex = 11;
            this.lblInfo.Text = "Alquileres activos (aprobados)";
            // 
            // dgvAlquileres
            // 
            this.dgvAlquileres.AllowUserToAddRows = false;
            this.dgvAlquileres.AllowUserToDeleteRows = false;
            this.dgvAlquileres.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlquileres.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlquileres.Location = new System.Drawing.Point(12, 115);
            this.dgvAlquileres.Name = "dgvAlquileres";
            this.dgvAlquileres.MultiSelect = false;
            this.dgvAlquileres.ReadOnly = true;
            this.dgvAlquileres.RowHeadersVisible = false;
            this.dgvAlquileres.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlquileres.Size = new System.Drawing.Size(776, 200);
            this.dgvAlquileres.TabIndex = 12;
            this.dgvAlquileres.SelectionChanged += new System.EventHandler(this.dgvAlquileres_SelectionChanged);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(13, 325);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(114, 13);
            this.lblFecha.TabIndex = 13;
            this.lblFecha.Text = "Fecha de devolución";
            // 
            // dtpDevolucion
            // 
            this.dtpDevolucion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDevolucion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDevolucion.Location = new System.Drawing.Point(16, 343);
            this.dtpDevolucion.Name = "dtpDevolucion";
            this.dtpDevolucion.Size = new System.Drawing.Size(150, 22);
            this.dtpDevolucion.TabIndex = 14;
            this.dtpDevolucion.ValueChanged += new System.EventHandler(this.dtpDevolucion_ValueChanged);
            // 
            // lblDanio
            // 
            this.lblDanio.AutoSize = true;
            this.lblDanio.Location = new System.Drawing.Point(185, 325);
            this.lblDanio.Name = "lblDanio";
            this.lblDanio.Size = new System.Drawing.Size(114, 13);
            this.lblDanio.TabIndex = 15;
            this.lblDanio.Text = "Monto por daños (₡)";
            // 
            // nudDanio
            // 
            this.nudDanio.DecimalPlaces = 2;
            this.nudDanio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDanio.Location = new System.Drawing.Point(188, 343);
            this.nudDanio.Name = "nudDanio";
            this.nudDanio.Maximum = new decimal(new int[] {
                        10000000,
                        0,
                        0,
                        0});
            this.nudDanio.Size = new System.Drawing.Size(140, 22);
            this.nudDanio.TabIndex = 16;
            this.nudDanio.ThousandsSeparator = true;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(345, 325);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(120, 13);
            this.lblDescripcion.TabIndex = 17;
            this.lblDescripcion.Text = "Descripción del daño";
            // 
            // txtDescripcionDanio
            // 
            this.txtDescripcionDanio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcionDanio.Location = new System.Drawing.Point(348, 343);
            this.txtDescripcionDanio.Name = "txtDescripcionDanio";
            this.txtDescripcionDanio.Size = new System.Drawing.Size(250, 22);
            this.txtDescripcionDanio.TabIndex = 18;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(612, 340);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(176, 28);
            this.btnRegistrar.TabIndex = 19;
            this.btnRegistrar.Text = "Registrar devolución";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // lblRetraso
            // 
            this.lblRetraso.AutoSize = true;
            this.lblRetraso.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetraso.Location = new System.Drawing.Point(13, 382);
            this.lblRetraso.Name = "lblRetraso";
            this.lblRetraso.Size = new System.Drawing.Size(10, 17);
            this.lblRetraso.TabIndex = 20;
            this.lblRetraso.Text = "";
            // 
            // lblAviso
            // 
            this.lblAviso.AutoSize = true;
            this.lblAviso.ForeColor = System.Drawing.Color.Gray;
            this.lblAviso.Location = new System.Drawing.Point(13, 410);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(520, 13);
            this.lblAviso.TabIndex = 21;
            this.lblAviso.Text = "Si se devuelve después de la fecha pactada se cobra multa por retraso (precio del día + 25% por cada día de atraso).";
            // 
            // FrmDevoluciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblAviso);
            this.Controls.Add(this.lblRetraso);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.txtDescripcionDanio);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.nudDanio);
            this.Controls.Add(this.lblDanio);
            this.Controls.Add(this.dtpDevolucion);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dgvAlquileres);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "FrmDevoluciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Devoluciones";
            this.Load += new System.EventHandler(this.FrmDevoluciones_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlquileres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDanio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvAlquileres;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpDevolucion;
        private System.Windows.Forms.Label lblDanio;
        private System.Windows.Forms.NumericUpDown nudDanio;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcionDanio;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lblRetraso;
        private System.Windows.Forms.Label lblAviso;
    }
}