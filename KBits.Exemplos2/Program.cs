using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBits.Exemplos2
{
    class Program
    {

        //Para utilizar algumas funcionalidades do c# 7.0 no visual studio "2015" pode ser necessario:
        /*
         * 1 - Incluir no campo “Conditional compilation symbols” os valores __DEMO__ e __DEMO_EXPERIMENTAL__, separando os mesmos por vírgula.
         * 2 - Instalar o pacote nuget: Install-Package Microsoft.Net.Compilers
         */
        static void Main(string[] args)
        {
        }

        #region Pattern Matching

        //c# 7.0 

        //Classe com herança
        public abstract class Cotacao
        {
            public DateTime DataCotacao { get; set; }
            public abstract string SiglaMoeda { get; }
            public abstract string NomeMoeda { get; }
        }

        public class CotacaoDolar : Cotacao
        {
            public override string SiglaMoeda
            { get { return "USD"; } }

            public override string NomeMoeda
            { get { return "Dólar norte-americano"; } }

            public double ValorComercial { get; set; }
            public double ValorTurismo { get; set; }
        }

        public class CotacaoEuro : Cotacao
        {
            public override string SiglaMoeda
            { get { return "EUR"; } }

            public override string NomeMoeda
            { get { return "Euro"; } }

            public double ValorCotacao { get; set; }
        }

        //Forma antiga de validar o tipo do Objeto. é Necessario dar Cast após a validação.
        public static void ExibirInformacoesCotacao(Cotacao cotacao)
        {
            double valorCotacao = 0;
            if (cotacao is CotacaoDolar)
            {
                valorCotacao =
                    ((CotacaoDolar)cotacao).ValorComercial;
            }
            else if (cotacao is CotacaoEuro)
            {
                valorCotacao =
                    ((CotacaoEuro)cotacao).ValorCotacao;
            }

            Console.WriteLine(new String('-', 40));
            Console.WriteLine($"Data: {cotacao.DataCotacao:dd/MM/yyyy}");
            Console.WriteLine($"Sigla: {cotacao.SiglaMoeda}");
            Console.WriteLine($"Moeda: {cotacao.NomeMoeda}");
            Console.WriteLine($"Valor: {valorCotacao:0.0000}");
        }

        //C# 7.0 
        /* A vantagem deste novo mecanismo está em eliminar a necessidade de codificação de um typecast, 
        contribuindo assim para um código mais limpo e direto.
        O cast é feito automaticamente 
        */

        public static void ExibirInformacoesCotacaoNovo(Cotacao cotacao)
        {
            double valorCotacao = 0;
            if (cotacao is CotacaoDolar dolar)
                valorCotacao = dolar.ValorComercial;
            else if (cotacao is CotacaoEuro euro)
                valorCotacao = euro.ValorCotacao;

            Console.WriteLine(new String('-', 40));
            Console.WriteLine($"Data: {cotacao.DataCotacao:dd/MM/yyyy}");
            Console.WriteLine($"Sigla: {cotacao.SiglaMoeda}");
            Console.WriteLine($"Moeda: {cotacao.NomeMoeda}");
            Console.WriteLine($"Valor: {valorCotacao:0.0000}");
        }

        #endregion

        #region Binary Literals e Digit Separators
        //c# 7

        public class TesteBinaryLiterals
        {
            /*
             * Versões anteriores do C# possibilitavam a declaração de valores numéricos no formato hexadecimal (recurso conhecido como “Hexadecimal Literals”),
             *  com tais representação sendo precedidas por “0x” ou “0X”
             */

            private const int LETRA_Y_HEX = 0x59; // 59 (hexadecimal) = 79 (decimal)
            private const int LETRA_Z_HEX = 0X5A; // 5A (hexadecimal) = 80 (decimal)

            /*
             * O C# 7 traz também a possibilidade de se declarar valores no padrão binário, recurso este chamado “Binary Literals”.
             * um exemplo de uso desta opção, com os valores binários sendo antecedidos por “0b” ou “0B”:
             */

            private const int LETRA_Y_BIN = 0b01011001; // 01011001 (binário) = 89 (decimal)
            private const int LETRA_Z_BIN = 0B01011010; // 01011010 (binário) = 90 (decimal)
 
            /*
             * Ao representar números extensos é comum que se utilizem separadores, de forma a tornar mais legível a 
             * visualização dos mesmos. O recurso do C# 7 chamado “Digit Separator” tem essa 
             * função, fazendo uso para isto do caracter “_” (underline) na separação de sequências de algarismos.
             */ 

            private const int LETRA_Y_BIN2 = 0b01_01_10_01; // 01011001 (binário) = 89 (decimal)
            private const int LETRA_Z_HEX2 = 0X5A; // 5A (hexadecimal) = 80 (decimal)
            private const int POPULACAO_ESTIMADA_BRASIL = 204_500_000;
            private const double RENDA_PER_CAPITA_BRASIL_USD = 11_208.08;


        }


        #endregion

        #region  Ref Returns
        //C# 7.0
        /*
        Em releases anteriores do C# a utilização da palavra-chave ref se resumia à
        passagem de parâmetros por referência na invocação de métodos. Com a versão 7 esta capacidade foi 
        estendida também para o retorno de métodos, sendo que este recurso foi batizado como Ref Returns.
        */

        public class ClasseExemplo
        {
            private int _contador = 0;

            public void Incrementar()
            {
                _contador++;
            }

            public int ObterValorContador()
            {
                return _contador;
            }

            public ref int ObterContador()
            {
                return ref _contador;
            }
        }

        public void ChamaRef()
        {
            ClasseExemplo exemplo = new ClasseExemplo();
            exemplo.Incrementar();
            Console.WriteLine(exemplo.ObterValorContador());
 
            ref int teste = ref exemplo.ObterContador();
            teste++;
            Console.WriteLine(exemplo.ObterValorContador());
            Console.ReadKey();
        }

        #endregion

        #region Local Functions
        //C# 7.0

        /*
         * O C# 7 agora também permite a declaração de funções dentro de métodos convencionais, algo antes possível apenas no corpo de uma classe.
         *  Do ponto de vista prático, este recurso batizado como Local Functions permitirá que se isolem métodos que teriam a 
         *  princípio uma abrangência de uso bem restrita (normalmente sendo invocados por uma única operação).
         *  
         *  Uma Local Function tende a ser interessante em cenários nos quais o uso de um método estaria restrito a uma operação específica. Além da sintaxe convencional, 
         *  este novo recurso do C# 7 pode ser declarado através da utilização de expressões lambdas.
         */

        public class TesteFunctions
        {
            public void TestaFunction()
            {
                //Local Function
                void ExibirHorarioAtual()
                {
                    Console.WriteLine(
                        $"Horário atual: {DateTime.Now.ToString("HH:mm:ss")}");

                }

                ExibirHorarioAtual();

                Console.WriteLine("Aguarde alguns segundos...");
                Random r = new Random();
                Thread.Sleep(new Random().Next(3000, 6000));

                ExibirHorarioAtual();
                Console.ReadKey();
            }
        }


        #endregion

    }
}
