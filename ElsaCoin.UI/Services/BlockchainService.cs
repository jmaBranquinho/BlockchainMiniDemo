using ElsaCoin.UI.Data;
using ElsaCoin.UI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElsaCoin.UI.Services
{
    public class BlockchainService : IBlockchainService
    {
        private readonly ApplicationDbContext _context;

        public BlockchainService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Block>> GetBlockchain()
        {
            var blockchain = await _context.Blockchain
                .ToListAsync();

            return (blockchain is null || !blockchain.Any())
                ? new List<Block> { await CreateGenesisBlock() }
                : blockchain;
        }

        public async Task<IEnumerable<Block>> GetLast50OfBlockchain()
        {
            var blockchain = await _context.Blockchain
                .OrderByDescending(b => b.TimeStamp)
                .Take(50)
                .ToListAsync();

            return (blockchain is null || !blockchain.Any())
                ? new List<Block> { await CreateGenesisBlock() }
                : blockchain;
        }

        public async Task<Block> GetCurrentBlock()
        {
            return await _context.Blockchain
                .OrderByDescending(b => b.TimeStamp)
                .FirstOrDefaultAsync()
                ?? await CreateGenesisBlock();
        }

        public async Task<int> GetBalance(string userAddress)
        {
            var balance = 0;

            var block = await GetCurrentBlock();

            if (!(block is null) && !(block.Transactions is null) && block.Transactions.Any())
            {
                foreach (var transaction in block?.Transactions)
                {
                    if (transaction.FromAddress == userAddress)
                    {
                        balance -= transaction.Amount;
                    }

                    if (transaction.ToAddress == userAddress)
                    {
                        balance += transaction.Amount;
                    }
                }
            }

            return balance;
        }

        private Task<Block> CreateGenesisBlock()
        {
            return SaveBlock(new Block
            {
                Id = 0,
                TimeStamp = DateTime.Now,
                Transactions = Enumerable.Empty<Transaction>(),
            }.GenerateHash());
        }

        private async Task<Block> SaveBlock(Block block)
        {
            _context.Blockchain.Add(block);
            await _context.SaveChangesAsync();

            return block;
        }

        public async Task<int> AddNewBlock(Block block)
        {
            _context.Blockchain.Add(block);

            await _context.SaveChangesAsync();

            return block.Id;
        }
    }
}
