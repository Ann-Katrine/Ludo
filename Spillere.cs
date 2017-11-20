using System;


namespace Ludo
{
    class Spillere
    {
        private int SpillereId;
        private string navn;
        private enum SpilleFarve {Gul, Grøn, Blå, rød};

        // ny spiller
        public Spillere(int id, string spillernavn)
            {
                this.SpillereId = id;
                this.navn = spillernavn;
            }
        
        //spillernes farve
        public static string SpilleFarve(Farve farve)
        {
            switch(Farve)
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
        }
            
        
          
        
    }
}
