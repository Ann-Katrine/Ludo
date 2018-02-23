using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    class Terning
    {

        private int terningens_vaerdi;
        private Random rnd = new Random();
        private int j = 0;

        //Laver en "kaster terning"
        public Terning()
        {
            this.terningens_vaerdi = this.rnd.Next(1, 7);
        }

        //metdoen, kaster terning
        public int kaste()
        {
            Console.WriteLine(" ");
            this.terningens_vaerdi = this.rnd.Next(1, 7);

            return this.terningens_vaerdi;
        }

        //Viser hvad værdien blev
        public int Getvaerdien()
        {
            return this.terningens_vaerdi;
        }

        //Viser hvor langt du er noget
        public int tallet()
        {
            return this.terningens_vaerdi + j;
        }
    }
}
