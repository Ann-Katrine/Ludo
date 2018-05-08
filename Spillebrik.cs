using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum Terningstate { Hjemme, I_spil, Sikker, Faerdig};

    class Spillebrik
    {
        int brikid;
        Terningstate state;
        Colors color; 
        internal int? Felt { get; set; } = null; //til brikken
        internal int Felter_tilbage { get; set; } = 56; // til spilleren

        public Spillebrik(int id, Colors colors)
        {
            this.brikid = id;
            this.state = Terningstate.Hjemme;
            this.color = colors;
        }

        //Her får brikken sit id
        public int Getbrikid()
        {
            return this.brikid;
        }

        //Her finder man ud af hvor man så på pladen
        public Terningstate Getstate
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
        public Colors BrikColor()
        {
            return this.color;
        }
    }
}