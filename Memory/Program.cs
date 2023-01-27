//Projet Informatique à rendre pour le 16 Décembre 
//Auteurs Julien DEBIDOUR , Lucie DELLA-NEGRA , Joanne GANGAPAL
//Date de début 22 Novembre 2022
//Version 7
//Date de mise à jour 15 Décembre 2022

using System;
using System.IO;
using System.Text;


//Demande du type de caractères choisi pour jouer
int DemanderCaract()//Demande au joueur le type de caractères qu'il souhaite utiliser
{
    int typeCaract = -1;
    Console.WriteLine("====================================================================================================");
    Console.WriteLine("\t Quel est le type de caractère que vous voulez utiliser ?");
    Console.WriteLine("\t\t 1 : miniscules(limité à 26 N-uplets)");
    Console.WriteLine("\t\t 2 : majuscules(limité à 26 N-uplets)");
    Console.WriteLine("\t\t 3 : caractères spéciaux(limité à 14 N-uplets)");
    Console.WriteLine("\t\t 4 : chiffres(limité à 10 N-uplets)");
    Console.WriteLine("\t\t 5 : tous les caractères(limité à 93 N-uplets)\n\t\t");
    Console.Write("\t \t \t \t \t \t");
    string reponse = Console.ReadLine()!;
    bool validite = false;
    do
    {

        int.TryParse(reponse, out typeCaract);//On regarde si on peut convertir l'entrée en entier
        if ((typeCaract > 0) && (typeCaract <= 5))
            validite = true;//C'est un entier compris entre 0 et 4, l'entrée est donc valide

        else
        {
            Console.WriteLine("\t\tVotre entrée n'est pas valide, veuillez rentrer un chiffre compris entre 1 et 5.");//Si l'utilisateur s'est trompé, on lui redemande   
            Console.Write("\t \t \t \t \t \t");
            reponse = Console.ReadLine()!;
        }

    } while (!(validite));
    typeCaract--;

    return typeCaract;


}

//Demande de N
int DemanderN()//Demande au joueur du nombre de cartes identiques formant les n-uplets
{
    int nbCartesParUplets = -1;
    Console.WriteLine("====================================================================================================");
    Console.WriteLine("\t\t Quel est le nombre de cartes identiques formant un n-uplet ?");
    Console.Write("\t \t \t \t \t \t");
    string reponse = Console.ReadLine()!;


    bool validite = false;
    do
    {

        int.TryParse(reponse, out nbCartesParUplets);//On essaie de convertir l'entrée en entier
        if (nbCartesParUplets >= 2)
            validite = true;//C'est un entier supérieur à 2, l'entrée est donc valide

        else
        {
            Console.WriteLine("\t\tVotre entrée n'est pas valide, veuillez rentrer un entier supérieur ou égal à 2.");//Si ça ne fonctionne pas, on redemande   
            Console.Write("\t \t \t \t \t \t");
            reponse = Console.ReadLine()!;
        }

    } while (!(validite));

    return nbCartesParUplets;
}

//Demande du nombre d'uplets
int DemanderNuplets(int typeCaract)
{
    int nbUplets = -1;
    Console.WriteLine("====================================================================================================");
    Console.WriteLine("\t\t Pour votre Jeu, avec combien de n-uplets souhaitez-vous jouer ?");
    Console.Write("\t \t \t \t \t \t");
    string Uplet = Console.ReadLine()!;
    int maximum = int.MaxValue;
    switch (typeCaract)
    {
        case 0:
            maximum = 26;
            break;
        case 1:
            maximum = 26;
            break;
        case 2:
            maximum = 14;
            break;
        case 3:
            maximum = 10;
            break;
        case 4:
            maximum = 93;
            break;

    }

    bool Upletok = false;
    do
    {

        int.TryParse(Uplet, out nbUplets);//On regarde si il est possible de convertir la réponse de l'utilisateur en entier
        if (nbUplets > 0 && nbUplets < maximum)
        {
            Upletok = true;//Il s'agit un entier différent de 0, l'entrée est donc valide
        }

        else
        {
            Console.WriteLine("\t\t Votre entrée n'est pas valide. Merci de réessayer.");//Si l'entrée n'est pas valide, on réessaye  
            Console.Write("\t \t \t \t \t \t");
            Uplet = Console.ReadLine()!;
        }

    } while (!(Upletok));

    return nbUplets;
}

//Fonction d'initialisation de tableau rempli de * 
char[,] InitierTabGrille(int nbCartesParUplets, int nbUplets)

{
    int nbCartesParUplets2 = OptimiserLaTaille(nbCartesParUplets, nbUplets)[0];//Optimisation des dimensions
    int nbUplets2 = OptimiserLaTaille(nbCartesParUplets, nbUplets)[1];//Idem
    char[,] tableau = new char[nbCartesParUplets2, nbUplets2];
    for (int i = 0; i < nbCartesParUplets2; i++)//Boucles imbriquées du tableau
    {
        for (int j = 0; j < nbUplets2; j++)
        {
            tableau[i, j] = '*';//Grille initiale en *
        }
    }
    return tableau;//On retourne le tableau
}

//Fonction d'initialisation du tableau "final" rempli des caractères représentants les faces des cartes 
char[,] InitierTabCaractère(int nbCartesParUplets, int nbUplets, int typeCaract)
{
    int ligne = OptimiserLaTaille(nbCartesParUplets, nbUplets)[0];//Optimisation de la taille du tableau retourné
    int colonne = OptimiserLaTaille(nbCartesParUplets, nbUplets)[1];
    int[] tableauRestant = new int[nbUplets];//Tableau (liste) comptant le nombre d'occurences restantes à placer pour chaque caractère
    for (int i = 0; i < nbUplets; i++)
    {
        tableauRestant[i] = nbCartesParUplets;
    }

    char[] tabLettreMinuscule = new char[26];//Tableau (liste) contenant les différents caractères minuscules utilisés (sélectionnés par hasard) pour le jeu dans le format souhaité par le joueur
    for (int i = 97; i < 123; i++)
    {
        int nombre0 = i;
        char lettre0 = (char)nombre0;
        tabLettreMinuscule[i - 97] = lettre0;
    }

    char[] tabLettreMajuscule = new char[26];//Tableau (liste) contenant les différents caractères majuscules utilisés (sélectionnés par hasard) pour le jeu dans le format souhaité par le joueur
    for (int i = 65; i < 91; i++)
    {
        int nombre0 = i;
        char lettre0 = (char)nombre0;
        tabLettreMajuscule[i - 65] = lettre0;
    }

    char[] tabCaract = new char[16];//Tableau (liste) contenant les différents caractères spéciaux utilisés (sélectionnés par hasard) pour le jeu dans le format souhaité par le joueur
    for (int i = 33; i < 48; i++)
    {
        int nombre0 = i;
        char lettre0 = (char)nombre0;
        if (i == 42)
        {
            nombre0 = 123;
            lettre0 = (char)nombre0;
            tabCaract[i - 33] = lettre0;
        }
        else
        {
            tabCaract[i - 33] = lettre0;
        }

    }

    char[] tabChiffre = new char[10];//Tableau (liste) contenant les différents caractères spéciaux utilisés (sélectionnés par hasard) pour le jeu dans le format souhaité par le joueur
    for (int i = 48; i < 58; i++)
    {
        int nombre0 = i;
        char lettre0 = (char)nombre0;
        tabChiffre[i - 48] = lettre0;
    }

    char[] tabComplet = new char[94];//Tableau (liste) contenant les différents caractères spéciaux utilisés (sélectionnés par hasard) pour le jeu dans le format souhaité par le joueur
    for (int i = 33; i < 126; i++)
    {
        int nombre0 = i;
        char lettre0 = (char)nombre0;
        if (i == 42)
        {
            nombre0 = 126;
            lettre0 = (char)nombre0;
            tabCaract[i - 33] = lettre0;
        }
        else
        {
            tabComplet[i - 33] = lettre0;
        }
    }


    char[] tabChoisi = new char[nbUplets];//On récupère le type de tableau choisi par l'utilisateur
    int nombre1;
    if (typeCaract == 0)
    {
        Random hasard = new Random();
        for (int i = 0; i < nbUplets; i++)
        {
            nombre1 = hasard.Next(26);//Choix du caractère au hasard 
            for (int j = 0; j < i; j++)
            {
                while (tabChoisi[j] == tabLettreMinuscule[nombre1])
                {
                    nombre1 = hasard.Next(26);
                }
            }
            tabChoisi[i] = tabLettreMinuscule[nombre1];
        }
    }
    else if (typeCaract == 1)
    {
        Random hasard = new Random();
        for (int i = 0; i < nbUplets; i++)
        {
            nombre1 = hasard.Next(26);//Choix du caractère au hasard 
            for (int j = 0; j < i; j++)
            {
                while (tabChoisi[j] == tabLettreMajuscule[nombre1])
                {
                    nombre1 = hasard.Next(26);
                }
            }
            tabChoisi[i] = tabLettreMajuscule[nombre1];
        }
    }
    else if (typeCaract == 2)
    {
        Random hasard = new Random();
        for (int i = 0; i < nbUplets; i++)
        {
            nombre1 = hasard.Next(16);//Choix du caractère au hasard 
            for (int j = 0; j < i; j++)
            {
                while (tabChoisi[j] == tabCaract[nombre1])
                {
                    nombre1 = hasard.Next(16);
                }
            }
            tabChoisi[i] = tabCaract[nombre1];
        }
    }
    else if (typeCaract == 3)
    {
        Random hasard = new Random();
        for (int i = 0; i < nbUplets; i++)
        {
            nombre1 = hasard.Next(10);//Choix du caractère au hasard 
            for (int j = 0; j < i; j++)
            {
                while (tabChoisi[j] == tabCaract[nombre1])
                {
                    nombre1 = hasard.Next(10);
                }
            }
            tabChoisi[i] = tabChiffre[nombre1];
        }
    }
    else if (typeCaract == 4)
    {
        Random hasard = new Random();
        for (int i = 0; i < nbUplets; i++)
        {
            nombre1 = hasard.Next(94);//Choix du caractère au hasard 
            for (int j = 0; j < i; j++)
            {
                while (tabChoisi[j] == tabComplet[nombre1])
                {
                    nombre1 = hasard.Next(94);
                }
            }
            tabChoisi[i] = tabComplet[nombre1];
        }
    }
    char[,] tableauRetourné = new char[ligne, colonne];//Tableau final à retourner contenant les caractères placés
    int nombre;
    int c = 0;
    for (int i = 0; i < ligne; i++)
    {
        for (int j = 0; j < colonne; j++)
        {
            nombre = new Random().Next(nbUplets);//Choix du caractère au hasard 
            while (tableauRestant[nombre] == 0) //On vérifie si on a pas déjà placé toutes les occurences de la lettre sélectionnée
            {
                nombre = new Random().Next(nbUplets);//Sinon, on change de lettre jusqu'à ce que cela soit une lettre dont on n'a pas placé toutes les occurenres
            }
            tableauRetourné[i, j] = tabChoisi[nombre];//On place le caractère dans le tableau final
            tableauRestant[nombre] = tableauRestant[nombre] - 1;//On désincrémente une occurence placée
            c = c + 1;
        }
    }
    return tableauRetourné;
}

//Affiche le tableau
void Affichertab(char[,] tableau)
{
    int nbCartesParUplets = tableau.GetLength(0);
    int nbUplets = tableau.GetLength(1);
    Console.Write("\t \t \t \t  ");
    Console.Write("   ");
    for (int k = 0; k < tableau.GetLength(1); k++)
    {
        Console.Write($"{k + 1}");
        if (k < tableau.GetLength(1) - 1 && k < 9)
            Console.Write("   ");
        else if (k < tableau.GetLength(1) - 1)
            Console.Write("  ");
    }
    Console.Write("\n");
    Console.Write("\t \t \t \t  ");
    Console.Write(" __");

    for (int colonne = 0; colonne < (nbUplets - 1) * 4; colonne += 1)
        Console.Write('_');
    Console.Write("___ ");

    Console.WriteLine();


    for (int i = 0; i < nbCartesParUplets; i++)
    {
        Console.Write("\t \t \t \t");
        Console.Write($"{i + 1} ");
        Console.Write("|  ");

        for (int j = 0; j < tableau.GetLength(1); j++)
        {
            Console.Write(tableau[i, j]); //Affichage des éléments du tableau un par un 
            if (j < tableau.GetLength(1) - 1)
                Console.Write(" | ");
        }

        Console.Write("  |");
        Console.WriteLine();
        Console.Write("\t \t \t \t  ");
        Console.Write("|__");
        for (int j = 0; j < tableau.GetLength(1); j++)
        {
            Console.Write("_");
            if (j < tableau.GetLength(1) - 1)
                Console.Write("_|_");
        }
        Console.Write("__|");
        Console.WriteLine();
    }
    Console.WriteLine();
}

//Affiche le tableau avec le choix de l'utilisateur en bleu 
void AfficherTabCouleur(char[,] tableau, int ligneChoisi, int colonnnechoisi)
{
    int nbCartesParUplets = tableau.GetLength(0);
    int nbUplets = tableau.GetLength(1);
    Console.Write("\t \t \t \t  ");
    Console.Write("   ");
    for (int k = 0; k < tableau.GetLength(1); k++)
    {
        Console.Write($"{k + 1}");
        if (k < tableau.GetLength(1) - 1 && k < 9)
            Console.Write("   ");
        else if (k < tableau.GetLength(1) - 1)
            Console.Write("  ");
    }
    Console.Write("\n");
    Console.Write("\t \t \t \t  ");
    Console.Write(" __");

    for (int colonne = 0; colonne < (nbUplets - 1) * 4; colonne += 1)
        Console.Write('_');
    Console.Write("___ ");

    Console.WriteLine();


    for (int i = 0; i < nbCartesParUplets; i++)
    {
        Console.Write("\t \t \t \t");
        Console.Write($"{i + 1} ");
        Console.Write("|  ");

        for (int j = 0; j < tableau.GetLength(1); j++)
        {
            if (i == ligneChoisi && j == colonnnechoisi)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(tableau[i, j]); //Affichage des éléments du tableau un par un
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
                Console.Write(tableau[i, j]); //Affichage des éléments du tableau un par un 
            if (j < tableau.GetLength(1) - 1)
                Console.Write(" | ");
        }

        Console.Write("  |");
        Console.WriteLine();
        Console.Write("\t \t \t \t  ");
        Console.Write("|__");
        for (int j = 0; j < tableau.GetLength(1); j++)
        {
            Console.Write("_");
            if (j < tableau.GetLength(1) - 1)
                Console.Write("_|_");
        }
        Console.Write("__|");
        Console.WriteLine();
    }
    Console.WriteLine();
}

//Verification de la disponibilité de la case d'un tableau
bool VerifierLaDisponibilite(char[,] tableau, int ligne, int colonne)
{
    bool disponible = true;
    if (tableau[ligne - 1, colonne - 1] != '*') //Si la case est une * alors la carte est déjà retourné 
        disponible = false;
    return disponible;
}

//Demande une case à l'utilisateur
bool DemanderLaCase(ref int ligne, ref int colonne, char[,] tableau)
{
    bool conditions, disponible;
    Console.Write("\n");
    do
    {
        do
        {
            //Demande un numéro de ligne sur lequel l'utilisateur veut jouer
            conditions = true;
            Console.WriteLine($"\t\t Veuillez saisir une ligne inférieur à {tableau.GetLength(0)} et positive. La première ligne est 1: ");
            Console.Write("\t \t \t \t \t \t");
            string reponse = Console.ReadLine()!;
            if (reponse == "p")
            {
                return true;
            }
            int.TryParse((reponse), out ligne);
            if (ligne == 0)
            {
                Console.WriteLine("\t\t Votre saisie n'est pas un entier suppérieur à 0.");
                conditions = false;
            }
            else if (ligne < 0 || ligne > tableau.GetLength(0))
            {
                Console.WriteLine($"\t\t Votre saisie est supérieur à {tableau.GetLength(0)} ou non positive");
                conditions = false;

            }

        } while (conditions == false);

        do
        {
            //Demande un numéro de colonne sur lequel l'utilisateur veut jouer
            conditions = true;
            Console.WriteLine($"\t\t Veuillez saisir une colonne inférieur à {tableau.GetLength(1)} et positive. La première colonne est 1: ");
            Console.Write("\t \t \t \t");
            string reponse = Console.ReadLine()!;
            if (reponse == "p")
            {
                return true;
            }
            int.TryParse(reponse, out colonne);
            if (colonne == 0)
            {
                Console.WriteLine("\t\t Votre saisie n'est pas un entier suppérieur à 0.");
                conditions = false;
            }
            else if (colonne < 0 || colonne > tableau.GetLength(1))
            {

                Console.WriteLine($"\t\t Votre saisie est supérieur à {tableau.GetLength(1)} ou non positive");
                conditions = false;

            }
        } while (conditions == false);
        disponible = VerifierLaDisponibilite(tableau, ligne, colonne); //Vérification si la carte n'est pas déjà retournée
        if (disponible == false)
        {
            Console.WriteLine($"\t\t La carte ligne {ligne} et colonne {colonne} est déjà retourné"); //Si oui on lui redemande un choix de case
        }
        else
        {
            ligne--;
            colonne--;
        }

    } while (disponible == false);
    return false;

}

bool Menu(int nbCartesParUplets, int nbUplets, char[,] tableau, char[,] tableauResult, int nbTentativesFaites, int nbMaxTentatives, int nbrJoueurs, int tourJoueurs, string[] pseudos, int[] tableauScore, int reste, int lignechosi, int colonneChoisi, int compteurCartes)
{
    string reponse = "p";
    while (reponse == "p")
    {
        Console.Write("\n");
        Console.WriteLine("\t \t =============================== Menu ===============================");
        Console.Write("\n");
        Console.WriteLine("\t \t                     Vous avez mis la partie en pause.                                ");
        Console.Write("\n");
        Console.WriteLine("\t \t                     Tapez r pour reprendre la partie.                                ");
        Console.Write("\n");
        Console.WriteLine("\t \t                     Tapez s pour sauvegarder la partie.                                ");
        Console.Write("\n");
        Console.WriteLine("\t \t                     Tapez q pour quitter la partie.                                ");
        Console.Write("\n");
        Console.WriteLine("\t \t ======================================================================");
        do
        {
            Console.Write("\t \t \t \t \t \t");
            reponse = Console.ReadLine()!;
            if (reponse == "s")
            {
                Sauvegarde(tableau, tableauResult, nbCartesParUplets, nbUplets, nbTentativesFaites, nbMaxTentatives, nbrJoueurs, tourJoueurs, pseudos, tableauScore, reste, lignechosi, colonneChoisi, compteurCartes);


                Console.Write("\n");
                Console.WriteLine("\t \t =============================== Menu ===============================");
                Console.Write("\n");
                Console.WriteLine("\t \t                     La partie a bien été sauvegardé.                                ");
                Console.Write("\n");
                Console.WriteLine("\t \t                     Tapez q pour quitter la partie.                                ");
                Console.Write("\n");
                Console.WriteLine("\t \t                     Tapez r pour reprendre la partie.                                ");
                Console.Write("\n");
                Console.WriteLine("\t \t ======================================================================");
                Console.Write("\t \t \t \t \t \t");
                reponse = Console.ReadLine()!;
                if (reponse == "q")
                {
                    return true;
                }
            }

            else if (reponse == "q")
                return true;
        } while (reponse != "r");
    }
    if (reponse == "r")
        AfficherTabCouleur(tableau, lignechosi, colonneChoisi);
    return false;

}


void Sauvegarde(char[,] tableau, char[,] tableauResult, int nbCartesParUplets, int nbUplets, int nbTentativesFaites, int nbMaxTentatives, int nbrJoueurs, int tourJoueurs, string[] pseudos, int[] tableauScore, int reste, int ligne, int colonne, int compteurCartes)
{
    string path = @"c:.\PartiePrecedente.txt";
    try
    {
        //Créer un fichier, ou le réécris si il existe déjà.
        /*Listes des informations à stocker:
            -Nombre de ligne du plateu de jeu
            -Nombre de colonne du plateau de jeu 
            -Nombre de cartes identiques formant le n-uplets
            -Nombre de n-uplets sur le plateau
            -Plateau de jeu courant
            -Plateau de jeu final
            -Nombre de tentatives maximales
            -Nombre de tentatives qui se sont effectuées
            -Nombre de joueurs
            -Quel est le joueur qui doit jouer
            -Dernière ligne chosi (si en pleine partie)
            -Dernière colonne choisi (si en pleine partie)
            -Nombre de carte d'un même n-Uplets déjà retourné mais non complet
            -Le nombre de n-uplets qu'il reste dans le tableau courant
            -Noms des joeurs
            -Le tableau des scores
        */
        using (FileStream fs = File.Create(path)) //Créer un fihcier PartiePrecedente.txt
        {
            //début écriture dans le fichier
            byte[] info = new UTF8Encoding(true).GetBytes($"{tableau.GetLength(0)}\n{tableau.GetLength(1)}\n{nbCartesParUplets}\n{nbUplets}\n");
            fs.Write(info, 0, info.Length);
        }
        StreamWriter sw = new StreamWriter(path, true, Encoding.ASCII);
        for (int i = 0; i < tableau.GetLength(0); i++)
        {
            for (int j = 0; j < tableau.GetLength(1); j++)
            {
                sw.Write(tableau[i, j]);
            }
            sw.Write("\n");
        }
        for (int i = 0; i < tableauResult.GetLength(0); i++)
        {
            for (int j = 0; j < tableauResult.GetLength(1); j++)
            {
                sw.Write(tableauResult[i, j]);
            }
            sw.Write("\n");
        }
        sw.Write($"{nbMaxTentatives}\n{nbTentativesFaites}\n{nbrJoueurs}\n{tourJoueurs}\n{ligne}\n{colonne}\n{compteurCartes}\n{reste}\n");
        for (int i = 0; i < pseudos.Length; i++)
        {

            sw.Write(pseudos[i]);
            sw.Write("\n");

        }
        for (int i = 0; i < tableauScore.Length; i++)
        {

            sw.Write(tableauScore[i]);
            sw.Write("\n");

        }
        sw.Close(); //fermeture du fichier
    }

    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}

//Fonction test pour savoir si on a gagné
bool TesterLaVictoire(char[,] tab)
{
    int nbLignes = tab.GetLength(0);
    int nbColonnes = tab.GetLength(1);

    for (int i = 0; i < nbLignes; i += 1)
    {
        for (int j = 0; j < nbColonnes; j += 1)//On parcourt l'entièreté du tableau et vérifie si tous sont des caractères différents de *
        {
            if (tab[i, j] == '*') //Les dessins des cartes sont représentées par des lettres minuscules dans un premier temps
                return false;
        }
    }
    return true;
}

//Fonction qui demande à l'utilisateur le nombre de joueurs
int NombreJoueurs()
{
    int nbrJoueurs;
    Console.WriteLine("====================================================================================================");
    do
    {
        Console.WriteLine("\t\t \t Veuillez indiquer le nombre de joueurs?");
        Console.Write("\t \t \t \t \t \t");
        int.TryParse(Console.ReadLine()!, out nbrJoueurs);
        if (nbrJoueurs < 1)
        {
            Console.WriteLine("\t\t Votre saisie est incorrect. Ce n'est pas un entier supérieur ou égal à 1");
        }
    } while (nbrJoueurs < 1);
    return nbrJoueurs;
}

string[] DemanderPseudonymes(int nbrJoueurs)
{
    string[] pseudos = new string[nbrJoueurs];

    for (int i = 0; i < nbrJoueurs; i += 1)
    {
        Console.WriteLine("\t \t \t ============================================");
        Console.WriteLine($"\t \t \t \t Joueur {i + 1}, entrez un pseudonyme : ");
        Console.Write("\t \t \t \t \t    ");
        pseudos[i] = Console.ReadLine()!;
    }
    return pseudos;
}

//Trie le tableau des pseudonymes des joueurs en fonction de leurs  scores de manière décroissante
void TrierTab(ref int[] tab, ref string[] pseudos)
{
    int i = 0;
    int temp;
    string temp2;
    while (i < tab.Length - 1)
    {

        if (tab[i] < tab[i + 1])
        {
            while (i < tab.Length - 1 && tab[i] < tab[i + 1])
            {
                temp = tab[i + 1];
                temp2 = pseudos[i + 1];
                tab[i + 1] = tab[i];
                pseudos[i + 1] = pseudos[i];
                tab[i] = temp;
                pseudos[i] = temp2;
                i++;
            }
            i = 0;
        }
        else
        {

            i++;
        }
    }
}


//Détermination d'un gagnant
void Gagnant(int[] tableauScore, string[] pseudos, int nbrTentatives, int reste, int nbCartesParUplets)
{
    TrierTab(ref tableauScore, ref pseudos);
    Console.WriteLine("====================================================================================================");
    if (tableauScore[0] > 0)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"\t\tBravo au joueur {pseudos[0]} ");
        Console.Write($"vous avez gagné(s) avec un score de {tableauScore[0]} de combinaisons révélés \n\n");

        int h = pseudos.Length;
        int x = 0;
        int joueurs = 1;
        for (int i = h; i > 0; i--)
        {
            x++;
            Console.Write("\t \t \t \t \t ");
            for (int j = 0; j < h + (h - 1); j++)
            {
                int y = 0;
                if (j < h - x)
                {
                    Console.Write(" ");
                }
                else if (j >= h)
                {
                    Console.Write(" ");
                }
                else
                {
                    if (x == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{pseudos[0]}({tableauScore[0]})");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        int k = 0;
                        while (y != x - 1)
                        {
                            if (joueurs < pseudos.Length)
                            {
                                if (joueurs == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                Console.Write($"{pseudos[joueurs]}({tableauScore[joueurs]})  ");
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                                Console.Write($" ");
                            y++;
                            k++;
                            if (joueurs < pseudos.Length)
                                joueurs++;

                        }
                        if (joueurs < pseudos.Length)
                        {
                            if (joueurs == 2)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                            }
                            Console.Write($"{pseudos[joueurs]}({tableauScore[joueurs]})");
                            Console.ForegroundColor = ConsoleColor.Green;
                            joueurs++;
                        }
                        else
                            Console.Write($" ");
                        j += 2 * k;
                    }
                }
            }
            Console.Write("\n");
        }
        Console.ForegroundColor = ConsoleColor.White;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\t\tDommage vous n'avez découvert aucune cartes en {nbrTentatives} tentatives.\n\t\tIls vous manquaient {reste} combinaison(s) de {nbCartesParUplets} cartes");
        Console.ForegroundColor = ConsoleColor.White;
    }
    Console.WriteLine("====================================================================================================");

}

int DemanderNbTentativesMax(int nbCartesParUplets, int nbUplets)
{
    Console.WriteLine($"\t \t Voulez-vous fixer un nombre max de tentatives ?(Tapez oui ou non)");
    string reponse;
    int nbMaxTentatives = int.MaxValue;
    do
    {
        Console.Write("\t \t \t \t \t \t");
        reponse = Console.ReadLine()!;
    } while (reponse != "oui" && reponse != "non");
    if (reponse == "oui")
    {
        Console.WriteLine("\t\tVeuillez rentrer le nombre maximal de tentatives.");//Si ça ne fonctionne pas, on redemande   
        Console.Write("\t \t \t \t \t \t");
        reponse = Console.ReadLine()!;
        bool validite = false;
        do
        {
            int.TryParse(reponse, out nbMaxTentatives);//On essaie de convertir l'entrée en entier
            if (nbMaxTentatives >= 1)
                validite = true;//C'est un entier supérieur à 1, l'entrée est donc valide

            else
            {
                Console.WriteLine("\t\t Votre entrée n'est pas valide, veuillez rentrer un entier supérieur ou égal à 1.");//Si ça ne fonctionne pas, on redemande   
                Console.Write("\t \t \t \t");
                reponse = Console.ReadLine()!;
            }

        } while (!(validite));

    }
    return nbMaxTentatives;
}

//Fonction de Jeu (Tour et autres)
bool Jouer(ref char[,] tableau, char[,] tableauResult, int nbCartesParUplets, int nbUplets, int nbrJoueurs, string[] pseudos, int nbrTentatives, int tourJoueurs, ref int[] tableauScore, int nbMaxTentatives, int reste, int ligne, int colonne, int compteurCartes)
{
    bool quitter = false;
    char symbole;
    bool partieExistante = false;

    Console.WriteLine("====================================================================================================\n");
    Console.WriteLine("*************************************  Début de la partie :  ****************************************");

    //Début de partie 
    do
    {
        if (nbrJoueurs > 1)
        {
            Console.Write("\n");
            Console.WriteLine($"                                 C'est à {pseudos[tourJoueurs]} de jouer.                             ");
        }

        if (nbMaxTentatives != int.MaxValue)
            Console.WriteLine($"******************************    Il vous reste {nbMaxTentatives - nbrTentatives + 1} Tentative(s).    **********************************\n                               Il reste {reste} combinaison(s) de {nbCartesParUplets} cartes.\n");
        if (ligne != -1 && colonne != -1)
        {
            AfficherTabCouleur(tableau, ligne, colonne);
            symbole = tableau[ligne, colonne];
            partieExistante = true;

        }

        else
        {
            symbole = '*';
            Affichertab(tableau); //Affichage du tableau à chaque nouvelle tentative
        }
        bool symbolediff = false;
        char[,] tableauTemp = new char[tableau.GetLength(0), tableau.GetLength(1)]; //Stockage du tableau dans un tableau temporaire
        for (int i = 0; i < tableau.GetLength(0); i++)
        {
            for (int j = 0; j < tableau.GetLength(1); j++)
            {
                tableauTemp[i, j] = tableau[i, j];
            }
        }

        //Choix de cartes à retourné tant que le nombre de nbCartesParUplets n'a pas été retounés
        //Ou qu'il n'y a pas eu d'erreur
        do
        {
            int nbLigne = OptimiserLaTaille(nbCartesParUplets, nbUplets)[0];
            int nbColonnes = OptimiserLaTaille(nbCartesParUplets, nbUplets)[1];
            bool misEnPause = true;
            while (misEnPause && !quitter)
            {
                misEnPause = false;
                misEnPause = DemanderLaCase(ref ligne, ref colonne, tableau);//Demande la case à l'utilisateur et possibilité de mettre le jeu en pause
                if (misEnPause) //Mise en pause de l'utilisateur affichage des options : 
                //sauvegarder 
                //recommencer
                //quitter du menu du jeu
                {
                    quitter = Menu(nbCartesParUplets, nbUplets, tableau, tableauResult, nbrTentatives, nbMaxTentatives, nbrJoueurs, tourJoueurs, pseudos, tableauScore, reste, ligne, colonne, compteurCartes);
                }
            }
            compteurCartes++;
            if (!quitter)
            {
                tableau[ligne, colonne] = tableauResult[ligne, colonne];//Modifie le tableau en affichant la carte 
                if (compteurCartes == 1) //Stockage du premier symbole retourné au cours de la tentative
                {
                    symbole = tableauResult[ligne, colonne];
                }
                //Verification si c'est le même symbole que la carte precedement retourné
                else
                {
                    if (tableau[ligne, colonne] != symbole)
                    {
                        symbolediff = true;
                    }
                }
                AfficherTabCouleur(tableau, ligne, colonne); //Affichage du tableau à chaque carte retourné
            }
        } while (!(symbolediff) && compteurCartes < nbCartesParUplets && !quitter);
        if (symbolediff) //Si il y a eu erreur on retourne les cartes dévoilés
        {
            nbrTentatives++;
            if (partieExistante)
                for (int i = 0; i < tableau.GetLength(0); i++)
                {
                    for (int j = 0; j < tableau.GetLength(1); j++)
                    {
                        if (tableauTemp[i, j] == symbole)
                            tableauTemp[i, j] = '*';
                    }
                }
            tableau = tableauTemp;
            tourJoueurs = (tourJoueurs + 1) % nbrJoueurs;
            ligne = -1;
            colonne = -1;
            compteurCartes = 0;
            Thread.Sleep(3000);
            if (nbMaxTentatives - nbrTentatives + 1 > 0)
                Console.Clear();
        }
        else if (!quitter) //Si il n'y a pas eu d'erreur on décrémente le nombre de n-Uplets qui restent
        {
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\mywavfile.wav"); poser la question
            //player.Play();
            reste--;
            if (nbrJoueurs > 1)
                tableauScore[tourJoueurs]++;
            ligne = -1;
            colonne = -1;
            compteurCartes = 0;
            Thread.Sleep(1000);
        }
    } while (!(TesterLaVictoire(tableau)) && nbrTentatives <= nbMaxTentatives && !quitter);//On vérifie si toutes les cartes ont été retournés ou si c'est le nombre de tentatives max est dépassé ou si l'utilisateur veut quitter la partie

    if (TesterLaVictoire(tableau))//L'utilisateur a découvert toutes les cartes en un nombre de tentatives maximum
    {
        if (nbrJoueurs == 1)
        {
            if (nbMaxTentatives < int.MaxValue - 100)
            {
                Console.WriteLine("====================================================================================================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\t\tBravo vous avez découvert toutes les cartes en {nbrTentatives} tentatives.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("====================================================================================================");
            }
            else
            {
                Console.WriteLine("====================================================================================================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\t\tBravo vous avez découvert toutes les cartes, vous avez gagné !");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("====================================================================================================");
            }
        }
        else
        {
            Gagnant(tableauScore, pseudos, nbrTentatives, reste, nbCartesParUplets);
        }
    }

    else if (!quitter)//L'utilisateur n'a pas découvert toutes les cartes en un nombre de tentatives maximum
    {
        if (nbrJoueurs == 1)
        {
            Console.WriteLine("====================================================================================================");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t\tDommage vous n'avez pas découvert toutes les cartes en {nbrTentatives} tentatives.\n\t\tIl vous manquait {reste} combinaison(s) de {nbCartesParUplets} cartes");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("====================================================================================================");
        }
        else
        {
            Gagnant(tableauScore, pseudos, nbrTentatives, reste, nbCartesParUplets);
        }
        Affichertab(tableauResult);
    }

    return quitter;

}

//Options
//Calcul de l'optimisation de la taille du tableau

int[] OptimiserLaTaille(int nbCartesParUplet, int nbUplet)
{
    double racine = Math.Sqrt(nbCartesParUplet * nbUplet);
    int produit = nbCartesParUplet * nbUplet;

    for (int i = (int)racine; i > 0; i -= 1)
    {
        if (produit % i == 0)
        {
            return new int[] { i, produit / i };
        }
    }
    return new int[] { nbCartesParUplet, nbUplet };
}

bool Memory()
{
    int typeCaract, nbLignesTableau, nbColonnesTableau, nbCartesParUplets, nbUplets, nbrJoueurs, nbrTentatives, nbMaxTentatives, tourJoueurs, reste, ligneChoisi, colonneChoisi, compteurCartes;
    int[] tableauScore;
    string[] pseudos;
    char[,] tableau;
    char[,] tableauResult;
    string path = @"c:.\PartiePrecedente.txt";
    bool quitter = false;
    bool existe = File.Exists(path);
    string reponse = "non";

    //Si il existe une partie précédente
    if (existe)
    {
        Console.WriteLine($"\t\t Il existe une partie sauvegardée, voulez-vous la reprendre(tapez oui ou non)");
        do
        {
            Console.Write(("\t \t \t \t \t \t"));
            reponse = Console.ReadLine()!; //Demande à l'utilisateur si il veut reprendre sa partie précédente
        } while (reponse != "oui" && reponse != "non");
    }

    //Si l'utilisateur veut jouer avec la partie précédente
    if (reponse == "oui")
    {
        //Récupération des infos 
        try
        {
            StreamReader sr = new StreamReader(path); //Ouverture du fichier
            string line = sr.ReadLine()!;
            nbLignesTableau = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            nbColonnesTableau = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            nbCartesParUplets = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            nbUplets = Convert.ToInt32(line);
            tableau = new char[nbLignesTableau, nbColonnesTableau];
            tableauResult = new char[nbLignesTableau, nbColonnesTableau];
            for (int i = 0; i < nbLignesTableau; i++)
            {
                line = sr.ReadLine()!;
                for (int j = 0; j < nbColonnesTableau; j++)
                {
                    tableau[i, j] = Convert.ToChar(line[j]);
                }
            }

            for (int i = 0; i < nbLignesTableau; i++)
            {
                line = sr.ReadLine()!;
                for (int j = 0; j < nbColonnesTableau; j++)
                {
                    tableauResult[i, j] = Convert.ToChar(line[j]);
                }
            }
            line = sr.ReadLine()!;
            nbMaxTentatives = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            nbrTentatives = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            nbrJoueurs = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            tourJoueurs = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            ligneChoisi = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            colonneChoisi = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            compteurCartes = Convert.ToInt32(line);
            line = sr.ReadLine()!;
            reste = Convert.ToInt32(line);
            pseudos = new string[nbrJoueurs];
            for (int j = 0; j < nbrJoueurs; j++)
            {
                pseudos[j] = sr.ReadLine()!;
            }
            tableauScore = new int[nbrJoueurs];
            for (int j = 0; j < nbrJoueurs; j++)
            {
                line = sr.ReadLine()!;
                tableauScore[j] = Convert.ToInt32(line);
            }
            sr.Close();//fermeture du fichier
            Console.WriteLine();
            Console.WriteLine("\t \t Mettre la partie en pause, taper 'p' à tout moment.");
            //Début de la partie
            quitter = Jouer(ref tableau, tableauResult, nbCartesParUplets, nbUplets, nbrJoueurs, pseudos, nbrTentatives, tourJoueurs, ref tableauScore, nbMaxTentatives, reste, ligneChoisi, colonneChoisi, compteurCartes);

            //Si l'utilisateur a terminé la partie on supprime le fichier de sauvegarde
            if (!quitter)
                File.Delete(path);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }

    //L'utilisateur ne souhaite pas utiliser la partie sauvegardée
    else if (reponse == "non")
    {
        typeCaract = DemanderCaract();
        nbCartesParUplets = DemanderN();
        nbUplets = DemanderNuplets(typeCaract);
        tableau = InitierTabGrille(nbCartesParUplets, nbUplets);
        tableauResult = InitierTabCaractère(nbCartesParUplets, nbUplets, typeCaract);
        nbrJoueurs = NombreJoueurs(); //Demande  du nombre de joueurs
        pseudos = DemanderPseudonymes(nbrJoueurs);
        nbrTentatives = 1; //Variable pour compter le nombres de tentatives
        tourJoueurs = 0;//Pour savoir à qui est le tour
        tableauScore = new int[nbrJoueurs];
        nbMaxTentatives = DemanderNbTentativesMax(nbCartesParUplets, nbUplets); //Définition du nombre maximum de tentatives (Voir entête)
        reste = nbUplets; //Nombre de N-Uplets qui reste au cours de la partie
        ligneChoisi = -1;
        colonneChoisi = -1;
        compteurCartes = 0;
        //Début de la partie
        Console.WriteLine("\t \t Si vous souhaitez mettre la partie en pause, taper 'p' à tout moment.");
        quitter = Jouer(ref tableau, tableauResult, nbCartesParUplets, nbUplets, nbrJoueurs, pseudos, nbrTentatives, tourJoueurs, ref tableauScore, nbMaxTentatives, reste, ligneChoisi, colonneChoisi, compteurCartes);

        //Si l'utilisateur a terminé sa partie
        if (!quitter)
            //Vérification si cette partie n'a pas été sauvegardée
            if (SuppressionSauvegarde(tableauResult, nbrJoueurs, pseudos, tableauScore, path))
                //Si oui suppression du fichier 
                File.Delete(path);
    }

    // Demande à la fin de la partie si l'utilisateur veut recommencer une nouvelle partie ou quitter le jeu 
    if (!quitter)
    {
        Console.Write("\n");
        Console.WriteLine("\t \t =============================== Menu ===============================");
        Console.Write("\n");
        Console.WriteLine("\t \t                Tapez 'recommencer' pour recommenncer une partie.                                ");
        Console.Write("\n");
        Console.WriteLine("\t \t                     Tapez quitter pour quitter le jeu.                                ");
        Console.Write("\n");
        Console.WriteLine("\t \t ======================================================================");
        Console.Write("\t \t \t \t \t");
    }
    return quitter;
}

//Si il existe une partie sauvegardée compare avec la partie qui vient de se terminer pour savoir si on supprime la sauvegarde ou pas
bool SuppressionSauvegarde(char[,] tableauResult, int nbrJoueurs, string[] pseudos, int[] tableauScore, string path)
{

    bool existe = File.Exists(path);
    //Si il existe une partie sauvegardée
    if (existe)
    {
        try
        {
            StreamReader sr = new StreamReader(path);//Ouvre le fichier
            //Récupération des informations de la partie sauvegardée
            string line = sr.ReadLine()!;
            int nbLignesTableau2 = Convert.ToInt32(line);
            if (Convert.ToInt32(line) != tableauResult.GetLength(0)) //Comparaison 
                return false;
            line = sr.ReadLine()!;
            int nbColonnesTableau2 = Convert.ToInt32(line);
            if (Convert.ToInt32(line) != tableauResult.GetLength(1)) //Comparaison 
                return false;
            int compteur = 0;
            int nbrJoueurs2;
            while (compteur < 2 + nbLignesTableau2)
            {
                line = sr.ReadLine()!;
                compteur++;
            }
            for (int i = 0; i < nbLignesTableau2; i++)
            {
                line = sr.ReadLine()!;
                for (int j = 0; j < nbColonnesTableau2; j++)
                {
                    if (tableauResult[i, j] != Convert.ToChar(line[j]))
                        return false;
                }
            }
            compteur = 0;
            while (compteur < 2)
            {
                line = sr.ReadLine()!;
                compteur++;
            }
            line = sr.ReadLine()!;
            nbrJoueurs2 = Convert.ToInt32(line);
            if (nbrJoueurs != nbrJoueurs2) //Comparaison 
                return false;
            compteur = 0;
            while (compteur < 2)
            {
                line = sr.ReadLine()!;
                compteur++;
            }
            for (int j = 0; j < nbrJoueurs2; j++)
            {
                if (pseudos[j] != sr.ReadLine()!)
                    return false;
            }
            for (int j = 0; j < nbrJoueurs2; j++)
            {
                line = sr.ReadLine()!;
                if (tableauScore[j] != Convert.ToInt32(line)) //Comparaison 
                    return false;
            }
            sr.Close(); //fermeture du fichier

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    return true;
}


//Main
bool quitter;
string rep = "";
Console.WriteLine();
Console.WriteLine("****************************************************************************************************");
Console.WriteLine("****************************************************************************************************");
Console.Write("**************              ");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write("N\tU\tP\tL\tE\tM\tO             ");
Console.ForegroundColor = ConsoleColor.White;
Console.Write("**************\n");
Console.WriteLine("****************************************************************************************************");
Console.WriteLine("****************************************************************************************************");
do
{
    Console.WriteLine();
    quitter = Memory();
    if (!quitter)
    {
        rep = Console.ReadLine()!;
        if (rep == "recommencer")
        {
            Console.Clear();
        }
    }

} while (!quitter && rep == "recommencer");
Console.WriteLine();
Console.Write("******************************         ");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write("Merci d'avoir joué. A bientôt!         ");
Console.ForegroundColor = ConsoleColor.White;
Console.Write("******************************");