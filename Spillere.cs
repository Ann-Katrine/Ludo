using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum colors {  blå, grøn, gul, rød, ingen}

    class Spillere
    {
        int SpillereId;
        string Navn;
        colors color;
        Spillebaerk[] brik;

        // Ny spiller
        public Spillere(int id, string spillernavn, Spillebaerk[] brik, colors color)
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
        public colors Colors
        {
            get => this.color;
        }

        //Beskrivelse på spilleren
        public string Getbeskrivelse()
        {
            return "#" + GetSpillereId() + " " + Colors + " " + "spiller: " + GetNavn;
        }

        public Spillebaerk[] getbrikker()
        {
            return this.brik;
        }

        public Spillebaerk getbrik(int brikz) => this.brik[brikz];

    }
}
