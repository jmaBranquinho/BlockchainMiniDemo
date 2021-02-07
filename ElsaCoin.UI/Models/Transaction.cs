namespace ElsaCoin.UI.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public int Amount { get; set; }

        public bool IsProcessed { get; set; }
    }
}