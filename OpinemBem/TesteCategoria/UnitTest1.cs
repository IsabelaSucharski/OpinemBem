using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpinemBem.Models;

namespace TesteCategoria
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //instanciar objeto
            Categoria obj = new Categoria();
            //escolher o metodo
            obj.Inserir();                        
        }
    }
}
