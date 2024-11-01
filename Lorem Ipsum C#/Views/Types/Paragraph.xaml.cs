using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using Flow.Launcher.Plugin.LoremIpsumGenerator.Classes;

namespace Flow.Launcher.Plugin.LoremIpsumGenerator.Views.Types
{
    public partial class Paragraph : UserControl
    {
        private readonly PluginInitContext context;
        private Settings settings = new Settings();
        private string fullPath;
        public Paragraph(PluginInitContext context)
        {
            InitializeComponent();
            this.context = context;
            fullPath = System.IO.Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "lorem-settings.json");

            if (File.Exists(fullPath) && !(new FileInfo(fullPath).Length <= 1))
            {
                this.settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fullPath));
            }
            else if (File.Exists(fullPath) && new FileInfo(fullPath).Length <= 1)
            {
                this.settings.Paragraph.Length = this.settings.Paragraph.defaultLenghth();
                SaveSettingsToFile();
            }
            LoadSettings();
        }
        private void LenghthChanged(object sender, EventArgs e)
        {
            try
            {
                this.settings.Paragraph.Length = Convert.ToInt32(LenghthTextBox.Text);
                SaveSettingsToFile();
            }
            catch (Exception ex) { }
        }
        private void LenghthFocus(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(LenghthTextBox.Text) < this.settings.Paragraph.defaultLenghth())
            {
                LenghthTextBox.Text = this.settings.Paragraph.defaultLenghth().ToString();
            }
        }

        private void LoadSettings()
        {
            LenghthTextBox.Text = this.settings.Paragraph.Length.ToString();
        }

        private void SaveSettingsToFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this.settings, Formatting.Indented);
                File.WriteAllText(fullPath, json);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
