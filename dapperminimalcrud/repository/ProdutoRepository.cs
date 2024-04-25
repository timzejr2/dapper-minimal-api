using dapperminimalcrud.models;
using MySql.Data.MySqlClient;
using Dapper;

namespace dapperminimalcrud.repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private string connectionString;

        public ProdutoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutos()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryAsync<Produto>("SELECT * FROM produtos");
            }
        }

        public async Task<Produto> GetProduto(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM produtos WHERE id = @Id", new { Id = id });
            }
        }

        public async Task AddProduto(Produto produto)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync("INSERT INTO produtos(nome, descricao, preco, estoque) VALUES(@Nome, @Descricao, @Preco, @Estoque)", produto);
            }
        }

        public async Task UpdateProduto(Produto produto)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync("UPDATE produtos SET nome = @Nome, descricao = @Descricao, preco = @Preco, estoque = @Estoque WHERE id = @Id", produto);
            }
        }

        public async Task DeleteProduto(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync("DELETE FROM produtos WHERE id = @Id", new { Id = id });
            }
        }
    }
}
