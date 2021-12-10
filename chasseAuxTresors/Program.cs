using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace chasseAuxTresors
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayGame();
        }
        static bool stopGame = false;
        static void AfficherGrille(string[,] grille)
        {
            int indiceligne = 0;
            int indicecolonne = 0;

            Console.WriteLine();
            Console.Write("    ");
            for (int i = 0; i < grille.GetLength(1); i++)
            {
                if (i >= 10)
                {
                    Console.WriteLine(" " + i + "  ");
                }
                else
                {
                    Console.Write(" " + i + "   ");
                }
            }
            for (int i = 0; i < (grille.GetLength(0)); i++)
            {
                Console.WriteLine();
                indicecolonne = 0;
                if (i >= 10)
                {
                    Console.WriteLine(i + " ");
                }
                else
                {
                    Console.Write(i + "  ");
                }
                for (int j = 0; j < (grille.GetLength(1)); j++)
                {
                    /* cette condition affiche du rouge en arrière plan s'il y a
                     * une bombe dans la case analysée*/
                    Console.Write(" ");
                    if (grille[indiceligne, indicecolonne] == "B")
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + grille[indiceligne, indicecolonne] + " ");
                    }
                    else if (grille[indiceligne, indicecolonne] == "T")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + grille[indiceligne, indicecolonne] + " ");
                    }
                    else if (grille[indiceligne, indicecolonne] != "#" && (!string.IsNullOrEmpty(grille[indiceligne, indicecolonne])))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + grille[indiceligne, indicecolonne] + " ");
                    }
                    else if (grille[indiceligne, indicecolonne] == "#")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("   ");
                    }
                    indicecolonne++;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
                indiceligne++;
                Console.WriteLine();
            }
        }
        static void SetEndGame(ref bool stopGame)
        {
            stopGame = true;
        }
        static int[] AskDimGrille()
        {
            Console.Clear();
            //fonction créant un tableau de dimension entrer par l'user
            bool testEntryLigne = false;
            int nbColonne = 0;
            int nbLigne = 0;
            int nbErrorUser = 0;
            //Entrer user du nombre de ligne dans la grille
            while (testEntryLigne == false)
            {
                try
                {
                    Console.WriteLine("Combien de lignes voulez-vous ?");
                    nbLigne = int.Parse(Console.ReadLine());
                    testEntryLigne = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un nombre entier de ligne! Réessayer!");
                    nbErrorUser++;
                    if(nbErrorUser == 3)
                    {
                        Console.WriteLine("Cela fait 3 fois que vous ne repondez pas à la question, le jeu va maintenant s'arrêter. Merci d'avoir joué !");
                        SetEndGame(ref stopGame);
                        testEntryLigne = true;
                        Console.ReadLine();
                    }
                }
            }
            bool testEntryCol = false;
            nbErrorUser = 0;
            //Entrer user du nombre de colonne dans la grille
            while (testEntryCol == false && stopGame ==false)
            {
                try
                {
                    Console.WriteLine("Combien de colonnes voulez-vous ?");
                    nbColonne = int.Parse(Console.ReadLine());
                    testEntryCol = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un caractère");
                    nbErrorUser++;
                    if (nbErrorUser == 3)
                    {
                        Console.WriteLine("Cela fait 3 fois que vous ne repondez pas à la question, le jeu va maintenant s'arrêter. Merci d'avoir joué !");
                        SetEndGame(ref stopGame);
                        testEntryCol=true;
                        Console.ReadLine();
                    }
                }
            }
            int[] DimGrille = {nbLigne,nbColonne};
            return DimGrille;
        }
        static void creerBombesTresors(string[,] GrilleAll, int entreeLigne1, int entreeColonne1)
        {
            /* programme permettant de créer les bombes et les trésors dans la grille 
            entreLigne1 et entreeColonne1 correspondent aux coordonnées de la première case inspecter par l'user,
            cette information permet d'éviter de créer une bombe à cet emplacement et donc de perdre immédiatement */
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
                while (GrilleAll[NumLigne, NumColonne] == "T"); // on fait bien attention ici que le nouveau trésor créer n'écrase pas l'ancien 
                GrilleAll[NumLigne, NumColonne] = "T";
            }
            //Création des Bombes
            //
            int NbBombes = RdNumber.Next((NbLigne / 2), ((NbLigne * NbColonne) / 2 + 1));
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
                // on fait attention ici que la bombe créer n'écrase pas un trésor ou une bombe mais aussi que la bombe créer ne corresponde pas à la première case inspecter par l'user
                GrilleAll[NumLigne, NumColonne] = "B";
            }
        }
        static void definirValeurCase(string[,] GrilleAll, int NumLigne, int NumColonne)
        {
            /*programmme permettant de définir la valeur d'une case suivant le nombre de bombes et trésors à proximité
            Une bombe ajoute +1 et un trésor +2
            une case sans bombe ou trésor à proximité reste vide*/
            if (string.IsNullOrEmpty(GrilleAll[NumLigne, NumColonne])) // on vérifie que la case est bien vide pour exclure les cas où la case contient une bombe ou un trésor 
            {
                int valeur = 0;
                /*dans cette double boucle for on teste si autour de la case vide il y a des bombes ou des trésors pour mettre a jour la valeur de cette case 
                si jamais le test de présence de bombe ou trésor sort du tableau, on ignore l'erreur et on ne fais rien ce qui permet de passer à la case de test suivante*/
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
                //On ne mets à jour la valeur de la case que si la valeur est différente de 0, cela permet de garder une case vide s'il n'y a rien a proximité
                if (valeur != 0)
                {
                    GrilleAll[NumLigne, NumColonne] = valeur.ToString(); // on rapelle que nous utilisons un tableau de string il est donc nécéssaire de transformer la valeur entière de la case vers une chaine de caractère
                }
            }
        }
        static int[] entrerInspectionUser(string[,] mainGrille)
        {
            bool testEntry = false;
            int ligneAnal = 0;
            int colonneAnal = 0;
            int nbErrorUser = 0;
            while (testEntry == false)
            {
                try
                {
                    Console.WriteLine("Quelle ligne voulez-vous sonder?");
                    ligneAnal = int.Parse(Console.ReadLine());
                    testEntry = true;
                    if (ligneAnal >= mainGrille.GetLength(0))
                    {
                        Console.WriteLine("Ceci n'est pas une ligne existante. Réessayer!");
                        nbErrorUser++;
                        testEntry = false;
                    }
                    if (nbErrorUser == 3)
                    {
                        Console.WriteLine("Cela fait 3 fois que vous ne repondez pas à la question, le jeu va maintenant s'arrêter. Merci d'avoir joué !");
                        SetEndGame(ref stopGame);
                        testEntry = true;
                        Console.ReadLine();
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un nombre entier!");
                    nbErrorUser++;
                }
            }
            testEntry = false;
            while (testEntry == false && stopGame == false)
            {
                try
                {
                    Console.WriteLine("Quelle colonne voulez-vous sonder?");
                    colonneAnal = int.Parse(Console.ReadLine());
                    testEntry = true;
                    if (ligneAnal >= mainGrille.GetLength(0))
                    {
                        Console.WriteLine("Ceci n'est pas une colonne existante. Réessayer!");
                        nbErrorUser++;
                        testEntry = false;
                    }
                    if (nbErrorUser == 3)
                    {
                        Console.WriteLine("Cela fait 3 fois que vous ne repondez pas à la question, le jeu va maintenant s'arrêter. Merci d'avoir joué !");
                        SetEndGame(ref stopGame);
                        testEntry = true;
                        Console.ReadLine();
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Ceci n'est pas un nombre entier!");
                    nbErrorUser++;
                }
            }
            int[] analLigCol = { ligneAnal, colonneAnal };
            return analLigCol;
        }
        static void inspecterGrille(string[,] GrilleAll, string[,] GrilleUser, int NumLigne, int NumColonne)
        {
            if (string.IsNullOrEmpty(GrilleAll[NumLigne, NumColonne]))
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
            }
        }
        static bool testeLaVictoire(string[,] GrilleAll, string[,] GrilleUser)
        {
            /*Fonction permettant de tester si l'user a gagné sa partie
            renvoie true si l'user a gagné, sinon renvoie false*/
            for (int i = 0; i < GrilleAll.GetLength(0); i++)
            {
                for (int j = 0; j < GrilleAll.GetLength(1); j++)
                {
                    if (GrilleAll[i, j] != "B" && GrilleUser[i, j] != GrilleAll[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        static bool testeLaDefaite(string[,] GrilleAll, string[,] GrilleUser)
        {
            /*Fonction permettant de tester si une bombe est apparu est donc si l'user a perdu
            renvoie true si l'user a perdu, sinon renvoie false*/
            for (int i = 0; i < GrilleAll.GetLength(0); i++)
            {
                for (int j = 0; j < GrilleAll.GetLength(1); j++)
                {
                    if (GrilleUser[i, j] == "B")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static bool CheckResult(string[,] mainGrille , string[,] calque)
        {
            if(testeLaDefaite(mainGrille , calque))
            {
                Explosion();
                YouLoose();
                return true;
            }
            else if (testeLaVictoire(mainGrille, calque))
            {
                Victory();
                return true;
            }
            return false;
        }
        static void PlayerMove(string[,] mainGrille, string[,] calque)
        {
                Console.Clear();
                AfficherGrille(calque);
                int [] CoordCase = entrerInspectionUser(mainGrille);
                inspecterGrille(mainGrille, calque, CoordCase[0], CoordCase[1]);
        }
        static void Explosion()
        {
            Console.Clear();
            Console.WriteLine("Boom");
            Console.ReadLine();
        }
        static void AskLoadSave(string[,] mainGrille , string[,]calque)
        {
            string entryUser = "";
            bool testSave = false;
            int nbErrorUser = 0;
            while (testSave == false)
            {
                Console.WriteLine("Avant de quiter voulez vous enregistrer votre partie ? (répondre par O/N)");
                entryUser = Console.ReadLine();
                entryUser = entryUser.ToUpper();
                if (entryUser == "O" || entryUser == "OUI" || entryUser == "YES" || entryUser == "Y")
                {
                    testSave = true;
                    Console.Clear();
                    Serialization(mainGrille, calque);

                }
                else if (entryUser == "N" || entryUser == "NON" || entryUser == "NO")
                {
                    Console.WriteLine("Merci d'avoir joué à notre jeu");
                    testSave = true;
                    Console.ReadLine();
                }
                else
                {
                    nbErrorUser++;
                    testSave = false;
                }
                if (nbErrorUser == 3)
                {
                    Console.WriteLine("Cela fait 3 fois que vous ne repondez pas à la question, le jeu va maintenant s'arrêter. Merci d'avoir joué !");
                    testSave = true;
                }
            }
        }
        static string[][,] CreateNewGame()
        {
            int[] DimGrille = AskDimGrille();
            if (!stopGame)
            {
                string[,] mainGrille = new string[DimGrille[0], DimGrille[1]];
                string[,] calque = new string[DimGrille[0], DimGrille[1]];
                Console.Clear();
                AfficherGrille(calque);
                int[] positionInspection = entrerInspectionUser(mainGrille);
                creerBombesTresors(mainGrille, positionInspection[0], positionInspection[1]);
                SetAllCasesValues(mainGrille);
                inspecterGrille(mainGrille, calque, positionInspection[0], positionInspection[1]);

                string[][,] BothGrille = { mainGrille, calque };
                return BothGrille;
            }
            return null;
        }
        static string[][,] ReloadGame()
        {
            StreamReader game = new StreamReader(@"C:\Users\quent\OneDrive\Bureau\ENSC\C#\chasseAuxTresors\chasseAuxTresors\bin\Release\Test.txt");
            int Nbligne = int.Parse(game.ReadLine());
            int NbColonne = int.Parse(game.ReadLine());
            int NbCase = NbColonne * Nbligne;
            string[,] mainGrille = new string[Nbligne, NbColonne];
            string[,] calque = new string[Nbligne, NbColonne];


            int indLigne = 0;
            int indCol = 0;

            for (int i = 0; i < NbCase; i++)
            {
                Console.WriteLine(indLigne);
                Console.WriteLine(indCol);
                mainGrille[indLigne, indCol] = game.ReadLine();
                indCol++;
                if (indCol == Nbligne)
                {
                    indCol = 0;
                    indLigne++;
                }
            }
            indLigne = 0;
            indCol = 0;

            for (int i = 0; i < NbCase; i++)
            {
                calque[indLigne, indCol] = game.ReadLine();
                indCol++;
                if (indCol == Nbligne)
                {
                    indCol = 0;
                    indLigne++;
                }
            }
            string[][,] BothGrille = { mainGrille, calque };
            return BothGrille;
        }
        static bool AskReloadSave()
        {
            string entryUser = "";
            bool testSave = false;
            int nbErrorUser = 0;
            while (testSave == false)
            {
                Console.WriteLine("Voulez vous reprendre votre dernière partie enregistré la où vous l'aviez laissé (répondre par O/N)");
                entryUser = Console.ReadLine();
                entryUser = entryUser.ToUpper();
                if (entryUser == "O" || entryUser == "OUI" || entryUser == "YES" || entryUser == "Y")
                {
                    testSave = true;
                    return true;

                }
                else if (entryUser == "N" || entryUser == "NON" || entryUser == "NO")
                {
                    Console.WriteLine("Une nouvelle partie va etre créer!");
                    testSave = true;
                    return false;
                }
                else
                {
                    nbErrorUser++;
                    testSave = false;
                }
                if (nbErrorUser == 3)
                {
                    Console.WriteLine("Cela fait 3 fois que vous ne repondez pas à la question, Une nouvelle partie va etre créer !");
                    Console.ReadLine();
                    testSave = true;

                }
            }
            return false;
        }
        static bool AskStopGame()
        {
            bool testGame = false;
            int compteurErrorUser = 0;
            while (testGame == false)
            {
                Console.WriteLine("Voulez vous arreter de jouer ? (répondre par O/N)");
                string entryUser = Console.ReadLine();
                entryUser = entryUser.ToUpper();
                if (entryUser == "O" || entryUser == "OUI" || entryUser == "YES" || entryUser == "Y")
                {
                    Console.WriteLine("Merci d'avoir joué à notre jeu");
                    testGame = true;
                    Console.Clear();
                    return true;

                }
                else if (entryUser == "N" || entryUser == "NON" || entryUser == "NO")
                {
                    testGame = true;
                    Console.ReadLine();
                    return false;
                }
                else
                {
                    compteurErrorUser++;
                }
                if (compteurErrorUser == 3)
                {
                    Console.WriteLine("Cela fait 3 fois que vous ne repondez pas à la question, le jeu va maintenant s'arrêter. Merci d'avoir joué !");
                    testGame = true;
                    return true;
                }
            }
            return false;
        }
        static bool AskReplayGame()
        {
            bool test = false;
            int compteurErrorUser = 0;
            //Entrer user s'il veut rejouer ou non 
            while (test == false)
            {
                Console.WriteLine("Voulez vous rejouer ? (répondre par O/N)");
                string entryUser = Console.ReadLine();
                entryUser = entryUser.ToUpper();
                if (entryUser == "O" || entryUser == "OUI" || entryUser == "YES" || entryUser == "Y")
                {
                    test = true;
                    Console.Clear();
                    Console.WriteLine("Une nouvelle partie va commencer ! ");
                    return true;
                }
                else if (entryUser == "N" || entryUser == "NON" || entryUser == "NO")
                {
                    Console.WriteLine("Merci d'avoir joué à notre jeu");
                    test = true;
                    Console.ReadLine();
                    return false;
                }
                else
                {
                    compteurErrorUser++;
                }
                if (compteurErrorUser == 3)
                {
                    Console.WriteLine("Cela fait 3 fois que vous ne repondez pas à la question, le jeu va maintenant s'arrêter. Merci d'avoir joué !");
                    test = true;
                    return false;
                }
            }
            return false;
        }
        static void Victory()
        {
            Console.Clear();
            Console.WriteLine(@"                             __   __  __     ______    ______   ______    ______     __  __    
                            /\ \ / / /\ \   /\  ___\  /\__  _\ /\  __ \  /\  == \   /\ \_\ \   
                            \ \ \'/  \ \ \  \ \ \____ \/_/\ \/ \ \ \/\ \ \ \  __<   \ \____ \  
                             \ \_|    \ \_\  \ \_____\   \ \_\  \ \_____\ \ \_\ \_\  \/\_____\ 
                              \/_/     \/_/   \/ ___ /    \/_/   \/ ___ /  \/_/ /_/   \/_____/
");
            Console.ReadLine();
        }
        static void YouLoose()
        {
            Console.Clear();
            Console.WriteLine(@"                     __  __     ______     __  __        __         ______     ______     ______     ______    
                    /\ \_\ \   /\  __ \   /\ \/\ \      /\ \       /\  __ \   /\  __ \   /\  ___\   /\  ___\   
                    \ \____ \  \ \ \/\ \  \ \ \_\ \     \ \ \____  \ \ \/\ \  \ \ \/\ \  \ \___  \  \ \  __\   
                     \/\_____\  \ \_____\  \ \_____\     \ \_____\  \ \_____\  \ \_____\  \/\_____\  \ \_____\ 
                      \/_____/   \/_____/   \/_____/      \/_____/   \/_____/   \/_____/   \/_____/   \/_____/
");
            Console.ReadLine();
        }
        static void Welcome()
        {
            Console.Clear();
            Console.WriteLine(@"
                                    ____  _                                     
                                   / __ )(_)__  ____ _   _____  ____  __  _____ 
                                  / __  / / _ \/ __ \ | / / _ \/ __ \/ / / / _ \
                                 / /_/ / /  __/ / / / |/ /  __/ / / / /_/ /  __/
                                /_____/_/\___/_/ /_/|___/\___/_/ /_/\__,_/\___/ 

");
            Console.WriteLine(@"

                                                 __                
                                            ____/ /___ _____  _____
                                           / __  / __ `/ __ \/ ___/
                                          / /_/ / /_/ / / / (__  ) 
                                          \__,_/\__,_/_/ /_/____/  

");
            Console.WriteLine(@"

      __              __                                                __        __                      
     / /___ _   _____/ /_  ____ ______________     ____ ___  ___  __   / /_______/_/ _________  __________
    / / __ `/  / ___/ __ \/ __ `/ ___/ ___/ _ \   / __ `/ / / / |/_/  / __/ ___/ _ \/ ___/ __ \/ ___/ ___/
   / / /_/ /  / /__/ / / / /_/ (__  |__  )  __/  / /_/ / /_/ />  <   / /_/ /  /  __(__  ) /_/ / /  (__  ) 
  /_/\__,_/   \___/_/ /_/\__,_/____/____/\___/   \__,_/\__,_/_/|_|   \__/_/   \___/____/\____/_/  /____/  
");
            Console.ReadLine();
        }
        static void Rules()
        {
            Console.Clear();
            Console.WriteLine("Rules");
            Console.ReadLine();
        }
        static void SetAllCasesValues(string[,]mainGrille)
        {
            for (int i = 0; i < mainGrille.GetLength(0); i++)
            {
                for (int j = 0; j < mainGrille.GetLength(1); j++)
                {
                    definirValeurCase(mainGrille, i, j);
                }
            }
        }
        static string[][,] PreGame()
        {
            Welcome();
            Rules();
            if (AskReloadSave())
            {
                return  ReloadGame();
            }
            else if (!stopGame)
            {
                return CreateNewGame();
            }
            return null;
        }
        static void MidGame(string[,] mainGrille, string[,] calque)
        {

            while (!CheckResult(mainGrille, calque) && !stopGame)
            {
                PlayerMove(mainGrille, calque);
            } 
        }
        static void EndGame()
        {
            if (AskReplayGame())
            {
                RePlay();
            }
            else
            {
                //add possibilté menu ou arret
            }
        }
        static void PlayGame()
        {
            string[][,] BothGrille = PreGame();
            if (!stopGame)
            {
                string[,] mainGrille = BothGrille[0];
                string[,] calque = BothGrille[1];
                MidGame(mainGrille, calque);
            }
            EndGame();
        }
        static void RePlay()
        {
            string[][,] BothGrille = CreateNewGame();
            if (!stopGame)
            {
                string[,] mainGrille = BothGrille[0];
                string[,] calque = BothGrille[1];
                MidGame(mainGrille, calque);
            }
            EndGame();
        }
        static void Serialization(string[,] maingrille , string[,] calque)
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter game = new StreamWriter(@"C:\Users\quent\OneDrive\Bureau\ENSC\C#\chasseAuxTresors\chasseAuxTresors\bin\Release\Test.txt");
            try
            {
                game.WriteLine(maingrille.GetLength(0));
                game.WriteLine(maingrille.GetLength(1));
                for (int i = 0; i < maingrille.GetLength(0); i++)
                {
                    for (int j  = 0; j < maingrille .GetLength(1); j++)
                    {
                        game.WriteLine(maingrille[i,j]);
                    }
                }
                for (int i = 0; i < maingrille.GetLength(0); i++)
                {
                    for (int j = 0; j < maingrille.GetLength(1); j++)
                    {
                        game.WriteLine(calque[i, j]);
                    }
                }
                game.Close();
            }
            catch (Exception e)//
            {
                Console.WriteLine("Une erreur dans l'enregistrement de votre partie est survenue: " + e.Message);
                game.Dispose();
            }
        }
    }
}
