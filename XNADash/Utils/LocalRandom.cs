using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNADash.Utils
{
    public class LocalRandom
    {
        private static Random r = new Random();

        public static LocalRandom Instance= new LocalRandom();

        private LocalRandom() { }

        public static int Next( int min, int max )
        {
            return r.Next(min, max);
        }
    }
}
