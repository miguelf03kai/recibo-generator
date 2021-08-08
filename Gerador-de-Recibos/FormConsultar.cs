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
    public partial class FormConsultar : Form
    {
        public FormConsultar()
        {
            InitializeComponent();
        }

        SQLite sqlite = new SQLite();

        private void FormConsultar_Load(object sender, EventArgs e)
        {
            refresh();
        }

        public void refresh()
        {
            try
            {
                dataGridView1.DataSource = sqlite.list();
                gridCustom();

            }catch(Exception error){
                MessageBox.Show("Erro: " + error,"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btEmitir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sqlite.busca(tbBusca.Text);
            gridCustom();
        }

        public void gridCustom()
        {
            dataGridView1.Columns[0].HeaderText = "N° Recib.";
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[0].DefaultCellStyle.Format = "D10";
            dataGridView1.Columns[1].HeaderText = "Cliente";
            dataGridView1.Columns[1].Width = 230;
            dataGridView1.Columns[2].HeaderText = "CPF/CNPJ";
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].HeaderText = "Valor";
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;

            //to change header color
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            print();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        public void print()
        {
            if (Convert.ToBoolean(dataGridView1.CurrentRow.Cells[5].Value) == true)
            {
                printPreviewDialog1.Document = printDocument2;
                (printPreviewDialog1 as Form).WindowState = FormWindowState.Maximized;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                printPreviewDialog1.Document = printDocument1;
                (printPreviewDialog1 as Form).WindowState = FormWindowState.Maximized;
                printPreviewDialog1.ShowDialog();
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

                if (File.ReadLines(@"config.ini").Skip(9).Take(1).First().Substring(5) == "")
                {

                }
                else
                {
                    Image image = Image.FromFile(File.ReadLines(@"config.ini").Skip(9).Take(1).First().Substring(5));
                    e.Graphics.DrawImage(image, new Rectangle(80, 60, 150, 110));

                    Image image2 = Image.FromFile(File.ReadLines(@"config.ini").Skip(9).Take(1).First().Substring(5));
                    e.Graphics.DrawImage(image2, new Rectangle(80, 63*10, 150, 110));
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
                e.Graphics.DrawString("N°: " + dataGridView1.CurrentRow.Cells[0].Value.ToString().PadLeft(10, '0'), content, new SolidBrush(Color.Black), new Rectangle(640, 85, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605, 128, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 127, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645, 127, 138, 30));
                ////Valor
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[3].Value.ToString().Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(650, 128, 120, 30), format);

                e.Graphics.DrawString("Recebi(emos) de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 212), new Point(530, 212));
                ////Nome Cliente
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[1].Value.ToString(), content, new SolidBrush(Color.Blue), new Rectangle(190, 196, 400, 30));
                e.Graphics.DrawString("CPF/CNPJ: ", content, new SolidBrush(Color.Black), new Rectangle(540, 197, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(630, 212), new Point(780, 212));
                ////Cpf/cnpj
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[2].Value.ToString(), content, new SolidBrush(Color.Blue), new Rectangle(630, 196, 200, 30));
                e.Graphics.DrawString("A importância de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 227, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 242), new Point(780, 242));
                ////valor Extenso

                string str = conversor.EscreverExtenso(Decimal.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString().Substring(1))).ToLower();
                ////coloca em maiúsculo apenas a primeira letra da string
                e.Graphics.DrawString(str[0].ToString().ToUpper() + str.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(190, 226, 600, 30));
                e.Graphics.DrawString("Referente a.....: ", content, new SolidBrush(Color.Black), new Rectangle(60, 257, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 272), new Point(780, 272));
                ////referente
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[4].Value.ToString(), content, new SolidBrush(Color.Blue), new Rectangle(190, 256, 600, 30));

                DateTime dt = DateTime.Now;

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7) + ", " + dt.ToLongDateString(),
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
                e.Graphics.DrawString("N°: " + dataGridView1.CurrentRow.Cells[0].Value.ToString().PadLeft(10, '0'), content, new SolidBrush(Color.Black), new Rectangle(640, 650, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605,695, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 695, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645,695, 138, 30));
                //Valor
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[3].Value.ToString().Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(650, 695, 120, 30), format);

                e.Graphics.DrawString("Recebi(emos) de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 765, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 780), new Point(530, 780));
                //Nome Cliente
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[1].Value.ToString(), content, new SolidBrush(Color.Blue), new Rectangle(190, 763, 400, 30));
                e.Graphics.DrawString("CPF/CNPJ: ", content, new SolidBrush(Color.Black), new Rectangle(540, 765, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(630, 780), new Point(780, 780));
                //Cpf/cnpj
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[2].Value.ToString(), content, new SolidBrush(Color.Blue), new Rectangle(630, 763, 200, 30));
                e.Graphics.DrawString("A importância de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 797, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 813), new Point(780, 813));
                //valor Extenso

                //coloca em maiúsculo apenas a primeira letra da string
                e.Graphics.DrawString(str[0].ToString().ToUpper() + str.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(190, 796, 600, 30));
                e.Graphics.DrawString("Referente a.....: ", content, new SolidBrush(Color.Black), new Rectangle(60, 831, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 846), new Point(780, 846));
                //referente
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[4].Value.ToString(), content, new SolidBrush(Color.Blue), new Rectangle(190, 828, 600, 30));

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7) + ", " + dt.ToLongDateString(),
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

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Você realmente quer excluir esse registro?", "Atenção", MessageBoxButtons.YesNo,MessageBoxIcon.Error);
            if (result == DialogResult.Yes)
            {
                sqlite.delete(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                Console.WriteLine(dataGridView1.CurrentRow.Cells[0].Value + " Deleted");
                refresh();
            }
            else if (result == DialogResult.No)
            {
                //don't delete anything
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

                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[1].Value.ToString(), nome, new SolidBrush(Color.Black), new Rectangle(75, 75, 500, 30), format);
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[2].Value.ToString(), cpfCnpj, new SolidBrush(Color.Black), new Rectangle(75, 100, 500, 30), format);
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(50, 177), new Point(800, 177));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(600, 50), new Point(600, 177));

                e.Graphics.DrawString("RECIBO", headers, new SolidBrush(Color.Black), new Rectangle(650, 60, 200, 30));
                e.Graphics.DrawString("N°: " + dataGridView1.CurrentRow.Cells[0].Value.ToString().PadLeft(10, '0'), content, new SolidBrush(Color.Black), new Rectangle(640, 85, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605, 128, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 127, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645, 127, 138, 30));
                ////Valor
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[3].Value.ToString().Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(650, 128, 120, 30), format);

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

                string str = conversor.EscreverExtenso(Decimal.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString().Substring(1))).ToLower();
                //coloca em maiúsculo apenas a primeira letra da string
                e.Graphics.DrawString(str[0].ToString().ToUpper() + str.Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(190, 226, 600, 30));
                e.Graphics.DrawString("Referente a.....: ", content, new SolidBrush(Color.Black), new Rectangle(60, 257, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 272), new Point(780, 272));
                //referente
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[4].Value.ToString(), content, new SolidBrush(Color.Blue), new Rectangle(190, 256, 600, 30));

                DateTime dt = DateTime.Now;

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7) + ", " + dt.ToLongDateString(),
                                      content, new SolidBrush(Color.Black), new Rectangle(60, 327, 300, 30));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(400, 397), new Point(780, 397));
                e.Graphics.DrawString("Assinatura", content, new SolidBrush(Color.Black), new Rectangle(550, 407, 200, 30));

                e.Graphics.DrawString("1ª Via", content, new SolidBrush(Color.Black), new Rectangle(748, 425, 50, 30));

                for (int i = 10; i < 850; i += 20) {
                    int j = 0;
                        j += (i + 10);

                        e.Graphics.DrawLine(new Pen(Color.Black), new Point(i, 550), new Point(j, 550));
                }


                //2a via
                //------------------------------------------
                e.Graphics.DrawRectangle(lapis, new Rectangle(50, 51 * 12, 750, 400));

                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[1].Value.ToString(), nome, new SolidBrush(Color.Black), new Rectangle(75, 645, 500, 30), format);
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[2].Value.ToString(), cpfCnpj, new SolidBrush(Color.Black), new Rectangle(75, 670, 500, 30), format);
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(50, 745), new Point(800,745));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(600, 612), new Point(600, 745));

                e.Graphics.DrawString("RECIBO", headers, new SolidBrush(Color.Black), new Rectangle(650, 625, 200, 30));
                e.Graphics.DrawString("N°: " + dataGridView1.CurrentRow.Cells[0].Value.ToString().PadLeft(10, '0'), content, new SolidBrush(Color.Black), new Rectangle(640, 650, 200, 30));
                e.Graphics.DrawString("R$: ", headers, new SolidBrush(Color.Black), new Rectangle(605, 695, 200, 30));
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(645, 695, 138, 30));
                e.Graphics.DrawRectangle(lapis, new Rectangle(645, 695, 138, 30));
                //Valor
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[3].Value.ToString().Substring(1), content, new SolidBrush(Color.Blue), new Rectangle(650, 695, 120, 30), format);

                e.Graphics.DrawString("Recebi(emos) de: ", content, new SolidBrush(Color.Black), new Rectangle(60, 765, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(190, 780), new Point(530, 780));
                //Nome Cliente
                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(1).Take(1).First().Substring(8), content, new SolidBrush(Color.Blue), new Rectangle(190, 763, 400, 30));
                e.Graphics.DrawString("CPF/CNPJ: ", content, new SolidBrush(Color.Black), new Rectangle(540, 765, 200, 30));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(630, 780), new Point(780,780));
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
                e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[4].Value.ToString(), content, new SolidBrush(Color.Blue), new Rectangle(190,828, 600, 30));

                e.Graphics.DrawString(File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7) + ", " + dt.ToLongDateString(),
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow Myrow in dataGridView1.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                //if (Convert.ToInt32(Myrow.Cells[2].Value) < Convert.ToInt32(Myrow.Cells[1].Value))// Or your condition 
                if(Convert.ToBoolean(Myrow.Cells[5].Value) == true)
                {
                    Myrow.DefaultCellStyle.BackColor = Color.LightGray;
                }
                //else
                //{
                //    Myrow.DefaultCellStyle.BackColor = Color.Green;
                //}
            }
        }


    }
}
