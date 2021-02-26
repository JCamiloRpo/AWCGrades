using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AppMovil.Models
{
    public class PlanXMateria
    {
        [PrimaryKey, NotNull]
        public string IdPlan { get; set; }
        [ForeignKey(typeof(MateriaXSemestre)), NotNull]
        public string IdMateria { get; set; }
        [NotNull]
        public string Descripcion { get; set; }
        [NotNull]
        public float Porcentaje { get; set; }

        public PlanXMateria(string descripcion, string idMateria, float porcentaje)
        {
            Porcentaje = porcentaje;
            Descripcion = descripcion;
            IdMateria = idMateria;
            IdPlan = Descripcion + "-" + IdMateria;
        }

        public PlanXMateria()
        { 
        }
    }
}
