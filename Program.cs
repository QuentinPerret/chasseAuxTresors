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
            afficherGrille(mainGrille);
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
            
            
            string[,] grille = new string[nbLigne, nbColonne];
            return grille;
            
        }
        static void afficherGrille(string[,] mainGrille)
        {
            for(int i=0;i<mainGrille.GetLength(0); i++)
            {
                for(int j=0; j < mainGrille.GetLength(1); j++)
                {
                    mainGrille[i, j] = "|_";
                    Console.Write(mainGrille[i, j]);
                }
                Console.WriteLine();
            }

        }
        static void entrerInspectionUser(string[,] mainGrille)
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


        }

    }
}
