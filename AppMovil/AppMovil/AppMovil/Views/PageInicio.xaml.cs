using AppMovil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInicio : ContentPage
    {
        public static string Usuario { get; set; }
        public static int Grupo { get; set; }
        public PageInicio()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Inicializar();
        }

        private void Inicializar()
        {
            BtnEntrar.Clicked += BtnEntrar_Clicked; //Se crea el evento cuando se haga click en el botón
            
        }

        private void BtnEntrar_Clicked(object sender, EventArgs e) //Sería el método del botón
        {
            try
            {
                InicializarDB();
                //Validar usuario y contraseña sean diferentes de nullo
                if (String.IsNullOrEmpty(TxUsuario.Text))
                {
                    DisplayAlert("Error", "Debe Ingresar el usuario", "Aceptar");
                    TxUsuario.Focus();
                    return;
                }
                else if (String.IsNullOrEmpty(TxContraseña.Text))
                {
                    DisplayAlert("Error", "Debe Ingresar la contraseña", "Aceptar");
                    TxContraseña.Focus();
                    return;
                }
                else
                {
                    /*Conectar con la base datos para hacer la consulta del usuario y contraseña
                    con una sentencia SQL y si es exitosa se ingresa a la otra pagina con:*/
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.CreateTable<Usuarios>();

                        string sql = "SELECT * FROM Usuarios WHERE Usuario = '" + TxUsuario.Text + "' AND Contraseña = '" + TxContraseña.Text + "'";
                        SQLiteCommand cmd = new SQLiteCommand(conn){ CommandText = sql };

                        if (cmd.ExecuteQuery<Usuarios>().Count == 1)
                        {
                            DisplayAlert("Iniciar sesion", "Inicio de sesion exitoso", "Aceptar");
                            Usuario = TxUsuario.Text;
                            Grupo = cmd.ExecuteQuery<Usuarios>()[0].Grupo;
                            TxContraseña.Text = "";
                            TxUsuario.Text = "";
                            Navigation.PushAsync(new PageMenu());
                        }
                        else DisplayAlert("Iniciar sesion", "Usuario o contraseña incorrecta", "Aceptar");
                    }
                }
            }
            catch (Exception er)
            {
                DisplayAlert("Error ", "" + er.Message, "Aceptar");//Mostrar mensaje del error
            }
        }

        private void InicializarDB()
        {
            try
            {
                GrupoUsuarios Admin = new GrupoUsuarios { Grupo = 0 }, 
                    Estu = new GrupoUsuarios { Grupo = 1 };
                Usuarios usuario = new Usuarios { Usuario = "Admin", Contraseña = "1234", Grupo = 0 };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                {
                    conn.CreateTable<GrupoUsuarios>();
                    string sql = "SELECT * FROM GrupoUsuarios";
                    SQLiteCommand cmd = new SQLiteCommand(conn) { CommandText = sql };
                    if (cmd.ExecuteQuery<GrupoUsuarios>().Count == 0)
                    {
                        conn.Insert(Admin);
                        conn.Insert(Estu);
                    }
                    conn.CreateTable<Usuarios>();
                    sql = "SELECT * FROM Usuarios";
                    cmd = new SQLiteCommand(conn) { CommandText = sql };
                    if (cmd.ExecuteQuery<Usuarios>().Count == 0)
                    {
                        conn.Insert(usuario);
                    }
                }
            }
            catch(Exception e)
            {
                DisplayAlert("Inicializar DataBase", e.Message, "Aceptar");
            }
        }
    }
}

