using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chasseAuxTresors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans la chasse au trésor !!!");
            string[,] mainGrille = creerGrille();
            afficherGrille1(mainGrille);
            string[,] calque = mainGrille;
            entrerInspectionUser(mainGrille);
        }
        static string[,] creerGrille()
        {
            bool test = false;
            int nbColonne = 0 ;
            int nbLigne = 0 ;
            while (test == false)
            {
                try
                {
                    Console.WriteLine("Combien de lignes voulez-vous ?");
                    nbLigne = int.Parse(Console.ReadLine());
                    test = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un caractère");
                }
            }
            bool test2 = false;
            while (test2 == false)
            {
                try
                {
                    Console.WriteLine("Combien de colonnes voulez-vous ?");
                    nbColonne = int.Parse(Console.ReadLine());
                    test2 = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un caractère");
                }
            }
            
            
            string[,] grille = new string[nbLigne*2, nbColonne];
            return grille;
            
        }
        static void afficherGrille1(string[,] mainGrille)
        {
            for(int i=0;i<mainGrille.GetLength(0); i++)
            {
                if (i % 2 == 0) 
                { 
                    for(int j=0; j < mainGrille.GetLength(1); j++)
                    {
                        mainGrille[i, j] = "| ";
                        Console.Write( mainGrille[i, j] + " " );
                    }
                    Console.WriteLine();
                }
                else
                {
                    for(int k=0; k<mainGrille.GetLength(1); k++)
                    {
                        mainGrille[i, k] = "|_";
                        Console.Write( mainGrille[i, k] + " ");
                    }
                    Console.WriteLine();
                }
            }

        }
        static void afficheGrilleN(string[,] mainGrille)
        {
            for (int i = 0; i < mainGrille.GetLength(0); i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < mainGrille.GetLength(1); j++)
                    {
                        
                        Console.Write(mainGrille[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    for (int k = 0; k < mainGrille.GetLength(1); k++)
                    {
                        
                        Console.Write(mainGrille[i, k] + " ");
                    }
                    Console.WriteLine();
                }
            }

        }
        static int[] entrerInspectionUser(string[,] mainGrille)
        {
            bool test = false;
            int ligneAnal = 0;
            int colonneAnal = 0;
            while (test == false)
            {
                try
                {
                    Console.WriteLine("Quelle ligne voulez-vous sonder?");
                    ligneAnal = int.Parse(Console.ReadLine());
                    test = true;
                    if (ligneAnal > mainGrille.GetLength(0))
                        test = false;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un caractère");
                }
            }
            bool test2 = false;
            while (test2 == false)
            {
                try
                {
                    Console.WriteLine("Quelle colonne voulez-vous sonder?");
                    colonneAnal = int.Parse(Console.ReadLine());
                    test2 = true;
                    if (colonneAnal > mainGrille.GetLength(1))
                        test2 = false;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un caractère");
                }
            }
            int[] analLigCol = { ligneAnal, colonneAnal };
            return analLigCol;

        }

    }
}
