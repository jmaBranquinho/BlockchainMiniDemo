using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ElsaCoin.UI.Models
{
    public class Block
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

        public string PreviousHash { get; set; }

        public string Hash { get; set; }

        public int Nonce { get; set; } = 0;

        public Block GenerateHash()
        {
            using (var sha256 = SHA256.Create())
            {
                var x = (PreviousHash ?? string.Empty) + TimeStamp.ToString("ddfffDDMMYYYY") + Nonce;
                Hash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(x))).Replace("-", "");
            }

            return this;
        }
    }
}
