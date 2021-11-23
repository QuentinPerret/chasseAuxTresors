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
            PlayGame();
        }
        static string[,] creerGrille()
        {
            bool test = false;
            int nbColonne = 0;
            int nbLigne = 0;
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
            test = false;
            while (test == false)
            {
                try
                {
                    Console.WriteLine("Combien de colonnes voulez-vous ?");
                    nbColonne = int.Parse(Console.ReadLine());
                    test = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un caractère");
                }
            }
            string[,] grille = new string[nbLigne, nbColonne];
            return grille;
        }
        static void creerBombesTresors(string[,] GrilleAll , int entreeLigne1 , int entreeColonne1)
        {
            Random RdNumber = new Random();
            int NbLigne = GrilleAll.GetLength(0);
            int NbColonne = GrilleAll.GetLength(1);
            //Création des trésors 
            int NbTresor = RdNumber.Next(1, 4);
            for (int i = 0; i < NbTresor; i++)
            {
                int NumLigne = 0;
                int NumColonne = 0;
                do
                {
                    NumLigne = RdNumber.Next(0, NbLigne);
                    NumColonne = RdNumber.Next(0, NbColonne);
                }
                while (GrilleAll[NumLigne, NumColonne] == "T") ;
                GrilleAll[NumLigne, NumColonne] = "T";
            }
            //Création des Bombes
            int NbBombes = RdNumber.Next((NbLigne / 2), ((NbLigne * NbColonne) / 2 + 1));
            for (int i = 0; i < NbTresor; i++)
            {
                int NumLigne = 0;
                int NumColonne = 0;
                do
                {
                    NumLigne = RdNumber.Next(0, NbLigne);
                    NumColonne = RdNumber.Next(0, NbColonne);
                }
                while (GrilleAll[NumLigne, NumColonne] == "T" || GrilleAll[NumLigne, NumColonne] == "B" || (NumLigne == entreeLigne1 & NumColonne == entreeColonne1));
                GrilleAll[NumLigne, NumColonne] = "B";
            }
        }
        static void definirValeurCase(string[,] GrilleAll , int NumLigne, int NumColonne)
        {
            int valeur = 0;
            try
            {
                for(int i = -1; i < 2; i++)
                {
                    for(int j  = -1; j< 2; j++)
                    {
                        if(GrilleAll[(NumLigne + i),(NumColonne + j)] == "T")
                        {
                            valeur += 2;
                        }
                        else if (GrilleAll[(NumLigne + i), (NumColonne + j)] == "B")
                        {
                            valeur += 1;
                        }
                    }
                }
                if (valeur != 0)
                {
                    GrilleAll[NumLigne, NumColonne] = valeur.ToString();
                }
            }
            catch(System.IndexOutOfRangeException) { }
        }
        static void PlayGame()
        {
            Console.WriteLine("Bienvenue dans la chasse au trésor !!!");
            string[,] mainGrille = creerGrille();
            string[,] calque = mainGrille;
        }
    }
}
