//programme creant une matrice rempli d'espaces de taille choisi par l'utilisateur
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

//programme créant une matrice remplie de 0
int[,] MatriceEntiers(int taille)
{
    int[,] matriceJeu = new int[taille, taille];
    for (int i = 0; i < taille; i++)
    {
        for (int j = 0; j < taille; j++)
        {
            matriceJeu[i, j] = 0;   //on parcourt tout le tableau et on met un 0 en chaque position 
        }
    }
    return matriceJeu;
}


//programme placant un 2 dans la matrice sur une position aléatoire
int[,] SymboleMatrice2(int[,] matrice)
{
    if (VerificationMatrice(matrice) == true)
    {
        return matrice;
    }
    Random aleatoire = new Random();
    int ligne = aleatoire.Next(0, matrice.GetLength(0));        //on donne un indice aléatoire pour la ligne compris entre 0 et (la taille de la matrice)-1
    int colonne = aleatoire.Next(0, matrice.GetLength(1));      //on donne un indice aléatoire pour la colonne compris entre 0 et (la taille de la matrice)-1
    while (matrice[ligne, colonne] != 0)                        //on verifie si cette position ne soit pas déjà occupée 
    {
        ligne = aleatoire.Next(0, matrice.GetLength(0));        //si elle est prise on change les indices de ligne et de colonne
        colonne = aleatoire.Next(0, matrice.GetLength(1));
    }
    matrice[ligne, colonne] = 2;                                //on introduit "un bonbon"
    return matrice;
}


//programme affichant la matrice avec un joli design
void AfficherMatrice(char[,] tab)
{
    Console.WriteLine("+--+--+--+--+");
    for (int i = 0; i < tab.GetLength(0); i++)
    {
        for (int j = 0; j < tab.GetLength(1); j++)
        {
            Console.Write($"|{tab[i, j]} ");                    //on affiche le début de la grille de jeu
            if (j == tab.GetLength(1) - 1)
            {   
                Console.Write("|");                              //on affiche la dernière barre de la grille du jeu
                Console.WriteLine();

            }  
        }

        Console.WriteLine("+--+--+--+--+");                        
    }
}


//programme du jeu
Console.WriteLine();
Console.WriteLine("Bienvenue sur 🍭🍭🍭CANDYMIX🍭🍭🍭");
Console.WriteLine("Connaissez-vous les règles du jeu ? Repondez par oui ou non.");
string reponse = Console.ReadLine()!;

if (reponse == "non" || reponse == "Non")
{
    Console.WriteLine("Le but du jeu est de déplacer les bonbons dans la grille du jeu afin qu'ils se rencontrent et se transforment dans le treat supérieur! Après chaque coup joué, un nouveau bonbon est introduit dans la grille. Le jeu s'arrête une fois que vous avez atteint le nombre de coups maximum ou lorsque la grille est remplie de bonbons. Facile! Maintenant, à vous de jouer !");
}

else
{
    Console.WriteLine("✨Parfait, c'est parti !✨");
}

Console.WriteLine();
Console.WriteLine("🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄🍭🎄");
Console.WriteLine("Choissisez le nombre de coups autorisés:");
int nbCoups = Convert.ToInt32(Console.ReadLine()!);
Console.WriteLine($"✨Vous avez choisi de jouer en {nbCoups} coups, c'est parti !✨");
Random aleatoire = new Random();
int[,] matriceDeJeuEntiers = SymboleMatrice2(MatriceEntiers(4));    //on place le premier bonbon dans une case aléatoire
matriceDeJeuEntiers = SymboleMatrice2(matriceDeJeuEntiers);         //on place le deuxième bonbon dans une case aléatoire
Console.WriteLine();
Console.WriteLine("Voici votre plateau de jeu de départ");
AfficherMatrice(ConversionMatrice(matriceDeJeuEntiers));          //affichage de la matrice de départ
for (int i = 0; i < nbCoups; i++)
{
    int deplacement;
    do                                                        //boucle qui permet de réafficher la consigne si l'utilisation des commandes n'a pas ete respectee 
    {
        Console.WriteLine();
        Console.WriteLine("Déplacez les bonbons grâce aux touches 8(↑), 4(<--), 2(↓) et 6(→):");
        deplacement = Convert.ToInt32(Console.ReadLine()!);        //on convertit les données rentrées par l'utilisateur en entier
    } while (deplacement != 8 && deplacement != 4 && deplacement != 6 && deplacement != 2);

    switch (deplacement)
    {
        case 8:
        //chercher cases ou se trouvent les bonbons et les deplacer le plus proche possible de la position [0,j]
        MoveUp(matriceDeJeuEntiers);
        break;

        case 4:
        //chercher cases ou se trouvent les bonbons et les deplacer le plus proche possible de la position [i,0]
        MoveLeft(matriceDeJeuEntiers);
        break;

        case 2:
        //chercher cases ou se trouvent les bonbons et les deplacer le plus proche possible de la position [3,j]
        MoveDown(matriceDeJeuEntiers);
        break;

        case 6:
        //chercher cases ou se trouvent les bonbons et les deplacer le plus proche possible de la position [i,3]
        MoveRight(matriceDeJeuEntiers);
        break;

        default:
        break;
    }
    Console.WriteLine();
    Console.WriteLine($"Tour numero {i}");                      //affiche le nombre de tour auquel le joueur en est 
    Console.WriteLine("-----------------");
    AfficherMatrice(ConversionMatrice(SymboleMatrice2(matriceDeJeuEntiers)));       //on affiche la matrice après avoir déplacé les bonbons
    if (VerificationMatrice(matriceDeJeuEntiers) == true)                           //on verifie si il y a un blocage dans la grille
    {
        Console.WriteLine();
        Console.WriteLine("Fin de la partie. Le plateau de jeu est remplie de bonbons ... Rejouez et faites mieux !");
    }   
}
Console.WriteLine();
Console.WriteLine("Fin de la partie 😢");
Console.WriteLine("VOUS AVEZ ATTEINT VOTRE NOMBRE DE COUPS MAXIMALE.");
Console.WriteLine();
Console.WriteLine("Mais puisque c'est bientôt 🎄Noël🎄, nous avons une surprise pour vous....");
Console.WriteLine("ouiSi vous voulez ajouter des coups, vous pouvez! Si vous en voulez repondez par oui, sinon repondez par non.");
string coupSup= Console.ReadLine()!;
if (coupSup=="oui" || coupSup=="Oui")
{
    Console.WriteLine("Pour gagner des coups supplémentaires, repondez à l'enigme suivante....");
    Console.WriteLine("Girafe = 3, Éléphant = 3, Hippopotame = 5, Lion = ... ?");  
}
else 
{
    Console.WriteLine("Dommage... Refaites une partie et améliorez votre score !");
}

int enigme=Convert.ToInt32(Console.ReadLine()!);
if (enigme==2)
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("⭐️⭐️⭐️Bien joué⭐️⭐️⭐️");
    Console.WriteLine("Vous avez obtenu 5 coups supplémentaires.");
    AfficherMatrice(ConversionMatrice(matriceDeJeuEntiers));
        for (int j = 0; j < 5; j++)
        {
            int deplacement=0;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Déplacez les bonbons grâce aux touches 8(↑), 4(<--), 2(↓) et 6(→):");
                deplacement = Convert.ToInt32(Console.ReadLine()!);        //on convertit les données rentrées par l'utilisateur en entier
            } while (deplacement != 8 && deplacement != 4 && deplacement != 6 && deplacement != 2);

            switch (deplacement)        
            {
                case 8:
                //chercher cases ou se trouvent les bonbons et les deplacer le plus proche possible de la position [0,j]
                MoveUp(matriceDeJeuEntiers);
                break;

                case 4:
                //chercher cases ou se trouvent les bonbons et les deplacer le plus proche possible de la position [i,0]
                MoveLeft(matriceDeJeuEntiers);
                break;

                case 2:
                //chercher cases ou se trouvent les bonbons et les deplacer le plus proche possible de la position [3,j]
                MoveDown(matriceDeJeuEntiers);
                break;

                default:
                Console.WriteLine("❌Nombre rentré faux❌");
                break;
            }
            Console.WriteLine();
            AfficherMatrice(ConversionMatrice(SymboleMatrice2(matriceDeJeuEntiers)));       //on affiche la matrice après avoir déplacé les bonbons
            if (VerificationMatrice(matriceDeJeuEntiers) == true)                           //on verifie si il y a un blocage dans la grille
            {
                Console.WriteLine();
                Console.WriteLine("Fin du jeu. Il y a un blocage dans la grille.");
                break;
            }
        }
        Console.WriteLine();
        Console.WriteLine("Vous avez atteint votre nombre de coups maximale, la partie est finie.");      
        Console.WriteLine("Rejouez et faites un meilleur score !");  
 }   


else
{
    Console.WriteLine();
    Console.WriteLine("Bien tenté mais c'est la mauvaise réponse. Recommencez une nouvelle partie!");
    Console.WriteLine("----------------------------------------------------------------------------");
}
Console.WriteLine();
Console.WriteLine("🍭Fin de la partie🍭");      



// programme qui permet de faire le deplacement en haut
void MoveUp(int[,] tab)
{
    for (int c = 0; c < tab.GetLength(1); c++)
    {
        for (int l = 1; l < tab.GetLength(0); l++)
        {
            if (tab[l, c] != 0)
            {
                int ligne = l;
                while (ligne > 0 && tab[ligne - 1, c] == 0)
                {
                    tab[ligne - 1, c] = tab[ligne, c];          //on déplace le bonbon d'une ligne vers le haut
                    tab[ligne, c] = 0;                          //on met la case initiale du bonbon à 0
                    ligne--;                                    //on met l'indice de la ligne de ou se situe le bonbon actuellement dans la variable 'ligne'
                }

                if (ligne > 0 && tab[ligne - 1, c] == tab[ligne, c] && tab[ligne - 1, c] != 16)
                {
                    tab[ligne - 1, c] *= 2;          //si les deux entiers cote a cote sont egaux et sont différent de 'J', alors on les "rassemble"
                    tab[ligne, c] = 0;              //on met la case précendente à 0
                }
            }
        }
    }
}


//programme qui permet de faire le deplacement vers la gauche
void MoveLeft(int[,] tab)
{
    for (int c = 1; c < tab.GetLength(1); c++)
    {
        for (int l = 0; l < tab.GetLength(0); l++)
        {
            if (tab[l, c] != 0)
            {
                int colonne = c;
                while (colonne > 0 && tab[l, colonne - 1] == 0)
                {
                    tab[l, colonne - 1] = tab[l, colonne];      //on deplace l'entier d'une colonne vers la gauche 
                    tab[l, colonne] = 0;
                    colonne--;
                }
                if (colonne > 0 && tab[l, colonne - 1] == tab[l, colonne] && tab[l, colonne - 1] != 16)
                {
                    tab[l, colonne - 1] *= 2;           //si les deux entiers cote a cote sont egaux et sont pas egal a 'J', alors on les "rassemble"
                    tab[l, colonne] = 0;       
                }
            }
        }
    }
}


//programme qui permet le deplacement vers la droite
void MoveRight(int[,] tab)
{
    for (int c = 0; c < tab.GetLength(1)-1; c++)
    {
        for (int l = 0; l < tab.GetLength(0); l++)
        {
            if (tab[l, c] != 0)
            {
                int colonne = c;
                while (colonne < 3 && tab[l, colonne + 1] == 0)
                {
                    tab[l, colonne + 1] = tab[l, colonne];      //on deplace l'entier d'une colonne vers la droite
                    tab[l, colonne] = 0;                        //on met la position du bonbon dans la colonne d'avant a 0
                    colonne++;                                  //on refait la meme avec les deux colonnes suivantes doncon augmente l'indice de la colonne
                }

                if (colonne < 3 && tab[l, colonne + 1] == tab[l, colonne] && tab[l, colonne + 1] != 16)
                {
                    tab[l, colonne + 1] *= 2;    //si les deux entiers cote a cote sont egaux et ne sont pas le plus grand bonbon possible, alors on les "rassemble"
                    tab[l, colonne] = 0; 
                }
            }
        }
    }
}


//programme qui permet le deplacement vers le bas
void MoveDown(int[,] tab)
{
    for (int c = 0; c < tab.GetLength(1); c++)
    {
        for (int l = 0; l < tab.GetLength(0)-1; l++)
        {
            if (tab[l, c] != 0)
            {
                int ligne = l;
                while (ligne < 3 && tab[ligne + 1, c] == 0)
                {
                    tab[ligne + 1, c] = tab[ligne, c];  //on deplace le bonbon une ligne au dessus
                    tab[ligne, c] = 0;                  //on met un 0 à la place du bonbon
                    ligne++;                            //on fait de même pour les deux lignes suivantes
                }
                if (ligne < 3 && tab[ligne + 1, c] == tab[ligne, c] && tab[ligne + 1, c] != 16)
                {
                    tab[ligne + 1, c] *= 2;    //si les deux entiers cote a cote sont egaux, alors on les "rassemble"
                    tab[ligne, c] = 0;
                }
            }
        }
    }
}



//programme qui convertit une matrice d'entiers en matrice de caractères et qui compte le score du joueur
char[,] ConversionMatrice(int[,] tab)
{
    int score = 0;
    char[,] matriceJeu = new char[tab.GetLength(0), tab.GetLength(1)];  //on crée un nouveau tableau de caractères
    for (int i = 0; i < tab.GetLength(0); i++)
    {
        for (int j = 0; j < tab.GetLength(1); j++)
        {
            switch (tab[i, j])
            {
                case 0:
                    {
                        matriceJeu[i, j] = ' ';
                        break;
                    }

                case 2:
                    {
                        matriceJeu[i, j] = '*';
                        score++;
                        break;
                    }

                case 4:
                    {
                        matriceJeu[i, j] = '@';
                        score += 3;
                        break;
                    }

                case 8:
                    {
                        matriceJeu[i, j] = 'o';
                        score += 7;
                        break;
                    }

                case 16:
                    {
                        matriceJeu[i, j] = 'J';
                        score += 15;
                        break;
                    }

                default:
                    break;

            }
        }
    }
    Console.WriteLine();
    Console.WriteLine("⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️");
    Console.WriteLine($"   Votre score est égal à {score}.");
    Console.WriteLine("⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️⭐️");
    Console.WriteLine();
    return matriceJeu;
}


//programme permettant de verifier si la grille est bloquée
bool VerificationMatrice(int[,] tab)
{
    int[,] tab2 = new int[tab.GetLength(0), tab.GetLength(1)];
    tab2 = tab;
    for (int i = 0; i < tab.GetLength(0); i++)  //on verifie si la matrice contient des 0
    {
        for (int j = 0; j < tab.GetLength(1); j++)
        {
            if (tab[i, j] == 0)
            {
                return false;       //si elle contient des 0 alors il n'y a pas de blocage
            }
        }
    }
    MoveDown(tab2);        //sinon on effectue chaque deplacement possible sur une copie de la matrice d'origine
    MoveLeft(tab2);
    MoveRight(tab2);
    MoveUp(tab2);
    if (tab == tab2)       //et on fait une comparaison entre les deux tableaux
    {
        return true;       //si elles sont pareilles alors il y a un blocage
    }

    else
    {
        return false;      //si elles sont différentes alors non
    }
}