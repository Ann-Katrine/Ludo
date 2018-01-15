using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ludo
{
    public enum colors { rød, blå, gul, grøn }

    class Spillere
    {
        private int SpillereId;
        private string Navn;
        private colors color;
        private readonly Spillebaerk[] baerk;
        private Spillebaerk[] tkns;

        // ny spiller
        public Spillere(int id, string spillernavn, Spillebaerk[] tkns, colors color, Spillebaerk[] baerk)
        {
            this.SpillereId = id;
            this.Navn = spillernavn;
            this.color = color;
            this.tkns = tkns;
            this.baerk = baerk;
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
            return "#" + this.SpillereId + " " + this.Colors + " "+"spiller: " + this.GetNavn;
        }

        public Spillebaerk[] getbaerk()
        {
            return this.baerk;
        }

    }
}
