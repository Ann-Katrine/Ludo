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
        private int v;
        private colors grøn;

        public Spillere(int v, colors grøn)
        {
            this.v = v;
            this.grøn = grøn;
        }

        // ny spiller
        public Spillere(int id, string spillernavn, Spillere[] tkns)
        {
            this.SpillereId = id;
            this.Navn = spillernavn;
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

    }

}
