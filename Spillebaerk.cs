using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum terningstate { Hjemme, I_spil, Sikker, Faerdig};

    class Spillebaerk
    {
        int brikid;
        terningstate state;
        colors color;
        internal int? felt { get; set; } = null; //til brikken
        internal int felter_tilbage { get; set; } = 56; // til spilleren

        public Spillebaerk(int id, colors colors)
        {
            this.brikid = id;
            this.state = terningstate.Hjemme;
            this.color = colors;
        }

        //Her får brikken sit id
        public int getbrikid()
        {
            return this.brikid;
        }

        //Her finder man ud af hvor man så på pladen
        public terningstate getstate
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        //Brikken får tildelt sin farve
        public colors BrikColor()
        {
            return this.color;
        }
    }
}