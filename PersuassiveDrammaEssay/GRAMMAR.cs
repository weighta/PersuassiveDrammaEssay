using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PersuassiveDrammaEssay
{
    public class GRAMMAR
    {
        public string NAME;
        public string PATH;
        public string DESC;
        public string[] LINE;
        public int NUM_PHRASES;
        public GRAMMAR_PHRASE[] GrammarPhrase;

        public GRAMMAR(string PATH)
        {
            this.PATH = PATH;
            NAME = Path.GetFileNameWithoutExtension(PATH);
            INST();
        }
        private void INST()
        {
            getDATA();
            parseDATA();
        }
        private void getDATA()
        {
            LINE = File.ReadAllLines(PATH);
        }
        public void parseDATA()
        {
            GetPhrases();
        }
        private void GetPhrases()
        {
            GrammarPhrase = new GRAMMAR_PHRASE[LINE.Length - 1];
            DESC = LINE[0].Substring(2, LINE[0].Length - 2);
            Console.WriteLine("LINE: -> " + DESC);
            for (int i = 0; i < LINE.Length - 1; i++)
            {
                GrammarPhrase[i] = new GRAMMAR_PHRASE(LINE[i + 1]);
            }
        }
    }
}
