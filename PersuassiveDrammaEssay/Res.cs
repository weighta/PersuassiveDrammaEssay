using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PersuassiveDrammaEssay
{

    public class Res
    {
        Random ran = new Random();

        private const string PATH_CONTEXT = @"res\context";
        private const string PATH_GRAMMAR = @"res\grammar";
        private string DEAREST;
        public CONTEXT[] Context;
        public GRAMMAR[] Grammar;
        public string[] CONTEXT_NAME;
        public string[] GRAMMAR_NAME;
        public int AVG_CONTEXT_CHAR;
        public Res(string DEAREST)
        {
            this.DEAREST = DEAREST;
            INST();
        }
        private void INST()
        {
            CONTEXT_NAME = Directory.GetFiles(PATH_CONTEXT);
            GRAMMAR_NAME = Directory.GetFiles(PATH_GRAMMAR);
            Context = new CONTEXT[CONTEXT_NAME.Length];
            Grammar = new GRAMMAR[GRAMMAR_NAME.Length];

            for (int i = 0; i < Context.Length; i++) Context[i] = new CONTEXT(CONTEXT_NAME[i]);
            for (int i = 0; i < Grammar.Length; i++) Grammar[i] = new GRAMMAR(GRAMMAR_NAME[i]);

            FillContextAcronymns();

            for (int i = 0; i < Context.Length; i++)
            {
                Context[i].parseDATA();
            }

            ReadStatistics();
        }
        private void FillContextAcronymns()
        {
            for (int i = 0; i < Context.Length; i++)
            {
                for (int j = 0; j < Context[i].DAT.Length; j++)
                {
                    if (Context[i].DAT[j] == 123)
                    {
                        int a = 1;
                        string acronymn = "";
                        while (Context[i].DAT[j + a] != 125)
                        {
                            acronymn += Convert.ToChar(Context[i].DAT[j + a]);
                            a++;
                        }
                        string replace = GetReplacementForAcronymn(acronymn);

                        int len = acronymn.Length + 2;
                        DATA_METHODS.Replace(ref Context[i].DAT, j, len, replace);
                        j += len;
                    }
                }
            }
        }
        private string GetReplacementForAcronymn(string acronymn)
        {
            if (acronymn == "name") return DEAREST;
            return FindUnusedGrammarTerm(GramIndexByName(acronymn));
        }
        private int GramIndexByName(string a)
        {
            for (int i = 0; i < Grammar.Length; i++)
            {
                if (a == Grammar[i].DESC)
                {
                    return i;
                }
            }
            throw new Exception("Grammar name " + a + " not found");
        }
        private string FindUnusedGrammarTerm(int grammar_index)
        {
            int b = Grammar[grammar_index].GrammarPhrase.Length;
            int ran_index = ran.Next(0, b);
            for (int i = 0; i < b; i++)
            {
                int a = (ran_index + i) % b;
                if (!Grammar[grammar_index].GrammarPhrase[a].USED)
                {
                    Grammar[grammar_index].GrammarPhrase[a].USED = true;
                    return Grammar[grammar_index].GrammarPhrase[a].PHRASE;
                }
            }
            throw new Exception("Ran out of grammar for " + Grammar[grammar_index].NAME);
        }
        private void ReadStatistics()
        {
            GetAvgContextChar();
        }
        private void GetAvgContextChar()
        {
            int sum = 0;
            for (int i = 0; i < Context.Length; i++)
            {
                sum += Context[i].LEN_AVG;
            }
            AVG_CONTEXT_CHAR = sum / Context.Length;
        }
    }
}
