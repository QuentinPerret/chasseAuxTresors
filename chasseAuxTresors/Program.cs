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
                            if (string.IsNullOrEmpty(grille[indiceligne, indicecolonne]))
                            {
                                Console.Write("  ");
                            }
                            else
                            {
                                Console.Write(" " + grille[indiceligne, indicecolonne]);
                                
                            }
                            indicecolonne++;
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
            }
            //Création des Bombes
            //int NbBombes = RdNumber.Next((NbLigne / 2), ((NbLigne * NbColonne) / 2 + 1));
            int NbBombes = RdNumber.Next(1,10);
            for (int i = 0; i < NbBombes; i++)
            {
                int NumLigne = 0;
                int NumColonne = 0;
                do
                {
                    NumLigne = RdNumber.Next(0, NbLigne);
                    NumColonne = RdNumber.Next(0, NbColonne);
                }
                while (GrilleAll[NumLigne, NumColonne] == "T" || GrilleAll[NumLigne, NumColonne] == "B" || (NumLigne == entreeLigne1 && NumColonne == entreeColonne1));
                GrilleAll[NumLigne, NumColonne] = "B";
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
                    Console.WriteLine("Ceci n'est pas un nombre entier!");
                }
            }
            test = false;
            while (test == false)
            {
                try
                {
                    Console.WriteLine("Quelle colonne voulez-vous sonder?");
                    colonneAnal = int.Parse(Console.ReadLine());
                    test = true;
                    if (colonneAnal > mainGrille.GetLength(1))
                        test = false;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un nombre entier!");
                }
            }
            int[] analLigCol = { ligneAnal, colonneAnal };
            return analLigCol;
        }
        static void inspecterGrille(string[,] GrilleAll, string[,] GrilleUser, int NumLigne, int NumColonne)
        {
            if (string.IsNullOrEmpty(GrilleAll[NumLigne, NumColonne]) && (string.IsNullOrEmpty(GrilleUser[NumLigne, NumColonne])))
            {
                GrilleUser[NumLigne, NumColonne] = "#";
                GrilleAll[NumLigne, NumColonne] = "#";
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (!(i == j && i == 0))
                        {
                            try
                            {
                                inspecterGrille(GrilleAll, GrilleUser, NumLigne + i, NumColonne + j);
                            }
                            catch (System.IndexOutOfRangeException) { }
                        }
                    }
                }
            }
            else
            {
                GrilleUser[NumLigne, NumColonne] = GrilleAll[NumLigne, NumColonne];
                Console.WriteLine(GrilleUser[NumLigne, NumColonne]);
            }
        }
        static void PlayGame()
        {
            Console.WriteLine("Bienvenue dans la chasse au trésor !!!");
            string[,] mainGrille = creerGrille();
            string[,] calque = new string[mainGrille.GetLength(0), mainGrille.GetLength(1)]; ;
            AfficherGrille(calque);
            int[] positionInspection = entrerInspectionUser(mainGrille);
            creerBombesTresors(mainGrille, positionInspection[0], positionInspection[1]);
            for (int i = 0; i < mainGrille.GetLength(0); i++ )
            {
                for (int j = 0; j < mainGrille.GetLength(1); j++)
                {
                    definirValeurCase(mainGrille ,i, j);
                }
            }
            inspecterGrille(mainGrille, calque, positionInspection[0], positionInspection[1]);
            AfficherGrille(calque);
            positionInspection = entrerInspectionUser(mainGrille);
            inspecterGrille(mainGrille, calque, positionInspection[0], positionInspection[1]);
            AfficherGrille(calque);
        }
    }
}