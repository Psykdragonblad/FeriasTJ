namespace FeriasTJBase.Domain.Entities
{
    public class Usufruto
    {
        public int IdUsufruto { get; set; }
        public int IdFerias { get; set; }
        public DateTime UsufrutoInicial { get; set; }
        public DateTime UsufrutoFinal { get; set; }
        public bool Status { get; set; }
        public Ferias Ferias { get; set; } // Propriedade de navegação
    }
}
