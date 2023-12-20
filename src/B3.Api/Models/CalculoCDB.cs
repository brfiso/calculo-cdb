using System.ComponentModel.DataAnnotations;

namespace B3.Api.Models;

public class CalculoCdb : BaseModel
{
    [Required]
    //[Range(1, double.MaxValue, ErrorMessage = "O valor monetário deve ser positivo.")]
    public decimal ValorMonetario { get; set; }

    [Required]
    //[Range(2, int.MaxValue, ErrorMessage = "Prazo mínimo de 2 meses para o resgate.")]
    public int MesesResgate { get; set; }

    public override bool EhValido()
    {
        ErrosValidacao.Clear();

        if (MesesResgate <= 1)
            AdicionarErroValidacao("Prazo mínimo de 2 meses para o resgate.");

        if (ValorMonetario <= 0)
            AdicionarErroValidacao("O valor monetário deve ser positivo.");

        if (MesesResgate > 1000)
            AdicionarErroValidacao("Máximo de 1000 meses.");

        if (MesesResgate > 1000)
            AdicionarErroValidacao("Máximo de 1000 meses.");

        if (ValorMonetario > 100_000_000)
            AdicionarErroValidacao("Máximo de 1 milhão neste exemplo.");

        return !ErrosValidacao.Any();
    }
}
