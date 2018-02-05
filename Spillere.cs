using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum colors {  blå, grøn, gul, rød, }

    class Spillere
    {
        private int SpillereId;
        private string Navn;
        private colors color;
        private Spillebaerk[] braek;

        // ny spiller
        public Spillere(int id, string spillernavn, Spillebaerk[] braek, colors color)
        {
            this.SpillereId = id;
            this.Navn = spillernavn;
            this.color = color;
            this.braek = braek;
        }

        //spillerens navn
        public string GetNavn
        {
            get
            {
                return this.Navn;
            }
        }

        //spillerens id
        public int GetSpillereId()
        {
            return this.SpillereId;
        }

        public colors Colors
        {
            get => this.color;
        }

        public string Getbeskrivelse()
        {
            return "#" + GetSpillereId() + " " + Colors + " " + "spiller: " + GetNavn;
        }

        public Spillebaerk[] getbaerk()
        {
            return this.braek;
        }

    }
}
