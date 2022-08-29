using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace PersuassiveDrammaEssay
{
    public class Builder
    {
        private const int numParagraphs = 5;
        private int numParagraphsCurr = 0;
        private string ESSAY = "";
        private string DEAR_WHOM;
        private int CHAR_COUNT;
        Bitmap wave;
        Res RES;
        Graph GRAPH;
        public Builder(string DEAR_WHOM, int CHAR_COUNT)
        {
            this.DEAR_WHOM = DEAR_WHOM;
            this.CHAR_COUNT = CHAR_COUNT;
            INST();
        }
        private void INST()
        {
            wave = new Bitmap(@"res\wave.png");
            RES = new Res(DEAR_WHOM);
            GRAPH = new Graph(wave, CHAR_COUNT, RES.AVG_CONTEXT_CHAR);
            BuildEssay();
            outputEssay("Essay for " + DEAR_WHOM);
        }
        private void BuildEssay()
        {
            intro();
            body();
            outro();
        }
        private void intro()
        {
            add("\t");
            Console.WriteLine("NODE COUNT: -> " + GRAPH.Node[0].type);
            add(getSentence(GRAPH.Node[0].level, getContextIndexByName("intro")));
            add("\n");
        }
        private void body()
        {
            for (int i = 0; i < GRAPH.Node.Length; i++) Console.WriteLine("NODE " + i + ": " + GRAPH.Node[i].level);
            for (int i = 0; i < GRAPH.NUM_SENTENCES; i++)
            {
                add(getSentence(GRAPH.Node[i].level, getContextIndexByName(GRAPH.Node[i].type)));
                add(". ");
                detParagraph(i);
            }
        }
        private void outro()
        {
            add(getSentence(GRAPH.Node[GRAPH.NUM_SENTENCES - 1].level, getContextIndexByName("outro")));
        }
        private void detParagraph(int i)
        {
            float perc = (float)i / GRAPH.NUM_SENTENCES;
            if (Math.Floor(perc * numParagraphs) > numParagraphsCurr)
            {
                numParagraphsCurr++;
                add("\n" + "\t");
            }
        }
        private int getContextIndexByName(string a)
        {
            for (int i = 0; i < RES.Context.Length; i++)
            {
                if (a == RES.Context[i].NAME)
                {
                    return i;
                }
            }
            throw new Exception("Context name not found");
        }
        private string getSentence(int level, int context_index)
        {
            int startpos = getStartingPoint(context_index, level);
            for (int i = 0; i < RES.Context[context_index].NUM_SENTENCES; i++)
            {
                int index = startpos - i;
                if (index < 0)
                {
                    index = startpos - (startpos - i);
                }
                if (!RES.Context[context_index].Sentence[index].USED)
                {
                    RES.Context[context_index].Sentence[index].USED = true;
                    return RES.Context[context_index].Sentence[index].PHRASE;
                }
            }
            throw new Exception("Could not find anymore unused sentences in " + RES.Context[context_index].NAME);
        }
        private int getStartingPoint(int context_index, int level)
        {
            int index = 0;
            int oldDiff = 10;
            for (int i = 0; i < RES.Context[context_index].NUM_SENTENCES; i++)
            {
                int newDiff = diff(RES.Context[context_index].Sentence[i].LEVEL, level);
                if (newDiff < oldDiff)
                {
                    index = i;
                    oldDiff = diff(RES.Context[context_index].Sentence[index].LEVEL, level);
                }
            }
            Console.WriteLine("LEVEL: " + level);
            Console.WriteLine("STARTING: " + RES.Context[context_index].NAME + " -> " + index);
            return index;
        }
        private int diff(int a, int b)
        {
            return Math.Abs(a - b);
        }
        private void add(string a)
        {
            ESSAY += a;
        }
        private void outputEssay(string name)
        {
            File.WriteAllText(name + ".txt", ESSAY);
        }
    }
}
