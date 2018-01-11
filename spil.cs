﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{

    class Spil
    {
        private int deltager;
        private Spillere[] spillere;

        //Hvad der  hvises på skræmen
        public Spil()
        {
            Console.WriteLine("Velkommen til Ludo");
            setdeltager();
            lavspiller();
            hvis_spiller();

            Console.ReadKey();
        }   

        //Hvor mange deltager der er i spil.
        private void setdeltager()
        {
            do
            {
                Console.WriteLine("Hvor mange deltage 2-4?: ");
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

        //laver en ny spiller
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

            for (int i =0; i <3; i++)
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
            Console.WriteLine("Okay, her er dine spiller");
            foreach (Spillere pl in spillere)
            {
                Console.WriteLine(pl.Getbeskrivelse());
            }
        }
    }
}
