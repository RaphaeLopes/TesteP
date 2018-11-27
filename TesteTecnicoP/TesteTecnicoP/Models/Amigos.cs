using System.Collections.Generic;
using System.Linq;

namespace TesteTecnicoP.Models
{
    public class Amigo
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double Distancia { get; set; }
        public string AmigosMaisProximosStr
        {
            get
            {
                return string.Join(" - ", AmigosMaisProximos.Select(x => x.Nome).ToArray());
            }
        }
        public List<Amigo> AmigosMaisProximos { get; set; }

        public int Teste { get; set; }
    }
}