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
	public partial class PageNotasGenerales : ContentPage
	{
        public static string Materia { get; set; }

		public PageNotasGenerales ()
		{
			InitializeComponent ();
            Inicializar();
        }

        public void Inicializar()
        {
            TxEstudiante.Text = PageInicio.Usuario;
            List<Nota> notas = new List<Nota>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<NotasXEstudiante>();
                string sql = "SELECT * FROM NotasXEstudiante INNER JOIN PlanXMateria ON NotasXEstudiante.IdPlan = PlanXMateria.IdPlan WHERE Usuario ='" + TxEstudiante.Text + "'";
                SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                List<NotasXEstudiante> conidnota = cmd.ExecuteQuery<NotasXEstudiante>();
                List<PlanXMateria> conidmateria = cmd.ExecuteQuery<PlanXMateria>();
                double AVG = 0;
                string idmateria = "";
                for (int i = 0; i < conidnota.Count; i++)
                {
                    AVG += conidnota[i].Nota * (conidmateria[i].Porcentaje/100d);
                    idmateria = conidmateria[i].IdMateria;
                    if (conidnota.Count > (i + 1))
                    {
                        if (conidmateria[i].IdMateria.Equals(conidmateria[i + 1].IdMateria) == false)
                        {
                            notas.Add(new Nota { Materia = idmateria, NotaProm = "Nota promedio " + AVG });
                            AVG = 0;
                            idmateria = "";
                        }
                    }
                    else
                    {
                        notas.Add(new Nota { Materia = idmateria, NotaProm = "Nota promedio " + AVG });
                    }
                }
            }
            LtNotas.ItemsSource = notas;

        }

        public class Nota
        {
            public string Materia { get; set; }
            public string NotaProm { get; set; }

        }

        private void LtNotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var nota = e.SelectedItem as Nota;
            if (nota != null)
            {
                Materia = nota.Materia;
                Navigation.PushAsync(new PageNotasMateria());
                LtNotas.SelectedItem = false;
            }
        }
    }
}