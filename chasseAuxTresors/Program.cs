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
            
        }
        static int NbLigne = 5;
        static int NbColonne = 6;
        static string[,] GrilleAll = new string[NbLigne,NbColonne];

        static void creerBombesTresors(int entreeLigne1 , int entreeColonne1)
        {
            Random RdNumber = new Random();
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
    }
}
