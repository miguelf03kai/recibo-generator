﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerador_de_Recibos
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            sqlite.CreateData();
            configCreator();
            createCounter();

            tbValor.Text = "R$ 0,00";
            
            //necessarie to turns backcolor label transparent over the picture box
            var pos = this.PointToScreen(lAuto.Location);
            var pos2 = this.PointToScreen(lCon.Location);
            var pos3 = this.PointToScreen(label7.Location);
            
            pos = pbBanner.PointToClient(pos);
            lAuto.Parent = pbBanner;
            lAuto.Location = pos;
            lAuto.BackColor = Color.Transparent;

            pos2 = pbBanner.PointToClient(pos2);
            lCon.Parent = pbBanner;
            lCon.Location = pos2;
            lCon.BackColor = Color.Transparent;

            pos3 = pbBanner.PointToClient(pos3);
            label7.Parent = pbBanner;
            label7.Location = pos3;
            label7.BackColor = Color.Transparent;
        }

        FormDadosEmissor dEmissor = new FormDadosEmissor();

        validaCNPJ vCNPJ = new validaCNPJ();
        validaCPF vCPF = new validaCPF();
        SQLite sqlite = new SQLite();

        CultureInfo lang;

        string valor = "";

        int numeroRecibo = 0;

        public void saveData()
        {
            int tipo = 0;
            
            if(radioButton2.Checked)
                tipo = 1;

            numeroRecibo = sqlite.automaticId();
            sqlite.persistData(numeroRecibo, tbCliente.Text, tbCpfCnpj.Text, tbValor.Text, tbCorresp.Text,tipo);
        }


        public void Emitir()
        {
            if(radioButton1.Checked){
                printPreviewDialog1.Document = printDocument1;
                (printPreviewDialog1 as Form).WindowState = FormWindowState.Maximized;
                (printPreviewDialog1 as Form).Icon = new System.Drawing.Icon("print.ico");
                (printPreviewDialog1 as Form).Text = "Recibo";
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                printPreviewDialog1.Document = printDocument2;
                (printPreviewDialog1 as Form).WindowState = FormWindowState.Maximized;
                (printPreviewDialog1 as Form).Icon = new System.Drawing.Icon("print.ico");
                (printPreviewDialog1 as Form).Text = "Recibo";
                printPreviewDialog1.ShowDialog();
            }
        }

        void emissorValidation()
        {
            if (File.ReadLines(@"config.ini").Skip(1).Take(1).First().Length > 8)
            {
                saveData();
                Emitir();
            }
            else
                MessageBox.Show("Por Favor Preencha os dados do Emissor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dEmissor.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                valor = tbValor.Text.Substring(1);

                if (tbCliente.Text == "")
                {
                    MessageBox.Show("Por Favor digite nome do Cliente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbCliente.Focus();
                }
                else if (tbCorresp.Text == "")
                {
                    MessageBox.Show("Por Favor Preencha o campo correspondente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbCorresp.Focus();
                }
                else if (tbCpfCnpj.TextLength < 15)
                {
                    if (vCPF.IsCpf(tbCpfCnpj.Text.Replace(".", "").Replace("-", "")) == false)
                    {
                        MessageBox.Show("CPF Inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbCpfCnpj.Focus();
                    }
                    else
                    {
                        emissorValidation();
                    }
                }
                else if (tbCpfCnpj.TextLength > 14)
                {
                    if (vCNPJ.IsCnpj(tbCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "")) == false)
                    {
                        MessageBox.Show("CNPJ Inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbCpfCnpj.Focus();
                    }
                    else
                    {
                        emissorValidation();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Campo Valor inválido: "+error, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbValor.Focus();

            }
        }

        //design para recibo de venda
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen lapis = new Pen(Color.Black);
            Font headers = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel);
            Font cpfCnpj = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Pixel);
            Font content = new Font("Arial", 15, FontStyle.Regular, GraphicsUnit.Pixel);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            try
            {
                e.Graphics.DrawRectangle(lapis, new Rectangle(50, 50, 750, 400));

                if (File.ReadLines(@"config.ini").Skip(9).Take(1).First().Substring(5) == "")
                {

                }
                else
                {
                    Image image = Image.FromFile(File.ReadLines(@"config.ini").Skip(9).Take(1).First().Substring(5));
                    e.Graphics.DrawImage(image, new Rectangle(80, 60, 150, 110));

                    Image image2 = Image.FromFile(File.ReadLines(@"config.ini").Skip(9).Take(1).First().Substring(5));
                    e.Graphics.DrawImage(image2, new Rectangle(80, 625, 150, 110));
                }

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(1).Take(1).First().Substring(8), headers, new SolidBrush(Color.Black), new Rectangle(180, 50, 400, 30), format);
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(5).Take(1).First().Substring(9), cpfCnpj, new SolidBrush(Color.Black), new Rectangle(180, 75, 400, 30), format);
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(2).Take(1).First().Substring(9) + ", " +
                                      File.ReadLines(@"config.ini").Skip(3).Take(1).First().Substring(7),
                                      content, new SolidBrush(Color.Black), new Rectangle(180, 100, 400, 30), format);
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(6).Take(1).First().Substring(9) + " / " +
                                      File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7),
                                      content, new SolidBrush(Color.Black), new Rectangle(180, 125, 400, 30), format);

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(50, 177), new Point(800, 177));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(600, 50), new Point(600, 177));

                e.Graphics.DrawString("RECIBO", headers, new SolidBrush(Color.Black), new Rectangle(650, 60, 200, 30));
                e.Graphics.DrawString("N°: " + Convert.ToString(numeroRecibo).PadLeft(10, '0'), content, new SolidBrush(Color.Black), new Rectangle(640, 85, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605, 128, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 127, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645, 127, 138, 30));
                ////Valor
                e.Graphics.DrawString(tbValor.Text.Substring(2), content, new SolidBrush(Color.Blue), new Rectangle(650, 128, 120, 30), format);

                e.Graphics.DrawString("Recebi(emos) de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 212), new Point(530, 212));
                ////Nome Cliente
                e.Graphics.DrawString(tbCliente.Text, content, new SolidBrush(Color.Blue), new Rectangle(190, 196, 400, 30));
                e.Graphics.DrawString("CPF/CNPJ: ", content, new SolidBrush(Color.Black), new Rectangle(540, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(630, 212), new Point(780, 212));
                ////Cpf/cnpj
                e.Graphics.DrawString(tbCpfCnpj.Text, content, new SolidBrush(Color.Blue), new Rectangle(630, 196, 200, 30));
                e.Graphics.DrawString("A importância de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 227, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 242), new Point(780, 242));
                ////valor Extenso

                string str = conversor.EscreverExtenso(Decimal.Parse(tbValor.Text.Substring(2))).ToLower();
                ////coloca em maiúsculo apenas a primeira letra da string
                e.Graphics.DrawString(str[0].ToString().ToUpper() + str.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(190, 226, 600, 30));
                e.Graphics.DrawString("Referente a.....: ", content, new SolidBrush(Color.Black), new Rectangle(60, 257, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 272), new Point(780, 272));
                ////referente
                e.Graphics.DrawString(tbCorresp.Text, content, new SolidBrush(Color.Blue), new Rectangle(190, 256, 600, 30));

                DateTime dt = DateTime.Now;
                lang = new CultureInfo("pt-BR");

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7) + ", " + dt.ToString("D",lang),
                                      content, new SolidBrush(Color.Black), new Rectangle(60, 327, 300, 30));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(400, 397), new Point(780, 397));
                e.Graphics.DrawString("Assinatura", content, new SolidBrush(Color.Black), new Rectangle(550, 407, 200, 30));

                e.Graphics.DrawString("1ª Via", content, new SolidBrush(Color.Black), new Rectangle(748, 425, 50, 30));

                for (int i = 10; i < 850; i += 20)
                {
                    int j = 0;
                    j += (i + 10);

                    e.Graphics.DrawLine(new Pen(Color.Black), new Point(i, 550), new Point(j, 550));
                }


                //2a via
                //------------------------------------------
                e.Graphics.DrawRectangle(lapis, new Rectangle(50, 51 * 12, 750, 400));

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(1).Take(1).First().Substring(8), headers, new SolidBrush(Color.Black), new Rectangle(180, 616, 400, 30), format);
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(5).Take(1).First().Substring(9), cpfCnpj, new SolidBrush(Color.Black), new Rectangle(180, 643, 400, 30), format);
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(2).Take(1).First().Substring(9) + ", " +
                                      File.ReadLines(@"config.ini").Skip(3).Take(1).First().Substring(7),
                                      content, new SolidBrush(Color.Black), new Rectangle(180, 668, 400, 30), format);
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(6).Take(1).First().Substring(9) + " / " +
                                      File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7),
                                      content, new SolidBrush(Color.Black), new Rectangle(180, 693, 400, 30), format);

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(50, 745), new Point(800, 745));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(600, 612), new Point(600, 745));

                e.Graphics.DrawString("RECIBO", headers, new SolidBrush(Color.Black), new Rectangle(650, 625, 200, 30));
                e.Graphics.DrawString("N°: " + Convert.ToString(numeroRecibo).PadLeft(10, '0'), content, new SolidBrush(Color.Black), new Rectangle(640, 650, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605, 695, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 695, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645, 695, 138, 30));
                //Valor
                e.Graphics.DrawString(tbValor.Text.Substring(2), content, new SolidBrush(Color.Blue), new Rectangle(650, 695, 120, 30), format);

                e.Graphics.DrawString("Recebi(emos) de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 765, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 780), new Point(530, 780));
                //Nome Cliente
                e.Graphics.DrawString(tbCliente.Text, content, new SolidBrush(Color.Blue), new Rectangle(190, 763, 400, 30));
                e.Graphics.DrawString("CPF/CNPJ: ", content, new SolidBrush(Color.Black), new Rectangle(540, 765, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(630, 780), new Point(780, 780));
                //Cpf/cnpj
                e.Graphics.DrawString(tbCpfCnpj.Text, content, new SolidBrush(Color.Blue), new Rectangle(630, 763, 200, 30));
                e.Graphics.DrawString("A importância de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 797, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 813), new Point(780, 813));
                //valor Extenso

                //coloca em maiúsculo apenas a primeira letra da string
                e.Graphics.DrawString(str[0].ToString().ToUpper() + str.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(190, 796, 600, 30));
                e.Graphics.DrawString("Referente a.....: ", content, new SolidBrush(Color.Black), new Rectangle(60, 831, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 846), new Point(780, 846));
                //referente
                e.Graphics.DrawString(tbCorresp.Text, content, new SolidBrush(Color.Blue), new Rectangle(190, 828, 600, 30));

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7) + ", " + dt.ToString("D",lang),
                                      content, new SolidBrush(Color.Black), new Rectangle(60, 900, 300, 30));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(400, 960), new Point(780, 960));
                e.Graphics.DrawString("Assinatura", content, new SolidBrush(Color.Black), new Rectangle(550, 970, 200, 30));

                e.Graphics.DrawString("2ª Via", content, new SolidBrush(Color.Black), new Rectangle(748, 990, 50, 30));

            }
            catch(Exception error){
                throw error;
            }
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            tbCliente.Text = "";
            tbCpfCnpj.Text = "";
            tbValor.Text = "R$ 0,00";
            tbCorresp.Text = "";
        }

        private void btFe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void tbValor_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tbCpfCnpj_Leave(object sender, EventArgs e)
        {
            if (tbCpfCnpj.Text.Length < 12)
                tbCpfCnpj.Mask = "###.###.###-##";
            else if (tbCpfCnpj.Text.Length > 11)
                tbCpfCnpj.Mask = "##.###.###/####-##";
        }

        private void tbCpfCnpj_Enter(object sender, EventArgs e)
        {
            tbCpfCnpj.Mask = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormConsultar fconsult = new FormConsultar();
            fconsult.ShowDialog();
        }

        public void configCreator()
        {
            if (File.Exists("config.ini"))
            {
                //pass
            }
            else
            {
                StreamWriter sw = new StreamWriter(@"config.ini");
                sw.WriteLine("[EMITENTE]\n" +
                "EMPRESA=\n" +
                "ENDERECO=\n" +
                "BAIRRO=\n" +
                "CIDADE=\n" +
                "CFP_CNPJ=\n" +
                "TELEFONE=\n" +
                "EMAIL=\n" +
                "SITE=\n" +
                "LOGO=");
                sw.Close();
            }
        }

        public void createCounter()
        {
            if (File.Exists(".counter"))
            {
                //pass
            }
            else
            {
                StreamWriter sw = new StreamWriter(@".counter");
                sw.WriteLine(0);
                sw.Close();
            }
        }

        //design para recibo de compra
        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen lapis = new Pen(Color.Black);
            Font nome = new Font("Arial", 25, FontStyle.Bold, GraphicsUnit.Pixel);
            Font headers = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel);
            Font cpfCnpj = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Pixel);
            Font content = new Font("Arial", 15, FontStyle.Regular, GraphicsUnit.Pixel);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            try
            {
                e.Graphics.DrawRectangle(lapis, new Rectangle(50, 50, 750, 400));

                e.Graphics.DrawString(tbCliente.Text, nome, new SolidBrush(Color.Black), new Rectangle(75, 75, 500, 30), format);
                e.Graphics.DrawString(tbCpfCnpj.Text, cpfCnpj, new SolidBrush(Color.Black), new Rectangle(75, 100, 500, 30), format);
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(50, 177), new Point(800, 177));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(600, 50), new Point(600, 177));

                e.Graphics.DrawString("RECIBO", headers, new SolidBrush(Color.Black), new Rectangle(650, 60, 200, 30));
                e.Graphics.DrawString("N°: " + Convert.ToString(numeroRecibo).PadLeft(10, '0'), content, new SolidBrush(Color.Black), new Rectangle(640, 85, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605, 128, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 127, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645, 127, 138, 30));
                ////Valor
                e.Graphics.DrawString(tbValor.Text.Substring(2), content, new SolidBrush(Color.Blue), new Rectangle(650, 128, 120, 30), format);

                e.Graphics.DrawString("Recebi(emos) de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 212), new Point(530, 212));
                ////Nome Cliente
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(1).Take(1).First().Substring(8), content, new SolidBrush(Color.Blue), new Rectangle(190, 196, 400, 30));
                e.Graphics.DrawString("CPF/CNPJ: ", content, new SolidBrush(Color.Black), new Rectangle(540, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(630, 212), new Point(780, 212));
                ////Cpf/cnpj
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(5).Take(1).First().Substring(9), content, new SolidBrush(Color.Blue), new Rectangle(630, 196, 200, 30));
                e.Graphics.DrawString("A importância de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 227, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 242), new Point(780, 242));
                ////valor Extenso

                string str = conversor.EscreverExtenso(Decimal.Parse(tbValor.Text.Substring(2))).ToLower();
                //coloca em maiúsculo apenas a primeira letra da string
                e.Graphics.DrawString(str[0].ToString().ToUpper() + str.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(190, 226, 600, 30));
                e.Graphics.DrawString("Referente a.....: ", content, new SolidBrush(Color.Black), new Rectangle(60, 257, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 272), new Point(780, 272));
                //referente
                e.Graphics.DrawString(tbCorresp.Text, content, new SolidBrush(Color.Blue), new Rectangle(190, 256, 600, 30));

                DateTime dt = DateTime.Now;
                lang = new CultureInfo("pt-BR");

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7) + ", " + dt.ToString("D",lang),
                                      content, new SolidBrush(Color.Black), new Rectangle(60, 327, 300, 30));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(400, 397), new Point(780, 397));
                e.Graphics.DrawString("Assinatura", content, new SolidBrush(Color.Black), new Rectangle(550, 407, 200, 30));

                e.Graphics.DrawString("1ª Via", content, new SolidBrush(Color.Black), new Rectangle(748, 425, 50, 30));

                for (int i = 10; i < 850; i += 20)
                {
                    int j = 0;
                    j += (i + 10);

                    e.Graphics.DrawLine(new Pen(Color.Black), new Point(i, 550), new Point(j, 550));
                }


                //2a via
                //------------------------------------------
                e.Graphics.DrawRectangle(lapis, new Rectangle(50, 51 * 12, 750, 400));

                e.Graphics.DrawString(tbCliente.Text, nome, new SolidBrush(Color.Black), new Rectangle(75, 645, 500, 30), format);
                e.Graphics.DrawString(tbCpfCnpj.Text, cpfCnpj, new SolidBrush(Color.Black), new Rectangle(75, 670, 500, 30), format);
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(50, 745), new Point(800, 745));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(600, 612), new Point(600, 745));

                e.Graphics.DrawString("RECIBO", headers, new SolidBrush(Color.Black), new Rectangle(650, 625, 200, 30));
                e.Graphics.DrawString("N°: " + Convert.ToString(numeroRecibo).PadLeft(10, '0'), content, new SolidBrush(Color.Black), new Rectangle(640, 650, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605, 695, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 695, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645, 695, 138, 30));
                //Valor
                e.Graphics.DrawString(tbValor.Text.Substring(2), content, new SolidBrush(Color.Blue), new Rectangle(650, 695, 120, 30), format);

                e.Graphics.DrawString("Recebi(emos) de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 765, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 780), new Point(530, 780));
                //Nome Cliente
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(1).Take(1).First().Substring(8), content, new SolidBrush(Color.Blue), new Rectangle(190, 763, 400, 30));
                e.Graphics.DrawString("CPF/CNPJ: ", content, new SolidBrush(Color.Black), new Rectangle(540, 765, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(630, 780), new Point(780, 780));
                //Cpf/cnpj
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(5).Take(1).First().Substring(9), content, new SolidBrush(Color.Blue), new Rectangle(630, 763, 200, 30));
                e.Graphics.DrawString("A importância de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 797, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 813), new Point(780, 813));
                //valor Extenso

                //coloca em maiúsculo apenas a primeira letra da string
                e.Graphics.DrawString(str[0].ToString().ToUpper() + str.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(190, 796, 600, 30));
                e.Graphics.DrawString("Referente a.....: ", content, new SolidBrush(Color.Black), new Rectangle(60, 831, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 846), new Point(780, 846));
                //referente
                e.Graphics.DrawString(tbCorresp.Text, content, new SolidBrush(Color.Blue), new Rectangle(190, 828, 600, 30));

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7) + ", " + dt.ToString("D",lang),
                                      content, new SolidBrush(Color.Black), new Rectangle(60, 900, 300, 30));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(400, 960), new Point(780, 960));
                e.Graphics.DrawString("Assinatura", content, new SolidBrush(Color.Black), new Rectangle(550, 970, 200, 30));

                e.Graphics.DrawString("2ª Via", content, new SolidBrush(Color.Black), new Rectangle(748, 990, 50, 30));


            }
            catch (Exception error)
            {
                throw error;
            }
        }

        private void tbValor_KeyUp(object sender, KeyEventArgs e)
        {
            valor = tbValor.Text.Replace("R$", "").Replace(",", "").Replace(" ", "").Replace("00,", "");
            if (valor.Length == 0)
            {
                tbValor.Text = "0,00" + valor;
            }
            if (valor.Length == 1)
            {
                tbValor.Text = "0,0" + valor;
            }
            if (valor.Length == 2)
            {
                tbValor.Text = "0," + valor;
            }
            else if (valor.Length >= 3)
            {
                if (tbValor.Text.StartsWith("0,"))
                {
                    tbValor.Text = valor.Insert(valor.Length - 2, ",").Replace("0,", "");
                }
                else if (tbValor.Text.Contains("00,"))
                {
                    tbValor.Text = valor.Insert(valor.Length - 2, ",").Replace("00,", "");
                }
                else
                {
                    tbValor.Text = valor.Insert(valor.Length - 2, ",");
                }
            }
            valor = tbValor.Text;
            tbValor.Text = string.Format("{0:C}", Convert.ToDouble(valor));
            tbValor.Select(tbValor.Text.Length, 0);
        }

        private void tbValor_Leave(object sender, EventArgs e)
        {
            valor = tbValor.Text.Replace("R$", "");
            tbValor.Text = string.Format("{0:C}", Convert.ToDouble(valor));
        }

        private void tbValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (tbValor.Text.Contains(","));
                }
                else
                    e.Handled = true;
            }    
        }

        private void tbCpfCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            new FormDadosEmissor().apenasNumeros(e);
        }
    }
}
