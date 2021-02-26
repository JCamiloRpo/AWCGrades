using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AppMovil.Models
{
    public class MateriaXEstudiante
    {
        [PrimaryKey, NotNull]
        public string IdUsuario { get; set; }
        [ForeignKey(typeof(Usuarios)), NotNull]
        public string Usuario { get; set; }
        [ForeignKey(typeof(MateriaXSemestre)), NotNull]
        public string IdMateria { get; set; }

        public MateriaXEstudiante(string idMateria, string usuario)
        {
            IdMateria = idMateria;
            Usuario = usuario;
            IdUsuario = IdMateria + "-" + Usuario;
        }

        public MateriaXEstudiante()
        {
        }
    }
}
