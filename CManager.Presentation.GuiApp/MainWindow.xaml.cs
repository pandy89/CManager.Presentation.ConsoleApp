using CManager.Presentation.GuiApp.Models;
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

namespace CManager.Presentation.GuiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            var activityItem = new ActivityItem()
            {
                Activity = Activity.Text
            };

            Activities.Items.Add(activityItem);
            // Vad ska knappen göra? Lägger till text i textblocket
            //Activities.Items.Add(Activity.Text);
            //Rensa fältet
            Activity.Clear();
        }
        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is ActivityItem activityItem)
                {
                    activityItem.IsCompleted = true;
                }
            }
        }
    }
}