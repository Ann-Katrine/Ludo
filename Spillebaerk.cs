using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    class Spillebaerk
    {
        private int baerkid;
        private colors clr;

        public Spillebaerk(int id, colors colors)
        {
            this.baerkid = id;
            this.clr = colors;
        }

        public colors getclr()
        {
            return this.clr;
        }
    }
}
