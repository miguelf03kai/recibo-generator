using System;
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
        }

        FormDadosEmissor dEmissor = new FormDadosEmissor();

        validaCNPJ vCNPJ = new validaCNPJ();
        validaCPF vCPF = new validaCPF();
        SQLite sqlite = new SQLite();

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
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                printPreviewDialog1.Document = printDocument2;
                (printPreviewDialog1 as Form).WindowState = FormWindowState.Maximized;
                printPreviewDialog1.ShowDialog();
            }
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
                //Console.WriteLine(valor);

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
                        saveData();
                        Emitir();
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
                        saveData();
                        Emitir();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Campo Valor inválido: "+error, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                e.Graphics.DrawString("N°: "+Convert.ToString(numeroRecibo).PadLeft(10,'0'), content, new SolidBrush(Color.Black), new Rectangle(640, 85, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605, 128, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 127, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645, 127, 138, 30));
                //Valor
                e.Graphics.DrawString(tbValor.Text.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(650, 128, 120, 30), format);
                
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

                string str = conversor.EscreverExtenso(Decimal.Parse(valor)).ToLower();
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

                e.Graphics.DrawString("1ª Via", content, new SolidBrush(Color.Black), new Rectangle(748, 425, 50, 30));

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

        private void tbValor_TextChanged(object sender, EventArgs e)
        {
            //Remove previous formatting, or the decimal check will fail including leading zeros
            string value = tbValor.Text.Replace(",", "")
                .Replace("$", "").Replace(".", "").TrimStart('0');
            decimal ul;

            //Check we are indeed handling a number
            if (decimal.TryParse(value, out ul))
            {
                ul /= 100;
                //Unsub the event so we don't enter a loop
                tbValor.TextChanged -= tbValor_TextChanged;
                //Format the text as currency
                tbValor.Text = string.Format("{0:C2}", ul);
                tbValor.TextChanged += tbValor_TextChanged;
                tbValor.Select(tbValor.Text.Length, 0);
            }
            bool goodToGo = TextisValid(tbValor.Text);
            //enterButton.Enabled = goodToGo;
            if (!goodToGo)
            {
                tbValor.Text = "$0.00";
                tbValor.Select(tbValor.Text.Length, 0);
            }
        }

        private bool TextisValid(string text)
        {
            Regex money = new Regex(@"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }

        private void tbCpfCnpj_Leave(object sender, EventArgs e)
        {
            //if()

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

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

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
                //Valor
                e.Graphics.DrawString(tbValor.Text.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(650, 128, 120, 30), format);

                e.Graphics.DrawString("Recebi(emos) de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 212), new Point(530, 212));
                //Nome Cliente
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(1).Take(1).First().Substring(8), content, new SolidBrush(Color.Blue), new Rectangle(190, 196, 400, 30));
                e.Graphics.DrawString("CPF/CNPJ: ", content, new SolidBrush(Color.Black), new Rectangle(540, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(630, 212), new Point(780, 212));
                //Cpf/cnpj
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(5).Take(1).First().Substring(9), content, new SolidBrush(Color.Blue), new Rectangle(630, 196, 200, 30));
                e.Graphics.DrawString("A importância de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 227, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 242), new Point(780, 242));
                //valor Extenso

                string str = conversor.EscreverExtenso(Decimal.Parse(valor)).ToLower();
                //coloca em maiúsculo apenas a primeira letra da string
                e.Graphics.DrawString(str[0].ToString().ToUpper() + str.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(190, 226, 600, 30));
                e.Graphics.DrawString("Referente a.....: ", content, new SolidBrush(Color.Black), new Rectangle(60, 257, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 272), new Point(780, 272));
                //referente
                e.Graphics.DrawString(tbCorresp.Text, content, new SolidBrush(Color.Blue), new Rectangle(190, 256, 600, 30));

                DateTime dt = DateTime.Now;

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7) + ", " + dt.ToLongDateString(),
                                      content, new SolidBrush(Color.Black), new Rectangle(60, 327, 300, 30));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(400, 397), new Point(780, 397));
                e.Graphics.DrawString("Assinatura", content, new SolidBrush(Color.Black), new Rectangle(550, 407, 200, 30));

                e.Graphics.DrawString("1ª Via", content, new SolidBrush(Color.Black), new Rectangle(748, 425, 50, 30));

            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
