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
	public partial class PageMateriaSemestre : ContentPage
	{
		public PageMateriaSemestre ()
		{
			InitializeComponent ();
            Inicializar();
            InicialiarSQL();
        }

        private void Inicializar()
        {
            BtnAgregar.Clicked += BtnAgregar_Clicked; //Se crea el evento cuando se haga click en el botón
            BtnEliminar.Clicked += BtnEliminar_Clicked;
            PkMateria.Focused += Pk_MateriaFocused;
            PkSemestre.Focused += Pk_SemestreFocused;
        }

        private void Pk_MateriaFocused(object sender, FocusEventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                {
                    conn.CreateTable<Materias>();
                    string sql = "SELECT * FROM Materias";
                    SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                    List<Materias> conmaterias = cmd.ExecuteQuery<Materias>();
                    List<string> materias = new List<string>();
                    foreach (Materias i in conmaterias)
                    {
                        materias.Add(i.Materia);
                    }
                    PkMateria.ItemsSource = materias;
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");
            }
        }

        private void Pk_SemestreFocused(object sender, FocusEventArgs e)
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

        public void InicialiarSQL()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                {
                    conn.CreateTable<Materias>();
                    string sql = "SELECT * FROM Materias";
                    SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                    List<Materias> conmaterias = cmd.ExecuteQuery<Materias>();
                    List<string> materias = new List<string>();
                    foreach (Materias i in conmaterias)
                    {
                        materias.Add(i.Materia);
                    }
                    conn.CreateTable<Semestres>();
                    sql = "SELECT * FROM Semestres";
                    cmd = new SQLiteCommand(conn) { CommandText = sql };
                    List<Semestres> consemestres = cmd.ExecuteQuery<Semestres>();
                    List<string> semestres = new List<string>();
                    foreach (Semestres i in consemestres)
                    {
                        semestres.Add(i.Semestre);
                    }
                    PkMateria.ItemsSource = materias;
                    PkSemestre.ItemsSource = semestres;
                }
            }
            catch(Exception e)
            {
                DisplayAlert("Error", e.Message, "Aceptar");
            }
        }

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Validar materia diferentes de nulo
                if (PkMateria.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe ingresar seleccionar una materia", "Aceptar");
                    PkMateria.Focus();
                    return;
                }
                else if (PkSemestre.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe ingresar seleccionar un semestre", "Aceptar");
                    PkSemestre.Focus();
                    return;
                }
                else
                {
                    MateriaXSemestre materiaxsemestre = new MateriaXSemestre (PkMateria.SelectedItem.ToString(), PkSemestre.SelectedItem.ToString());

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<MateriaXSemestre>();
                        int r = conn.Insert(materiaxsemestre);
                        if (r > 0) DisplayAlert("Agregar", "Materia agregada en el semestre", "Aceptar");
                        else DisplayAlert("Agregar", "Materia no agregada en el semestre", "Aceptar");
                    }
                    PkMateria.SelectedItem = null;
                    PkSemestre.SelectedItem = null;
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");//Mostrar mensaje del error
            }
        }

        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Validar materia diferentes de nulo
                if (PkMateria.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe ingresar seleccionar una materia", "Aceptar");
                    PkMateria.Focus();
                    return;
                }
                else if (PkSemestre.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe ingresar seleccionar un semestre", "Aceptar");
                    PkSemestre.Focus();
                    return;
                }
                else
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        int r = 0;
                        conn.CreateTable<MateriaXSemestre>();
                        string sql = "SELECT * FROM MateriaXSemestre WHERE Materia = '" + PkMateria.SelectedItem.ToString() + "' AND Semestre = '" + PkSemestre.SelectedItem.ToString() + "'";
                        SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                        List<MateriaXSemestre> conmateria = cmd.ExecuteQuery<MateriaXSemestre>();
                        MateriaXSemestre materia = conmateria[0];
                        if (conmateria.Count > 0) r = conn.Delete(materia);
                        if (r > 0) DisplayAlert("Agregar", "Materia eliminada en el semestre", "Aceptar");
                        else DisplayAlert("Agregar", "Materia no eliminada en el semestre", "Aceptar");
                    }
                    PkMateria.SelectedItem = null;
                    PkSemestre.SelectedItem = null;
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");//Mostrar mensaje del error
            }
        }
        
    }
}