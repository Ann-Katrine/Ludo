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
        private colors farve ;

        public Spillebaerk(int id, colors colors)
        {
            this.baerkid = id;
            this.farve = colors;
            this.state = terningstate.Hjemme;
        }

        public int getbaerkid()
        {
            return this.baerkid;
        }

        public colors getFarve()
        {
            return this.farve;
        }

        public terningstate getstate()
        {
            return this.state;
        }
    }
}
