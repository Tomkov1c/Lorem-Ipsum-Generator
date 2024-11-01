using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Launcher.Plugin.LoremIpsumGenerator.Classes
{
    public class Settings
    {
        public Sentence Sentence { get; set; }
        public Paragraph Paragraph { get; set; }
        public Word Word { get; set; }
        public Title Title { get; set; }
        public Settings()
        {
            Sentence = new Sentence();
            Paragraph = new Paragraph();
            Word = new Word();
            Title = new Title();
        }
    }

    public class Sentence
    {
        public int Length { get; set; }

        public int defaultLenghth() { return 6; }
    }

    public class Paragraph
    {
        public int Length { get; set; }

        public int defaultLenghth() { return 4; }
    }

    public class Word
    {
        public int Length { get; set; }

        public int defaultLenghth() { return 1; }
    }

    public class Title
    {
        public int Length { get; set; }

        public int defaultLenghth() { return 8; }
    }
}
