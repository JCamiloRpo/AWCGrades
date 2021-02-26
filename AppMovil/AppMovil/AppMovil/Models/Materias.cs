using SQLite;

namespace AppMovil.Models
{
    public class Materias
    {
        [PrimaryKey, NotNull]
        public string Materia { get; set; }
    }
}
