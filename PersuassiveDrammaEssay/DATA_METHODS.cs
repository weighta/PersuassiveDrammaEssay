using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersuassiveDrammaEssay
{
    public class DATA_METHODS
    {
        public DATA_METHODS()
        {

        }
        public static void Replace(ref byte[] b, int pos, int len, string str)
        {
            byte[] ret = new byte[b.Length + (str.Length - len)];
            for (int i = 0; i < pos; i++) ret[i] = b[i];
            for (int i = 0; i < str.Length; i++) ret[pos + i] = (byte)str[i];
            for (int i = 0; i < b.Length - (pos + len); i++) ret[pos + str.Length + i] = b[pos + len + i];
            b = new byte[ret.Length];
            ret.CopyTo(b, 0);
        }

    }
}
