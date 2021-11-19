using MySql.Data.MySqlClient;
using Projeto_Vendas_Fatec_2.br.com.projeto.con;
using Projeto_Vendas_Fatec_2.br.com.projeto.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Vendas_Fatec_2.br.com.projeto.dao
{
    public class FuncionarioDAO
    {
        //Atributo
        private MySqlConnection conexao;

        //Construtor
        public FuncionarioDAO()
        {
            this.conexao = new ConnectionFactory().GetConnection();
        }


        #region Método que efetua login
        public void EfetuarLogin(string email, string senha)
        {
            try
            {
                //1 passo Criar o comando sql
                string sql = @"SELECT * FROM tb_funcionarios WHERE email = @email AND senha = @senha";

                
                //2 passo - Organizar o comando SQL
                MySqlCommand executasql = new MySqlCommand(sql, conexao);
                executasql.Parameters.AddWithValue("@email", email);
                executasql.Parameters.AddWithValue("@senha", senha);

                // 3 passo - Abrir a conexao e executa o comando sql
                conexao.Open();
                MySqlDataReader rs = executasql.ExecuteReader();

                if (rs.Read())
                {
                    //Fez o login
                    //Descobrir qual é o nivel de acesso deste usuário
                    string nivel = rs.GetString("nivel_acesso");
                    string nome = rs.GetString("nome");

                    //Criando um objeto da tela de menu
                    Frmmenu telamenu = new Frmmenu();
                    telamenu.lbllogado.Text = nome;


                    //Restrições
                    if (nivel.Equals("Administrador"))
                    {
                        MessageBox.Show("Bem vindo ao Sistema " + nome);
                       
                        //Abre a tela de Menu
                        telamenu.ShowDialog();
                    }

                    else if (nivel.Equals("Usuário"))
                    {
                        MessageBox.Show("Bem vindo ao Sistema " + nome);
                        
                        //Especificar as permissões;
                        telamenu.menu_relatorios.Enabled = false;
                        telamenu.menu_cadproduto.Visible = false;

                        //Abrir a tela de menu
                        telamenu.ShowDialog();
                    }

                }

                else
                {
                    MessageBox.Show("Usuário ou senha incorretos!");
                }

                //Fecha a conexao
                conexao.Close();

            }
            catch (Exception erro)
            {

                MessageBox.Show("Aconteceu o erro: " + erro);
            }

        }

        #endregion


    }
}
