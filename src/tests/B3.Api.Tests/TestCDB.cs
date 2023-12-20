using B3.Api.DTO;
using B3.Api.Interfaces;
using B3.Api.Models;
using B3.Api.Services;

namespace B3.Api.Tests
{
    public class TestCDB
    {
        private readonly ICDBService _cdbService;

        public TestCDB()
        {
            _cdbService = new CDBService();
        }

        [Fact]
        public void CDB_MES_INVALIDO()
        {
            CalculoCDB cdb = new CalculoCDB();
            cdb.MesesResgate = 1;
            cdb.ValorMonetario = 1000;

            Assert.False(cdb.EhValido());
        }

        [Fact]
        public void CDB_Valor_NEGATIVO()
        {
            CalculoCDB cdb = new CalculoCDB();
            cdb.MesesResgate = 6;
            cdb.ValorMonetario = 0;

            Assert.False(cdb.EhValido());
        }

        [Fact]
        public void CDB_TAXAIR()
        {
            var cdbService = new CDBService();

            var taxa = cdbService.ObterTaxaIR(0);
            Assert.Equal(22.5M, taxa);


            var taxa2 = cdbService.ObterTaxaIR(6);
            Assert.Equal(22.5M, taxa2);

            var taxa3 = cdbService.ObterTaxaIR(7);
            Assert.Equal(20M, taxa3);

            var taxa4 = cdbService.ObterTaxaIR(12);
            Assert.Equal(20M, taxa4);

            var taxa5 = cdbService.ObterTaxaIR(24);
            Assert.Equal(17.5M, taxa5);

            var taxa6 = cdbService.ObterTaxaIR(13);
            Assert.Equal(17.5M, taxa6);

            var taxa7 = cdbService.ObterTaxaIR(int.MaxValue);
            Assert.Equal(15M, taxa7);
        }

        [Fact]
        public void CDB_ObterValorLiquido()
        {
            var cdbService = new CDBService();
            decimal valorInicial = 1000;
            decimal valorBruto = 1200;
            int meses = 6;

            var valorLiquido = cdbService.ObterValorLiquido(valorInicial, valorBruto, meses);

            var rendimento = valorBruto - valorInicial;
            var rendimentoLiquido = (rendimento * cdbService.ObterTaxaIR(meses) / 100);
            var valorLiquido_Calculado = valorInicial + rendimentoLiquido;

            Assert.Equal(valorLiquido, valorLiquido_Calculado);
        }

        [Fact]
        public void CDB_Calcular()
        {
            var cdbService = new CDBService();
            var taxa = 97.2M;
            decimal valorInicial = 1000;
            int mesesResgate = 12;



            var model = cdbService.Calcular(new CalculoCDB
            {
                ValorMonetario = valorInicial,
                MesesResgate = 12
            });

            var valorFinal = 0M;

            for (int i = 1; i < mesesResgate; i++)
            {
                valorFinal += valorInicial * (1 + taxa);
            }

            var modelTeste = new CDB
            {
                ValorBruto = valorFinal,
                ValorLiquido = cdbService.ObterValorLiquido(valorInicial, valorFinal, mesesResgate)

            };

            Assert.Equal(model.ValorLiquido, modelTeste.ValorLiquido);
            Assert.Equal(model.ValorBruto, modelTeste.ValorBruto);
        }
    }
}