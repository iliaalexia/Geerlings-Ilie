char[,] Matrice(int taille)
{
    char[,] matriceJeu= new char[taille, taille];
    for (int i=0; i<taille; i++)
    {
        for (int j=0; j<taille; j++)
        {
            matriceJeu[i,j]= ' ';
        }
    }
    return matriceJeu;
}

Console.WriteLine(Matrice(4));