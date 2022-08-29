using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersuassiveDrammaEssay
{
    public class SENTENCE
    {
        public string PHRASE;
        public int LEVEL;
        public bool USED;

        public SENTENCE(string PHRASE)
        {
            this.PHRASE = PHRASE;
            USED = false;
        }
        public SENTENCE(string PHRASE, int LEVEL)
        {
            this.PHRASE = PHRASE;
            this.LEVEL = LEVEL;
            USED = false;
        }
    }
}
