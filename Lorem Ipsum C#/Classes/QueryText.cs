using Flow.Launcher.Plugin.Lorem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Launcher.Plugin.LoremIpsumGenerator.Classes
{
    public class QueryText
    {
        public Type Sentence { get; set; }
        public Type Paragraph { get; set; }
        public Type Word { get; set; }
        public Type Title { get; set; }
        public QueryText()
        {
            Sentence = new Type
            {
                Title = "Sentence",
                Subtitle = "Generate a sentence. Add a number to specify the amount of words in it.",
                Icon = "Images/Icon8/Windows/icons8-comma-512.png",
                Custom = new Custom
                {
                    Title = new Title
                    {
                        Part1 = "Sentence with",
                        Part2 = "words",
                    },
                    Subtitle = new Subtitle
                    {
                        Part1 = "Generate a sentence with",
                        Part2 = "words.",
                    }
                }
            };
            Paragraph = new Type {
                Title = "Paragraph",
                Subtitle = "Generate a paragraph. Add a number to specify the amount of sentences in it.",
                Icon = "Images/Icon8/Windows/icons8-paragraph-512.png",
                Custom = new Custom
                {
                    Title = new Title
                    {
                        Part1 = "Paragraph with",
                        Part2 = "sentences",
                    },
                    Subtitle = new Subtitle
                    {
                        Part1 = "Generate a paragraph with",
                        Part2 = "sentences.",
                    }
                }
            };
            Word = new Type {
                Title = "Word",
                Subtitle = "Generate a word. Add a number to specify the amount of words in a string.",
                Icon = "Images/Icon8/Windows/icons8-font-512.png",
                Custom = new Custom
                {
                    Title = new Title
                    {
                        Part1 = "A string of",
                        Part2 = "words",
                    },
                    Subtitle = new Subtitle
                    {
                        Part1 = "Generate a string of",
                        Part2 = "words.",
                    }
                }
            };
            Title = new Type {
                Title = "Title",
                Subtitle = "Generate a title (like a newspaper headline). Add a number to specify the amount of words in it.",
                Icon = "Images/Icon8/Windows/icons8-type-512.png",
                Custom = new Custom
                {
                    Title = new Title
                    {
                        Part1 = "Title with",
                        Part2 = "words",
                    },
                    Subtitle = new Subtitle
                    {
                        Part1 = "Generate a title with",
                        Part2 = "words.",
                    }
                }
            };
        }
    }

    public class Title 
    {
        public string Part1 { get; set; }
        public string Part2 { get; set; }

        public string combine(int amount)
        {
            string final = this.Part1 + " " + amount + " " + this.Part2;
            return final;
        }
    }

    public class Subtitle
    {
        public string Part1 { get; set; }
        public string Part2 { get; set; }

        public string combine(int amount)
        {
            string final = this.Part1 + " " + amount + " " + this.Part2;
            return final;
        }
    }

    public class Custom
    {
        public Title Title { get; set; }
        public Subtitle Subtitle { get; set; }

    }
    public class Type
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Icon { get; set; }

        public Custom Custom { get; set; }
    }
}
