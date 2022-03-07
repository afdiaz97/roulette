namespace roulette.Models
{
    public class Bet
    {
        public long Id { get; set; }
        public long IdRoulette { get; set; }
        public int number { get; set; }
        public string color { get; set; }
        public int money { get; set; }
        public int gain { get; set; }
    }
}
