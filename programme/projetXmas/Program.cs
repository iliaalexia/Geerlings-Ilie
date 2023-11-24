//programme creant une matrice rempli d'espaces de taille choisi par l'utilisateur
using System.ComponentModel.DataAnnotations.Schema;

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

char[,] tab = Matrice(4);



//programme placant un symbole dans la matrice dans une position aléatoire
char[,] SymboleMatrice(char[,] matrice)
{
    Random aleatoire = new Random();
    int ligne = aleatoire.Next(0, matrice.Length);
    int colonne = aleatoire.Next(0, matrice.Length);
    matrice[ligne, colonne] = '*';
    return matrice;
}
char[,] tab1 = SymboleMatrice(Matrice(4));



//programme affichant la matrice avec un joli design
void AfficherMatrice(char[,] tab)
{
    for (int i = 0; i < tab.GetLength(0); i++)
    {
        for (int j = 0; j < tab.GetLength(1); j++)
        {
            Console.Write("|  ");
            if (j == tab.GetLength(1) - 1)
            {
                Console.Write("|");
                Console.WriteLine();
            }


        }
    }
}

AfficherMatrice(Matrice(4));