using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum spilregler { I_spil, slut };

    class Spil
    {
        private terningstate state;
        private int deltager;
        private Spillere[] spillere;
        private int spilleren_tur = 1;
        private Terning terning = new Terning();
        public colors Colors;
        private Spillebaerk[] baerk;

        //Hvad der vises på skræmen
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
        
        //laver en ny spiller.
        private void lavspiller()
        {
            Console.WriteLine("Skriv dit spillernavn?: ");
            this.spillere = new Spillere[this.deltager];

            for(int i = 0; i < this.deltager; i++)
            {
                Console.WriteLine("Hvad hedder spiller#" + (i+1) + ": ");
                string navn = Console.ReadLine();

                Spillebaerk[] tkns = tildelebraeker(i);

                spillere[i] = new Spillere((i + 1), navn, tkns, tkns[i].BaerkColor());
            }
        }

        //farver til spillerne.
        private Spillebaerk[] tildelebraeker(int farveindex)
        {
            Spillebaerk[] Spillebaerker = new Spillebaerk[4];

            for (int i =0; i <=3; i++)
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
                Console.WriteLine("Du slog: " + terning.kaste().ToString());
                Hvis_kaste_muligheder(mintur.getbaerk());
                break;
            }
        }

        public void Hvis_kaste_muligheder(Spillebaerk[] baerk)
        {
            int valg = 0;

            Console.WriteLine("Her er dine brikker");
            foreach (Spillebaerk sb in baerk)
            {
                Console.WriteLine("Brik #" + sb.getbaerkid() + "; placeret:" + sb.getstate());
                switch (sb.getstate())
                {
                    case terningstate.Hjemme:
                        if (terning.Getvaerdien() == 6)
                        {
                            Console.WriteLine(" - Kan spilles");
                            valg++;
                        }
                        else
                        {
                            Console.WriteLine(" - Kan ikke spilles");
                        }
                        break;
                    case terningstate.I_spil:
                        Console.WriteLine(" - Kan spilles");
                        valg++;
                        break;
                    case terningstate.Sikker:
                        Console.WriteLine(" - kan spilles");
                        valg++;
                        break;
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("Du har " + valg.ToString() + " muligheder i denne tur.");

            //Du har ikke noget valg skift til næste spiller.
            if (valg == 0)
            {
                this.skift_tur();
            }
            else
            {
                Console.WriteLine("Vælg den spiller du vil spille med.");
            }
        }

        private void skift_tur()
        {
            Console.WriteLine(" ");
            if(spilleren_tur == deltager)
            {
                spilleren_tur = 1;
            }
            else
            {
                spilleren_tur++;
            }

            Console.WriteLine("Du har ikke noget valg, skifter til næste spiller ");
            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine(" ");
            }
            skifter();
        }
    }
}