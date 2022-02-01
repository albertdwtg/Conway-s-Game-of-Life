using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Quel nombre de lignes souhaitez-vous pour votre matrice ? : ");
            int a = Convert.ToInt32(Console.ReadLine());
            while (a < 1)
            {
                Console.WriteLine("Merci de choisir un nombre de lignes supérieur à 1 : ");
                a = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("Quel nombre de colonnes pour votre matrice ? : ");
            int b = Convert.ToInt32(Console.ReadLine());
            while (b < 1)
            {
                Console.WriteLine("Merci de choisir un nombre de colonnes supérieur à 1 : ");
                b = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("Quel taux de remplissage souhaitez-vous (entre 0,1 et 0,9) ? : ");
            double c = Convert.ToDouble(Console.ReadLine());
            while (c < 0.1 || c > 0.9)
            {
                Console.WriteLine("ERREUR ! Merci de resaisir un remplissage valable : ");
                c = Convert.ToDouble(Console.ReadLine());
            }
            int[,] matrice = CreationMatrice(a, b, c);
            int[,] matrice2 = CreationMatrice2populations(a, b, c);
            Console.WriteLine(" ");
            Console.WriteLine("Entrez 1 si vous vous voulez le jeu DLV classique sans visualisation intermédiaire des états futurs ");
            Console.WriteLine("Entrez 2 si vous vous voulez le jeu DLV classique avec visualisation intermédiaire des états futurs ");
            Console.WriteLine("Entrez 3 si vous vous voulez le jeu DLV variante sans visualisation des états futurs ");
            Console.WriteLine("Entrez 4 si vous vous voulez le jeu DLV variante avec visualisation des états futurs ");
            Console.WriteLine("Entrez 5 si vous voulez quitter le jeu ");
            Console.WriteLine(" ");

            while (true)
            {
                Console.WriteLine(" ");
                Console.Write("Saisir un choix de programme à exécuter ");
                int choix = Convert.ToInt32(Console.ReadLine());
                switch (choix)
                {
                    case 1:
                        Exo1(matrice, 1);
                        break;
                    case 2:
                        Exo2(matrice, 1);
                        break;
                    case 3:
                        Exo3(matrice2, 1);
                        break;
                    case 4:
                        Exo4(matrice2, 1);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }

                Console.ReadKey();
            }

        }
        static void Exo1(int[,] matrice, int génération)
        {

            Console.WriteLine("Ceci est la génération numéro " + génération);
            AfficherMatriceCaractères(matrice);
            int taille = compteur(matrice);
            Console.WriteLine("La taille de la population pour cette génération est : " + taille);
            Console.WriteLine(" ");
            int[,] matrice2 = GrandeMatrice(matrice); //cette matrice place les cellules voisines de chaque cellule quelle que soit sa position
            int[,] matriceBis = changementCellule(matrice2); //cette matrice donne un état mort ou vivant à chaque cellule en fonction des conditions de l'exercice
            int[,] matriceTer = extraireOriginale(matriceBis); //on récupère la matrice d'origine qui est au centre de la matrice 2
            Console.WriteLine("Saisir le prochain programme que vous souhaitez exécuter, puis la touche entrée : ");
            string nb = Console.ReadLine();
            int nombre = int.Parse(nb);//on convertit le choix du joueur en un entier
            if (nombre == 1)
            {
                génération = génération + 1;//on incrémente de 1 la génération à chaque lancement du jeu
                Exo1(matriceTer, génération);//on exécute la version du jeu choisie par le joueur
            }
            if (nombre == 2)
            {
                génération = génération + 1;
                Exo2(matriceTer, génération);
            }
            if (nombre == 3)//on retourne au Main car il faut recréer une matrice à deux populations
            {
                string[] a = { "0 " };
                Console.WriteLine("Vous devez resaisir une matrice et confirmer votre choix de programme ");
                Console.WriteLine(" ");
                Main(a);
            }
            if (nombre == 4)//on retourne au Main car il faut recréer une matrice à deux populations
            {
                string[] a = { "0 " };
                Console.WriteLine("Vous devez resaisir une matrice et confirmer votre choix de programme ");
                Console.WriteLine(" ");
                Main(a);
            }
            if (nombre == 5)
            {
                Environment.Exit(0);
            }
        }
        static void Exo2(int[,] matrice, int génération)
        {
            int[,] matrice2 = GrandeMatrice(matrice);//on place les voisins de chaque cellule quelle que soit sa position
            int[,] matrice3 = étatsFuturs(matrice2);//on étabit l'état présent de chaque cellule, et futur s'il y en a un
            int[,] matriceTer = extraireOriginale(matrice3);//on récupère la matrice après modifications, qui se trouve au centre de la matrice 3
            int[,] matrice4 = changementFuturPrésent(matriceTer);//on actualise l'état de chaque cellule afin que la prochaine génération soit réalisable
            Console.WriteLine(" ");
            Console.WriteLine("Ceci est la génération numéro " + génération);
            AfficherMatriceCaractères(matrice);
            Console.WriteLine(" ");
            Console.WriteLine("Les états futurs pour cette génération sont : ");
            AfficherMatriceCaractères(matriceTer);//on affiche la matrice en changeant les chiffres par les caractères souhaités
            Console.WriteLine(" ");
            int taille = compteurFutur(matriceTer);//on compte le nombre de cellules vivantes dans la matrice (nombre de 1 et de 2)
            Console.WriteLine("La taille de la population pour cette génération est : " + taille);
            Console.WriteLine(" ");
            Console.WriteLine("Saisir le prochain programme que vous souhaitez exécuter, puis la touche entrée :");
            string nb = Console.ReadLine();
            int nombre = int.Parse(nb);//on convertit le choix du joueur en un entier
            if (nombre == 1)
            {
                génération = génération + 1;//on incrémente de 1 la génération à chaque lancement du jeu
                Exo1(matrice4, génération);//on exécute la version du jeu choisie par le joueur
            }
            if (nombre == 2)
            {
                génération = génération + 1;
                Exo2(matrice4, génération);
            }
            if (nombre == 3)//on retourne au Main car il faut recréer une matrice à deux populations
            {
                string[] a = { "0 " };
                Console.WriteLine("Vous devez resaisir une matrice et confirmer votre choix de programme ");
                Console.WriteLine(" ");
                Main(a);
            }
            if (nombre == 4)//on retourne au Main car il faut recréer une matrice à deux populations
            {
                string[] a = { "0 " };
                Console.WriteLine("Vous devez resaisir une matrice et confirmer votre choix de programme ");
                Console.WriteLine(" ");
                Main(a);
            }
            if (nombre == 5)
            {
                Environment.Exit(0);
            }

        }

        static void Exo3(int[,] matrice, int génération)
        {
            AfficherMatriceCaractères2populationsExo3(matrice);
            int[,] matrice2 = ExtraMatrice(matrice); // on créé une matrice plus grande afin que chaque cellule puisse avoir ses 24 voisins à proximité 
            int[,] matriceBis = changementCellule2populations(matrice2); // on modifie notre grande matrice en fonction des règles imposés afin que les cellules évoluent
            int[,] matriceTer = extraireOriginale2populations(matriceBis); // on extrait ensuite notre nouvelle matrice de la même taille que celle créé par le joueur au début
            int population1 = compteur(matrice);
            int population2 = compteur2(matrice);
            Console.WriteLine(" ");
            Console.WriteLine("La taille de la population 1 pour cette génération est : " + population1);
            Console.WriteLine("La taille de la population 2 pour cette génération est : " + population2);
            Console.WriteLine(" ");
            Console.WriteLine("Ceci est la génération numéro " + génération);
            Console.WriteLine(" ");
            Console.WriteLine("Saisir le prochain programme que vous souhaitez exécuter, puis la touche entrée : ");
            string nb = Console.ReadLine();
            int nombre = int.Parse(nb);//on convertit le choix du joueur en un entier
            if (nombre == 1)//on retourne au Main car il faut recréer une matrice à population unique
            {
                string[] a = { "0 " };
                Console.WriteLine("Vous devez resaisir une matrice et confirmer votre choix de programme ");
                Console.WriteLine(" ");
                Main(a);
            }
            if (nombre == 2)//on retourne au Main car il faut recréer une matrice à population unique
            {
                string[] a = { "0 " };
                Console.WriteLine("Vous devez resaisir une matrice et confirmer votre choix de programme ");
                Console.WriteLine(" ");
                Main(a);
            }
            if (nombre == 3)
            {
                génération = génération + 1;//on incrémente de 1 la génération à chaque lancement du jeu
                Exo3(matriceTer, génération);//on exécute la version du jeu choisie par le joueur
            }
            if (nombre == 4)
            {
                génération = génération + 1;
                Exo4(matriceTer, génération);
            }
            if (nombre == 5)
            {
                Environment.Exit(0);
            }

        }

        static void Exo4(int[,] matrice, int génération)
        {
            int[,] matrice2 = ExtraMatrice(matrice);//on place les voisins de chaque cellule quelle que soit sa position
            int[,] matrice3 = etatsFuturs2populations(matrice2);//on étabit l'état présent de chaque cellule, et futur s'il y en a un
            int[,] matriceTer = extraireOriginale2populations(matrice3);//on récupère la matrice après modifications, qui se trouve au centre de la matrice 3
            int[,] matrice4 = changementFuturPrésent2populations(matriceTer);//on actualise l'état de chaque cellule afin que la prochaine génération soit réalisable
            Console.WriteLine(" ");
            Console.WriteLine("Ceci est la génération numéro " + génération);
            AfficherMatriceCaractères2populationsExo3(matrice);//on affiche la matrice en changeant les chiffres par les caractères souhaités
            Console.WriteLine(" ");
            Console.WriteLine("Les états futurs pour cette génération sont : ");
            AfficherMatriceCaractères2populationsExo4(matriceTer);//on affiche la matrice en changeant les chiffres par les caractères souhaités
            Console.WriteLine(" ");
            int population1 = compteurFutur1erepopulation(matriceTer);//on compte le nombre de cellules vivantes dans la matrice (nombre de 1 et de 3)
            int population2 = compteurFutur2emepopulation(matriceTer);//on compte le nombre de cellules vivantes dans la matrice (nombre de 2 et de 4)
            Console.WriteLine("La taille de la population 1 pour cette génération est : " + population1);
            Console.WriteLine("La taille de la population 2 pour cette génération est : " + population2);
            Console.WriteLine(" ");
            Console.WriteLine("Saisir le prochain programme que vous souhaitez exécuter, puis la touche entrée : ");
            string nb = Console.ReadLine();
            int nombre = int.Parse(nb);//on convertit le choix du joueur en un entier
            if (nombre == 1)//on retourne au Main car il faut recréer une matrice à population unique 
            {
                string[] a = { "0 " };
                Console.WriteLine("Vous devez resaisir une matrice et confirmer votre choix de programme ");
                Console.WriteLine(" ");
                Main(a);
            }
            if (nombre == 2)//on retourne au Main car il faut recréer une matrice à population unique
            {
                string[] a = { "0 " };
                Console.WriteLine("Vous devez resaisir une matrice et confirmer votre choix de programme ");
                Console.WriteLine(" ");
                Main(a);
            }
            if (nombre == 3)
            {
                génération = génération + 1;//on incrémente de 1 la génération à chaque lancement du jeu
                Exo3(matrice4, génération);//on exécute la version du jeu choisie par le joueur
            }
            if (nombre == 4)
            {
                génération = génération + 1;
                Exo4(matrice4, génération);
            }
            if (nombre == 5)
            {
                Environment.Exit(0);
            }
        }

        static int[,] CreationMatrice(int lignes, int colonnes, double remplissage)//cette fonction réalise une matrice en fonction des paramètres que souhaite l'utilisateur
        {

            int[,] matrice = new int[lignes, colonnes];
            Random aleatoire = new Random(); //on crée un générateur de nombre aléatoire
            double pourcentage = remplissage * 100;
            double population = remplissage * matrice.Length;
            double précisionPop = Math.Ceiling(population); //z prend la valeur de l'entier supérieur on égal à k
            while (précisionPop != compteur(matrice)) //on compare le nombre de cellules vivantes dans la matrice avec le nombre attendu
            {
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    for (int p = 0; p < matrice.GetLength(1); p++)
                    {
                        int nombreHasard = aleatoire.Next(1, 100); //le j prend une valeur aléatoire entre 1 et 100
                        if (nombreHasard <= pourcentage) //à chaque fois que j est en dessous du pourcentage souhaité, on remplit la matrice par 1 (et inversement avec 0)
                        {
                            matrice[i, p] = 1;

                        }
                        else
                        {
                            matrice[i, p] = 0;
                        }

                    }

                }

            }
            return matrice;
        }

        static void AfficherMatriceCaractères(int[,] matrice)//cette fonction affiche une matrice avec les caractères *; #; .; et - en fonction des conditions du jeu
        {
            if (matrice == null)
            {
                Console.WriteLine("la matrice est nulle");
            }
            else
            {
                for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                    {
                        if (matrice[ligne, colonne] < 10)
                        {
                            Console.Write(" ");
                        }
                        if (matrice[ligne, colonne] == 0)//cas d'une cellule morte
                        {
                            Console.Write(".");
                        }
                        if (matrice[ligne, colonne] == 1) //cas d'une cellule vivante
                        {
                            Console.Write("#");
                        }
                        if (matrice[ligne, colonne] == 2)//cas d'une cellule à mourir
                        {
                            Console.Write("*");
                        }
                        if (matrice[ligne, colonne] == 3)//cas d'une cellule à naître
                        {
                            Console.Write("-");
                        }
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }
        }

        static void AfficherMatriceCaractères2populationsExo4(int[,] matrice)//cette fonction affiche une matrice avec les caractères *; #; .; et - en fonction des conditions du jeu
        {
            if (matrice == null)
            {
                Console.WriteLine("la matrice est nulle");
            }
            else
            {
                for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                    {
                        if (matrice[ligne, colonne] < 10)
                        {
                            Console.Write(" ");
                        }
                        if (matrice[ligne, colonne] == 0)//cas d'une cellule morte
                        {
                            Console.Write(".");
                        }
                        if (matrice[ligne, colonne] == 1) //cas d'une cellule vivante de la population 1
                        {
                            Console.Write("#");
                        }
                        if (matrice[ligne, colonne] == 2) //cas d'une cellule vivante de la population 2
                        {
                            Console.Write("%");
                        }

                        if (matrice[ligne, colonne] == 3 || matrice[ligne, colonne] == 4)//cas d'une cellule à mourir de la population 1 et de la population 2
                        {
                            Console.Write("*");
                        }
                        if (matrice[ligne, colonne] == 5 || matrice[ligne, colonne] == 6)//cas d'une cellule à naître de la population 1 et 2
                        {
                            Console.Write("-");
                        }
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }
        }
        static int compteur(int[,] matrice)//cette fonction retourne le nombre de 1 dans une matrice
        {
            int comptage = 0;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int a = 0; a < matrice.GetLength(1); a++)
                {
                    if (matrice[i, a] == 1)//à chaque fois qu'on lit 1, on incrémente le compteur de 1
                    {
                        comptage = comptage + 1;
                    }

                }
            }
            return comptage;
        }
        static int[,] GrandeMatrice(int[,] matrice)//cette fonction retourne une matrice où les cellules sur le bord on désormais leurs voisins assignés
        {
            int[,] finale = new int[matrice.GetLength(0) + 2, matrice.GetLength(1) + 2];
            int lignes = 0;
            int colonnes2 = 0;

            for (int i = 1; i < finale.GetLength(0) - 1; i++)
            {
                int colonnes = 0;
                for (int a = 1; a < finale.GetLength(1) - 1; a++)// on copie les valeurs de la matrice d'origine pour les mettre au centre de la matrice finale
                {
                    finale[i, a] = matrice[lignes, colonnes];
                    colonnes++;

                }
                lignes++;
            }
            for (int q = 1; q < finale.GetLength(1) - 1; q++)//q est le numéro des colonnes et on le fait varier
            {
                finale[0, q] = matrice[matrice.GetLength(0) - 1, colonnes2];//on copie les valeurs de la dernière ligne de la matrice d'origine sur la première ligne de la matrice finale
                finale[finale.GetLength(0) - 1, q] = matrice[0, colonnes2];//on copie les valeurs de la première ligne de la matrice d'origine sur la dernière ligne de la matrice finale
                colonnes2++;
            }
            for (int s = 1; s < finale.GetLength(0) - 1; s++)//s est le numéro des lignes et on le fait varier
            {
                finale[s, 0] = matrice[s - 1, matrice.GetLength(1) - 1];//on copie les valeurs de la dernière colonne de la matrice d'origine sur la première colonne de la matrice finale
                finale[s, finale.GetLength(1) - 1] = matrice[s - 1, 0];//on copie les valeurs de la première colonne de la matrice d'origine sur la dernière colonne de la matrice finale 

            }
            finale[0, 0] = matrice[matrice.GetLength(0) - 1, matrice.GetLength(1) - 1]; //ces quatre lignes assignent les voisins pour les cellules se trouvant dans les 4 angles
            finale[0, finale.GetLength(1) - 1] = matrice[matrice.GetLength(0) - 1, 0];
            finale[finale.GetLength(0) - 1, 0] = matrice[0, matrice.GetLength(1) - 1];
            finale[finale.GetLength(0) - 1, finale.GetLength(1) - 1] = matrice[0, 0];
            return finale;
        }
        static int[,] matriceCellulesVoisines(int[,] matrice, int lignes, int colonnes)//cette fonction réalise une mini-matrice composée des voisins d'une certaine cellule
        {
            int[,] matrice2 = new int[3, 3]; //une cellule a huit voisins, la fonction doit donc retourner une matrice contenant neuf cases
            int lignes2 = 0;
            for (int i = lignes - 1; i <= lignes + 1; i++) //ce "for" est possible car on va appliquer cette fonction qu'au centre de la GrandeMatrice, où se situe la matrice originale
            {
                int colonnes2 = 0;
                for (int a = colonnes - 1; a <= colonnes + 1; a++)
                {
                    matrice2[lignes2, colonnes2] = matrice[i, a];
                    colonnes2++;
                }
                lignes2++;
            }
            return matrice2;//la matrice retourne les huits cases voisines de la cellule ainsi que la cellule elle-même
        }
        static int[,] extraireOriginale(int[,] matrice)// cette fonction permet de récupérer le centre de la grande matrice après modification des cellules, où se situe la matrice d'origine
        {
            int lignes = 0;
            int[,] matrice2 = new int[matrice.GetLength(0) - 2, matrice.GetLength(1) - 2];//on enlève deux lignes et deux colonnes pour retrouver la taille de la matrice d'origine
            for (int i = 1; i < matrice.GetLength(0) - 1; i++)//on retire la première et la dernière ligne
            {
                int colonnes = 0;
                for (int a = 1; a < matrice.GetLength(1) - 1; a++)//on retire la première et la dernière colonne
                {
                    matrice2[lignes, colonnes] = matrice[i, a];
                    colonnes++;
                }
                lignes++;
            }
            return matrice2;//on retourne la matrice qui se trouvait au centre de la grande matrice

        }
        static int[,] étatsFuturs(int[,] matrice)//fonction qui donne un état à chaque cellule en fonction des conditions du jeu
        {
            int[,] matrice2 = copieMatrice(matrice);
            for (int i = 1; i < matrice.GetLength(0) - 1; i++)//avec ces deux "for" on lit seulement le centre de la grande matrice où se trouve la matrice d'origine 
            {
                for (int a = 1; a < matrice.GetLength(1) - 1; a++)
                {
                    int[,] voisins = matriceCellulesVoisines(matrice, i, a); //on relève la matrice des voisins pour chaque cellule
                    if (matrice[i, a] == 1)//on relève les cellules vivantes et on applique les deux conditions du jeu
                    {
                        if (compteur(voisins) - 1 < 2) //On retire 1 car la cellule qu'on traite est déjà un 1 elle-même 
                        {
                            matrice2[i, a] = 2; //cas d'une cellule vivante à mourir (sous-population)
                        }
                        if (compteur(voisins) - 1 > 3)
                        {
                            matrice2[i, a] = 2;//cas d'une cellule vivante à mourir (sur-population)
                        }

                    }
                    if (matrice[i, a] == 0)//on relève les cellules mortes
                    {
                        if (compteur(voisins) == 3)
                        {
                            matrice2[i, a] = 3;//cas d'une cellule morte à naître
                        }
                    }
                }
            }
            return matrice2;
        }
        static int[,] changementCellule(int[,] matrice)//fonction permettant de déterminer l'état d'une cellule selon les conditions de l'exercice
        {
            int[,] matrice2 = copieMatrice(matrice);
            for (int i = 1; i < matrice.GetLength(0) - 1; i++)
            {
                for (int a = 1; a < matrice.GetLength(1) - 1; a++)//on lit que les cellules du centre de la grande matrice car c'est ici que se trouve la matrice d'origine
                {
                    if (matrice[i, a] == 1)
                    {
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 < 2)//on lit le nombre de voisins de la cellule étant un 1 (en soustrayant 1 car la cellule elle-même est un 1 dans ce if) 
                        {
                            matrice2[i, a] = 0;
                        }
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 > 3)//idem que précedemment
                        {
                            matrice2[i, a] = 0;
                        }
                    }
                    if (matrice[i, a] == 0)
                    {
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) == 3)//on lit le nombre de voisins de la cellule étant un 1 sans soustraire 1 car la cellule est morte dans ce if
                        {
                            matrice2[i, a] = 1;
                        }
                    }
                }
            }
            return matrice2;
        }
        static int[,] copieMatrice(int[,] matrice)//fonction permettant de dupliquer les valeurs d'une matrice dans une autre
        {
            int[,] matrice2 = new int[matrice.GetLength(0), matrice.GetLength(1)];
            for (int j = 0; j < matrice.GetLength(0); j++)
            {
                for (int k = 0; k < matrice.GetLength(1); k++)//on lit chaque valeur de la matrice initiale
                {
                    matrice2[j, k] = matrice[j, k];//on copie ces valeurs dans une matrice de taille identique qui sera retournée
                }
            }
            return matrice2;


        }
        static int[,] changementFuturPrésent(int[,] matrice)//cette matrice de rétablir l'état des cellules après les prédictions
        {
            int[,] matrice2 = copieMatrice(matrice);
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int a = 0; a < matrice.GetLength(1); a++)
                {
                    if (matrice[i, a] == 2)//les cellules qui étaient "à mourir" deviennent mortes
                    {
                        matrice2[i, a] = 0;
                    }
                    if (matrice[i, a] == 3)//les cellules qui étaient à "à naître" deviennent vivantes
                    {
                        matrice2[i, a] = 1;
                    }
                }
            }
            return matrice2;
        }
        static int compteurFutur(int[,] matrice)//fonction permettant d'établir le nombre de cellules vivantes dans le cas où on a des états futurs sont annoncés
        {
            int comptage = 0;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int a = 0; a < matrice.GetLength(1); a++)
                {
                    if (matrice[i, a] == 1 || matrice[i, a] == 2)//on comptabilise les cellules vivantes ainsi que les cellules "à mourir" qui sont encore vivantes
                    {
                        comptage = comptage + 1;
                    }

                }
            }
            return comptage;//on retourne le nombre de cellules vivantes
        }

        static int[,] CreationMatrice2populations(int lignes, int colonnes, double remplissage) // fonction permettant de créer une matrice aléatoire où cohabite 2 populations distinctes
        {

            int[,] matrice2 = new int[lignes, colonnes];
            Random aleatoire = new Random(); // création d'une fonction aléatoire
            double pourcentage = remplissage * 100;
            double pourcentage2 = pourcentage / 2; // on se sert de ce pourcentage pour attribuer aléatoirement les cellules vivantes sur toute la matrice
            double population = remplissage * matrice2.Length;
            double moitiéPop = population / 2;
            double précisionMoitié = Math.Floor(moitiéPop); // renvoie un nombre entier inférieur ou égal au nombre demandé pour le remplissage de la matrice
            while (compteur(matrice2) != précisionMoitié) // on remplit la matrice jusqu'à ce que le nombre de cellules vivantes de la population 1 demandé soit atteint
            {
                for (int i = 0; i < matrice2.GetLength(0); i++)
                {
                    for (int p = 0; p < matrice2.GetLength(1); p++)
                    {
                        int nombreHasard = aleatoire.Next(1, 100); // création d'un nombre aléatoire entre 1 et 99
                        if (nombreHasard <= pourcentage2)
                        {
                            matrice2[i, p] = 1; // cellule vivante de la population 1

                        }
                        else
                        {
                            matrice2[i, p] = 0; // cellule morte
                        }

                    }

                }

            }
            while (compteur2(matrice2) != précisionMoitié) // on remplit la matrice jusqu'à ce que le nombre de cellules vivantes de la population 2 demandé soit atteint
            {
                for (int i = 0; i < matrice2.GetLength(0); i++)
                {
                    for (int p = 0; p < matrice2.GetLength(1); p++)
                    {
                        int nombreHasard2 = aleatoire.Next(1, 100); // création d'un nombre entre 1 et 99
                        if (matrice2[i, p] != 1)
                        {
                            if (nombreHasard2 <= pourcentage2)
                            {

                                matrice2[i, p] = 2; // cellule vivante de la population 2
                            }
                            else
                            {
                                matrice2[i, p] = 0; // cellule morte
                            }
                        }

                    }

                }

            }
            return matrice2;
        }

        static void AfficherMatriceCaractères2populationsExo3(int[,] matrice) // fonction permettant d'afficher la matrice avec les 2 populations
        {
            if (matrice == null)
            {
                Console.WriteLine("la matrice est nulle");
            }
            else
            {
                for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                    {
                        if (matrice[ligne, colonne] < 10)
                        {
                            Console.Write(" ");
                        }
                        if (matrice[ligne, colonne] == 0)
                        {
                            Console.Write("."); // signe pour afficher une cellule morte
                        }
                        if (matrice[ligne, colonne] == 1)
                        {
                            Console.Write("#"); // signe pour afficher une cellule vivante de la population 1
                        }
                        if (matrice[ligne, colonne] == 2)
                        {
                            Console.Write("%"); // signe pour afficher une cellule vivante de la population 2
                        }
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }
        }

        static int compteur2(int[,] matrice) // fonction permettant de compter le nombre de cellule vivante de la population 2 dans une matrice
        {
            int comptage = 0;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int a = 0; a < matrice.GetLength(1); a++)
                {
                    if (matrice[i, a] == 2)
                    {
                        comptage = comptage + 1;
                    }

                }
            }
            return comptage;
        }

        static int[,] matriceCellulesVoisines2populations(int[,] matrice, int lignes, int colonnes) // fonction qui renvoie une matrice avec les 24 voisins d'une cellule au cas par cas
        {
            int[,] matrice2 = new int[5, 5]; // on créé matrice de taille 25 avec la cellule au milieu et les 24 voisins autour
            int lignes2 = 0;
            for (int i = lignes - 2; i <= lignes + 2; i++)
            {
                int colonnes2 = 0;
                for (int a = colonnes - 2; a <= colonnes + 2; a++)
                {
                    matrice2[lignes2, colonnes2] = matrice[i, a];
                    colonnes2++;
                }
                lignes2++;
            }
            return matrice2;
        }

        static int[,] extraireOriginale2populations(int[,] matrice) // cette fonction permet de récupérer le centre de la grande matrice après modification des cellules, où se situe la matrice d'origine
        {
            int lignes = 0;
            int[,] matrice2 = new int[matrice.GetLength(0) - 4, matrice.GetLength(1) - 4]; // on enlève 4 lignes et 4 colonnes pour retrouver la taille initiale de la matrice
            for (int i = 2; i < matrice.GetLength(0) - 2; i++)
            {
                int colonnes = 0;
                for (int a = 2; a < matrice.GetLength(1) - 2; a++)
                {
                    matrice2[lignes, colonnes] = matrice[i, a];
                    colonnes++;
                }
                lignes++;
            }
            return matrice2;

        }

        static int[,] changementCellule2populations(int[,] matrice) // cette fonction permet de définir les cellules pour chaque génération qui s'enchaîne
        {
            int[,] matrice2 = copieMatrice(matrice);
            for (int i = 2; i < matrice.GetLength(0) - 2; i++)
            {
                for (int a = 2; a < matrice.GetLength(1) - 2; a++)
                {
                    if (matrice[i, a] == 1)
                    {
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 < 2) //si une cellule vivante est entouré de moins de deux cellules vivantes alors elle meurt à la génération suivante
                        {
                            matrice2[i, a] = 0;
                        }
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 > 3) //si une cellule vivante est entouré de plus de tois cellules vivantes alors elle meurt à la génération suivante
                        {
                            matrice2[i, a] = 0;
                        }
                    }
                    if (matrice[i, a] == 2)
                    {
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 < 2) // idem pour la population 2
                        {
                            matrice2[i, a] = 0;
                        }
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 > 3)
                        {
                            matrice2[i, a] = 0;
                        }
                    }
                    if (matrice[i, a] == 0)
                    {
                        if (compteur2(matriceCellulesVoisines(matrice, i, a)) == 3 && compteur(matriceCellulesVoisines(matrice, i, a)) != 3) //si une cellule morte est entouré de 3 cellules vivantes de la population 2 alors elle vit dans cette population
                        {
                            matrice2[i, a] = 2;
                        }
                        if (compteur2(matriceCellulesVoisines(matrice, i, a)) != 3 && compteur(matriceCellulesVoisines(matrice, i, a)) == 3)  //si une cellule morte est entouré de 3 cellules vivantes de la population 1 alors elle vit dans cette population
                        {
                            matrice2[i, a] = 1;
                        }
                        if (compteur2(matriceCellulesVoisines(matrice, i, a)) == 3 && compteur(matriceCellulesVoisines(matrice, i, a)) == 3)  //si une cellule morte est entouré de 3 cellules vivantes de la population 2 et de la population 1 alors on regarde d'autres conditions
                        {
                            if (compteur2(matriceCellulesVoisines2populations(matrice, i, a)) > compteur(matriceCellulesVoisines2populations(matrice, i, a)))
                            {
                                matrice2[i, a] = 2; //on regarde qui a le plus de voisins parmis les 24, si c'est la population 2 alors la cellule naît dans la population 2
                            }
                            if (compteur2(matriceCellulesVoisines2populations(matrice, i, a)) < compteur(matriceCellulesVoisines2populations(matrice, i, a)))
                            {
                                matrice2[i, a] = 1; //on regarde qui a le plus de voisins parmis les 24, si c'est la population 1 alors la cellule naît dans la population 1
                            }
                            else
                            {
                                if (compteur(matrice) > compteur2(matrice)) // si il y a toujours égalité on compte donc le nombre total de cellules vivantes dans toute la matrice parmis les 2 populations
                                {
                                    matrice2[i, a] = 1;
                                }
                                if (compteur(matrice) < compteur2(matrice))
                                {
                                    matrice2[i, a] = 2;
                                }
                                if (compteur(matrice) == compteur2(matrice)) // si il y a encore égalité, la cellule reste morte
                                {
                                    matrice2[i, a] = 0;
                                }
                            }

                        }
                    }
                }
            }
            return matrice2;
        }

        static int[,] etatsFuturs2populations(int[,] matrice)
        {
            int[,] matrice2 = copieMatrice(matrice);
            for (int i = 2; i < matrice.GetLength(0) - 2; i++)
            {
                for (int a = 2; a < matrice.GetLength(1) - 2; a++)
                {
                    if (matrice[i, a] == 1)
                    {
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 < 2)
                        {
                            matrice2[i, a] = 3;
                        }
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 > 3)
                        {
                            matrice2[i, a] = 3;
                        }
                    }
                    if (matrice[i, a] == 2)
                    {
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 < 2)
                        {
                            matrice2[i, a] = 4;
                        }
                        if (compteur(matriceCellulesVoisines(matrice, i, a)) - 1 > 3)
                        {
                            matrice2[i, a] = 4;
                        }
                    }
                    if (matrice[i, a] == 0)
                    {
                        if (compteur2(matriceCellulesVoisines(matrice, i, a)) == 3 && compteur(matriceCellulesVoisines(matrice, i, a)) != 3)
                        {
                            matrice2[i, a] = 6;
                        }
                        if (compteur2(matriceCellulesVoisines(matrice, i, a)) != 3 && compteur(matriceCellulesVoisines(matrice, i, a)) == 3)
                        {
                            matrice2[i, a] = 5;
                        }
                        if (compteur2(matriceCellulesVoisines(matrice, i, a)) == 3 && compteur(matriceCellulesVoisines(matrice, i, a)) == 3)
                        {
                            if (compteur2(matriceCellulesVoisines2populations(matrice, i, a)) > compteur(matriceCellulesVoisines2populations(matrice, i, a)))
                            {
                                matrice2[i, a] = 6;
                            }
                            if (compteur2(matriceCellulesVoisines2populations(matrice, i, a)) < compteur(matriceCellulesVoisines2populations(matrice, i, a)))
                            {
                                matrice2[i, a] = 5;
                            }
                            else
                            {
                                if (compteur(matrice) > compteur2(matrice))
                                {
                                    matrice2[i, a] = 5;
                                }
                                if (compteur(matrice) < compteur2(matrice))
                                {
                                    matrice2[i, a] = 6;
                                }
                                if (compteur(matrice) == compteur2(matrice))
                                {
                                    matrice2[i, a] = 3;
                                }
                            }

                        }
                    }
                }
            }
            return matrice2;
        }

        static int[,] changementFuturPrésent2populations(int[,] matrice)//cette matrice permet de rétablir l'état des cellules après les prédictions
        {
            int[,] matrice2 = copieMatrice(matrice);
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int a = 0; a < matrice.GetLength(1); a++)
                {
                    if (matrice[i, a] == 3)//les cellules qui étaient "à mourir" dans la population 1 deviennent mortes
                    {
                        matrice2[i, a] = 0;
                    }
                    if (matrice[i, a] == 4)//les cellules qui étaient "à mourir" dans la population 2 deviennent mortes
                    {
                        matrice2[i, a] = 0;
                    }
                    if (matrice[i, a] == 5)//les cellules qui étaient à "à naître" deviennent vivantes dans la population 1
                    {
                        matrice2[i, a] = 1;
                    }
                    if (matrice[i, a] == 6)//les cellules qui étaient à "à naître" deviennent vivantes dans la population 2
                    {
                        matrice2[i, a] = 2;
                    }
                }
            }
            return matrice2;
        }

        static int[,] ExtraMatrice(int[,] matrice) // cette fonction reprend la fonction Grande Matrice sauf que celle-ci permet de se servir des 24 voisins d'une cellule
        {

            int lignes = 0;
            int lignes2 = 0;
            int colonnes2 = 0;
            int[,] finale = new int[matrice.GetLength(0) + 4, matrice.GetLength(1) + 4]; // on créé une matrice avec 4 lignes et 4 colonnes de plus que la matrice demandé
            int[,] intermédaire = GrandeMatrice(matrice);
            for (int y = 1; y < finale.GetLength(0) - 1; y++)
            {
                int colonnes = 0;
                for (int z = 1; z < finale.GetLength(1) - 1; z++)
                {
                    finale[y, z] = intermédaire[lignes, colonnes];
                    colonnes++;
                }
                lignes++;
            }
            for (int i = 1; i < finale.GetLength(0) - 1; i++)
            {
                finale[i, 0] = intermédaire[lignes2, intermédaire.GetLength(1) - 3];
                finale[i, finale.GetLength(1) - 1] = intermédaire[lignes2, 2];
                lignes2++;
            }
            for (int a = 1; a < finale.GetLength(1) - 1; a++)
            {
                finale[0, a] = intermédaire[intermédaire.GetLength(0) - 3, colonnes2];
                finale[finale.GetLength(0) - 1, a] = intermédaire[2, colonnes2];
                colonnes2++;
            }
            finale[0, 0] = intermédaire[intermédaire.GetLength(0) - 3, intermédaire.GetLength(1) - 3];
            finale[finale.GetLength(0) - 1, 0] = intermédaire[2, intermédaire.GetLength(1) - 3];
            finale[0, finale.GetLength(1) - 1] = intermédaire[intermédaire.GetLength(0) - 3, 2];
            finale[finale.GetLength(0) - 1, finale.GetLength(1) - 1] = intermédaire[2, 2];
            return finale;
        }

        static int compteurFutur1erepopulation(int[,] matrice)//fonction permettant d'établir le nombre de cellules vivantes dans le cas où on a des états futurs sont annoncés
        {
            int comptage = 0;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int a = 0; a < matrice.GetLength(1); a++)
                {
                    if (matrice[i, a] == 1 || matrice[i, a] == 3)//on comptabilise les cellules vivantes ainsi que les cellules "à mourir" qui sont encore vivantes de la population 1
                    {
                        comptage = comptage + 1;
                    }

                }
            }
            return comptage;//on retourne le nombre de cellules vivantes
        }

        static int compteurFutur2emepopulation(int[,] matrice)//fonction permettant d'établir le nombre de cellules vivantes dans le cas où des états futurs sont annoncés
        {
            int comptage = 0;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int a = 0; a < matrice.GetLength(1); a++)
                {
                    if (matrice[i, a] == 2 || matrice[i, a] == 4)//on comptabilise les cellules vivantes ainsi que les cellules "à mourir" qui sont encore vivantes de la population 2
                    {
                        comptage = comptage + 1;
                    }

                }
            }
            return comptage;//on retourne le nombre de cellules vivantes de la population 2
        }

    }
}

