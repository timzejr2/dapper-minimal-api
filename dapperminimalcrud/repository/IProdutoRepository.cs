using dapperminimalcrud.models;

namespace dapperminimalcrud.repository
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllProdutos();
        Task<Produto> GetProduto(int id);
        Task AddProduto(Produto produto);
        Task UpdateProduto(Produto produto);
        Task DeleteProduto(int id);
    }
}
