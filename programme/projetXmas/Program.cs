﻿//programme creant une matrice rempli d'espaces de taille choisi par l'utilisateur
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

//programme créant une matrice rempli de 0
int[,] MatriceEntiers(int taille)
{
    int[,] matriceJeu = new int[taille, taille];
    for (int i = 0; i < taille; i++)
    {
        for (int j = 0; j < taille; j++)
        {
            matriceJeu[i, j] = 0;
        }
    }
    return matriceJeu;
}


//programme placant un 2 dans la matrice sur une position aléatoire
int[,] SymboleMatrice2(int[,] matrice)
{
    Random aleatoire = new Random();
    int ligne = aleatoire.Next(0, matrice.GetLength(0));
    int colonne = aleatoire.Next(0, matrice.GetLength(1));
    while (matrice[ligne, colonne] != 0)
    {
        ligne = aleatoire.Next(0, matrice.GetLength(0));
        colonne = aleatoire.Next(0, matrice.GetLength(1));
    }
    matrice[ligne, colonne] = 2;
    return matrice;
}


//programme affichant la matrice avec un joli design
void AfficherMatrice(char[,] tab)
{
    for (int i = 0; i < tab.GetLength(0); i++)
    {
        for (int j = 0; j < tab.GetLength(1); j++)
        {
            Console.Write($"| {tab[i, j]} ");
            if (j == tab.GetLength(1) - 1)
            {
                Console.Write("|");
                Console.WriteLine();
            }
        }
    }
}


//programme du jeu
Console.WriteLine("Est-ce que vous connaissez les règles du jeu? Repondez par oui ou non");
string reponse = Console.ReadLine()!;

if (reponse == "non" || reponse == "Non")
    Console.WriteLine("Le but du jeu est de déplacer les bonbons dans la grille du jeu afin qu'ils se rencontrent et se transforment dans le treat supérieur! Après chaque coup joué, un nouveau bonbon est introduit dans la grille. Le jeu s'arrête une fois que vous aviez atteint le nombre de coups maximale ou qu'il y ait un blocage dans la grille. Facile!");
else
    Console.WriteLine("Parfait! Commencons le jeu");
Console.WriteLine("--------------------------------------------");
Console.WriteLine("Choissisez le nombre de coups autorisés:");
int nbCoups = Convert.ToInt32(Console.ReadLine()!);
Random aleatoire = new Random();
int[,] matriceDeJeuEntiers = SymboleMatrice2(MatriceEntiers(4));    //on place le premier bonbon dans une case aléatoire
matriceDeJeuEntiers = SymboleMatrice2(matriceDeJeuEntiers);         //on place le deuxième bonbon dans une case aléatoire
Console.WriteLine();
Console.WriteLine();
Console.WriteLine("Voici votre grille de jeu de départ:");
Console.WriteLine("------------------------------------");
AfficherMatrice(ConversionMatrice(matriceDeJeuEntiers));            //affichage de la matrice de départ
for (int i = 0; i < nbCoups; i++)
{
    Console.WriteLine();
    Console.WriteLine("Deplacez les bonbons grace aux touches 8 (↑), 4(<--), 2(↓) et 6(→):");
    int deplacement = Convert.ToInt32(Console.ReadLine()!);
    switch (deplacement)
    {
        case 8:
            //chercher cases ou se trouvent les bonbons et les deplacer en position [0,j]
            MoveUp(matriceDeJeuEntiers);
            break;

        case 4:
            //chercher cases ou se trouvent les bonbons et les deplacer en position [i,0]
            MoveLeft(matriceDeJeuEntiers);
            break;

        case 2:
            //chercher cases ou se trouvent les bonbons et les deplacer en position [3,j]
            MoveDown(matriceDeJeuEntiers);
            break;

        case 6:
            //chercher cases ou se trouvent les bonbons et les deplacer en position [i,3]
            MoveRight(matriceDeJeuEntiers);
            break;

        default:
            break;

    }
    Console.WriteLine();
    AfficherMatrice(ConversionMatrice(SymboleMatrice2(matriceDeJeuEntiers)));
}



// programme qui permet de faire le deplacement en haut
void MoveUp(int[,] tab)
{
    for (int c = 0; c < 4; c++)
    {
        for (int l = 1; l < 4; l++)
        {
            if (tab[l, c] != 0)
            {
                int ligne = l;
                while (ligne > 0 && tab[ligne - 1, c] == 0)
                {
                    tab[ligne - 1, c] = tab[ligne, c];
                    tab[ligne, c] = 0;
                    ligne--;
                }
                if (ligne > 0 && tab[ligne - 1, c] == tab[ligne, c])
                {
                    tab[ligne - 1, c] *= 2;
                    tab[ligne, c] = 0;
                }
            }
        }
    }
}


//programme qui permet de faire le deplacement vers la gauche
void MoveLeft(int[,] tab)
{
    for (int c = 1; c < 4; c++)
    {
        for (int l = 0; l < 4; l++)
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
                if (colonne > 0 && tab[l, colonne - 1] == tab[l, colonne])
                {
                    tab[l, colonne - 1] *= 2;           //si les deux entiers cote a cote sont egaux, alors on les "rassemble"
                    tab[l, colonne] = 0;
                }
            }
        }
    }
}


//programme qui permet le deplacement vers la droite
void MoveRight(int[,] tab)
{
    for (int c = 0; c < 3; c++)
    {
        for (int l = 0; l < 4; l++)
        {
            if (tab[l, c] != 0)
            {
                int colonne = c;
                while (colonne < 3 && tab[l, colonne + 1] == 0)
                {
                    tab[l, colonne + 1] = tab[l, colonne];
                    tab[l, colonne] = 0;
                    colonne++;
                }
                if (colonne < 3 && tab[l, colonne + 1] == tab[l, colonne])
                {
                    tab[l, colonne + 1] *= 2;
                    tab[l, colonne] = 0;
                }
            }
        }
    }
}


//programme qui permet le deplacement vers le bas
void MoveDown(int[,] tab)
{
    for (int c = 0; c < 4; c++)
    {
        for (int l = 0; l < 3; l++)
        {
            if (tab[l, c] != 0)
            {
                int ligne = l;
                while (ligne < 3 && tab[ligne + 1, c] == 0)
                {
                    tab[ligne + 1, c] = tab[ligne, c];
                    tab[ligne, c] = 0;
                    ligne++;
                }
                if (ligne < 3 && tab[ligne + 1, c] == tab[ligne, c])
                {
                    tab[ligne + 1, c] *= 2;
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
    char[,] matriceJeu = new char[tab.GetLength(0), tab.GetLength(1)];
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
    Console.WriteLine("---------------------------------");
    Console.WriteLine($"Votre score est égal à {score}.");
    Console.WriteLine("---------------------------------");
    Console.WriteLine();
    return matriceJeu;
}