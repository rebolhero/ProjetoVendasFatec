using MySql.Data.MySqlClient;
using Projeto_Vendas_Fatec_2.br.com.projeto.con;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Vendas_Fatec_2.br.com.projeto.dao
{
    public class FornecedoresDAO
    {
        //Atributo
        private MySqlConnection conexao;

        //Construtor
        public FornecedoresDAO()
        {
            this.conexao = new ConnectionFactory().GetConnection();
        }


        #region Métódo que Lista Todos os Clientes
        public DataTable ListarTodosFornecedores()
        {
            try
            {
                //1 Passo - Criar o comando SQL e o nosso DataTable
                DataTable tabelaFornecedor = new DataTable();
                string sql = @"select * from tb_fornecedores";

                //2 Passo - Organizar e executar o comando sql               
                MySqlCommand executasql = new MySqlCommand(sql, conexao);

                //3 passo - Abrir a conexao e executa o comando sql
                conexao.Open();
                executasql.ExecuteNonQuery();

                //4 Passo - Preencher o nosso DataTable com os dados do select
                MySqlDataAdapter adapter = new MySqlDataAdapter(executasql);
                adapter.Fill(tabelaFornecedor);
                conexao.Close();

                return tabelaFornecedor;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu o erro: " + erro);
                return null;
            }
        }
        #endregion




    }
}
