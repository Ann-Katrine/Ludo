using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ludo
{
    public enum colors{rød, blå, gul, grøn}

    class Spillere
    {
        
        private int SpillereId;
        private string Navn;
        private colors clr;

        // ny spiller
        public Spillere(int id, string spillernavn, colors farve)
        {
            this.SpillereId = id;
            this.Navn = spillernavn;
            this.clr = farve;
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

        //spillerens farve
        public colors Farve()
        {
            return this.clr;
        } 


        
    }

}
