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
        private colors Colors;
        private Spillebaerk[] baerk;
        private int i = 0;
        
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
            } while (deltager < 2 || deltager > 4);
        }

        //laver en ny spiller.
        private void lavspiller()
        {
            Console.WriteLine("Skriv dit spillernavn?: ");
            this.spillere = new Spillere[this.deltager];
            for (int i = 0; i < this.deltager; i++)
            {
                Console.WriteLine("Hvad hedder spiller#" + (i + 1) + ": ");
                string navn = Console.ReadLine();

                Spillebaerk[] tkns = tildelebraeker(i);

                spillere[i] = new Spillere((i + 1), navn, tkns, tkns[i].BaerkColor());
            }
        }

        //farver til spillerne.
        private Spillebaerk[] tildelebraeker(int farveindex)
        {
            Spillebaerk[] Spillebaerker = new Spillebaerk[4];
            for (int i = 0; i <= 3; i++)
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
        private void skifter()
        {
            while (this.state == terningstate.I_spil)
            {
                Spillere mintur = spillere[(spilleren_tur - 1)];
                Console.WriteLine(" ");
                Console.WriteLine(mintur.GetNavn + "'s tur");
                Console.WriteLine("Det er " + mintur.Getbeskrivelse() + " tur");
                do
                {
                    Console.WriteLine("Klar til at (k)aste? ");
                } while (Console.ReadKey().KeyChar != 'k');
                Console.WriteLine(" ");
                Console.WriteLine("Du slog: " + terning.kaste().ToString());
                hvis_muligheder(mintur.getbaerk());
                break;
            }
        }
        
        public void hvis_muligheder(Spillebaerk[] baerk)
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
                        Console.WriteLine(" - Kan spilles");
                        valg++;
                        break;
                }
                Console.WriteLine(" ");
                Console.WriteLine("Du har " + valg + " muligheder i denne tur.");
            }

            //enden skrifter du tur eller du skal bestemme hvilken brik du vil vælge.
            if (valg == 0)
            {
                this.skift_tur();
            }
            else 
            {
                this.valg_baerk();
            }
            Console.WriteLine("");
        }

        public void skift_tur()
        {
            Console.WriteLine(" ");
            if (spilleren_tur == deltager)
            {
                spilleren_tur = 1;
            }
            else
            {
                spilleren_tur++;
            }
            Console.WriteLine("Du har ikke noget valg, jeg skifter til næste spiller ");
            
            skifter();
        }

        //igang her
        public void valg_baerk()
        {
            /*- opgaver
            ~Skal have noget til at sammerbejde med "do-whilen" til at jeg vælger en af de brikker man har at vælge imellem.
            ~Skal være noget med når alle 4 brikker har været hele vejen rundt skal spillet være slut, skal have fundet en
            komando til det.*/

            do
            {
                Console.WriteLine("Vælg den brik du vil spille med.");
                try
                {
                    i = Convert.ToInt16(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Der findes ikke nogen brik med den værdi, prøv igen.");
                }
            } while (i < 1 || i > 4);

            Console.WriteLine("hej");
            
            //Her skrifter til næste spiller
            if (spilleren_tur == deltager)
            {
                spilleren_tur = 1;
            }
            else
            {
                spilleren_tur++;
            }

            //Her har du enden vundet eller du går vider i spil
            if (terning.tallet() == 57)
            {
                Console.WriteLine("Du har vundet spillet");
            }
            else
            {
                skifter();
            } 
        }   
        
    }
}