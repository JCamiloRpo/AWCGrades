using AppMovil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovil
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageRegistrar : ContentPage
	{
		public PageRegistrar ()
		{
            InitializeComponent ();
            Inicializar();
        }

        private void Inicializar()
        {
            List<string> tipos = new List<string>()
            {
                "Administrador", "Estudiante"
            };
            PkTipoUser.ItemsSource = tipos;
            BtnAceptar.Clicked += BtnEntrar_Clicked; //Se crea el evento cuando se haga click en el botón
        }

        private void BtnEntrar_Clicked(object sender, EventArgs e) //Sería el método del botón
        {
            try
            {
                //Validar usuario y contraseña sean diferentes de nullo
                if (String.IsNullOrEmpty(TxUsuario.Text))
                {
                    DisplayAlert("Error", "Debe ingresar el usuario", "Aceptar");
                    TxUsuario.Focus();
                    return;
                }
                else if (String.IsNullOrEmpty(TxContraseña.Text))
                {
                    DisplayAlert("Error", "Debe ingresar la contraseña", "Aceptar");
                    TxContraseña.Focus();
                    return;
                }
                else if (PkTipoUser.SelectedItem == null)
                {
                    DisplayAlert("Error", "Debe selecionar un grupo de usuario", "Aceptar");
                    PkTipoUser.Focus();
                    return;
                }
                else
                {
                    Usuarios usuario = new Usuarios { Usuario = TxUsuario.Text, Contraseña = TxContraseña.Text, Grupo = PkTipoUser.SelectedIndex };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<Usuarios>();
                        int r = conn.Insert(usuario);
                        if(r > 0) DisplayAlert("Registrar", "Usuario creado", "Aceptar");
                        else DisplayAlert("Registrar", "Usuario no creado", "Aceptar");
                    }
                    PkTipoUser.SelectedItem = null;
                    TxContraseña.Text = "";
                    TxUsuario.Text = "";
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error", er.Message, "Aceptar");//Mostrar mensaje del error
            }
        }
    }
}
