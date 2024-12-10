using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FeriasTJBase.Domain.Entities
{
    [Table("usufruto")]
    public class Usufruto
    {
        public int IdUsufruto { get; set; }
        public int IdFerias { get; set; }
        public DateTime UsufrutoInicial { get; set; }
        public DateTime UsufrutoFinal { get; set; }
        public bool Status { get; set; }
        [JsonIgnore]
        public Ferias Ferias { get; set; } // Propriedade de navegação*/
    }
}
