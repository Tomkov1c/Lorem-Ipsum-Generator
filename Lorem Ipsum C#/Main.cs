using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Flow.Launcher.Plugin.LoremIpsumGenerator.Classes;
using Flow.Launcher.Plugin.LoremIpsumGenerator.Views;
using Flow.Launcher.Plugin;
using System.Configuration;
using Humanizer.Configuration;
using Newtonsoft.Json;
using System.IO;
using Flow.Launcher.Plugin.SharedCommands;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Flow.Launcher.Plugin.LoremIpsumGenerator
{
    public class MyFlowPlugin : IPlugin, ISettingProvider
    {
        string settingFullPath;
        string num;
        int amount;

        public Func<ActionContext, bool> Action { get; set; }
        private PluginInitContext context;
        private Settings settings;

        string[] loremIpsumWords = LoremWords.Words;

        public void Init(PluginInitContext context)
        {
            this.context = context;
            settingFullPath = Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "lorem-settings.json");

            if (!settingFullPath.FileExists())
            {
                using (File.Create(settingFullPath)) { }

                Settings defaultSettings = new Settings();
                defaultSettings.Sentence.Length = defaultSettings.Sentence.defaultLenghth();
                defaultSettings.Paragraph.Length = defaultSettings.Paragraph.defaultLenghth();
                defaultSettings.Word.Length = defaultSettings.Word.defaultLenghth();
                defaultSettings.Title.Length = defaultSettings.Title.defaultLenghth();


                string json = JsonConvert.SerializeObject(defaultSettings, Formatting.Indented);
                File.WriteAllText(settingFullPath, json);
            }
            else
            {
                this.settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingFullPath));
            }

        }

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();
            this.settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingFullPath));
            results.Clear();
            num = "";

			string afterKeyword = query.Search.ToLower();

            foreach (char c in afterKeyword)
            {
                if (char.IsDigit(c) || char.IsUpper(c))
                {
                    num += c;
                }
            }
            QueryText queryText = new QueryText();

            if (afterKeyword != "" && afterKeyword != " ")
            {
                if (afterKeyword[0] == 'p')
                {
                    results.Add(new Result
                    {
                        Title = num == "" ? queryText.Paragraph.Title : queryText.Paragraph.Custom.Title.combine(num),
                        SubTitle = num == "" ? queryText.Paragraph.Subtitle : queryText.Paragraph.Custom.Subtitle.combine(num),
                        IcoPath = queryText.Paragraph.Icon,
                        Action = clipboardParagraph
                    });
                }
                else if (afterKeyword[0] == 's')
                {
                    results.Add(new Result
                    {
                        Title = num == "" ? queryText.Sentence.Title : queryText.Sentence.Custom.Title.combine(num),
                        SubTitle = num == "" ? queryText.Sentence.Subtitle : queryText.Sentence.Custom.Subtitle.combine(num),
                        IcoPath = queryText.Sentence.Icon, Action = clipboardSentence
                    });

                }
                else if(afterKeyword[0] == 'w')
                {
                    results.Add(new Result
                    {
                        Title = num == "" ? queryText.Word.Title : queryText.Word.Custom.Title.combine(num),
                        SubTitle = num == "" ? queryText.Word.Subtitle : queryText.Word.Custom.Subtitle.combine(num),
                        IcoPath = queryText.Word.Icon, Action = clipboardWord
                    });
                }
                else if(afterKeyword[0] == 't') 
                {
                    results.Add(new Result
                    {
                        Title = num == "" ? queryText.Title.Title : queryText.Title.Custom.Title.combine(num),
                        SubTitle = num == "" ? queryText.Title.Subtitle : queryText.Title.Custom.Subtitle.combine(num),
                        IcoPath = queryText.Title.Icon, Action = clipboardTitle
                    });
                }
                else
                {
                    results.Add(new Result { Title = queryText.Error.Title, SubTitle = queryText.Error.Subtitle, IcoPath = queryText.Error.Icon});
                }

            }else
            {
                results.Add(new Result { Title = queryText.Sentence.Title, SubTitle = queryText.Sentence.Subtitle, IcoPath = queryText.Sentence.Icon, Action = clipboardSentence });
                results.Add(new Result { Title = queryText.Paragraph.Title, SubTitle = queryText.Paragraph.Subtitle, IcoPath = queryText.Paragraph.Icon, Action = clipboardParagraph });
                results.Add(new Result { Title = queryText.Word.Title, SubTitle = queryText.Word.Subtitle, IcoPath = queryText.Word.Icon, Action = clipboardWord });
                results.Add(new Result { Title = queryText.Title.Title, SubTitle = queryText.Title.Subtitle, IcoPath = queryText.Title.Icon, Action = clipboardTitle });
            }

            amount = (!string.IsNullOrWhiteSpace(num)) ? Convert.ToInt32(num) : 0;
            return results;
        }
        public bool clipboardSentence(ActionContext context)
        {
            string output = genSentence(amount);
            Clipboard.SetText(output);
            return true;
        }
        public bool clipboardParagraph(ActionContext context)
        {
            string output = "";
            amount = amount == 0 ? this.settings.Paragraph.Length : amount;
            for (int i = 0; i < amount; i++)
            {
                output += genSentence(this.settings.Sentence.Length) + " ";
            }
            // Settings Idea: output += "\n";
            //                seperate sentence settings
            Clipboard.SetText(output);
            return true;
        }
        public bool clipboardWord(ActionContext context)
        {

            string output = "";
            amount = amount == 0 ? 1 : amount;
            for (int i = 0; i < amount; i++)
            {
                output += genWord() + (i != amount - 1 ? " " : null );
            }
            
            Clipboard.SetText(output);
            return true;
        }
        public bool clipboardTitle(ActionContext context)
        {
            string output = genTitle(amount);
            Clipboard.SetText(output);
            return true;
        }




        public string genSentence(int length)
        {
            string output = "";
            string temp = "";
            output = genWord();
            string privWord = " ";
            Random rng = new Random();

            length = (length == 0) ? this.settings.Sentence.Length : length;

            for (int i = 0; i < length; i++)
            {
                int needsComma = rng.Next(10);
                temp = genWord();

                if ((temp == privWord) || (temp.Length == privWord.Length) || (temp[0] == privWord[0]))
                {
                    i--;
                    continue;
                }

                output += temp;
                privWord = temp;

                if (i != (length - 1))
                {
                    output += (needsComma >= 7) ? ", " : " ";
                }

            }

            int whichEndMark = rng.Next(1, 11);
            if (whichEndMark <= 7) { output += "."; }
            else if (whichEndMark <= 9) { output += "!"; }
            else { output += "?"; }

            output = char.ToUpper(output[0]) + output.Substring(1);
            return output;
        }
        public string genWord()
        {
            Random rng = new Random();
            int rand = rng.Next(loremIpsumWords.Length);

            return loremIpsumWords[rand];
        }
        public string genTitle(int length)
        {
            string output = "";
            string temp = "";
            output = genWord();
            string privWord = " ";
            Random rng = new Random();

            length = (length == 0) ? this.settings.Title.Length : length;

            for (int i = 0; i < length; i++)
            {
                int needsComma = rng.Next(10);
                temp = genWord();

                if ((temp == privWord) || (temp.Length == privWord.Length) || (temp[0] == privWord[0]))
                {
                    i--;
                    continue;
                }

                output += temp;
                privWord = temp;

                if (i != (length - 1))
                {
                    output += " ";
                }

            }

            output = char.ToUpper(output[0]) + output.Substring(1);
            return output;
        }

        public Control CreateSettingPanel()
        {
            return new PluginSettings(context);
        }
    }
}