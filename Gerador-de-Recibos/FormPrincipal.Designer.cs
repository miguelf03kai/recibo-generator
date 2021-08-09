namespace Gerador_de_Recibos
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.tbCpfCnpj = new System.Windows.Forms.MaskedTextBox();
            this.tbValor = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.tbCorresp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btLimpar = new System.Windows.Forms.Button();
            this.btFe = new System.Windows.Forms.Button();
            this.btnDadosEmissor = new System.Windows.Forms.Button();
            this.btEmitir = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tbCpfCnpj);
            this.groupBox1.Controls.Add(this.tbValor);
            this.groupBox1.Controls.Add(this.maskedTextBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btLimpar);
            this.groupBox1.Controls.Add(this.btFe);
            this.groupBox1.Controls.Add(this.tbCorresp);
            this.groupBox1.Controls.Add(this.btnDadosEmissor);
            this.groupBox1.Controls.Add(this.btEmitir);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbCliente);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(558, 216);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados do Recibo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(17, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 41);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(210, 16);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(79, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Pagamento";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(88, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Recebimento";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // tbCpfCnpj
            // 
            this.tbCpfCnpj.Location = new System.Drawing.Point(277, 77);
            this.tbCpfCnpj.Name = "tbCpfCnpj";
            this.tbCpfCnpj.Size = new System.Drawing.Size(158, 20);
            this.tbCpfCnpj.TabIndex = 2;
            this.tbCpfCnpj.Enter += new System.EventHandler(this.tbCpfCnpj_Enter);
            this.tbCpfCnpj.Leave += new System.EventHandler(this.tbCpfCnpj_Leave);
            // 
            // tbValor
            // 
            this.tbValor.Location = new System.Drawing.Point(449, 77);
            this.tbValor.Name = "tbValor";
            this.tbValor.Size = new System.Drawing.Size(91, 20);
            this.tbValor.TabIndex = 3;
            this.tbValor.Text = "$0,00";
            this.tbValor.TextChanged += new System.EventHandler(this.tbValor_TextChanged);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(966, 105);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBox1.TabIndex = 15;
            // 
            // tbCorresp
            // 
            this.tbCorresp.Location = new System.Drawing.Point(17, 135);
            this.tbCorresp.Name = "tbCorresp";
            this.tbCorresp.Size = new System.Drawing.Size(523, 20);
            this.tbCorresp.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Correspondente a:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(446, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Valor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "CPF/CNPJ:";
            // 
            // tbCliente
            // 
            this.tbCliente.Location = new System.Drawing.Point(17, 77);
            this.tbCliente.Name = "tbCliente";
            this.tbCliente.Size = new System.Drawing.Size(246, 20);
            this.tbCliente.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cliente:";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Desenvolvido por:";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Email:";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Telefone:";
            this.label7.Visible = false;
            // 
            // printDocument2
            // 
            this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
            // 
            // pbBanner
            // 
            this.pbBanner.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBanner.Image = global::Gerador_de_Recibos.Properties.Resources.banner;
            this.pbBanner.Location = new System.Drawing.Point(12, 12);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(558, 87);
            this.pbBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBanner.TabIndex = 1;
            this.pbBanner.TabStop = false;
            // 
            // button1
            // 
            this.button1.Image = global::Gerador_de_Recibos.Properties.Resources.search;
            this.button1.Location = new System.Drawing.Point(308, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Consultar/Reimprimir ";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btLimpar
            // 
            this.btLimpar.Image = global::Gerador_de_Recibos.Properties.Resources.refresh;
            this.btLimpar.Location = new System.Drawing.Point(227, 178);
            this.btLimpar.Name = "btLimpar";
            this.btLimpar.Size = new System.Drawing.Size(75, 23);
            this.btLimpar.TabIndex = 7;
            this.btLimpar.Text = "Limpar";
            this.btLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btLimpar.UseVisualStyleBackColor = true;
            this.btLimpar.Click += new System.EventHandler(this.btLimpar_Click);
            // 
            // btFe
            // 
            this.btFe.Image = global::Gerador_de_Recibos.Properties.Resources.exit;
            this.btFe.Location = new System.Drawing.Point(465, 178);
            this.btFe.Name = "btFe";
            this.btFe.Size = new System.Drawing.Size(75, 23);
            this.btFe.TabIndex = 9;
            this.btFe.Text = "Fechar";
            this.btFe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btFe.UseVisualStyleBackColor = true;
            this.btFe.Click += new System.EventHandler(this.btFe_Click);
            // 
            // btnDadosEmissor
            // 
            this.btnDadosEmissor.Image = global::Gerador_de_Recibos.Properties.Resources.settings;
            this.btnDadosEmissor.Location = new System.Drawing.Point(17, 178);
            this.btnDadosEmissor.Name = "btnDadosEmissor";
            this.btnDadosEmissor.Size = new System.Drawing.Size(123, 23);
            this.btnDadosEmissor.TabIndex = 5;
            this.btnDadosEmissor.Text = "Dados do Emissor";
            this.btnDadosEmissor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDadosEmissor.UseVisualStyleBackColor = true;
            this.btnDadosEmissor.Click += new System.EventHandler(this.button2_Click);
            // 
            // btEmitir
            // 
            this.btEmitir.Image = global::Gerador_de_Recibos.Properties.Resources.print;
            this.btEmitir.Location = new System.Drawing.Point(146, 178);
            this.btEmitir.Name = "btEmitir";
            this.btEmitir.Size = new System.Drawing.Size(75, 23);
            this.btEmitir.TabIndex = 6;
            this.btEmitir.Text = "Emitir";
            this.btEmitir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btEmitir.UseVisualStyleBackColor = true;
            this.btEmitir.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 333);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbBanner);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.Text = "Emissor de Recibos v1.0";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDadosEmissor;
        private System.Windows.Forms.Button btEmitir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button btLimpar;
        private System.Windows.Forms.Button btFe;
        private System.Windows.Forms.TextBox tbCorresp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox tbValor;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox tbCpfCnpj;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Drawing.Printing.PrintDocument printDocument2;
    }
}

