using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TesteTecnicoP.Models;

namespace AmigoTeste
{
    [TestClass]
    public class AmigoTest
    {
        List<Amigo> amigos = new List<Amigo>()
            {
                new Amigo() { Nome = "MINAS GERAIS", Latitude =-18.55, Longitude = -44.55},
                new Amigo() { Nome = "SAO PAULO", Latitude =-22, Longitude = -49},
                new Amigo() { Nome = "RIO DE JANEIRO", Latitude =-22.25, Longitude = -42.5},
                new Amigo() { Nome = "BAHIA", Latitude =-12, Longitude = -42},
                new Amigo() { Nome = "RIO GRANDE DO SUL", Latitude =-30, Longitude = -53.5},
                new Amigo() { Nome = "ACRE", Latitude =-9, Longitude = -70},
                new Amigo() { Nome = "TOCANTINS", Latitude =-10.5, Longitude = -48},
                new Amigo() { Nome = "MATO GROSSO", Latitude =-13, Longitude = -56},
                new Amigo() { Nome = "MATO GROSSO DO SUL", Latitude =-20.5, Longitude = -55},
                new Amigo() { Nome = "ESPIRITO SANTO", Latitude =-20, Longitude = -40.75}
            };

        [TestMethod]
        public void TesteListarAmigosMaisProximos()
        {
            TesteTecnicoP.Program.listaAmigos.AddRange(amigos);
            TesteTecnicoP.Program.CalcularDistancia();
            TesteTecnicoP.Program.ListaAmigosMaisProximos();
        }

        [TestMethod]
        public void TesteLocalizarAmigo()
        {
            TesteTecnicoP.Program.listaAmigos.AddRange(amigos);
            TesteTecnicoP.Program.CalcularDistancia();
            TesteTecnicoP.Program.ConsultarAmigo("SAO PAULO");
        }
    }
}