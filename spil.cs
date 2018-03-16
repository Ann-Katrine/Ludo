using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public enum Spilregler { I_spil, slut };

    class Spil
    {
        terningstate state;
        int deltager;
        Spillere[] spillere;
        int spilleren_tur = 0;
        Terning terning = new Terning();
        colors Colors;
        bool slår_3_gange = false;
        int i = 0;

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

        //Laver en ny spiller.
        private void lavspiller()
        {
            Console.WriteLine("Skriv dit spillernavn?: ");
            this.spillere = new Spillere[this.deltager];
            for (int i = 0; i < this.deltager; i++)
            {
                Console.WriteLine("Hvad hedder spiller#" + (i + 1) + ": ");
                string navn = Console.ReadLine();

                Spillebaerk[] tkns = tildelebraeker(i);

                spillere[i] = new Spillere((i + 1), navn, tkns, tkns[i].BrikColor());
            }
        }

        //Farverne til spillerne.
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

        //Hviser spillerne
        private void hvis_spiller()
        {
            Console.WriteLine("Okay, her er dine spillere");
            foreach (Spillere pl in spillere)
            {
                Console.WriteLine(pl.Getbeskrivelse());
            }
        }
        
        //Hver spiller slår
        private void skifter()
        {
            int i = 0;
            int j = 0;
            while (this.state == terningstate.I_spil)
            { 
                Spillere mintur = spillere[(spilleren_tur)];
                Console.WriteLine(" ");
                Console.WriteLine(mintur.GetNavn + "'s tur");
                Console.WriteLine("Det er " + mintur.Getbeskrivelse() + " tur");
                foreach (Spillebaerk sb in mintur.getbrikker())
                {
                    if (sb.getstate == terningstate.Hjemme || sb.getstate == terningstate.Faerdig)
                    {
                        j++;
                    }
                }
                bool slår_6 = false;
                if (j == 4)
                {
                    do
                    {
                        do
                        {
                            Console.WriteLine("Klar til at (k)aste? ");
                        } while (Console.ReadKey().KeyChar != 'k');

                        Console.WriteLine(" ");
                        Console.WriteLine("Du slog: " + terning.kaste().ToString());

                        if (terning.Getvaerdien() == 6)
                        {
                            slår_6 = true;
                        }

                        i++;
                    } while (slår_6 == false && i < 3);
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Klar til at (k)aste? ");
                    } while (Console.ReadKey().KeyChar != 'k');

                    Console.WriteLine(" ");
                    Console.WriteLine("Du slog: " + terning.kaste().ToString());
                }
                hvis_muligheder(mintur.getbrikker());
                break;
            }
        }
        
        //igang her
        public void hvis_muligheder(Spillebaerk[] brik)
        {
            /*- Opgaver
            ~Skal være sådan at man kan se hvor langt man er i spillet.*/
            int valg = 0;
            Console.WriteLine("Her er dine brikker");
            
            foreach (Spillebaerk sb in brik)
            {
                Console.WriteLine("Brik #" + sb.getbrikid() + "; placeret:" + sb.getstate + " " );
                switch (sb.getstate)
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
                    case terningstate.Faerdig:
                        if(terning.tallet() >= 57)
                        {
                            Console.WriteLine(" - Er i mål");
                        }
                        break;
                }
                Console.WriteLine(" ");
            }
             Console.WriteLine("Du har " + valg + " muligheder i denne tur.");

            //Enden skrifter du tur eller du skal bestemme hvilken brik du vil vælge.
            if (valg == 0)
            {
                this.skift_tur();
            }
            else 
            {
                Valg_brik(brik);
            }
            Console.WriteLine("");
        }

        public void skift_tur()
        {
            //Gør sådan at man skrifter spiller
            Console.WriteLine(" ");
            if (spilleren_tur >= deltager - 1)
            {
                spilleren_tur = 0;
                foreach (Spillebaerk sb in spillere[spilleren_tur].getbrikker())
                {
                    if (sb.getstate == terningstate.Hjemme)
                    {
                        slår_3_gange = true;
                    }
                }
            }
            else
            {
                spilleren_tur++;
            }
            Console.WriteLine("Du har ikke noget valg, jeg skifter til næste spiller ");
            
            skifter();
        }

        //igang her
        public void Valg_brik(Spillebaerk[] spillebaerk)
        {
            /*- Opgaver
            ~Skal være sådan når du har valgt en brik, skal der komme til at stå hvad du slog, 
            så man ved hvor langt man er("næsten færdig").
            ~Skal være sådan at man ikke kan hoppe den første brik over, man spiller med.*/
            //Vælg brik
            bool Flytte = false;
            while(Flytte == false)
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
                if (terning.Getvaerdien() == 6)
                {
                    if (spillebaerk[(i - 1)].getstate == terningstate.Hjemme)
                    {
                        spillebaerk[(i - 1)].getstate = terningstate.I_spil;
                        Flytte = true;
                    }
                    else if(spillebaerk[(i - 1)].getstate == terningstate.I_spil)
                    {
                        Console.WriteLine("Du er så langt i spillet, plads " + terning.tallet());
                        Flytte = true;
                    }
                }
                else if(spillebaerk[(i - 1)].getstate == terningstate.I_spil)
                {
                    Console.WriteLine("Du er så langt i spillet, plads " + terning.tallet());
                    Flytte = true;
                }
                else
                {
                    Console.WriteLine("Du kan ikke ryke med denne brik.");
                }
            }

            //Enden skal du at slå igen eller du skifter spiller
            if (terning.Getvaerdien() == 6)
            {
                skifter();
            }
            else
            {
                //Gør sådan at man skrifter spiller
                if (spilleren_tur >= deltager - 1)
                {
                    spilleren_tur = 0;
                    foreach (Spillebaerk sb in spillere[spilleren_tur].getbrikker())
                    {
                        if (sb.getstate == terningstate.Hjemme)
                        {
                            slår_3_gange = true;
                        }
                    }
                }
                else
                {
                    spilleren_tur++;
                }

                skifter();
            }
        }

    }
}
//kpop
//83.054.654 - Who you 12:37 -> 13/3-18 ~ 83.147.070 12:38 -> 15/3-18
//102.137 -bigbang 12:38 -> 13/3-18 ~ 2.174.877 12:31 -> 15/3-18
//60.500.560 - heartbraker 12:48 -> 13/3-18 ~ 60.521.248 12:34 -> 15/3-18
//28.104.613 - g-dragon song 14:50 -> 15/3-18
//31.382.281 - heavn 14:38 -> 15/3-18