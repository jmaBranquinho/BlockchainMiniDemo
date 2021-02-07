using ElsaCoin.UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElsaCoin.UI.Services
{
    public interface IBlockchainService
    {
        Task<IEnumerable<Block>> GetBlockchain();
        Task<IEnumerable<Block>> GetLast50OfBlockchain();
        Task<Block> GetCurrentBlock();
        Task<int> GetBalance(string userAddress);
        Task<int> AddNewBlock(Block block);
    }
}