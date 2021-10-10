namespace Lecture06.UI.Models
{
    public class ExchangeRate
    {
        public bool Success { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public double Rate { get; set; }

        public double Amount { get; set; }

        public string Message { get; set; }
    }
}
