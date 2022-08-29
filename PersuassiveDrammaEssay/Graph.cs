using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PersuassiveDrammaEssay
{
    public class Graph
    {
        public int AVG_CONTEXT_CHAR;
        public int ESSAY_CHAR_COUNT;
        public int NUM_SENTENCES;
        public Bitmap Wave;
        public NODE[] Node;
        public Graph(Bitmap Wave, int ESSAY_CHAR_COUNT, int AVG_CONTEXT_CHAR)
        {
            this.ESSAY_CHAR_COUNT = ESSAY_CHAR_COUNT;
            this.AVG_CONTEXT_CHAR = AVG_CONTEXT_CHAR;
            this.Wave = Wave;
            INST();
        }
        private void INST()
        {
            NUM_SENTENCES = ESSAY_CHAR_COUNT / AVG_CONTEXT_CHAR;
            Node = new NODE[NUM_SENTENCES];
            Color c;
            float WAVE_INC = (float)(Wave.Width) / NUM_SENTENCES;
            for (int i = 0; i < NUM_SENTENCES; i++)
            {
                for (int j = 0; j < Wave.Height; j++)
                {
                    c = Wave.GetPixel((int)(i * WAVE_INC), j);
                    if (!isWhite(c))
                    {
                        Console.WriteLine("I: -> " + i);
                        if (isBlack(c))
                        {
                            Node[i] = new NODE(c, "serious", getLevel(j));
                        }
                        else
                        {
                            if (isRed(c))
                            {
                                Node[i] = new NODE(c, "hot", getLevel(j));
                            }
                            else if (isGreen(c))
                            {
                                Node[i] = new NODE(c, "neutral", getLevel(j));
                            }
                            else if (isBlue(c))
                            {
                                Node[i] = new NODE(c, "cold", getLevel(j));
                            }
                            else //BLEND?
                            {
                                throw new Exception("Color does not exist: " + c.ToString());
                            }
                        }
                        j = Wave.Height;
                    }
                }
            }
        }
        private int getLevel(int j)
        {
            return (int)(((float)(Wave.Height - j) / Wave.Height) * 10);
        }
        private bool isRed(Color c)
        {
            return c.R > c.B && c.R > c.G;
        }
        private bool isBlue(Color c)
        {
            return c.B > c.R && c.B >= c.G;
        }
        private bool isGreen(Color c)
        {
            return c.G > c.R && c.G > c.B;
        }
        private bool isWhite(Color c)
        {
            return (c.R & c.G & c.B) == 255;
        }
        private bool isBlack(Color c)
        {
            return (c.R == c.G) && (c.R == c.B);
        }
    }
}
