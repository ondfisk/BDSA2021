namespace AsyncAndParallel.UI.Models;

public class ExchangeRate
{
    public bool Success { get; set; }

    public string Source { get; set; } = string.Empty;

    public string Target { get; set; } = string.Empty;

    public double Rate { get; set; }

    public double Amount { get; set; }

    public string Message { get; set; } = string.Empty;
}
