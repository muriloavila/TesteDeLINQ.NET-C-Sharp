using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteDeLINQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            //Primeiro Passo - Base de Dados
            dbLojaDataContext dbloja = new dbLojaDataContext();
            //Criação da Query
            var queryTodos = from produtos in dbloja.tabitens select produtos;
            //Execução da Query
            foreach (var item in queryTodos)
            {
                dataGridView1.Rows.Add(item.Id.ToString(),
                                        item.nome.ToString(),
                                        item.marca.ToString(),
                                        item.preco.ToString(),
                                        item.descricao.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Primeiro Passo - Base de dados
            dbLojaDataContext dbLoja = new dbLojaDataContext();
            //Criação da Query
            tabiten iten = new tabiten();
            iten.nome = "LG NEXUS";
            iten.marca = "LG";
            iten.preco = 1500;
            iten.descricao = "Celular da LG que deve ser maneiro mas ngm nunca viu um";

            dbLoja.tabitens.InsertOnSubmit(iten);

            dbLoja.SubmitChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Primeira etapa - Base de Dados
            dbLojaDataContext dbLoja = new dbLojaDataContext();
            //Criar a Query
            var nomeMoto = from nomemoto in dbLoja.tabitens
                           where nomemoto.nome.Contains("Moto X")
                           select nomemoto;
            //Execução
            foreach (var iten in nomeMoto)
            {
                if (iten.nome == "Moto X")
                {
                    iten.nome = "Moto MAXX";
                }
            }
            dbLoja.SubmitChanges();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Base de Dados
            dbLojaDataContext dbLoja = new dbLojaDataContext();
            //Query
            var deleteQuery = from cost in dbLoja.tabitens
                              where cost.nome == "Moto MAXX"
                              select cost;
            //Execução
            if (deleteQuery.Count() > 0)
            {
                dbLoja.tabitens.DeleteOnSubmit(deleteQuery.First());
                dbLoja.SubmitChanges();
            }
        }
    }
}
