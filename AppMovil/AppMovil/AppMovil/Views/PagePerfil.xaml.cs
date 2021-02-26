using AppMovil.Models;
using SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovil
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagePerfil : ContentPage
	{
		public PagePerfil ()
		{
			InitializeComponent ();
            Inicializar();
        }

        private void Inicializar()
        {
            TxUsuario.Text = PageInicio.Usuario;
            BtnAceptar.Clicked += BtnAceptar_Clicked;
        }

        private void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (TxOldContraseña.Text.Equals(""))
                {
                    DisplayAlert("Cambiar Contraseña", "Debe Ingresar la antigua contraseña", "Aceptar");
                    TxOldContraseña.Focus();
                    return;
                }
                else if (TxNewContraseña.Text.Equals(""))
                {
                    DisplayAlert("Cambiar Contraseña", "Debe Ingresar la nueva contraseña", "Aceptar");
                    TxNewContraseña.Focus();
                    return;
                }
                else if (TxOldContraseña.Text.Equals(TxNewContraseña.Text))
                {
                    DisplayAlert("Cambiar Contraseña", "Debe Ingresar una contraseña diferente a la antigua", "Aceptar");
                    TxOldContraseña.Focus();
                    return;
                }
                else
                {
                    Usuarios usuario = new Usuarios
                    {
                        Usuario = TxUsuario.Text,
                        Contraseña = TxNewContraseña.Text,
                        Grupo = PageInicio.Grupo
                    };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<Usuarios>();
                        string sql = "SELECT * FROM Usuarios WHERE Usuario = '" + TxUsuario.Text + "' AND Contraseña = '" + TxOldContraseña.Text + "'";

                        SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                        int r = 0;
                        if (cmd.ExecuteQuery<Usuarios>().Count == 1) r = conn.Update(usuario);
                        if (r > 0)
                        {
                            TxNewContraseña.Text = "";
                            TxOldContraseña.Text = "";
                            DisplayAlert("Cambiar Contraseña", "Contraseña actualizada", "Aceptar");
                        }
                        else DisplayAlert("Cambiar Contraseña", "La contraseña no se pudo actualizar", "Aceptar");
                    }
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error ", "" + er, "Aceptar");//Mostrar mensaje del error
            }
        }
    }
}