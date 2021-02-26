using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AppMovil.Models
{
    public class Usuarios
    {
        [PrimaryKey, NotNull]
        public string Usuario { get; set; }
        [NotNull]
        public string Contraseña { get; set; }
        [ForeignKey(typeof(GrupoUsuarios)), NotNull]
        public int Grupo { get; set; }
    }
}
