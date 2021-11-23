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

        static void AfficherGrille(string[,] grille)
        {
            int indiceligne = 0;
            int indicecolonne = 0;

            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < grille.GetLength(1); i++)
            {
                Console.Write(" " + i + "  ");
            }
            for (int i = 0; i < (grille.GetLength(0) * 2 - 1); i++)
            {
                Console.WriteLine();
                indicecolonne = 0;
                if (i % 2 == 0)
                {
                    Console.Write((i / 2) + " ");
                    for (int j = 0; j < (grille.GetLength(1) * 2 - 1); j++)
                    {
                        if (j % 2 == 0)
                        {
                            /*if (string.IsNullOrEmpty(grille[indiceligne, indicecolonne]))
                            {
                                Console.Write("  ");
                            }
                            else
                            {*/
                                Console.Write(" " + grille[indiceligne, indicecolonne]);
                                indicecolonne++;
                            //}
                        }
                        else
                        {
                            Console.Write(" |");
                        }
                    }
                    indiceligne++;
                }
                else
                {
                    Console.Write("  ");
                    for (int j = 0; j <= (grille.GetLength(1)*3 + grille.GetLength(1) -2); j++)
                    {
                        Console.Write("-");
                    }
                }
            }
            Console.WriteLine();
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
                Console.WriteLine("tl:" + NumLigne);
                Console.WriteLine("tc " + NumColonne);
            }
            //Création des Bombes
            int NbBombes = RdNumber.Next((NbLigne / 2), ((NbLigne * NbColonne) / 2 + 1));
            Console.WriteLine(NbBombes);
            for (int i = 0; i < NbBombes; i++)
            {
                int NumLigne = 0;
                int NumColonne = 0;
                do
                {
                    NumLigne = RdNumber.Next(0, NbLigne);
                    NumColonne = RdNumber.Next(0, NbColonne);
                }
                while (GrilleAll[NumLigne, NumColonne] == "T" | GrilleAll[NumLigne, NumColonne] == "B" | (NumLigne == entreeLigne1 && NumColonne == entreeColonne1));
                GrilleAll[NumLigne, NumColonne] = "B";
                Console.WriteLine( "bl:"+ NumLigne);
                Console.WriteLine("bc:" + NumColonne);
            }
        }
        static void definirValeurCase(string[,] GrilleAll , int NumLigne, int NumColonne)
        {
            if(string.IsNullOrEmpty(GrilleAll[NumLigne, NumColonne]))
            {
                int valeur = 0;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        try
                        {
                            if (GrilleAll[(NumLigne + i), (NumColonne + j)] == "T")
                            {
                                valeur += 2;
                            }
                            else if (GrilleAll[(NumLigne + i), (NumColonne + j)] == "B")
                            {
                                valeur += 1;
                            }
                        }
                        catch (System.IndexOutOfRangeException) { }
                    }
                }
                if (valeur != 0)
                {
                    GrilleAll[NumLigne, NumColonne] = valeur.ToString();
                }
            }
        }
        static void PlayGame()
        {
            Console.WriteLine("Bienvenue dans la chasse au trésor !!!");
            string[,] mainGrille = creerGrille();
            string[,] calque = mainGrille;
            creerBombesTresors(mainGrille, 2, 3);
            for (int i = 0; i < mainGrille.GetLength(0); i++ )
            {
                for (int j = 0; j < mainGrille.GetLength(1); j++)
                {
                    definirValeurCase(mainGrille ,i, j);
                }
            }
            AfficherGrille(mainGrille);
        }
    }
}
