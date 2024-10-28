using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Flow.Launcher.Plugin.Lorem;
using Flow.Launcher.Plugin.LoremIpsumGenerator.Views.Types;
using ModernWpf;

namespace Flow.Launcher.Plugin.Lorem.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class PluginSettings : UserControl
    {

        public PluginSettings()
        {
            InitializeComponent();
            SettingsFrame.Navigate(new General());
            GeneralButton.Style = (Style)this.FindResource("AccentButtonStyle");
        }

        void SwitchToType(object sender, RoutedEventArgs e)
        {
            Button pressedButton = (Button)sender;
            foreach (var child in TypeContainer.Children)
            {
                if (child is Button button && button != pressedButton)
                {
                    button.ClearValue(Button.StyleProperty);
                }
            }
            GeneralButton.ClearValue(Button.StyleProperty);
            pressedButton.Style = (Style)this.FindResource("AccentButtonStyle");
            string type = pressedButton.Content.ToString();

            if (type == "Sentence")
            {
                SettingsFrame.Navigate(new Sentence());
            }
        }
    }
}
