using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    class Terning
    {
        private int side;
        private int sidste_vaerdi;
        private Random rnd;

        public Terning(int nummer_af_sider)
        {
            this.side  = nummer_af_sider;
            this.rnd = new Random();
        }

        public int Throw()
        {
            int result;

            result = rnd.Next(1, (this.side + 1));
            this.sidste_vaerdi = result;
            return result;
        }

        public int Getsidste_vaerdi()
        {
            return this.sidste_vaerdi;
        }
    }
}
