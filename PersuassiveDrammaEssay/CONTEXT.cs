using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PersuassiveDrammaEssay
{
    public class CONTEXT
    {
        public string NAME;
        public string DESC;
        public string PATH;
        public byte[] DAT;
        public SENTENCE[] Sentence;
        public int LEN_AVG;
        public int NUM_SENTENCES;

        public CONTEXT(string PATH)
        {
            this.PATH = PATH;
            NAME = Path.GetFileNameWithoutExtension(PATH);
            getDATA();
            //parseDATA();
        }

        public void getDATA()
        {
            DAT = File.ReadAllBytes(PATH);
        }
        /// <summary>
        /// FIRE AFTER DAT ACRONYMNS ARE SET
        /// </summary>
        public void parseDATA()
        {
            DESC = GetDesc();
            GetSentences();
            GetLevel();
            GetLenAvg();
            SortSentencesByLevel();
        }
        private void SortSentencesByLevel()
        {
            int num_sorted = 0;
            SENTENCE[] tmp = new SENTENCE[NUM_SENTENCES];

            while (num_sorted < NUM_SENTENCES)
            {
                int index = 0;
                int current_lvl = 10;
                for (int i = 0; i < NUM_SENTENCES; i++)
                {
                    if ((current_lvl >= Sentence[i].LEVEL) && !Sentence[i].USED)
                    {
                        current_lvl = Sentence[i].LEVEL;
                        index = i;
                    }
                }
                tmp[num_sorted] = new SENTENCE(Sentence[index].PHRASE, Sentence[index].LEVEL);
                Sentence[index].USED = true;
                num_sorted++;
            }
            Console.WriteLine("\n");
            tmp.CopyTo(Sentence, 0);
            for (int i = 0; i < Sentence.Length; i++)
            {
                Console.WriteLine("SORTED? -> " + Sentence[i].PHRASE + " :: " + Sentence[i].LEVEL);
            }
            for (int i = 0; i < Sentence.Length; i++) Sentence[i].USED = false;
        }
        private void GetLevel()
        {
            int a = 0;
            for (int i = 0; i < DAT.Length;)
            {
                while (DAT[i] < 48 || DAT[i] > 57)
                {
                    i++;
                }
                Sentence[a++].LEVEL = int.Parse(Convert.ToChar(DAT[i++]) + "");
            }
        }
        private string GetDesc()
        {
            string ret = "";
            int i = 2;
            while (DAT[i] != 0x0D)
            {
                ret += Convert.ToChar(DAT[i]);
                i++;
            }
            return ret;
        }
        private void GetSentences()
        {
            int num_sentence = 0;
            int startPos = 0x0;
            bool startPosFound = false;
            for (int i = 0; i < DAT.Length; i++)
            {
                //48-57
                if (!startPosFound)
                {
                    if (DAT[i] == 0x0D && DAT[i + 1] == 0x0A)
                    {
                        startPos = i + 2;
                        startPosFound = true;
                    }
                }
                bool prevWasNum = false;
                if (DAT[i] >= 48 && DAT[i] <= 57 && !prevWasNum)
                {
                    num_sentence++;
                    prevWasNum = true;
                }
                else
                {
                    prevWasNum = false;
                }
            }
            NUM_SENTENCES = num_sentence;
            Sentence = new SENTENCE[NUM_SENTENCES];
            num_sentence = 0;
            for (int i = startPos; i < DAT.Length;)
            {
                string phrase = "";
                while (DAT[i] < 48 || DAT[i] > 57)
                {
                    if (phrase.Length == 0) //ENSURE CAPITAL
                    {
                        if (DAT[i] >= 97 && DAT[i] <= 122) DAT[i] -= 0x20;
                    }
                    phrase += Convert.ToChar(DAT[i]);
                    i++;
                }
                Sentence[num_sentence] = new SENTENCE(phrase.Substring(0, phrase.Length - 1));
                Console.WriteLine("phrase: " + phrase);
                num_sentence++;
                if (num_sentence == Sentence.Length)
                {
                    break;
                }
                while (!((DAT[i] >= 65 && DAT[i] <= 90) || (DAT[i] >= 97 && DAT[i] <= 122)))
                {
                    i++;
                }
            }
        }
        private void GetLenAvg()
        {
            int sum = 0;
            for (int i = 0; i < Sentence.Length; i++) sum += Sentence[i].PHRASE.Length;
            LEN_AVG = sum / NUM_SENTENCES;
        }
    }
}
