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
	public partial class PageSubirNota : ContentPage
	{
		public PageSubirNota ()
		{
			InitializeComponent ();
            Inicializar();
            InicialiarSQL();
        }

        private void Inicializar()
        {
            BtnSubir.Clicked += BtnSubir_Clicked; //Se crea el evento cuando se haga click en el botón
            PkIdPlan.Focused += Pk_IdPlanFocused;
            PkEstudiante.Focused += Pk_EstudianteFocused;
            PkIdMateria.Focused += Pk_IdMateriaFocused;
            PkSemestre.Focused += PkSemestre_Focused;
        }

        private void PkSemestre_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                {
                    conn.CreateTable<Semestres>();
                    string sql = "SELECT * FROM Semestres";
                    SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                    List<Semestres> consemestres = cmd.ExecuteQuery<Semestres>();
                    List<string> semestres = new List<string>();
                    foreach (Semestres i in consemestres)
                    {
                        semestres.Add(i.Semestre);
                    }
                    PkSemestre.ItemsSource = semestres;
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");
            }
        }

        private void Pk_IdMateriaFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (PkSemestre.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar un semestre", "Aceptar");
                    PkSemestre.Focus();
                    return;
                }
                else if(PkEstudiante.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar un estudiante", "Aceptar");
                    PkEstudiante.Focus();
                    return;
                }
                else
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<MateriaXSemestre>();
                        string sql = "SELECT * FROM MateriaXSemestre, MateriaXEstudiante WHERE MateriaXEstudiante.IdMateria = MateriaXSemestre.IdMateria AND Semestre ='" + PkSemestre.SelectedItem.ToString() + "' AND Usuario = '" + PkEstudiante.SelectedItem.ToString() +"'";
                        SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                        List<MateriaXSemestre> conidmaterias = cmd.ExecuteQuery<MateriaXSemestre>();
                        List<string> idmaterias = new List<string>();
                        foreach (MateriaXSemestre i in conidmaterias)
                        {
                            idmaterias.Add(i.IdMateria);
                        }
                        PkIdMateria.ItemsSource = idmaterias;
                    }
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");
            }
        }

        private void Pk_IdPlanFocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (PkIdMateria.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar una materia con semestre", "Aceptar");
                    PkIdMateria.Focus();
                    return;
                }
                else
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<PlanXMateria>();
                        string sql = "SELECT * FROM PlanXMateria WHERE IdMateria ='" + PkIdMateria.SelectedItem.ToString() + "'";
                        SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                        List<PlanXMateria> conidplan = cmd.ExecuteQuery<PlanXMateria>();
                        List<string> idplan = new List<string>();
                        foreach (PlanXMateria i in conidplan)
                        {
                            idplan.Add(i.IdPlan + "");
                        }
                        PkIdPlan.ItemsSource = idplan;
                    }
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");
            }
        }

        private void Pk_EstudianteFocused(object sender, FocusEventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                {
                    conn.CreateTable<MateriaXEstudiante>();
                    string sql = "SELECT * FROM Usuarios WHERE Grupo = 1";
                    SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                    List<Usuarios> conestudiantes = cmd.ExecuteQuery<Usuarios>();
                    List<string> estudiantes = new List<string>();
                    foreach (Usuarios i in conestudiantes)
                    {
                        estudiantes.Add(i.Usuario);
                    }
                    PkEstudiante.ItemsSource = estudiantes;
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");
            }
        }

        public void InicialiarSQL()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                {
                    conn.CreateTable<PlanXMateria>();
                    string sql = "SELECT * FROM PlanXMateria";
                    SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                    List<PlanXMateria> conidplan = cmd.ExecuteQuery<PlanXMateria>();
                    List<string> idplan = new List<string>();
                    foreach (PlanXMateria i in conidplan)
                    {
                        idplan.Add(i.IdPlan + "");
                    }
                    PkIdPlan.ItemsSource = idplan;

                    conn.CreateTable<Usuarios>();
                    sql = "SELECT * FROM Usuarios WHERE Grupo = 1";
                    cmd = new SQLiteCommand(conn) { CommandText = sql };
                    List<Usuarios> conestudiantes = cmd.ExecuteQuery<Usuarios>();
                    List<string> estudiantes = new List<string>();
                    foreach (Usuarios i in conestudiantes)
                    {
                        estudiantes.Add(i.Usuario);
                    }
                    PkEstudiante.ItemsSource = estudiantes;
                }
            }
            catch (Exception e)
            {
                DisplayAlert("Error", e.Message, "Aceptar");
            }
        }

        private void BtnSubir_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Validar materia diferentes de nulo
                if (PkIdPlan.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar un plan", "Aceptar");
                    PkIdPlan.Focus();
                    return;
                }
                else if (PkEstudiante.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar un estudiante", "Aceptar");
                    PkEstudiante.Focus();
                    return;
                }
                else if (String.IsNullOrEmpty(TxNota.Text))
                {
                    DisplayAlert("Error", "Debe ingresar una nota", "Aceptar");
                    TxNota.Focus();
                    return;
                }
                else
                {
                    if (float.TryParse(TxNota.Text, out float nota))
                    {
                        if (nota >= 0 && nota <= 5)
                        {
                            NotasXEstudiante notaestudiante = new NotasXEstudiante(PkIdPlan.SelectedItem.ToString(), PkEstudiante.SelectedItem.ToString(), nota);
                            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                            {
                                conn.CreateTable<NotasXEstudiante>();
                                string sql = "SELECT * FROM NotasXEstudiante WHERE IdNota ='" + notaestudiante.IdNota + "'";
                                SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql};
                                List<NotasXEstudiante> conidnota = cmd.ExecuteQuery<NotasXEstudiante>();
                                int r = 0;
                                if (conidnota.Count() > 0) r = conn.Update(notaestudiante);
                                else r = conn.Insert(notaestudiante);
                                if (r > 0) DisplayAlert("Subir", "Nota subida a estudiante", "Aceptar");
                                else DisplayAlert("Subir", "Nota no subida a estudiante", "Aceptar");
                            }
                            PkIdPlan.SelectedItem = null;
                            TxNota.Text = "";
                        }
                        else DisplayAlert("Subir", "Debe ser una nota entre 0 y 5", "Aceptar");
                    }
                    else DisplayAlert("Subir", "Debe escribir solo numeros para la nota (si es necesario con ',')", "Aceptar");
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");//Mostrar mensaje del error
            }
        }

    }
}