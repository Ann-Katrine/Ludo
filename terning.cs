using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    class Terning
    {
        private int side;
        private int terningens_vaerdi;
        private Random rnd = new Random();

        //laver en "kaster terning"
        public Terning()
        {
            this.terningens_vaerdi = this.rnd.Next(1, 7);
        }

        //metdoen, kaster terning
        public int kaste()
        {
            this.terningens_vaerdi = this.rnd.Next(1, 7);

            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine(" . ");
                System.Threading.Thread.Sleep(500);
            }

            return this.terningens_vaerdi;
        }

        //Hviser hvad værdien blev
        public int Getvaerdien()
        {
            return this.terningens_vaerdi;
        }
    }
}
