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
                deltager = Convert.ToInt32(Console.ReadLine());

                if (deltager >= 2 && deltager <= 4)
                {

                }
                else 
                {
                    Console.WriteLine("Det er ikke den rigtig værdi, prøv igen.");
                    Console.WriteLine("");
                }
                
            } while (deltager < 2 || deltager >4);
        }

   
    }
}
