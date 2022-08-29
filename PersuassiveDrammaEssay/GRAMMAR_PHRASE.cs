using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersuassiveDrammaEssay
{
    public class GRAMMAR_PHRASE
    {
        public string PHRASE;
        public bool USED;

        public GRAMMAR_PHRASE(string PHRASE)
        {
            this.PHRASE = PHRASE;
            USED = false;
        }
    }
}
