using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chinok.Data;
using System.Linq;
using Chinook.Entities;

namespace Chinook.Data.Test
{
    [TestClass]
    public class ArtistDATest
    {
        [TestMethod]
        public void GetCount()
        {
            var da = new ArtistaDA();
            var cantidad = da.GetCount();
            Assert.IsTrue(cantidad > 0);
        }

        [TestMethod]
        public void GetListado()
        {
            var da = new ArtistaDA();
            var listado = da.Gets();
            Assert.IsTrue(listado.Count() > 0);

        }

        [TestMethod]
        public void GetsWithParam()
        {
            var da = new ArtistaDA();
            var listado = da.GetsWithParam("a%");
            Assert.IsTrue(listado.Count() > 0);

        }

        [TestMethod]
        public void GetsWithParamSP()
        {
            var da = new ArtistaDA();
            var listado = da.GetsWithParamSP("a%");
            Assert.IsTrue(listado.Count() > 0);

        }

        [TestMethod]
        public void InsertArtist()
        {
            var da = new ArtistaDA();
            var id = da.InsertArtist(new Artista() { Name = "Prueba"});
            Assert.IsTrue(id > 0);

        }


    }
}
