//programme creant une matrice rempli d'espaces de taille choisi par l'utilisateur
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

char[,] Matrice(int taille)
{
    char[,] matriceJeu = new char[taille, taille];
    for (int i = 0; i < taille; i++)
    {
        for (int j = 0; j < taille; j++)
        {
            matriceJeu[i, j] = ' ';
        }
    }
    return matriceJeu;
}

//char[,] tab = Matrice(4);



//programme placant un symbole dans la matrice dans une position aléatoire
char[,] SymboleMatrice(char[,] matrice)
{
    Random aleatoire = new Random();
    int ligne = aleatoire.Next(0, matrice.GetLength(0));
    int colonne = aleatoire.Next(0, matrice.GetLength(1));
    matrice[ligne, colonne] = '*';
    return matrice;
}
//char[,] tab1 = SymboleMatrice(Matrice(4));



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

//AfficherMatrice(Matrice(4));

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
char[,] matriceDeJeu = SymboleMatrice(Matrice(4));  //on place le premier bonbon dans une case aléatoire
matriceDeJeu = SymboleMatrice(matriceDeJeu);        //on place le deuxième bonbon dans une case aléatoire
Console.WriteLine("Voici votre grille de jeu de départ:");
AfficherMatrice(matriceDeJeu);
for (int i = 0; i < nbCoups; i++)
{
    Console.WriteLine("Deplacez les bonbons grace aux touches 8 (↑), 4(<--), 2(↓) et 6(→):");
    int deplacement = Convert.ToInt32(Console.ReadLine()!);
    for (int j = 0; j < 4; j++)
    {
        switch (deplacement)
        {
            case 8:
                //chercher cases ou se trouvent les bonbons et les deplacer en position [0,j]
                for (int k = 0; k < 4; k++)
                {
                    if (matriceDeJeu[3 - j, k] == '*' && matriceDeJeu[3 - j, k + 1] == matriceDeJeu[3 - j, k])
                    {
                        matriceDeJeu[3 - j, k] = ' ';
                        matriceDeJeu[3 - j, k + 1] = ' ';
                        matriceDeJeu[0, k] = '@';
                        AfficherMatrice(matriceDeJeu);
                    }
                    else if (matriceDeJeu[3 - j, k] == '*')
                    {
                        matriceDeJeu[3 - j, k] = ' ';
                        matriceDeJeu[0, k] = '*';
                    }
                }
                break;

            case 4:
                //chercher cases ou se trouvent les bonbons et les deplacer en position [i,0]
                for (int k = 1; k < 5; k++)
                {
                    if (matriceDeJeu[j, 4 - k] == '*' && matriceDeJeu[j, 4 - k - 1] == matriceDeJeu[j, 4 - k])
                    {
                        matriceDeJeu[j, 4 - k] = ' ';
                        matriceDeJeu[j, 4 - k - 1] = ' ';
                        matriceDeJeu[j, 0] = '@';

                    }
                    else if (matriceDeJeu[j, 3 - k] == '*')
                    {
                        matriceDeJeu[j, 3 - k] = ' ';
                        matriceDeJeu[j, 0] = '*';
                    }
                }
                break;

            case 2:
                //chercher cases ou se trouvent les bonbons et les deplacer en position [3,j]
                for (int k = 0; k < 4; k++)
                {
                    if (matriceDeJeu[j, k] == '*' && matriceDeJeu[j, k] == matriceDeJeu[j + 1, k])
                    {
                        matriceDeJeu[j, k] = ' ';
                        matriceDeJeu[j + 1, k] = ' ';
                        matriceDeJeu[3, k] = '@';
                        AfficherMatrice(matriceDeJeu);
                    }
                    else if (matriceDeJeu[j, k] == '*')
                    {
                        matriceDeJeu[j, k] = ' ';
                        matriceDeJeu[3, k] = '*';
                    }
                }
                break;

            case 6:
                //chercher cases ou se trouvent les bonbons et les deplacer en position [i,3]
                for (int k = 0; k < 4; k++)
                {
                    if (matriceDeJeu[j, k] == '*' && matriceDeJeu[j, k + 1] == matriceDeJeu[j, k])
                    {
                        matriceDeJeu[j, k] = ' ';
                        matriceDeJeu[j, k + 1] = ' ';
                        matriceDeJeu[j, 3] = '@';
                        AfficherMatrice(matriceDeJeu);
                    }
                    else if (matriceDeJeu[j, k] == '*')
                    {
                        matriceDeJeu[j, k] = ' ';
                        matriceDeJeu[j, 3] = '*';
                    }
                }
                break;

            default:
                break;
        }
    }
    int l = aleatoire.Next(0, 4);
    int c = aleatoire.Next(0, 4);
    matriceDeJeu[l, c] = '*';
    AfficherMatrice(matriceDeJeu);
}


