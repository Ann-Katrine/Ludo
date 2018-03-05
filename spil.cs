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
        private int spilleren_tur = 0;
        private Terning terning = new Terning();
        private colors Colors;
        //private Spillebaerk[] brik;
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

        //Farver til spillerne.
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
            int j = 0;
            bool slår_3_gange = false;
            while (this.state == terningstate.I_spil)
            { 
                Spillere mintur = spillere[(spilleren_tur)];
                Console.WriteLine(" ");
                Console.WriteLine(mintur.GetNavn + "'s tur");
                Console.WriteLine("Det er " + mintur.Getbeskrivelse() + " tur");
                foreach (Spillebaerk sb in mintur.getbrikker())
                {
                    if (sb.getstate() == terningstate.Hjemme)
                    {
                        slår_3_gange = true;
                    }
                }
                if(slår_3_gange == true)
                {

                }
                bool slår_6 = false; 
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

                    j++;
                } while (j >= 3 ^ slår_6 == false);
                hvis_muligheder(mintur.getbrikker());
                break;
            }
        }
        
        //igang her
        public void hvis_muligheder(Spillebaerk[] brik)
        {
            /*- Opgaver
            ~Skal være sådan når du har valgt en brik, skal der komme til at stå hvad du slog, så man ved hvor langt du er (H.M.H.T).*/
            int valg = 0;
            Console.WriteLine("Her er dine brikker");
            
            foreach (Spillebaerk sb in brik)
            {
                Console.WriteLine("Brik #" + sb.getbrikid() + "; placeret:" + sb.getstate() + " " );
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
            }
             Console.WriteLine("Du har " + valg + " muligheder i denne tur.");

            //Enden skrifter du tur eller du skal bestemme hvilken brik du vil vælge.
            if (valg == 0)
            {
                this.skift_tur();
            }
            else 
            {
                this.Valg_brik();
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
                bool slår_3_gange;
                foreach (Spillebaerk sb in spillere[spilleren_tur].getbrikker())
                {
                    if (sb.getstate() == terningstate.Hjemme)
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
        public void Valg_brik()
        {
            /*- opgaver
            ~Skal være sådan når du har valgt en brik, skal der komme til at stå hvad du slog, så man ved hvor langt du er.
            ~Skal være noget med når alle 4 brikker har været hele vejen rundt, skal spillet være slut, skal have fundet en
            komando til det.*/

            //Den gør sådan at du kan vælge en brik
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
            Spillebaerk tmpBrik = spillere[spilleren_tur].getbrik(i - 1);
            
            Console.WriteLine("Du er så langt i spillet, plads " + terning.tallet());

            //Gør sådan at man skrifter spiller
            if (spilleren_tur >= deltager - 1)
            {
                spilleren_tur = 0;
                bool slår_3_gange;
                foreach (Spillebaerk sb in spillere[spilleren_tur].getbrikker())
                {
                    if (sb.getstate() == terningstate.Hjemme)
                    {
                        slår_3_gange = true;
                    }
                }
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