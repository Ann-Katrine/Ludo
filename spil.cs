using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum spilregler { I_spil, faerdig };

    class Spil
    {
        private terningstate state;
        private int deltager;
        private Spillere[] spillere;
        private int spilleren_tur = 1;
        private Terning terning = new Terning();

        //Hvad der  hvises på skræmen
        public Spil()
        {
            Console.WriteLine("Velkommen til Ludo");
            setdeltager();
            lavspiller();
            hvis_spiller();
            this.state = terningstate.I_spil;
            skifter();

            Console.ReadKey();
        }   

        //Hvor mange deltager der er i spil.
        private void setdeltager()
        {
            do
            {
                Console.WriteLine("Hvor mange deltager 2-4?: ");
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
        
        //laver en ny spiller, virker ikke
        private void lavspiller()
        {
            Console.WriteLine("Skriv dit spillernavn?: ");
            this.spillere = new Spillere[this.deltager];

            for(int i = 0; i < deltager; i++)
            {
                Console.WriteLine("Hvad hedder spiller#" + (i+1) + ": ");
                string navn = Console.ReadLine();

                Spillebaerk[] tkns = tildelebraeker(i);

                spillere[i] = new Spillere((i + 1), navn, tkns, tkns[i].getclr());
            }
        }

        //farver til spillerne
        private Spillebaerk[] tildelebraeker(int farveindex)
        {
            Spillebaerk[] Spillebaerker = new Spillebaerk[4];

            for (int i =0; i <4; i++)
            {
                switch (farveindex)
                {
                    case 0:
                        Spillebaerker[i] = new Spillebaerk((i + 1), colors.blå);
                        break;
                    case 1:
                        Spillebaerker[i] = new Spillebaerk((i + 1), colors.grøn);
                        break;
                    case 2:
                        Spillebaerker[i] = new Spillebaerk((i + 1), colors.gul);
                        break;
                    case 3:
                        Spillebaerker[i] = new Spillebaerk((i + 1), colors.rød);
                        break;
                }
            }
            return Spillebaerker;
        }
        
        //Hviser spiller
        private void hvis_spiller()
        {
            Console.WriteLine("Okay, her er dine spillere");
            foreach (Spillere pl in spillere)
            {
                Console.WriteLine(pl.Getbeskrivelse());
            }
        }

        //Hver spiller skrifter
        public void skifter()
        {
            while (this.state == terningstate.I_spil)
            {
                Spillere mintur = spillere[(spilleren_tur-1)];
                Console.WriteLine(mintur.GetNavn + "'s tur");
                Console.WriteLine("Det er " + mintur.Getbeskrivelse() + " tur");
                do
                {
                    Console.WriteLine("Klar til at (k)aste? ");
                } while (Console.ReadKey().KeyChar != 'k');
                Console.WriteLine("du slog: " + terning.kaste().ToString());
                Console.WriteLine(" ");
                Hvis_kaste_muligheder(mintur.getbaerk());
                break;
            }
        }

        //ikke færdig
        public void Hvis_kaste_muligheder(Spillebaerk[] baerk)
        {
            //int vælg = 0;

            //Console.WriteLine("Her er dine brikker");
            //foreach (Spillebaerk tk in baerk)
            //{
            //    Console.WriteLine("Brik #" + tk.getbaerkid() + "; placeret:" + tk.getstate());

            //    switch (tk.getstate())
            //    {
            //        case terningstate.Hjemme:
            //            if (Terning.getvaerdig() == 6)
            //            {

            //            }
            //    }
            //}

        }

    }
}
