using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Convention */
//  - Les pieces noirs sont en haut
//  - La première case (haut gauche) est blanche
//  - Les lignes/colonnes vont de haut en bas et de gauche à droite. Origine 0;0 en haut a gauche
//  - Les pièces sont sur les cases noirs


namespace Dames
{
    public enum couleur { NOIR, BLANC }
    
    public class DameException : Exception
    {
        public DameException(string e):base(e)
        {
        }        
    }
    //TODO: Revoir les "enum":


    // Classe gérant le damier (plateau du jeu des dame):
    public class Damier
    {        
        /*
        public Case LireCase(int Ligne, int Colonne)
        {
            return _cases[Ligne,Colonne];
        } */

        public Damier()
        {
            _cases = new Case[10, 10];
            Init();
        }

        public void Init()
        {
            var debut = couleur.BLANC;

            for(int ligne = 0; ligne < 10; ligne++)
                for(int col = 0; col < 10; col++)
                {
                    _cases[ligne, col] = new Case(ligne, col, debut);

                    if(col < 9)
                        debut = debut == couleur.NOIR ? couleur.BLANC : couleur.NOIR;
                }
        }

        public void InitPiece()
        {
            for (int ligne = 0; ligne < 3; ligne++)
                for (int col = 0; col < 10; col++)
                {
                    Case c = getCase(ligne, col);
                    if(c.Couleur == couleur.NOIR)
                        c.MettrePiece(new Pion(couleur.NOIR));
                }
            for (int ligne = 7; ligne < 10; ligne++)
                for (int col = 0; col < 10; col++)
                {
                    Case c = getCase(ligne, col);
                    if (c.Couleur == couleur.NOIR)
                        c.MettrePiece(new Pion(couleur.BLANC));
                }
        }

        public void PionADame(Case c)
        {
            c.MettrePiece(new Dame(c.Piece.Couleur));
        }

        public void BougerPiece( int sourceLigne, int sourceCol, int cibleLigne, int cibleCol)
        {
            BougerPiece(_cases[sourceLigne, sourceCol], _cases[cibleLigne, cibleCol]);
        }

        public void BougerPiece(Case source , Case cible)
        {

            //Check de la piece presente 
            if (!source.Ocuppe)
                throw new DameException("Pas de pièce présente sur la case");

            if (cible.Ocuppe)
                throw new DameException("Pièce présente sur la case de destination");

            //Mouvement diagonal
            if (Math.Abs(source.Ligne - cible.Ligne) != Math.Abs(source.Colonne - cible.Colonne))
                throw new DameException("Mouvement non diagonal");

            if (cible.Ocuppe)
                throw new DameException("Pièce présente sur la case cible");

            //TODO: Checker mouvemenent de 1 en diagonal si aucune autre pièce et plus si pièce adverse pour chaque fonction
            if (source.Piece is Dame)
                BougerDame(source, cible);
            else
                BougerPion(source, cible);


            if (cible.Piece.Couleur == couleur.NOIR && cible.Ligne == 9 && cible.Piece is Pion ||
                cible.Piece.Couleur == couleur.BLANC && cible.Ligne == 0 && cible.Piece is Pion )
                PionADame(cible);
        }

        private void BougerDame(Case source, Case cible)
        {

        }

        private void BougerPion(Case source, Case cible)
        {
            //Savoir si le pion descend ou monte en fonction de la couleur
            if (source.Piece.Couleur == couleur.BLANC)
                if (source.Ligne - cible.Ligne >= 0)
                    throw new DameException("Déplacement impossible");

            if (source.Piece.Couleur == couleur.NOIR)
                if (cible.Ligne - source.Ligne >= 0)
                    throw new DameException("Déplacement impossible");

            //Mouvement de 1 max
            if (Math.Abs(cible.Ligne - source.Ligne) == 1)
            {
                cible.MettrePiece(source.Piece);
                source.EnleverPiece();
                return;
            }
            else if (Math.Abs(cible.Ligne - source.Ligne) == 2)
            {
                var x = cible.Ligne - source.Ligne > 0 ? 1 :  -1;
                var y = cible.Colonne - source.Colonne > 0 ? 1 : -1;
                cible.MettrePiece(source.Piece);
                Case interm = _cases[source.Ligne + x, source.Colonne + y];

                if (interm.Piece.Couleur == source.Piece.Couleur)
                    throw new DameException("Déplacement impossible");

                interm.EnleverPiece();
                source.EnleverPiece();
                return;
            }
            else
                throw new DameException("Déplacement impossible");
        }

        public Case getCase(int ligne, int col)
        {
            if (col <= 9 && col >= 0 && ligne <= 9 && ligne >= 0)
                return _cases[ligne, col];
            else
                throw new DameException("Hors case");
        }

        // Création d'un tableau représentant de damier:
        private Case[,] _cases;


    }

    // Classe fournissant les caractéristique d'une case sur le damier:
    public class Case
    {
        public couleur Couleur { get; private set; }
        public Case(int Ligne, int Colonne, couleur c)
        {
            _pce = null;
            this.Ligne = Ligne;
            this.Colonne = Colonne;
            Couleur = c;
        }

        public Case(int Ligne, int Colonne, Piece piece, couleur c)
        {
            _pce = piece;
            this.Ligne = Ligne;
            this.Colonne = Colonne;
            Couleur = c;
        }

        // Méthode spécifiant si la case est occupée ou non:
        public bool Ocuppe { get; private set; }

        public int Ligne { get; private set; }
        public int Colonne { get; private set; }

        public void EnleverPiece()
        {
            Piece = null;
        }

        public void MettrePiece(Piece piece)
        {
            Piece = piece;
        }

        // Déclaration d'une pièce de type Piece:
        private Piece _pce;

        public Piece Piece
        {
            get
            {
                return _pce;
            }
            private set
            {
                if(value != null)
                    Ocuppe = true;
                else                   
                    Ocuppe = false;
                _pce = value;

            }
        }
    }

    // Classe caractérisant une pièce (couleur, déplacement, mouvement d'attaque):
    public abstract class Piece
    {
        public couleur Couleur { get; private set; }

        public Piece(couleur c)
        {
            Couleur = c;
        }
    }


    public class Pion : Piece
    {
        public Pion(couleur c) : base(c)
        {
        }
    }

    public class Dame : Piece
    {
        public Dame(couleur c) : base(c)
        {
        }
    }

}
