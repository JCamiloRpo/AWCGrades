using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AppMovil.Models
{
    public class NotasXEstudiante
    {
        [PrimaryKey, NotNull]
        public string IdNota { get; set; }
        [ForeignKey(typeof(Usuarios)), NotNull]
        public string Usuario { get; set; }
        [ForeignKey(typeof(PlanXMateria)), NotNull]
        public string IdPlan { get; set; }
        [NotNull]
        public float Nota { get; set; }

        public NotasXEstudiante(string idPlan, string usuario, float nota)
        {
            IdPlan = idPlan;
            Usuario = usuario;
            Nota = nota;
            IdNota = IdPlan + "-" + Usuario;
        }

        public NotasXEstudiante()
        {
        }
    }
}
