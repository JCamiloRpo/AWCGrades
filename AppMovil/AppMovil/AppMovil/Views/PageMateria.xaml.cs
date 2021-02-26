using AppMovil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovil.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageMateria : ContentPage
	{
		public PageMateria ()
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
                if (String.IsNullOrEmpty(TxMateria.Text))
                {
                    DisplayAlert("Error", "Debe ingresar una materia", "Aceptar");
                    TxMateria.Focus();
                    return;
                }
                else
                {
                    Materias materia = new Materias { Materia = TxMateria.Text };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<Materias>();
                        int r = conn.Insert(materia);
                        if (r > 0) DisplayAlert("Crear", "Materia creada", "Aceptar");
                        else DisplayAlert("Crear", "Materia no creada", "Aceptar");

                    }
                    TxMateria.Text = "";
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
                if (String.IsNullOrEmpty(TxMateria.Text))
                {
                    DisplayAlert("Error", "Debe ingresar una materia", "Aceptar");
                    TxMateria.Focus();
                    return;
                }
                else
                {
                    Materias materia = new Materias { Materia = TxMateria.Text };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<Materias>();
                        int r = conn.Delete(materia);
                        if (r > 0) DisplayAlert("Eliminar", "Materia eliminada", "Aceptar");
                        else DisplayAlert("Eliminar", "Materia no eliminada", "Aceptar");

                    }
                    TxMateria.Text = "";
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");//Mostrar mensaje del error
            }
        }
    }
}