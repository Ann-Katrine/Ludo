using System;


namespace Ludo
{
    class Spillere
    {
        
        private int SpillereId;
        private string navn;
        private colors clr;

        // ny spiller
        public Spillere(int id, string spillernavn, colors farve)
        {
            this.SpillereId = id;
            this.navn = spillernavn;
            this.clr = farve;
        }
        
        //spillernes farve
        /*public string Farve(String farve)
        {
            switch(farve)
            {
                case Farve.Gul:
                    return "gul";
                case Farve.Grøn:
                    return "Grøn";
                case Farve.Blå:
                    return "Blå";
                case Farve.Rød:
                    return "Rød";
            }
         }*/  
    }
            
        
          
        
    
}
