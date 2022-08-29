using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersuassiveDrammaEssay
{
    class Program
    {
        static void Main(string[] args)
        {
            //doOwn();
            Builder b = new Builder(args[0], Convert.ToInt32(args[1]));
        }
        static void doOwn()
        {
            Builder b = new Builder("Rena", 1000);
        }
    }
}
