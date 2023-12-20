using System.ComponentModel.DataAnnotations;

namespace B3.Api.Models;

public class CalculoCDB : BaseModel
{
    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "O valor monetário deve ser positivo.")]
    public decimal ValorMonetario { get; set; }

    [Required]
    [Range(2, int.MaxValue, ErrorMessage = "Prazo mínimo de 2 meses para o resgate.")]
    public int MesesResgate { get; set; }

    public override bool EhValido()
    {
        ErrosValidacao.Clear();

        if (MesesResgate <= 1)
            AdicionarErroValidacao("Prazo mínimo de 2 meses para o resgate.");

        if (ValorMonetario <= 0)
            AdicionarErroValidacao("O valor monetário deve ser positivo.");

        return !ErrosValidacao.Any();
    }
}
