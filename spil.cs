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
        Terningstate state;
        int deltager;
        Spiller[] spiller;
        int spilleren_tur = 0;
        Terning terning = new Terning();
        Colors Colors;
        bool slår_3_gange = false;
        Braedt braedt = new Braedt();
        int Terning_Vaerdi;
        bool Flytte = false;
        bool falsk_ryk = false;
        int? Gammel_felt { get; set; }

        //Hvad der vises på skræmen
        public Spil()
        {
            Console.WriteLine("Velkommen til Ludo");
            Setdeltager();
            Lavspiller();
            Hvis_spiller();
            this.state = Terningstate.I_spil;
            Skifter();

            Console.ReadKey();
        }

        //Hvor mange deltager der er i spil.
        private void Setdeltager()
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
        private void Lavspiller()
        {
            Console.WriteLine("Skriv dit spillernavn?: ");
            this.spiller = new Spiller[this.deltager];
            for (int i = 0; i < this.deltager; i++)
            {
                Console.WriteLine("Hvad hedder spiller#" + (i + 1) + ": ");
                string navn = Console.ReadLine();

                Spillebrik[] tkns = Tildelebriker(i);

                spiller[i] = new Spiller((i + 1), navn, tkns, tkns[i].BrikColor());
            }
        }

        //Farverne til spillerne.
        private Spillebrik[] Tildelebriker(int farveindex)
        {
            Spillebrik[] Spillebaerker = new Spillebrik[4];
            for (int i = 0; i <= 3; i++)
            {
                switch (farveindex)
                {
                    case 0:
                        Spillebaerker[i] = new Spillebrik((i + 1), Colors.gul);
                        break;
                    case 1:
                        Spillebaerker[i] = new Spillebrik((i + 1), Colors.blå);
                        break;
                    case 2:
                        Spillebaerker[i] = new Spillebrik((i + 1), Colors.rød);
                        break;
                    case 3:
                        Spillebaerker[i] = new Spillebrik((i + 1), Colors.grøn);
                        break;
                }
            }
            return Spillebaerker;
        }

        //Hviser spillerne
        private void Hvis_spiller()
        {
            Console.WriteLine("Okay, her er dine spillere");
            foreach (Spiller pl in spiller)
            {
                Console.WriteLine(pl.Getbeskrivelse());
            }
        }

        //Hver spiller slår
        private void Skifter()
        {
            int i = 0;
            int j = 0;
            while (this.state == Terningstate.I_spil)
            {
                Spiller mintur = spiller[(spilleren_tur)];
                Console.WriteLine(" ");
                Console.WriteLine(mintur.GetNavn + "'s tur");
                Console.WriteLine("Det er " + mintur.Getbeskrivelse() + " tur");
                foreach (Spillebrik sb in mintur.Getbrikker())
                {
                    if (sb.Getstate == Terningstate.Hjemme || sb.Getstate == Terningstate.Faerdig)
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
                        Console.WriteLine("Du slog: " + terning.Kaste().ToString());
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
                    Console.WriteLine("Du slog: " + terning.Kaste().ToString());
                    Terning_Vaerdi = terning.Getvaerdien();
                }
                Hvis_muligheder(mintur.Getbrikker());
                break;
            }
        }

        public void Hvis_muligheder(Spillebrik[] brik)
        {
            int valg = 0;
            Console.WriteLine("Her er dine brikker");
            foreach (Spillebrik sb in brik)
            {
                string ord;
                ord = ("Brik #" + sb.Getbrikid() + "; placeret:" + sb.Getstate);
                Console.Write(ord);
                switch (sb.Getstate)
                {
                    case Terningstate.Hjemme:
                        if (terning.Getvaerdien() == 6)
                        {
                            Console.Write(" - Kan spilles");
                            Console.Write(" " + (sb.Felter_tilbage));
                            valg++;
                        }
                        else
                        {
                            Console.Write(" - Kan ikke spilles");
                            Console.Write(" " + (sb.Felter_tilbage));
                        }
                        break;
                    case Terningstate.I_spil:
                        Console.Write(" - Kan spilles");
                        Console.Write(" " + (sb.Felter_tilbage) + " " + (sb.Felt) + "<- er felt");
                        valg++;
                        break;
                    case Terningstate.Sikker:
                        Console.Write(" - Kan spilles");
                        Console.Write(" " + (sb.Felter_tilbage) + " " + (sb.Felt) + "<- er felt");
                        valg++;
                        break;
                    case Terningstate.Faerdig:
                        Console.Write(" - Er i mål");
                        break;
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine("");
            Console.WriteLine("Du har " + valg + " muligheder i denne tur.");

            //Enden skrifter du tur eller du skal bestemme hvilken brik du vil vælge.
            if (valg == 0)
            {
                this.Skift_tur();
            }
            else
            {
                Valg_brik(brik);
            }
            Console.WriteLine("");
        }

        public void Skift_tur()
        {
            //Gør sådan at man skrifter spiller
            Console.WriteLine(" ");
            if (spilleren_tur >= deltager - 1)
            {
                spilleren_tur = 0;
                foreach (Spillebrik sb in spiller[spilleren_tur].Getbrikker())
                {
                    if (sb.Getstate == Terningstate.Hjemme)
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

            Skifter();
        }

        public void Valg_brik(Spillebrik[] spillebrik)
        {
            Spillebrik brik;
            int i = 0;
            int slut = 0;

            //Sådan at du kan vælge brik
            Flytte = false;
            while (Flytte == false)
            {
                Console.WriteLine("Vælg den brik du vil spille med.");
                do
                {
                    try
                    {
                        i = Convert.ToInt16(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Der findes ikke nogen brik med den værdi, prøv igen.");
                    }
                } while (i < 1 || i > 4);
                brik = spillebrik[i - 1];
                Gammel_felt = brik.Felt;
                if (Terning_Vaerdi == 6)
                {
                    if (brik.Getstate == Terningstate.Hjemme)
                    {
                        Ryk_Spillebrik_Ud(brik);
                    }
                    else if (brik.Getstate == Terningstate.I_spil || brik.Getstate == Terningstate.Sikker)
                    {
                        Ryk(brik);
                    }
                }
                else if (brik.Getstate == Terningstate.I_spil || brik.Getstate == Terningstate.Sikker)
                {
                    Ryk(brik);
                }
                else
                {
                    Console.WriteLine("Du kan ikke ryke med denne brik.");
                }
            }
            foreach (Spillebrik sb in spillebrik)
            {
                if (sb.Getstate == Terningstate.Faerdig)
                {
                    slut++;
                }
            }

            // Enden slå igang, går videre eller har vundet
            if (slut == 4)
            {
                Tekst();
            }
            else if (Terning_Vaerdi == 6)
            {
                Skifter();
            }
            else
            {
                //Gør sådan at man skrifter spiller
                if (spilleren_tur >= deltager - 1)
                {
                    spilleren_tur = 0;
                    foreach (Spillebrik sb in spiller[spilleren_tur].Getbrikker())
                    {
                        if (sb.Getstate == Terningstate.Hjemme)
                        {
                            slår_3_gange = true;
                        }
                    }
                }
                else
                {
                    spilleren_tur++;
                }

                Skifter();
            }
        }

        private void Ryk_Spillebrik_Ud(Spillebrik brik)
        {
            brik.Getstate = Terningstate.I_spil;
            switch (brik.BrikColor())
            {
                case Colors.gul:
                    brik.Felt = 2;
                    break;
                case Colors.blå:
                    brik.Felt = 15;
                    break;
                case Colors.rød:
                    brik.Felt = 28;
                    break;
                case Colors.grøn:
                    brik.Felt = 41;
                    break;
            }
            Tilfojer(brik);
            Flytte = true;
        }

        private void Tilfojer(Spillebrik brik)
        {
            var braedtdims = braedt.BraedtFeltter[brik.Felt.Value];
            //Brædet få en farve
            if (braedtdims.OptagetFarve == Colors.ingen)
            {
                braedtdims.OptagetFarve = brik.BrikColor();
                braedtdims.Optagetbrik.Add(brik);
            }
            else if (braedtdims.OptagetFarve == brik.BrikColor())
            {
                braedtdims.Optagetbrik.Add(brik);
                braedtdims.Optagetbrik[0].Getstate = Terningstate.Sikker;
                brik.Getstate = Terningstate.Sikker;
            }
            //Du bliver slået hjem på grund af sikker
            else if (braedtdims.Optagetbrik[0].Getstate == Terningstate.Sikker)
            {
                brik.Felt = null;
                brik.Getstate = Terningstate.Hjemme;
                brik.Felter_tilbage = 56;
                Console.WriteLine("Du begik selvmord!!!");
                Console.WriteLine("Nej, jeg laver sjov du begik ikke selvmord, du døde bare og røg hjem.");
            }
            //Du slår nogen hjem
            else
            {
                braedtdims.Optagetbrik[0].Getstate = Terningstate.Hjemme;
                braedtdims.Optagetbrik[0].Felter_tilbage = 56;
                braedtdims.Optagetbrik[0].Felt = null;
                Console.WriteLine("Du slog modspilleren hjem");
                braedtdims.Optagetbrik.RemoveAt(0);
                braedtdims.Optagetbrik.Add(brik);
                braedtdims.OptagetFarve = brik.BrikColor();
            }
            Flytte = true;
        }

        private void Tager_væk(Spillebrik brik)
        {
            var braedtdims = braedt.BraedtFeltter[Gammel_felt.Value];
            if (brik.Getstate == Terningstate.Sikker)
            {
                if (braedtdims.Optagetbrik.Count > 2)
                {
                    braedtdims.Optagetbrik.Remove(brik);
                    brik.Getstate = Terningstate.I_spil;
                }
                else
                {
                    brik.Getstate = Terningstate.I_spil;
                    braedtdims.Optagetbrik.Remove(brik);
                    braedtdims.Optagetbrik[0].Getstate = Terningstate.I_spil;
                }
            }
            else
            {
                braedtdims.Optagetbrik.Remove(brik);
                braedtdims.OptagetFarve = Colors.ingen;
            }
        }

        public void Ryk(Spillebrik brik)
        {
            if (brik.Felt != null)
            {
                if (brik.Felter_tilbage - Terning_Vaerdi < 6)
                {
                    Tager_væk(brik);
                    brik.Felt = null;
                    Flytte = true;
                }
            }
            if (brik.Felter_tilbage - Terning_Vaerdi < 0)
            {
                brik.Felter_tilbage = (brik.Felter_tilbage - Terning_Vaerdi) * -1;
                Console.WriteLine("Du er så langt i spillet, plads " + (brik.Felter_tilbage));
                Flytte = true;
            }
            else if (brik.Felter_tilbage - Terning_Vaerdi == 0)
            {
                brik.Felter_tilbage = 0;
                brik.Getstate = Terningstate.Faerdig;
                Flytte = true;
            }
            else
            {
                brik.Felter_tilbage = brik.Felter_tilbage - Terning_Vaerdi;
                Console.WriteLine("Du er så langt i spillet, plads " + brik.Felter_tilbage);
                Flytte = true;
            }
            //Den gør at man kan finde ud af om brikken er på brædtet(felt)
            if (brik.Felt != null)
            {
                //Så den går fra 51 til 0
                if (brik.Felt + Terning_Vaerdi > 51)
                {
                    for (int j = 0; j < Terning_Vaerdi; j++)
                    {
                        // Enden går du fra 51 til 0 eller gør sådan at du rykker en gang
                        if (brik.Felt + 1 > 51)
                        {
                            brik.Felt = 0;
                        }
                        else
                        {
                            brik.Felt++;
                        }
                    }
                }
                //Hvis ikke tæt på 51 går man bare vidrer
                else
                {
                    brik.Felt = brik.Felt + Terning_Vaerdi;
                }
                Flytte = true;
                Tager_væk(brik);
                Tilfojer(brik);
            }
        }

        public void Ikke_hopper_over_Hinanden(Spillebrik brik)
        {
            int falskbrik = brik.Felt.Value;
            for (int i = 1; i <= Terning_Vaerdi; i++)
            {
                if (falskbrik + 1 > 51)
                {
                    falskbrik = 0;
                    if (braedt.BraedtFeltter[falskbrik].OptagetFarve == brik.BrikColor() & Terning_Vaerdi < i)
                    {
                        falsk_ryk = true;
                        Console.WriteLine("Du kan ikke bruge denne brik, fordi at du ikke må hoppe over den foran dig, så vælg en ny brik.");
                    }
                    if (brik.BrikColor() == Colors.gul)
                    {
                        brik.Felt = 0;
                    }
                }
                else
                {
                    if (braedt.BraedtFeltter[falskbrik + 1].OptagetFarve == brik.BrikColor() & Terning_Vaerdi < i)
                    {
                        falsk_ryk = true;
                        Console.WriteLine("Du kan ikke bruge denne brik, fordi at du ikke må hoppe over den foran dig, så vælg en ny brik.");
                    }
                    if (falskbrik == 13 & brik.BrikColor() == Colors.blå)
                    {
                        break;
                    }
                    if (falskbrik == 26 & brik.BrikColor() == Colors.rød)
                    {
                        break;
                    }
                    if (falskbrik == 39 & brik.BrikColor() == Colors.grøn)
                    {
                        break;
                    }
                    falskbrik++;
                }
            }
        }

        public void Tekst()
        {
            Console.WriteLine("Du har vundet");
        }
    }
}       