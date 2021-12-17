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
        static string link = @"C:\Users\quent\OneDrive\Bureau\ENSC\C#\chasseAuxTresors\chasseAuxTresors\bin\Release\Test.txt";
        static bool stopGame = false;
        static bool testFlag = false;
        static void AfficherGrille(string[,] grille, int[] Coordcase)
        {
            int indiceligne = 0;
            int indicecolonne = 0;

            //on commence par afficher une première ligne avec les numéros de colonnes 
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

            //puis pour chaque ligne on affiche le numéro de la ligne et tout ce qu'elle contient
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
                    // suivant ce que contient la case la couleur est modifier
                    Console.Write(" ");
                    if(indiceligne == Coordcase[0] && indicecolonne == Coordcase[1])
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" P ");
                    }
                    else if (grille[indiceligne, indicecolonne] == "B") //pour une bombe
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + grille[indiceligne, indicecolonne] + " ");
                    }
                    else if (grille[indiceligne, indicecolonne] == "T") // pour un trésor
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + grille[indiceligne, indicecolonne] + " ");
                    }
                    else if (grille[indiceligne,indicecolonne] == "F") // pour un flag
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + grille[indiceligne, indicecolonne] + " ");
                    }
                    else if (grille[indiceligne, indicecolonne] == "#") // si la case et vide et découverte
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("   ");
                    }
                    else if (grille[indiceligne, indicecolonne] != "#" && (!string.IsNullOrEmpty(grille[indiceligne, indicecolonne])))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + grille[indiceligne, indicecolonne] + " ");
                    }
                    else // si la case est vide et non découverte
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
            /*fonction permettant de changer le bool stopGame de la valeur false vers true
             * cette fonction est utilisé si la partie doit est stopper avant que le joueur gagne ou perde*/
            stopGame = true;
        }
        static int[] AskDimGrille()
        {
            //Fonction retournant un tableau contenant les dimensions de la grille
            Console.Clear();
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
                    if(nbErrorUser == 3) //si jamais l'user fait 3 erreurs le jeu s'arrete
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
                    if (nbErrorUser == 3) //si jamais l'user fait 3 erreurs le jeu s'arrete
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
        static bool AskNextMove(string[,] calque , int[] Coordcase)
        {
            //programme demandant à l'user s'il veut continuer de jouer
            //si l'user entre f ou flag la prochaine case selectionner aura un flag dessus
            AfficherGrille(calque, Coordcase);
            Console.WriteLine("Que voulez vous faire de cette case? \n    - Pour l'inspecter appuyer sur la touche entrée. \n    - Pour poser un marqueur taper f ou flag. \n    - Et pour rechoisir un case taper Rechoice");
            bool testEntry = false;
            int nbErrorUser = 0;
            while (testEntry == false)
            {
                string entryUser = Console.ReadLine();
                entryUser = entryUser.ToUpper();
                if(string.IsNullOrEmpty(entryUser))
                {
                    return true;
                }
                else if (entryUser == "RE" || entryUser == "RECHOICE")
                {
                    return false;
                }
                else if (entryUser == "F" || entryUser == "FLAG")
                {
                    testFlag = true;
                    return true;
                }
                else if (nbErrorUser == 3)
                {
                    Console.WriteLine("Cela fait 3 fois que vous ne repondez pas à la question, le jeu va maintenant s'arrêter. Merci d'avoir joué !");
                    testEntry = true;
                }
                else
                {
                    nbErrorUser++;
                    testEntry = false;
                }
            }
            return false;
        }
        static int[] entrerInspectionUser(string[,] mainGrille, string[,] calque)
        {
            //fonction retournant un tableau contenant les coordonnées de la case à inspecter choisi par l'user
            bool testEntry = false;
            int ligneAnal = 0;
            int colonneAnal = 0;
            int nbErrorUser = 0;
            while (testEntry == false)
            {
                try
                {
                    Console.WriteLine("Quelle ligne voulez-vous choisir ? ");
                    ligneAnal = int.Parse(Console.ReadLine());
                    testEntry = true;
                    if (ligneAnal >= mainGrille.GetLength(0))
                    {
                        Console.WriteLine("Ceci n'est pas une ligne existante. Réessayer!");
                        nbErrorUser++;
                        testEntry = false;
                    }
                    if (nbErrorUser == 3)//si jamais l'user fait 3 erreurs le jeu s'arrete
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
                    Console.WriteLine("Quelle colonne voulez-vous choisir?");
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
            if (calque[ligneAnal, colonneAnal] == "F" || string.IsNullOrEmpty(calque[ligneAnal,colonneAnal]))
            {
                int[] analLigCol = { ligneAnal, colonneAnal };
                return analLigCol;
            }
            else  //si la case a deja était inspecter on redemande à l'user quelle case il veut inspecter
            {
                Console.WriteLine("Cette case n'est pas vide. Veuillez entrer une case vide");
                return entrerInspectionUser(mainGrille, calque);
            }
        }
        static void inspecterGrille(string[,] GrilleAll, string[,] GrilleUser, int NumLigne, int NumColonne)
        {
            // programme mettant a jour la grille du joueur en fonction de la case inspecteé
            if (string.IsNullOrEmpty(GrilleAll[NumLigne, NumColonne]) || GrilleAll[NumLigne, NumColonne] == "F")
            { 
                // pour eviter de repasser par une case deja inspecter on la rempli avec la char #
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
                                inspecterGrille(GrilleAll, GrilleUser, NumLigne + i, NumColonne + j);//tant que la case est vide on inspecte les 8 case autour en rappellant recursivement la fonction
                            }
                            catch (System.IndexOutOfRangeException) { }
                        }
                    }
                }
            }
            else
            {
                GrilleUser[NumLigne, NumColonne] = GrilleAll[NumLigne, NumColonne]; // si la case n'est pas vide on copie-colle son contenu dans la grille de l'user
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
            //programme retournant true si le joueur a gané ou perdu et false sinon 
            //si le joueur a gagné ou perdu cette méthode appelle dles méthodes d'affichache correspondantes
            if(testeLaDefaite(mainGrille , calque))
            {
                clearSave(mainGrille.Length);
                Explosion();
                YouLose();
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
            //méthode permettant au joueur de jouer un coup =
            int[] CoordCase = new int[2];
            do
            {
                CoordCase = entrerInspectionUser(mainGrille, calque);
            }
            while (AskNextMove(calque , CoordCase) == false);
            if (testFlag == true)
            {
                FlagMove(calque, CoordCase);
            }
            else
            {
                inspecterGrille(mainGrille, calque, CoordCase[0], CoordCase[1]);
            }
        }
        static void FlagMove(string[,] calque, int[]CoordCase)
        {
            //fonction permettant de poser un flag sur une case de la grille du joueur
            calque[CoordCase[0], CoordCase[1]] = "F";
            testFlag = false;
        }
        static void Explosion()
        { 
            //programme affichant une explosion
            Console.Clear();
            Console.WriteLine(@"









                                        *



");
            System.Threading.Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine(@"





                                    * ***#****
                                 ***#@##*#@#****
                               *@**@@@*##**#*#**#*
                              **@@*#**#@*#@****#***
                              ****@*@***#****@**@@*
                             *******@***@@***#****#*
                              *#***#*##@****##@@@**
                              **#@###****@*********
                               *****@**@*@*****@**
                                 ************@**
                                    ****#****

");
            System.Threading.Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine(@"
                                        *                                       
                               ********* **@******
                           ****   **#**@ #**#*#   ****
                         ***  **  **##** *@@*@*  **  ***
                       **  *  @@   *@*#* ***@*   *#  *  **
                     *** #  *  *#  *@#** ***@*  **  @  * *@*
                    ** *  * ** *@  ****@ @****  ** #* *  * **
                   ** * @* * ** *@  #### *#**  ** ** * @* * **
                  *# * # ** * #* *  **** ****  @ ** * ** * * #*
                 ** * *# * @ * #  @  @*@ *#*  *  @ # # * @* * **
                 *# * * * * # * @* * **# *#* * ** * * * * * # **
                 ** # * * @ * * # * # ** @* * * * * * # @ @ * **
                *# * * * * * * * * # * * * * * * @ @ * * * * * **
                 *# * @ * @ * @ * * * ** *@ * * # * * * * * @ @*
                 *# # @ * * # * *@ * *** @#@ @ ** * @ @ * * # **
                 *# * ** * * * @  @  **@ ***  *  @ * * * @* * #*
                  ** * * ** * #@ *  #*** **##  * #* * #@ * * @*
                   *# * *@ * @@ *#  **** @***  ** ** * #* * #*
                    *# *  * *@ **  @**@* *@#**  ** ** *  * @*
                     *#* @  *  @@  **##* ****@  **  #  * @**
                       **  @  #*   @*@#* @*@*#   @#  *  **
                         *#*  @*  @#*@*# **#*@#  **  ***
                           ****   *##**# #***@*   @***
                               ****@**@* *****@***
");
            System.Threading.Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine(@"
               *  -  -  --  --   ------- -------   --  --  -  -  *
             ** -  -  -  --  --   ------ ------   --  --  -  -  - **
            * -- - -- -- --  --   ------ ------   --  -- -- -- - -- *
          ** - -  - -- -- --  --  ------ ------  --  -- -- -- -  - - **
         *  - - -- - -- -  -  --   ----- -----   --  -  - -- - -- - -  *
        ** - - - -- - -- -  -  --  ----- -----  --  -  - -- - -- - - - **
        * - - - -  - - -  - -- --  ----- -----  -- -- -  - - -  - - - - *
       * - - - -  - - - -- - -- --  ---- ----  -- -- - -- - - -  - - - - *
      * -- - - - - - - - -- - -- -  ---- ----  - -- - -- - - - - - - - -- *
      * - - - - - - - -- - - - -  -  --- ---  -  - - - - -- - - - - - - - *
      * - - - - - - - - - - - - -- - --- --- - -- - - - - - - - - - - - - *
      * - - - - - - - - - - - - - - - -- -- - - - - - - - - - - - - - - - *
     * - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - *
      * - - - - - - - - - - - - - - - -- -- - - - - - - - - - - - - - - - *
      * - - - - - - - - - - - - -- - --- --- - -- - - - - - - - - - - - - *
      * - - - - - - - -- - - - -  -  --- ---  -  - - - - -- - - - - - - - *
      * -- - - - - - - - -- - -- -  ---- ----  - -- - -- - - - - - - - -- *
       * - - - -  - - - -- - -- --  ---- ----  -- -- - -- - - -  - - - - *
        * - - - -  - - -  - -- --  ----- -----  -- -- -  - - -  - - - - *
        ** - - - -- - -- -  -  --  ----- -----  --  -  - -- - -- - - - **
         *  - - -- - -- -  -  --   ----- -----   --  -  - -- - -- - -  *
          ** - -  - -- -- --  --  ------ ------  --  -- -- -- -  - - **
            * -- - -- -- --  --   ------ ------   --  -- -- -- - -- *
             ** -  -  -  --  --   ------ ------   --  --  -  -  - **

");
            System.Threading.Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine(@"














");
            Console.ReadLine();
            Console.Clear();
        }
        static string[][,] CreateNewGame()
        { 
            //prgramme permettant de créer les grilles et de les remplir en fonction du premier coup du joueur si une nouvelle partie est créer
            //le programme renvoie un tableau contenant les 2 grilles 
            int[] DimGrille = AskDimGrille();
            if (!stopGame)
            {
                string[,] mainGrille = new string[DimGrille[0], DimGrille[1]];
                string[,] calque = new string[DimGrille[0], DimGrille[1]];
                Console.Clear();
                int[] NoCase = { -2, -2 };
                AfficherGrille(calque ,NoCase );
                int[] positionInspection = entrerInspectionUser(mainGrille,calque);
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
            //programme permettanht de reprendre la partie enregistrer dans le fichier .txt
            // et retourne un tableau contenant les 2 grilles 
            StreamReader game = new StreamReader(link);
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
            game.Close();
            string[][,] BothGrille = { mainGrille, calque };
            return BothGrille;
        }
        static bool AskReloadSave()
        {
            //Programme demandant au joueur s'il veut reprendre la dernière partie enregistré
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
            //programme affichant le message de victoire
            Console.Clear();
            Console.WriteLine(@"                             __   __  __     ______    ______   ______    ______     __  __    
                            /\ \ / / /\ \   /\  ___\  /\__  _\ /\  __ \  /\  == \   /\ \_\ \   
                            \ \ \'/  \ \ \  \ \ \____ \/_/\ \/ \ \ \/\ \ \ \  __<   \ \____ \  
                             \ \_|    \ \_\  \ \_____\   \ \_\  \ \_____\ \ \_\ \_\  \/\_____\ 
                              \/_/     \/_/   \/ ___ /    \/_/   \/ ___ /  \/_/ /_/   \/_____/
");
            Console.ReadLine();
        }
        static void YouLose()
        {
            //programme affichant le message de défaite
            Console.Clear();
            Console.WriteLine(@"                     __  __     ______     __  __        __         ______     ______     ______    
                    /\ \_\ \   /\  __ \   /\ \/\ \      /\ \       /\  __ \   /\  ___\   /\  ___\   
                    \ \____ \  \ \ \/\ \  \ \ \_\ \     \ \ \____  \ \ \/\ \  \ \___  \  \ \  __\   
                     \/\_____\  \ \_____\  \ \_____\     \ \_____\  \ \_____\  \/\_____\  \ \_____\ 
                      \/_____/   \/_____/   \/_____/      \/_____/   \/_____/   \/_____/   \/_____/
");
            Console.ReadLine();
        }
        static void Welcome()
        {
            //programme affichant le message de bienvenue 
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
            //programme affichant le message des règles
            Console.Clear();
            Console.Write(@"Comment jouer?
Tout d'abord choisissez si vous voulez reprendre votre dernière partie, si jamais vous créez une nouvelle partie, entrez les dimensions de la grille.
Pour le premier coup, vous choisissez comme pour la taille de la grille une case en fonction de ses coordonnées: en premier la ligne puis la colonne
A chaque nouveau tour, choisissez une case celle ci sera affichée ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" P ");
            Console.ResetColor();
            Console.Write(@" , vous pouvez soit la dévoiler, soit y poser un drapeau ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" F ");
            Console.ResetColor();
            Console.Write(@" , soit enfin rechoisir une autre case si vous vous êtes trompé.
Tant que vous ne dévoilez pas de bombe la partie continue. Si il y a une bombe ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" B ");
            Console.ResetColor();
            Console.Write(@" à proximité toutes les cases adjacentes se voient attribuer la valeur + 1 (cette valeur est cumulable s’il y a plusieurs bombes).
De même il y a des trésors ");
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" T ");
            Console.ResetColor();
            Console.Write(@" à trouver dans la grille et toutes les cases adjacentes se voient attribuer la valeur + 2(cumulable aussi).
Ainsi s’il y a 2 bombes et 1 trésors à proximité d’une case, celle - ci affiche 4 ");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" 4 ");
            Console.ResetColor();
            Console.Write(@" Enfin un case vide mais dévoillé sera affiché ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("   ");
            Console.ResetColor();
            Console.Write(@" et une case vide non dévoilée ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("   ");
            Console.ResetColor();
            Console.Write(@"\nAprès chaque coup réalisé la partie est enregistrée, si vous quittez vous pourrez donc reprendre votre partie.
Bonne chance !!!");
            Console.ReadLine();
        }
        static void SetAllCasesValues(string[,]mainGrille)
        {
            // programme permettant de remplir entièrement la grille solution avec les valeurs numériques de chaque case
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
            //programme affichant les messages de débuts de game et créant les 2 grilles suivant les réponses du joueur 
            Welcome();
            Rules();
            if (AskReloadSave())
            {
                try
                {
                    return ReloadGame();
                }
                catch(System.ObjectDisposedException)
                {
                    Console.WriteLine("votre save est vide une nouvelle partie va être créé.");
                    Console.WriteLine();
                    return CreateNewGame();

                }
            }
            else if (!stopGame)
            {
                return CreateNewGame();
            }
            return null;
        }
        static void MidGame(string[,] mainGrille, string[,] calque)
        {
            //programme permttant au jeu de se dérouler jusqu'à la victoire, la défaite ou bien l'arret de la partie
            while (!CheckResult(mainGrille, calque) && !stopGame)
            {
                Console.Clear();
                int[] NoCase = { -2, -2 };
                AfficherGrille(mainGrille , NoCase);
                AfficherGrille(calque , NoCase);
                PlayerMove(mainGrille, calque);
                clearSave(mainGrille.Length);
                Serialization(mainGrille,calque);
            } 
        }
        static void EndGame()
        {
            //une fois la partie finis demande si le joueur veut rejouer 
            if (AskReplayGame())
            {
                RePlay();
            }
        }
        static void PlayGame()
        {
            //programme permettant de jouer au jeu
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
            //prgramme permettant une nouvelle partie après la fin de la partie précédente si le joueur veut rejouer
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
            //Programme permettant d'enregistrer la partie en conservant les informations de la partie dans un .txt
            StreamWriter game = new StreamWriter(link);
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
            catch (Exception e)
            {
                Console.WriteLine("Une erreur dans l'enregistrement de votre partie est survenue: " + e.Message);
                game.Dispose();
            }
        }
        static void clearSave(int nbCases)
        {
            StreamReader game = new StreamReader(link);
            for(int i = 0; i < nbCases+2;  i++)
            {
                game.ReadLine();
            }
            game.Close();
        }
    }
}
