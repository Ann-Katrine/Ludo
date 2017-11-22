using System;


namespace Ludo
{
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
            get{
                return this.Navn;
            }
        }

        public int GetSpillereId()
        {
            return this.SpillereId;
        }


    }
            
        
          
        
    
}
