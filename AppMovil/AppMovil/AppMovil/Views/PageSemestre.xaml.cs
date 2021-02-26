using AppMovil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovil.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageSemestre : ContentPage
	{
		public PageSemestre ()
		{
			InitializeComponent ();
            Inicializar();
        }

        private void Inicializar()
        {
            BtnCrear.Clicked += BtnCrear_Clicked; //Se crea el evento cuando se haga click en el botón
            BtnEliminar.Clicked += BtnEliminar_Clicked;
        }

        private void BtnCrear_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Validar materia diferentes de nulo
                if (String.IsNullOrEmpty(TxSemestre.Text))
                {
                    DisplayAlert("Error", "Debe ingresar un semestre", "Aceptar");
                    TxSemestre.Focus();
                    return;
                }
                else
                {
                    Semestres semestre = new Semestres { Semestre = TxSemestre.Text };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<Semestres>();
                        int r = conn.Insert(semestre);
                        if (r > 0) DisplayAlert("Crear", "Semestre creado", "Aceptar");
                        else DisplayAlert("Crear", "Semestre no creado", "Aceptar");

                    }
                    TxSemestre.Text = "";
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
                if (String.IsNullOrEmpty(TxSemestre.Text))
                {
                    DisplayAlert("Error", "Debe ingresar un semestre", "Aceptar");
                    TxSemestre.Focus();
                    return;
                }
                else
                {
                    Semestres semestre = new Semestres { Semestre = TxSemestre.Text };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<Semestres>();
                        int r = conn.Delete(semestre);
                        if (r > 0) DisplayAlert("Eliminar", "Semestre eliminado", "Aceptar");
                        else DisplayAlert("Eliminar", "semestre no eliminado", "Aceptar");

                    }
                    TxSemestre.Text = "";
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");//Mostrar mensaje del error
            }
        }
    }
}