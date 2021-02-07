using ElsaCoin.UI.Data;
using ElsaCoin.UI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ElsaCoin.UI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> GetNextPendingTransaction()
        {
            return await _context.Transactions
                .Where(t => !t.IsProcessed)
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddNewTransaction(Transaction transaction)
        {
            transaction.IsProcessed = false;

            _context.Transactions.Add(transaction);

            return await _context.SaveChangesAsync();
        }
    }
}
