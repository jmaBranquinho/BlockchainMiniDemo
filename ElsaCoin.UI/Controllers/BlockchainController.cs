using ElsaCoin.UI.Models;
using ElsaCoin.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElsaCoin.UI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BlockchainController : ControllerBase
    {
        private readonly IBlockchainService _blockchainService;

        public BlockchainController(IBlockchainService blockchainService)
        {
            _blockchainService = blockchainService;
        }

        [HttpGet("difficulty")]
        public int GetDifficulty()
        {
            return BlockchainSettings.Difficulty;
        }

        [HttpGet("reward")]
        public int GetMiningReward()
        {
            return BlockchainSettings.MiningReward;
        }

        [HttpGet("balance/{address}")]
        public Task<int> GetBalance(string address)
        {
            return _blockchainService.GetBalance(address);
        }

        [HttpGet("last50")]
        public Task<IEnumerable<Block>> GetLast50Blocks()
        {
            return _blockchainService.GetLast50OfBlockchain();
        }

        [HttpGet("current")]
        public Task<Block> GetCurrent()
        {
            return _blockchainService.GetCurrentBlock();
        }

        [HttpPost("add")]
        public Task<int> AddNewBlock([FromQuery]Block block)
        {
            return _blockchainService.AddNewBlock(block);
        }
    }
}
