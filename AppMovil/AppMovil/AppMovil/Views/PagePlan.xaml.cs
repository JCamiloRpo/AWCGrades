using AppMovil.Models;
using SQLite;
using System;
using System.Runtime;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovil.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagePlan : ContentPage
	{
		public PagePlan ()
		{
			InitializeComponent ();
            Inicializar();
            InicialiarSQL();
        }

        private void Inicializar()
        {
            BtnAgregar.Clicked += BtnAgregar_Clicked;
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

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                float porcentaje = 0f;
                //Validar materia diferentes de nulo
                if (PkIdMateria.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe seleccionar una materia con semestre", "Aceptar");
                    PkIdMateria.Focus();
                    return;
                }
                else if (String.IsNullOrEmpty(TxDescripcion.Text))
                {
                    DisplayAlert("Error", "Debe ingresar una descripcion", "Aceptar");
                    TxDescripcion.Focus();
                    return;
                }
                else if (String.IsNullOrEmpty(TxPorcenjate.Text))
                {
                    DisplayAlert("Error", "Debe ingresar un porcentaje", "Aceptar");
                    TxPorcenjate.Focus();
                    return;
                }
                else
                {
                    if (float.TryParse(TxPorcenjate.Text, out porcentaje))
                    {
                        if(porcentaje >= 0 && porcentaje <= 100)
                        {
                            PlanXMateria planxmateria = new PlanXMateria(TxDescripcion.Text, PkIdMateria.SelectedItem.ToString(), porcentaje);
                            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                            {
                                conn.CreateTable<PlanXMateria>();
                                int r = conn.Insert(planxmateria);
                                if (r > 0) DisplayAlert("Agregar", "Plan agregado a la materia con semestre", "Aceptar");
                                else DisplayAlert("Agregar", "Plan no agregado a la materia con semestre", "Aceptar");
                            }
                            PkIdMateria.SelectedItem = null;
                            TxDescripcion.Text = "";
                            TxPorcenjate.Text = "";
                        }
                        else DisplayAlert("Agregar", "Debe ser un porcentaje entre 0 y 100", "Aceptar");
                    }
                    else DisplayAlert("Agregar", "Debe escribir solo numeros para el porcentaje (si es necesario con ',')", "Aceptar");
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");//Mostrar mensaje del error
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
                }
            }
            catch (Exception e)
            {
                DisplayAlert("Error", e.Message, "Aceptar");
            }
        }

    }
}