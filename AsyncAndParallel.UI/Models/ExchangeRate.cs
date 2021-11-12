namespace AsyncAndParallel.UI.Models;

public class ExchangeRate
{
    public bool Success { get; set; }

    public string Source { get; set; } = null!;

    public string Target { get; set; } = null!;

    public double Rate { get; set; }

    public double Amount { get; set; }

    public string Message { get; set; } = null!;
}
