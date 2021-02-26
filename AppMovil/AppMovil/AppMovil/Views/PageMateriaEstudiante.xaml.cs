using AppMovil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovil.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageMateriaEstudiante : ContentPage
	{
		public PageMateriaEstudiante ()
		{
            InitializeComponent();
            Inicializar();
            InicialiarSQL();
        }

        private void Inicializar()
        {
            BtnAgregar.Clicked += BtnAgregar_Clicked; //Se crea el evento cuando se haga click en el botón
            BtnEliminar.Clicked += BtnEliminar_Clicked;
            PkIdMateria.Focused += Pk_IdMateriaFocused;
            PkEstudiante.Focused += Pk_EstudianteFocused;
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
                else
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<MateriaXSemestre>();
                        string sql = "SELECT * FROM MateriaXSemestre WHERE Semestre ='" + PkSemestre.SelectedItem.ToString() + "'";
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

        private void Pk_EstudianteFocused(object sender, FocusEventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                {
                    conn.CreateTable<Usuarios>();
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
                    conn.CreateTable<MateriaXSemestre>();
                    string sql = "SELECT * FROM MateriaXSemestre";
                    SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                    List<MateriaXSemestre> conidmaterias = cmd.ExecuteQuery<MateriaXSemestre>();
                    List<string> idmaterias = new List<string>();
                    foreach (MateriaXSemestre i in conidmaterias)
                    {
                        idmaterias.Add(i.Materia + "-" + i.Semestre);
                    }
                    PkIdMateria.ItemsSource = idmaterias;

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

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Validar materia diferentes de nulo
                if (PkIdMateria.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar una materia con semestre", "Aceptar");
                    PkIdMateria.Focus();
                    return;
                }
                else if (PkEstudiante.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar un estudiante", "Aceptar");
                    PkEstudiante.Focus();
                    return;
                }
                else
                {
                    MateriaXEstudiante materiaxestudiante = new MateriaXEstudiante (PkIdMateria.SelectedItem.ToString(), PkEstudiante.SelectedItem.ToString() );

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<MateriaXEstudiante>();
                        int r = conn.Insert(materiaxestudiante);
                        if (r > 0) DisplayAlert("Agregar", "Materia con semestre agregada a estudiante", "Aceptar");
                        else DisplayAlert("Agregar", "Materia con semestre no agregada a estudiante", "Aceptar");
                    }
                    PkIdMateria.SelectedItem = null;
                    PkEstudiante.SelectedItem = null;
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
                if (PkIdMateria.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar una materia con semestre", "Aceptar");
                    PkIdMateria.Focus();
                    return;
                }
                else if (PkEstudiante.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar un estudiante", "Aceptar");
                    PkEstudiante.Focus();
                    return;
                }
                else
                {
                    MateriaXEstudiante materiaxestudiante = new MateriaXEstudiante(PkIdMateria.SelectedItem.ToString(), PkEstudiante.SelectedItem.ToString());

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        int r = 0;
                        conn.CreateTable<MateriaXEstudiante>();
                        string sql = "SELECT * FROM MateriaXEstudiante WHERE IdMateria = '" + PkIdMateria.SelectedItem.ToString() + "' AND Usuario = '" + PkEstudiante.SelectedItem.ToString() + "'";
                        SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                        List<MateriaXEstudiante> conmateria = cmd.ExecuteQuery<MateriaXEstudiante>();
                        MateriaXEstudiante materia = conmateria[0];
                        if (conmateria.Count > 0) r = conn.Delete(materia);
                        if (r > 0) DisplayAlert("Eliminar", "Materia con semestre eliminada a estudiante", "Aceptar");
                        else DisplayAlert("Eliminar", "Materia con semestre no eliminada a estudiante", "Aceptar");
                    }
                    PkIdMateria.SelectedItem = null;
                    PkEstudiante.SelectedItem = null;
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");//Mostrar mensaje del error
            }
        }

    }
}