using SQLite;

namespace AppMovil.Models
{
    class GrupoUsuarios
    {
        [PrimaryKey, NotNull]
        public int Grupo { get; set; }
    }
}
