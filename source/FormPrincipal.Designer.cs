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
            this.button1 = new System.Windows.Forms.Button();
            this.btLimpar = new System.Windows.Forms.Button();
            this.btFe = new System.Windows.Forms.Button();
            this.tbCorresp = new System.Windows.Forms.TextBox();
            this.btnDadosEmissor = new System.Windows.Forms.Button();
            this.btEmitir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.lAuto = new System.Windows.Forms.Label();
            this.lCon = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.pbBanner = new System.Windows.Forms.PictureBox();
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
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Checked = true;
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // tbCpfCnpj
            // 
            this.tbCpfCnpj.Culture = new System.Globalization.CultureInfo("");
            resources.ApplyResources(this.tbCpfCnpj, "tbCpfCnpj");
            this.tbCpfCnpj.Name = "tbCpfCnpj";
            this.tbCpfCnpj.Enter += new System.EventHandler(this.tbCpfCnpj_Enter);
            this.tbCpfCnpj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCpfCnpj_KeyPress);
            this.tbCpfCnpj.Leave += new System.EventHandler(this.tbCpfCnpj_Leave);
            // 
            // tbValor
            // 
            this.tbValor.Culture = new System.Globalization.CultureInfo("");
            resources.ApplyResources(this.tbValor, "tbValor");
            this.tbValor.Name = "tbValor";
            this.tbValor.TextChanged += new System.EventHandler(this.tbValor_TextChanged);
            this.tbValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValor_KeyPress);
            this.tbValor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbValor_KeyUp);
            this.tbValor.Leave += new System.EventHandler(this.tbValor_Leave);
            // 
            // maskedTextBox1
            // 
            resources.ApplyResources(this.maskedTextBox1, "maskedTextBox1");
            this.maskedTextBox1.Name = "maskedTextBox1";
            // 
            // button1
            // 
            this.button1.Image = global::Gerador_de_Recibos.Properties.Resources.search;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btLimpar
            // 
            this.btLimpar.Image = global::Gerador_de_Recibos.Properties.Resources.refresh;
            resources.ApplyResources(this.btLimpar, "btLimpar");
            this.btLimpar.Name = "btLimpar";
            this.btLimpar.UseVisualStyleBackColor = true;
            this.btLimpar.Click += new System.EventHandler(this.btLimpar_Click);
            // 
            // btFe
            // 
            this.btFe.Image = global::Gerador_de_Recibos.Properties.Resources.exit;
            resources.ApplyResources(this.btFe, "btFe");
            this.btFe.Name = "btFe";
            this.btFe.UseVisualStyleBackColor = true;
            this.btFe.Click += new System.EventHandler(this.btFe_Click);
            // 
            // tbCorresp
            // 
            resources.ApplyResources(this.tbCorresp, "tbCorresp");
            this.tbCorresp.Name = "tbCorresp";
            // 
            // btnDadosEmissor
            // 
            this.btnDadosEmissor.Image = global::Gerador_de_Recibos.Properties.Resources.settings;
            resources.ApplyResources(this.btnDadosEmissor, "btnDadosEmissor");
            this.btnDadosEmissor.Name = "btnDadosEmissor";
            this.btnDadosEmissor.UseVisualStyleBackColor = true;
            this.btnDadosEmissor.Click += new System.EventHandler(this.button2_Click);
            // 
            // btEmitir
            // 
            this.btEmitir.Image = global::Gerador_de_Recibos.Properties.Resources.print;
            resources.ApplyResources(this.btEmitir, "btEmitir");
            this.btEmitir.Name = "btEmitir";
            this.btEmitir.UseVisualStyleBackColor = true;
            this.btEmitir.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbCliente
            // 
            resources.ApplyResources(this.tbCliente, "tbCliente");
            this.tbCliente.Name = "tbCliente";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // printPreviewDialog1
            // 
            resources.ApplyResources(this.printPreviewDialog1, "printPreviewDialog1");
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // lAuto
            // 
            resources.ApplyResources(this.lAuto, "lAuto");
            this.lAuto.BackColor = System.Drawing.Color.Transparent;
            this.lAuto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lAuto.Name = "lAuto";
            // 
            // lCon
            // 
            resources.ApplyResources(this.lCon, "lCon");
            this.lCon.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lCon.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lCon.Name = "lCon";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Name = "label7";
            // 
            // printDocument2
            // 
            this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
            // 
            // pbBanner
            // 
            this.pbBanner.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBanner.Image = global::Gerador_de_Recibos.Properties.Resources.banner1;
            resources.ApplyResources(this.pbBanner, "pbBanner");
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.TabStop = false;
            // 
            // FormPrincipal
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lAuto);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lCon);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pbBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
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
        private System.Windows.Forms.Label lAuto;
        private System.Windows.Forms.Label lCon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox tbCpfCnpj;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Drawing.Printing.PrintDocument printDocument2;
    }
}

