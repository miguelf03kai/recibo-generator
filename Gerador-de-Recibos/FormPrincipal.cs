using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerador_de_Recibos
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        FormDadosEmissor dEmissor = new FormDadosEmissor();

        validaCNPJ vCNPJ = new validaCNPJ();
        validaCPF vCPF = new validaCPF();

        public void Emitir()
        {
            printPreviewDialog1.Document = printDocument1;
            (printPreviewDialog1 as Form).WindowState = FormWindowState.Maximized;
            printPreviewDialog1.ShowDialog();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dEmissor.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //formata o valor digitado
                tbValor.Text = double.Parse(tbValor.Text).ToString("C2");
                tbValor.Text = tbValor.Text.Substring(1);

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
                else if (tbCpfCnpj.TextLength < 14)
                {
                    if (vCPF.IsCpf(tbCpfCnpj.Text) == false)
                    {
                        MessageBox.Show("CPF Inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbCpfCnpj.Focus();
                    }
                    else
                    {
                        Emitir();
                    }
                }
                else if (tbCpfCnpj.TextLength > 11)
                {
                    if (vCNPJ.IsCnpj(tbCpfCnpj.Text) == false)
                    {
                        MessageBox.Show("CNPJ Inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbCpfCnpj.Focus();
                    }
                    else
                    {
                        Emitir();
                    }
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("Campo Valor inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbValor.Focus();

            }
        }

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

                if (File.ReadLines(@"config.ini").Skip(9).Take(1).First().Substring(5) == ""){
 
                }
                else{
                    Image image = Image.FromFile(File.ReadLines(@"config.ini").Skip(9).Take(1).First().Substring(5));
                    e.Graphics.DrawImage(image, new Rectangle(80, 60, 150, 110));
                }

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(1).Take(1).First().Substring(8), headers, new SolidBrush(Color.Black), new Rectangle(180, 50, 400, 30),format);
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(5).Take(1).First().Substring(9), cpfCnpj, new SolidBrush(Color.Black), new Rectangle(180, 75, 400, 30), format);
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(2).Take(1).First().Substring(9) +", "+ 
                                      File.ReadLines(@"config.ini").Skip(3).Take(1).First().Substring(7),
                                      content, new SolidBrush(Color.Black), new Rectangle(180, 100, 400, 30), format);
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(6).Take(1).First().Substring(9)+" / "+
                                      File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7),
                                      content, new SolidBrush(Color.Black), new Rectangle(180, 125, 400, 30), format);

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(50, 177), new Point(800, 177));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(600, 50), new Point(600, 177));

                e.Graphics.DrawString("RECIBO", headers, new SolidBrush(Color.Black), new Rectangle(650, 60, 200, 30));
                e.Graphics.DrawString("N°: 0000000001", content, new SolidBrush(Color.Black), new Rectangle(640, 85, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605, 128, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 127, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645, 127, 138, 30));
                //Valor
                e.Graphics.DrawString(tbValor.Text, content, new SolidBrush(Color.Blue), new Rectangle(650, 128, 120, 30), format);
                
                e.Graphics.DrawString("Recebi(emos) de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 212), new Point(530, 212));
                //Nome Cliente
                e.Graphics.DrawString(tbCliente.Text, content, new SolidBrush(Color.Blue), new Rectangle(190, 196, 400, 30));
                e.Graphics.DrawString("CPF/CNPJ: ", content, new SolidBrush(Color.Black), new Rectangle(540, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(630, 212), new Point(780, 212));
                //Cpf/cnpj
                e.Graphics.DrawString(tbCpfCnpj.Text, content, new SolidBrush(Color.Blue), new Rectangle(630, 196, 200, 30));
                e.Graphics.DrawString("A importância de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 227, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 242), new Point(780, 242));
                //valor Extenso

                string str = conversor.EscreverExtenso(Decimal.Parse(tbValor.Text)).ToLower();
                //coloca em maiúsculo apenas a primeira letra da string
                e.Graphics.DrawString(str[0].ToString().ToUpper()+str.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(190, 226, 600, 30));
                e.Graphics.DrawString("Referente a.....: ", content, new SolidBrush(Color.Black), new Rectangle(60, 257, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 272), new Point(780, 272));
                //referente
                e.Graphics.DrawString(tbCorresp.Text, content, new SolidBrush(Color.Blue), new Rectangle(190, 256, 600, 30));

                DateTime dt = DateTime.Now;

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7)+", "+dt.ToLongDateString(),
                                      content, new SolidBrush(Color.Black), new Rectangle(60, 327, 300, 30));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(400, 397), new Point(780, 397));
                e.Graphics.DrawString("Assinatura", content, new SolidBrush(Color.Black), new Rectangle(550, 407, 200, 30));

            }
            catch(Exception error){
                throw error;
            }
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            tbCliente.Text = "";
            tbCpfCnpj.Text = "";
            tbValor.Text = "";
            tbCorresp.Text = "";
        }

        private void btFe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
