using ElsaCoin.UI.Models;
using ElsaCoin.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElsaCoin.UI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("next-pending")]
        public Task<Transaction> GetNextPendingTransaction()
        {
            return _transactionService.GetNextPendingTransaction();
        }

        [HttpPost("add")]
        public Task<int> AddNewTransaction(Transaction transaction)
        {
            return _transactionService.AddNewTransaction(transaction);
        }
    }
}
