using SQLite;

namespace AppMovil.Models
{
    public class Semestres
    {
        [PrimaryKey, NotNull]
        public string Semestre { get; set; }
    }
}
