using ElsaCoin.UI.Models;
using System.Threading.Tasks;

namespace ElsaCoin.UI.Services
{
    public interface ITransactionService
    {
        Task<int> AddNewTransaction(Transaction transaction);
        Task<Transaction> GetNextPendingTransaction();
    }
}