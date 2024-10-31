using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Flow.Launcher.Plugin.Lorem.Classes;
using Path = System.IO.Path;

namespace Flow.Launcher.Plugin.LoremIpsumGenerator.Views.Types
{
    public partial class Sentence : UserControl
    {
        private readonly PluginInitContext context;
        private Settings settings = new Settings();
        private string fullPath;

        public Sentence(PluginInitContext context)
        {
            InitializeComponent();
            this.context = context;
            fullPath = Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "lorem-settings.json");

            if (File.Exists(fullPath) && !(new FileInfo(fullPath).Length <= 1))
            {
                this.settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fullPath));
            }
            else if (File.Exists(fullPath) && new FileInfo(fullPath).Length <= 1)
            {
                this.settings.Sentence.Length = this.settings.Sentence.defaultLenghth();
                SaveSettingsToFile();
            }
            LoadSettings();
        }

        private void LenghthChanged(object sender, EventArgs e)
        {
            try
            {
                this.settings.Sentence.Length = Convert.ToInt32(LenghthTextBox.Text);
                SaveSettingsToFile();
            }
            catch (Exception ex) { }
        }
        private void LenghthFocus(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(LenghthTextBox.Text) < this.settings.Sentence.defaultLenghth())
            {
                LenghthTextBox.Text = this.settings.Sentence.defaultLenghth().ToString();
            }
        }

        private void LoadSettings()
        {
            LenghthTextBox.Text = this.settings.Sentence.Length.ToString();
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
