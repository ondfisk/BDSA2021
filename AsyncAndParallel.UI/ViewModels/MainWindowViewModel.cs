using AsyncAndParallel.UI.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace AsyncAndParallel.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        private readonly HttpClient _client;

        private double _dkk;
        public double DKK { get => _dkk; set => SetProperty(ref _dkk, value); }

        private double _usd;
        public double USD { get => _usd; set => SetProperty(ref _usd, value); }

        private double _gbp;
        public double GBP { get => _gbp; set => SetProperty(ref _gbp, value); }

        private double _eur;
        public double EUR { get => _eur; set => SetProperty(ref _eur, value); }

        private long _timer;
        public long Timer { get => _timer; set => SetProperty(ref _timer, value); }

        private readonly Stopwatch _stopWatch;

        private readonly DispatcherTimer _dispatcherTimer;

        public MainWindowViewModel(HttpClient client)
        {
            _client = client;

            _stopWatch = new Stopwatch();

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += (s, e) => Timer = _stopWatch.ElapsedMilliseconds;
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
            _dispatcherTimer.Start();
        }

        public ICommand Calculate => new RelayCommand(async _ => await CalculateRates());

        private async Task CalculateRates()
        {
            _stopWatch.Restart();

            USD = await GetRate("DKK", "USD") * DKK;
            GBP = await GetRate("DKK", "GBP") * DKK;
            EUR = await GetRate("DKK", "EUR") * DKK;

            _stopWatch.Stop();
        }

        private async Task<double> GetRate(string from, string to)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            var url = $"http://currency-api.appspot.com/api/{from}/{to}.json";

            var data = await _client.GetStringAsync(url);

            var json = JsonSerializer.Deserialize<ExchangeRate>(data, _serializerOptions) ?? new ExchangeRate();

            return json.Rate;
        }
    }
}
