using System;
using System.Collections;
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
    public partial class FormDadosEmissor : Form
    {
        public FormDadosEmissor()
        {
            InitializeComponent();
        }
        
        //grava dados no arquivo
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                StreamReader sr = new StreamReader(@"config.ini");

                String linha = "";
                ArrayList linhas = new ArrayList();

                while ((linha = sr.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }

                sr.Close();

                linhas.Insert(1, "EMPRESA=" + tbNome.Text);
                linhas.RemoveAt(2);
                linhas.Insert(2, "ENDERECO=" + tbEndereco.Text);
                linhas.RemoveAt(3);
                linhas.Insert(3, "BAIRRO=" + tbBairro.Text);
                linhas.RemoveAt(4);
                linhas.Insert(4, "CIDADE=" + tbCidade.Text);
                linhas.RemoveAt(5);
                linhas.Insert(5, "CFP_CNPJ=" + tbCpfCnpj.Text);
                linhas.RemoveAt(6);
                linhas.Insert(6, "TELEFONE=" + tbTelefone.Text);
                linhas.RemoveAt(7);
                linhas.Insert(7, "EMAIL=" + tbEmail.Text);
                linhas.RemoveAt(8);
                linhas.Insert(8, "SITE=" + tbSite.Text);
                linhas.RemoveAt(9);
                linhas.Insert(9, "LOGO=" + tbLogo.Text);
                linhas.RemoveAt(10);

                StreamWriter sw = new StreamWriter(@"config.ini");

                foreach (string lista in linhas)
                {
                    sw.WriteLine(lista);
                }

                sw.Close();

                if (tbLogo.Text == "")
                {
                    logo.ImageLocation = "";
                }

                MessageBox.Show("Dados Salvos", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }catch(Exception error){
                throw error;
            }
        }

        private void FormDadosEmissor_Load(object sender, EventArgs e)
        {
            tbNome.Text = File.ReadLines(@"config.ini").Skip(1).Take(1).First().Substring(8);
            tbEndereco.Text = File.ReadLines(@"config.ini").Skip(2).Take(1).First().Substring(9);
            tbBairro.Text = File.ReadLines(@"config.ini").Skip(3).Take(1).First().Substring(7);
            tbCidade.Text = File.ReadLines(@"config.ini").Skip(4).Take(1).First().Substring(7);
            tbCpfCnpj.Text = File.ReadLines(@"config.ini").Skip(5).Take(1).First().Substring(9);
            tbTelefone.Text = File.ReadLines(@"config.ini").Skip(6).Take(1).First().Substring(9);
            tbEmail.Text = File.ReadLines(@"config.ini").Skip(7).Take(1).First().Substring(6);
            tbSite.Text = File.ReadLines(@"config.ini").Skip(8).Take(1).First().Substring(5);
            tbLogo.Text = File.ReadLines(@"config.ini").Skip(9).Take(1).First().Substring(5);
            logo.ImageLocation = tbLogo.Text;
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btDiretorio_Click(object sender, EventArgs e)
        {
            OpenFileDialog aDialog = new OpenFileDialog();
            aDialog.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            DialogResult result = aDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbLogo.Text = aDialog.FileName;
                logo.ImageLocation = tbLogo.Text;
            }
            else
            {
                MessageBox.Show("Arquivo Não Selecionado", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbCpfCnpj_Leave(object sender, EventArgs e)
        {
            if(tbCpfCnpj.Text.Length > 11){
                tbCpfCnpj.Mask = "##.###.###/####-##";
            }
            else if (tbCpfCnpj.Text.Length < 14)
            {
                tbCpfCnpj.Mask = "###.###.###-##";
            }
        }

        private void tbCpfCnpj_Enter(object sender, EventArgs e)
        {
            tbCpfCnpj.Mask = "";
        }
    }
}
