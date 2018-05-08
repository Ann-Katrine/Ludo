using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum Colors {  blå, grøn, gul, rød, ingen}

    class Spiller
    {
        int SpillereId;
        string Navn;
        Colors color;
        Spillebrik[] brik;

        // Ny spiller
        public Spiller(int id, string spillernavn, Spillebrik[] brik, Colors color)
        {
            this.SpillereId = id;
            this.Navn = spillernavn;
            this.color = color;
            this.brik = brik;
        }

        //Spillerens navn
        public string GetNavn
        {
            get
            {
                return this.Navn;
            }
        }

        //Spillerens id
        public int GetSpillereId()
        {
            return this.SpillereId;
        }

        //Her få man sin farve
        public Colors Colors
        {
            get => this.color;
        }

        //Beskrivelse på spilleren
        public string Getbeskrivelse()
        {
            return "#" + GetSpillereId() + " " + Colors + " " + "spiller: " + GetNavn;
        }

        public Spillebrik[] Getbrikker()
        {
            return this.brik;
        }

        public Spillebrik Getbrik(int brikz) => this.brik[brikz];

    }
}
