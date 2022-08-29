using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PersuassiveDrammaEssay
{
    public class NODE
    {
        public Color c;
        public string type;
        public int level;
        public NODE(Color c, string type, int level)
        {
            this.c = c;
            this.type = type;
            this.level = level;
        }
    }
}
