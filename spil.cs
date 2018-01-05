using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ludo
{
    class Spil
    {
        private int deltager;
        private Spillere[] spillere;


        public Spil()
        {
            Console.WriteLine("Velkommen til Ludo");
            setdeltager();

            Console.ReadKey();
        }   

        private void setdeltager()
        {
            do
            {
                Console.WriteLine("Hvor mange deltage 2-4?: ");
                try
                {
                    deltager = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Det er ikke den rigtige værdi, prøv igen.");
                }
            } while (deltager < 2 || deltager >4);
        }

        private void lavspiller()
        {
            Console.WriteLine("Skriv dit spillernavn?: ");
            this.spillere = new Spillere[this.deltager];


        }
   
    }
}
