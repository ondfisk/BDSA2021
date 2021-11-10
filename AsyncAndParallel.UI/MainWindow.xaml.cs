namespace AsyncAndParallel.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var client = new HttpClient();

        DataContext = new MainWindowViewModel(client);
    }
}
