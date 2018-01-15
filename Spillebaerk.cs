using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum terningstate { Hjemme, I_spil, Helle};

    class Spillebaerk
    {
        private int baerkid;
        private colors clr;
        private terningstate state;

        public Spillebaerk(int id, colors colors)
        {
            this.baerkid = id;
            this.clr = colors;
            this.state = terningstate.Hjemme;
        }

        public int getbaerkid()
        {
            return this.baerkid;
        }

        public colors getclr()
        {
            return this.clr;
        }

        public terningstate getstate()
        {
            return this.state;
        }
    }
}
