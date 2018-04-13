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
        Braedt braedt = new Braedt();
        int Terning_Vaerdi;

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

                Spillebaerk[] tkns = tildelebriker(i);

                spillere[i] = new Spillere((i + 1), navn, tkns, tkns[i].BrikColor());
            }
        }

        //Farverne til spillerne.
        private Spillebaerk[] tildelebriker(int farveindex)
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
                        Terning_Vaerdi = terning.Getvaerdien();
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
                    Terning_Vaerdi = terning.Getvaerdien();
                }
                hvis_muligheder(mintur.getbrikker());
                break;
            }
        }
    
        public void hvis_muligheder(Spillebaerk[] brik)
        {
            int valg = 0;
            Console.WriteLine("Her er dine brikker");
            foreach (Spillebaerk sb in brik)
            {
                string ord;
                ord = ("Brik #" + sb.getbrikid() + "; placeret:" + sb.getstate);
                Console.Write(ord);
                switch (sb.getstate)
                {
                    case terningstate.Hjemme:
                        if (terning.Getvaerdien() == 6)
                        {
                            Console.Write(" - Kan spilles");
                            Console.Write(" " + (sb.felter_tilbage));
                            valg++;
                        }
                        else
                        {
                            Console.Write(" - Kan ikke spilles");
                            Console.Write(" " + (sb.felter_tilbage));
                        }
                        break;
                    case terningstate.I_spil:
                        Console.Write(" - Kan spilles");
                        Console.Write(" " + (sb.felter_tilbage));
                        valg++;
                        break;
                    case terningstate.Sikker:
                        Console.Write(" - Kan spilles");
                        Console.Write(" " + (sb.felter_tilbage));
                        valg++;
                        break;
                    case terningstate.Faerdig:
                        Console.Write(" - Er i mål");
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

        public void Valg_brik(Spillebaerk[] spillebaerk)
        {
            Spillebaerk brik;
            int i = 0;
            //Sådan at du kan vælge brik
            bool Flytte = false;
            while (Flytte == false)
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
                brik = spillebaerk[i - 1];
                if (brik.felter_tilbage - Terning_Vaerdi < 0)
                {
                    brik.felter_tilbage = (brik.felter_tilbage - Terning_Vaerdi) * -1;
                }
                //Den gør at man kan finde ud af om brikken er på brætet
                if (brik.felt != null)
                {
                    //Så den går fra 51 til 0
                    if (brik.felt + Terning_Vaerdi > 51)
                    {
                        for (int j = 0; j < Terning_Vaerdi; j++)
                        {
                            // enden går du fra 51 til 0 eller gør sådan at du rykker en gang
                            if (brik.felt + 1 > 51)
                            {
                                brik.felt = 0;
                            }
                            else
                            {
                                brik.felt++;
                            }
                        }
                    }
                    //hvis ikke tæt på 51 går man bare vidrer
                    else
                    {
                        brik.felt = brik.felt + Terning_Vaerdi;
                    }
                }
                //enden kommer du til at stå i_spil eller så rykker du fordi du er i spil
                if (terning.Getvaerdien() == 6)
                {
                    if (spillebaerk[(i - 1)].getstate == terningstate.Hjemme)
                    {
                        Ryk_Spillebrik_Ud(brik);
                        Flytte = true;
                    }
                    else if (spillebaerk[(i - 1)].getstate == terningstate.I_spil)
                    {
                        Console.WriteLine("Du er så langt i spillet, plads " + ( brik.felter_tilbage = brik.felter_tilbage - Terning_Vaerdi));
                        Flytte = true;
                    }
                }
                //du rykker plads
                else if (spillebaerk[(i - 1)].getstate == terningstate.I_spil)
                {
                    Console.WriteLine("Du er så langt i spillet, plads " + (brik.felter_tilbage = brik.felter_tilbage - Terning_Vaerdi));
                    Flytte = true;
                }
                //hvis du endet kan
                else
                {
                    Console.WriteLine("Du kan ikke ryke med denne brik.");
                }

                // enden slår igang eller går vidre
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
        /*- Opgaver
            ~Skal være sådan at man kan slår spiller hjem.
            ~Skal være sådan at man kan stå på helle hvis to spiller står på samme sted.
            ~Skal være sådan at man ikke kan hoppe over nogle af dine brikker, man spiller med.
            ~Skal have lavet sådan at man ikke ryger ned på minus at man ryger op på plus igen hvis man ikke lander på 0.*/
        private void Ryk_Spillebrik_Ud(Spillebaerk brik)
        {
            brik.getstate = terningstate.I_spil;
            switch (brik.BrikColor())
            {
                case colors.gul:
                    brik.felt = 2;
                    break;
                case colors.blå:
                    brik.felt = 15;
                    break;
                case colors.rød:
                    brik.felt = 28;
                    break;
                case colors.grøn:
                    brik.felt = 41;
                    break;
            }
            skifter();
        }

        public void flyt_brikken_til(Spillebaerk spillebaerk)
        {
            //så brikken kan komme i midten
            if (spillebaerk.felt != null)
            {
                if (spillebaerk.felter_tilbage - terning.Getvaerdien() < 6)
                {
                    spillebaerk.felt = null;
                }
            }
        }
    }
}