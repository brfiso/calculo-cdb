using B3.Api.DTO;
using B3.Api.Interfaces;
using B3.Api.Models;
using System.Diagnostics.Metrics;

namespace B3.Api.Services
{
    public class CdbService : ICdbService
    {


        public Cdb Calcular(CalculoCdb calculo)
        {
            var valorInicial = calculo.ValorMonetario;
            var valorFinal = 0M;

            // VF = VI x [1 + (CDI x TB)]
            for (int i = 1; i <= calculo.MesesResgate; i++) { 
                valorFinal = valorInicial * (1 + TAXA);
                valorInicial = valorFinal;
            }


            decimal valorLiquido = ObterValorLiquido(calculo.ValorMonetario, valorFinal, calculo.MesesResgate);

            return new Cdb()
            {
                ValorBruto = valorFinal,
                ValorLiquido = valorLiquido
            };
        }

        public decimal ObterValorLiquido(decimal valorInicial, decimal valorBruto, int mesesResgate)
        {
            var taxa_ir = ObterTaxaIR(mesesResgate);
            var rendimento = valorBruto - valorInicial;

            return valorInicial + rendimento * (1 - (taxa_ir / 100));
        }

        public decimal ObterTaxaIR(int mesesResgate)
        {
            // Em uma aplicação real esses valores provavelmente viriam de uma tabela.
            if (mesesResgate <= 6)
                return 22.5M;
            else if (mesesResgate <= 12)
                return 20;
            else if (mesesResgate <= 24)
                return 17.5M;
            else
                return 15M;
        }

        #region Constantes

        // Valores definidos pelo exercício
        public const decimal CDI = 0.9M / 100;
        public const decimal TB = 108M / 100;
        public decimal TAXA { get; } = CDI * TB;

        #endregion
    }
}
