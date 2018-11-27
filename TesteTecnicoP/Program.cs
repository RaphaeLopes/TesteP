using System;
using System.Collections.Generic;
using System.Linq;
using TesteTecnicoP.Models;

namespace TesteTecnicoP
{
    public class Program
    {

        private static void Msg(string msg) => Console.WriteLine($"{msg}");
        public static List<Amigo> listaAmigos = new List<Amigo>();

        /// <summary>
        /// Calcula a distancia de todos os amigos
        /// </summary>
        public static void CalcularDistancia()
        {
            if (listaAmigos.Count() == 0 || listaAmigos == null)
                Msg("Não existe Amigos cadastrados.");

            foreach (Amigo amigo in listaAmigos)
            {
                var lista = new List<Amigo>();
                lista.AddRange(listaAmigos);
                lista.Remove(amigo);
                foreach (Amigo amigoProximo in lista)
                {
                    // Como aqui não estamos preocupados com a distância em metros, mas apenas
                    // com quais amigos estão mais próximas, podemos usar a distância
                    // cartesiana normal
                    double x = amigo.Longitude - amigoProximo.Longitude;
                    double y = amigo.Latitude - amigoProximo.Latitude;
                    amigoProximo.Distancia = Math.Sqrt((x * x) + (y * y));
                }
                amigo.AmigosMaisProximos = lista.OrderBy(p => p.Distancia).Take(3).ToList();
            }
        }

        /// <summary>
        /// Pesquisa um amigo por nome
        /// </summary>
        /// <param name="Nome"></param>
        public static void ConsultarAmigo(string Nome)
        {
            var amigo = listaAmigos.Where(p => p.Nome == Nome).FirstOrDefault();
            if (amigo == null)
            {
                Msg("Amigo não encontrado");
                return;
            }

            foreach (var amigosMaisProximos in amigo.AmigosMaisProximos)
            {
                Msg(string.Concat("Amigo: ", amigosMaisProximos.Nome, " Distancia " + amigosMaisProximos.Distancia));
            }
        }

        /// <summary>
        /// Lista os amigos e os amigos proximos a eles
        /// </summary>
        public static void ListaAmigosMaisProximos()
        {
            if (listaAmigos == null || listaAmigos.Count() == 0)
            {
                Msg("Sua lista de amigos está vazia.");
                return;
            }

            foreach (var amigo in listaAmigos)
            {
                Msg(string.Concat("Amigo: ", amigo.Nome, " - Amigos Mais Proximos: " + amigo.AmigosMaisProximosStr));
            }
        }

        static void Main(string[] args)
        {
            for (;;)
            {
                GC.Collect();

                int operacao;
                Console.WriteLine("Operacoes: ");
                Console.WriteLine("1 - Cadastrar Amigo");
                Console.WriteLine("2 - Listar Amigos");
                Console.WriteLine("3 - Consultar Amigo");
                Console.WriteLine("99 - Sair");
                do
                {
                    Console.Write("Digite a operacao: ");
                    int.TryParse(Console.ReadLine().Trim(), out operacao);
                    if (operacao == 99)
                        return;
                } while (operacao < 1 || operacao > 3);

                var linha = string.Empty;
                switch (operacao)
                {
                    case 1:
                        InformarDados();
                        CalcularDistancia();
                        break;
                    case 2:
                        ListaAmigosMaisProximos();
                        break;
                    case 3:
                        Console.Write("Digite o Nome: ");
                        linha = Console.ReadLine().Trim().ToUpper();
                        int.TryParse(linha, out operacao);
                        if (operacao == 99)
                            return;
                        ConsultarAmigo(linha);
                        break;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Metodo que pega os dados da tela
        /// </summary>
        private static void InformarDados()
        {
            double lat, lng;
            var linha = string.Empty;
            bool bInformarDados = false;
            do
            {
                Amigo amigo = new Amigo();
                var nomeOK = false;
                do
                {
                    Console.Write("Digite o Nome: ");
                    linha = Console.ReadLine().Trim().ToUpper();
                    if (listaAmigos.Exists(p => p.Nome == linha))//Validar se o nome ja foi informado.
                    {
                        Msg("Nome já informado");
                        continue;
                    }
                    amigo.Nome = linha;
                    nomeOK = true;
                } while (!nomeOK);

                var latOK = false;
                do
                {
                    Console.Write("Digite a Latitude: ");
                    linha = Console.ReadLine().Trim();
                    if (double.TryParse(linha, out lat))
                    {
                        if (lat > -90 || lat < 90)
                        {
                            amigo.Latitude = lat;
                            latOK = true;
                        }
                        else
                        {
                            Msg("Latitude deve estar entre -90 e 90 graus.");
                        }
                    }
                    else
                    {
                        Msg("Latitude Inválida.");
                    }
                } while (!latOK);

                var lngOK = false;
                do
                {
                    Console.Write("Digite a Longitude: ");
                    linha = Console.ReadLine().Trim();
                    if (double.TryParse(linha, out lng))
                    {
                        if (lng > -180 || lng < 180)
                        {
                            amigo.Longitude = lng;
                            lngOK = true;
                        }
                        else
                        {
                            Msg("Longitude deve estar entre -180 e 180 graus.");
                        }
                    }
                    else
                    {
                        Msg("Longitude Inválida.");
                    }
                } while (!lngOK);

                //Validarse a latitude e longitude ja foi informado
                if (listaAmigos.Exists(p => p.Latitude == amigo.Latitude && p.Longitude == amigo.Longitude))
                {
                    Msg("amigo não incluido, Latitude e Longitude já informado.");
                    continue;
                }

                listaAmigos.Add(amigo);
                do
                {
                    Console.Write("Digitar um novo Amigo? S/N: ");
                    linha = Console.ReadLine().Trim().ToUpper();
                    bInformarDados = linha == "S";
                } while (linha != "S" && linha != "N");

            } while (bInformarDados);
        }
    }
}