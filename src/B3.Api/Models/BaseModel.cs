namespace B3.Api.Models
{
    public abstract class BaseModel
    {

        /// <summary>
        /// Verifica se o Model é válido
        /// </summary>
        /// <returns></returns>
        public virtual bool EhValido()
        {
            return !ErrosValidacao.Any();
        }

        public void AdicionarErroValidacao(string erro) =>
            ErrosValidacao.Add(erro);

        public void LimparErrosValidacao() =>
            ErrosValidacao.Clear();

        public List<string> ErrosValidacao { get; private set; } = new();
    }
}
