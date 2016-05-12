using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dames;

namespace DameUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dame = new Damier();
            Assert.IsNotNull(dame);

            Case c1 = dame.getCase(0, 0);
            Assert.IsNotNull(c1);
            Assert.IsFalse(c1.Ocuppe);

            //Ajout d'une pièce en 0,0
            Pion p1 = new Pion(couleur.BLANC);
            c1.MettrePiece(p1);
            Assert.IsTrue(c1.Ocuppe);
            Assert.IsFalse(dame.getCase(1, 1).Ocuppe);

            //Bouger la pièce en 1,1
            try
            {
                dame.BougerPiece(0, 0, 1, 1);
            }
            catch(DameException e)
            {
                Assert.Fail(e.Message);
            }
            
            Assert.IsFalse(c1.Ocuppe);
            Assert.IsTrue(dame.getCase(1, 1).Ocuppe);

            //Pose pièce en 2,2
            Pion p2 = new Pion(couleur.NOIR);
            dame.getCase(2, 2).MettrePiece(p2);

            //Bouger pion de 1,1 en 3,3 pour manger le pion en 2,2
            try
            {
                dame.BougerPiece(1, 1, 3, 3);
            }
            catch (DameException e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsFalse(dame.getCase(2, 2).Ocuppe);
            


        }
    }
}
