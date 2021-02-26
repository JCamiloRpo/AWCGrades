using AppMovil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovil.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageNotasMateria : ContentPage
	{
		public PageNotasMateria ()
		{
			InitializeComponent ();
            Inicializar();
		}

        private void Inicializar()
        {
            TxEstudiante.Text = PageInicio.Usuario;
            TxMateria.Text = PageNotasGenerales.Materia;
            List<Nota> notas = new List<Nota>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<NotasXEstudiante>();
                string sql = "SELECT * FROM NotasXEstudiante INNER JOIN PlanXMateria ON NotasXEstudiante.IdPlan = PlanXMateria.IdPlan WHERE Usuario ='" + TxEstudiante.Text + "' AND IdMateria = '" + TxMateria.Text + "'";
                SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                List<NotasXEstudiante> conidnota = cmd.ExecuteQuery<NotasXEstudiante>();
                List<PlanXMateria> conidmateria = cmd.ExecuteQuery<PlanXMateria>();
                for (int i = 0; i < conidnota.Count; i++)
                {
                    notas.Add(new Nota { Descripcion = conidmateria[i].Descripcion + "-" + conidmateria[i].Porcentaje + "% = " + conidnota[i].Nota.ToString() });
                }
            }
            LtNotas.ItemsSource = notas;

        }

        public class Nota
        {
            public string Descripcion { get; set; }
        }

    }
}