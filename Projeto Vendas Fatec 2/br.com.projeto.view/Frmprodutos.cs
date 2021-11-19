﻿using ClosedXML.Excel;
using Projeto_Vendas_Fatec_2.br.com.projeto.dao;
using Projeto_Vendas_Fatec_2.br.com.projeto.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Vendas_Fatec_2.br.com.projeto.view
{
    public partial class Frmprodutos : Form
    {
        public Frmprodutos()
        {
            InitializeComponent();
        }

        private void Frmprodutos_Load(object sender, EventArgs e)
        {
            //Carregar tela

            //Como Carregar e configurar o combobox
            FornecedoresDAO dao = new FornecedoresDAO();
            cbfornecedor.DataSource = dao.ListarTodosFornecedores();
            cbfornecedor.DisplayMember = "nome";
            cbfornecedor.ValueMember = "id";

            //Carregando o datagridView de produto
            ProdutosDAO dao_produto = new ProdutosDAO();
            dgprodutos.DataSource = dao_produto.ListarTodosProdutos();

        }

        private void Btncadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                //Botao cadastrar Produto
                Produtos produto = new Produtos();

                //1 passo - Receber os dados
                produto.descricao = txtdesc.Text;
                produto.preco = decimal.Parse(txtpreco.Text);
                produto.qtd_estoque = int.Parse(txtestoque.Text);
                produto.for_id = int.Parse(cbfornecedor.SelectedValue.ToString());

                //2 passo - Cadastrar no banco
                ProdutosDAO dao = new ProdutosDAO();
                dao.CadastrarProduto(produto);

                //Recarregar o datagrid
                dgprodutos.DataSource = dao.ListarTodosProdutos();

            }
            catch (Exception)
            {
                MessageBox.Show("Preencha todos os campos!");               
            }

        }

        private void Dgprodutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Pegar os dados de um produto selecionado
            txtcodigo.Text    = dgprodutos.CurrentRow.Cells[0].Value.ToString();
            txtdesc.Text      = dgprodutos.CurrentRow.Cells[1].Value.ToString();
            txtpreco.Text     = dgprodutos.CurrentRow.Cells[2].Value.ToString();
            txtestoque.Text   = dgprodutos.CurrentRow.Cells[3].Value.ToString();

            cbfornecedor.Text = dgprodutos.CurrentRow.Cells[4].Value.ToString();

            //Troca a aba
            tabControl1.SelectedTab = tabPage1;

        }

        private void Btnexcluir_Click(object sender, EventArgs e)
        {
            try
            {
                //Botao excluir Produto
                Produtos produto = new Produtos();

                //1 passo - Receber os dados
                produto.id = int.Parse(txtcodigo.Text);

                //2 passo - Cadastrar no banco
                ProdutosDAO dao = new ProdutosDAO();
                dao.ExcluirProduto(produto);

                //Recarregar o datagrid
                dgprodutos.DataSource = dao.ListarTodosProdutos();

            }
            catch (Exception)
            {
                MessageBox.Show("Preencha todos os campos!");
            }

        }

        private void Btneditar_Click(object sender, EventArgs e)
        {
            try
            {
                //Botao Alterar Produto
                Produtos produto = new Produtos();

                //1 passo - Receber os dados
                produto.descricao = txtdesc.Text;
                produto.preco = decimal.Parse(txtpreco.Text);
                produto.qtd_estoque = int.Parse(txtestoque.Text);
                produto.for_id = int.Parse(cbfornecedor.SelectedValue.ToString());

                produto.id = int.Parse(txtcodigo.Text);

                //2 passo - Alterar no banco
                ProdutosDAO dao = new ProdutosDAO();
                dao.AlterarProduto(produto);

                //Recarregar o datagrid
                dgprodutos.DataSource = dao.ListarTodosProdutos();

            }
            catch (Exception)
            {
                MessageBox.Show("Preencha todos os campos!");
            }
        }

        private void Txtconsulta_TextChanged(object sender, EventArgs e)
        {
            //Listar Produtos por Descricao
            string nome = "%" + txtconsulta.Text + "%";
            ProdutosDAO dao = new ProdutosDAO();
            dgprodutos.DataSource = dao.ListarTodosProdutosPorNome(nome);

        }

        private void Btnconsultar_Click(object sender, EventArgs e)
        {

        }


        private void planilha_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var local = openFileDialog1.FileName;

                    var xls = new XLWorkbook(local);
                    var planilha = xls.Worksheets.First(w => w.Name == "Planilha1");

                    for (int i = 2; i < planilha.RowCount(); i++)
                    {
                        try
                        {
                            string Descricao = planilha.Cell(i, 1).Value.ToString();
                            decimal.TryParse(planilha.Cell(i, 2).Value.ToString(), out decimal Preco);
                            int.TryParse(planilha.Cell(i, 3).Value.ToString(), out int Qtd_estoque);
                            int.TryParse(planilha.Cell(i, 4).Value.ToString(), out int For_id);

                            if (Descricao == "")
                            {
                                break;
                            }

                            Produtos produtos = new Produtos
                            {
                                descricao = Descricao,
                                preco = Preco,
                                qtd_estoque = Qtd_estoque,
                                for_id = For_id
                            };

                            ProdutosDAO dao = new ProdutosDAO();
                            dao.CadastrarProduto(produtos);

                            dgprodutos.DataSource = dao.ListarTodosProdutos();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("Deu erro!");

                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Deu erro, contate o suporte!");
            }
        }

        private void planilhaArquivo_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
