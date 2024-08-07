using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleChatAppWithoutDesign.MVM.ViewModel;

namespace SimpleChatAppWithoutDesign;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    

    private void Button_CancelSelect_OnClick(object sender, RoutedEventArgs e)
    {
        ListView_ChatWindow.SelectedItem = null;
        ListView_ChatWindow.SelectedItems.Clear();
        StackPanel_SelectedMessage.Visibility = Visibility.Hidden;
    }


    private async void ListView_ChatWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        await MonitorIsSelected();
    }

    private async Task MonitorIsSelected()
    {
        while (true)
        {
            if (ListView_ChatWindow.SelectedItem != null)
            {
                Dispatcher.Invoke(() => StackPanel_SelectedMessage.Visibility = Visibility.Visible);
            }

            await Task.Delay(200);
        }
    }
}