using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Flow.Launcher.Plugin;

namespace Flow.Launcher.Plugin.LoremIpsumGenerator
{
    public class MyFlowPlugin : IPlugin
    {

        int amount = 1;

        public Func<ActionContext, bool> Action { get; set; }

        string[] loremIpsumWords = new string[]
        {
            "lorem",
            "ipsum",
            "dolor",
            "sit",
            "amet",
            "consectetur",
            "adipiscing",
            "elit",
            "sed",
            "do",
            "eiusmod",
            "tempor",
            "incididunt",
            "ut",
            "labore",
            "et",
            "dolore",
            "magna",
            "aliqua",
            "enim",
            "ad",
            "minim",
            "veniam",
            "quis",
            "nostrud",
            "exercitation",
            "ullamco",
            "laboris",
            "nisi",
            "aliquip",
            "ex",
            "ea",
            "commodo",
            "consequat",
            "duis",
            "aute",
            "irure",
            "reprehenderit",
            "voluptate",
            "velit",
            "esse",
            "cillum",
            "eu",
            "fugiat",
            "nulla",
            "pariatur",
            "excepteur",
            "sint",
            "occaecat",
            "cupidatat",
            "non",
            "proident",
            "sunt",
            "culpa",
            "qui",
            "officia",
            "deserunt",
            "mollit",
            "anim",
            "id",
            "est",
            "laborum"
        };

        public void Init(PluginInitContext context)
        {
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
                            SubTitle = "Generate " + newNew + " paragraphs",
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
                            SubTitle = "Generate a paragraph",
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
                            Title = "Words in a sentence: " + newNew,
                            SubTitle = "Generate a patragraph with " + newNew + " words",
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
                            SubTitle = "Generate a sentence",
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
                            SubTitle = "Generate " + newNew + " words",
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
                            SubTitle = "Generate a word",
                            IcoPath = "images/app.png",
                            Action = clipboardWord,
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
                    SubTitle = "Generate a sentence.",
                    IcoPath = "images/app.png",
                    Action = clipboardSentence,
                    Score = 5000
                });
                results.Add(new Result
                {
                    Title = "Paragraph",
                    SubTitle = "Generate a paragraph / paragraphs.",
                    IcoPath = "images/app.png",
                    Action = clipboardParagraph,
                    Score = 5000
                });
                results.Add(new Result
                {
                    Title = "Word",
                    SubTitle = "Generate a word / string of words.",
                    IcoPath = "images/app.png",
                    Action = clipboardWord,
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
                output += genSentence(0) + "\n";
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

        public string genSentence(int length)
        {
            string output = "";
            Random rng = new Random();
            if (length <= 1) 
            { 
                length = rng.Next(9, 18); 
            }

            for (int i = 0; i < length; i++)
            {
                int needsComma = rng.Next(10);
                output += genWord();

                if (i != (length - 1))
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
        public string genParagraph()
        {
            string output = "";
            Random rng = new Random();
            int howMany = rng.Next(3, 17);

            for (int i = 0; i < howMany; i++)
            {
                output += genSentence(0);
            }

            return output;
        }


    }
}