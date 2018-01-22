using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum terningstate { Hjemme, I_spil, Sikker};

    class Spillebaerk
    {
        private int baerkid;
        private terningstate state;
        private colors color;

        public Spillebaerk(int id, colors colors)
        {
            this.baerkid = id;
            this.state = terningstate.Hjemme;
            this.color = colors;
        }

        public int getbaerkid()
        {
            return this.baerkid;
        }

        public terningstate getstate()
        {
            return this.state;
        }

        public colors BaerkColor()
        {
            return this.color;
        }
    }
}
