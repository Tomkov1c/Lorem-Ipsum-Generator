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

namespace Flow.Launcher.Plugin.LoremIpsumGenerator
{
    public class MyFlowPlugin : IPlugin, ISettingProvider
    {

        int amount = 1;

        public Func<ActionContext, bool> Action { get; set; }

        string[] loremIpsumWords = new string[]
        {
            "laborum",
            "eveniet",
            "sunt",
            "iure",
            "nobis",
            "odio",
            "quasi",
            "aut",
            "vel",
            "odit",
            "tempore",
            "facilis",
            "neque",
            "nihil",
            "vitae",
            "vero",
            "ipsum",
            "nisi",
            "animi",
            "cumque",
            "velit",
            "modi",
            "natus",
            "iusto",
            "illo",
            "sed",
            "tempora",
            "ratione",
            "rem",
            "sint",
            "unde",
            "qui",
            "amet",
            "quo",
            "culpa",
            "libero",
            "ipsa",
            "dicta",
            "nesciunt",
            "autem",
            "minima",
            "ipsam",
            "ullam",
            "totam",
            "quis",
            "dolores",
            "harum",
            "quia",
            "ea",
            "quas",
            "quam",
            "quae",
            "hic",
            "ut",
            "ad",
            "at",
            "in",
            "id",
            "quos",
            "sit",
            "eos",
            "alias",
            "dolore",
            "lorem",
            "ipsum",
            "dolor",
            "sit",
            "amet",
            "elit",
            "sed",
            "ut",
            "et"
        };


        private PluginInitContext context;

        private Settings settings;

        public void Init(PluginInitContext context)
        {
            this.context = context;

            settings = context.API.LoadSettingJsonStorage<Settings>();
        }

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();


			string _new = query.Search.ToLower();
			string newNew = "";


            foreach (char c in _new)
            {
                if (char.IsDigit(c) || char.IsUpper(c))
                {
                    newNew += c;
                }
            }


            if (_new != "")
            {
                if (_new[0] == 'p')
                {
                    if (newNew != "")
                    {
                        results.Clear();
                        amount = Convert.ToInt32(newNew);

                        results.Add(new Result
                        {
                            Title = "Paragraphs: " + newNew,
                            SubTitle = "Generate a paragraph with " + newNew + " sentences.",
                            IcoPath = "images/app.png",
                            Action = clipboardParagraph,
                            Score = 5000
                        });
                    }
                    else
                    {
                        results.Clear();

                        results.Add(new Result
                        {
                            Title = "Paragraph",
                            SubTitle = "Generates a paragraph. Add a number to specify the amount of sentences in the paragraph.",
                            IcoPath = "images/app.png",
                            Action = clipboardParagraph,
                            Score = 5000
                        });
                    }
                }
                else if (_new[0] == 's')
                {
                    results.Clear();

                    if (newNew != "")
                    {
                        results.Clear();
                        amount = Convert.ToInt32(newNew);

                        results.Add(new Result
                        {
                            Title = "Sentences with: " + newNew + " words.",
                            SubTitle = "Generate a paragraph with " + newNew + " sentences.",
                            IcoPath = "images/app.png",
                            Action = clipboardSentence,
                            Score = 5000
                        });
                    }
                    else
                    {
                        results.Clear();

                        results.Add(new Result
                        {
                            Title = "Sentence",
                            SubTitle = "Generates a sentence. Add a number to specify the amount of words in the sentence.",
                            IcoPath = "images/app.png",
                            Action = clipboardSentence,
                            Score = 5000
                        });
                    }
                }
                else if (_new[0] == 'w')
                {
                    if (newNew != "")
                    {
                        results.Clear();
                        amount = Convert.ToInt32(newNew);

                        results.Add(new Result
                        {
                            Title = "Words: " + newNew,
                            SubTitle = "Generate a string of " + newNew + " words.",
                            IcoPath = "images/app.png",
                            Action = clipboardWord,
                            Score = 5000
                        });
                    }
                    else
                    {
                        results.Clear();

                        results.Add(new Result
                        {
                            Title = "Word",
                            SubTitle = "Generates a word. Add a number to specify the amount of words.",
                            IcoPath = "images/app.png",
                            Action = clipboardWord,
                            Score = 5000
                        });
                    }
                }
                else if (_new[0] == 't')
                {
                    if (newNew != "")
                    {
                        results.Clear();
                        amount = Convert.ToInt32(newNew);

                        results.Add(new Result
                        {
                            Title = "Title with: " + newNew + " words.",
                            SubTitle = "Generate a title with: " + newNew + "words.",
                            IcoPath = "images/app.png",
                            Action = clipboardTitle,
                            Score = 5000
                        });
                    }
                    else
                    {
                        results.Clear();

                        results.Add(new Result
                        {
                            Title = "Title",
                            SubTitle = "Generates a title (Like a newspaper headline). Add a number to specify the amount of words in the the title.",
                            IcoPath = "images/app.png",
                            Action = clipboardTitle,
                            Score = 5000
                        });
                    }
                }
                else
                {
                     results.Add(new Result
                    {
                        Title = "Incorrect query",
                        SubTitle = "Please follow the suggestions.",
                        IcoPath = "images/x.png",
                        Score = 5000
                    });
                }
            }
            else
            {
                results.Clear();

                results.Add(new Result
                {
                    Title = "Sentence",
                    SubTitle = "Generates a sentence. Add a number to specify the amount of words in the sentence.",
                    IcoPath = "images/app.png",
                    Action = clipboardSentence,
                    Score = 5000
                });
                results.Add(new Result
                {
                    Title = "Paragraph",
                    SubTitle = "Generates a paragraph. Add a number to specify the amount of sentences in the paragraph.",
                    IcoPath = "images/app.png",
                    Action = clipboardParagraph,
                    Score = 5000
                });
                results.Add(new Result
                {
                    Title = "Word",
                    SubTitle = "Generates a word. Add a number to specify the amount of words.",
                    IcoPath = "images/app.png",
                    Action = clipboardWord,
                    Score = 5000
                });
                results.Add(new Result
                {
                    Title = "Title",
                    SubTitle = "Generates a title (Like a newspaper headline). Add a number to specify the amount of words in the the title.",
                    IcoPath = "images/app.png",
                    Action = clipboardTitle,
                    Score = 5000
                });
            }
            return results;
        }
        public bool clipboardSentence(ActionContext context)
        {
            string output = genSentence(amount);
            Clipboard.SetText(output);
            amount = 1;
            return true;
        }
        public bool clipboardParagraph(ActionContext context)
        {
            string output = "";
            if (amount == 1)
            {
                Random rng = new Random();
                amount = rng.Next(3, 10);
            }
            for (int i = 0; i < amount; i++)
            {
                output += genSentence(0) + " ";
            }
            output += "\n";
            Clipboard.SetText(output);
            amount = 1;
            return true;
        }
        public bool clipboardWord(ActionContext context)
        {
            string output = "";
            for (int i = 0; i < amount; i++)
            {
                output += genWord() + " ";
            }
            Clipboard.SetText(output);
            amount = 1;
            return true;
        }
        public bool clipboardTitle(ActionContext context)
        {
            string output = genTitle(amount);
            Clipboard.SetText(output);
            amount = 1;
            return true;
        }

        public string genSentence(int length)
        {
            string output = "";
            string temp = "";
            output = genWord();
            string privWord = " ";
            Random rng = new Random();
            if (length <= 1) 
            { 
                length = rng.Next(9, 18); 
            }

            for (int i = 0; i < (length - 1); i++)
            {
                int needsComma = rng.Next(10);
                temp = genWord();

                if ((temp == privWord) || (temp.Length == privWord.Length) || (temp[0] == privWord[0]))
                {
                    i--;
                    continue;
                }else
                {
                    output += temp;
                    privWord = temp;
                }


                if (i != (length - 2))
                {
                    if (needsComma == 7)
                    {
                        output += ",";
                    }
                    output += " ";
                }
                
            }
            int whichEndMark = rng.Next(7);
            if (whichEndMark == 5)
            {
                output += "!";
            }
            else if (whichEndMark == 7)
            {
                output += "?";
            }
            else
            {
                output += ".";
            }
            output = char.ToUpper(output[0]) + output.Substring(1);
            return output;
        }
        public string genWord()
        {
            string output = "";
            Random rng = new Random();
            int rand = rng.Next(loremIpsumWords.Length);
            output = loremIpsumWords[rand];

            return output;
        }
        public string genTitle(int length)
        {
            string output = "";
            string temp = "";
            output = genWord();
            string privWord = " ";
            Random rng = new Random();
            if (length <= 1)
            {
                length = rng.Next(9, 18);
            }

            for (int i = 0; i < (length - 1); i++)
            {
                temp = genWord();

                if ((temp == privWord) || (temp.Length == privWord.Length) || (temp[0] == privWord[0]))
                {
                    i--;
                    continue;
                }
                else
                {
                    output += temp;
                    privWord = temp;
                }


                if (i != (length - 2))
                {
                    output += " ";
                }

            }
            output = char.ToUpper(output[0]) + output.Substring(1);
            return output;
        }

        public Control CreateSettingPanel()
        {
            return new PluginSettings();
        }
    }
}