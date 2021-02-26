using Xamarin.Forms;
using Xamarin.Forms.Xaml;
    
namespace AppMovil
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagePrincipal : ContentPage
	{
		public PagePrincipal ()
		{
			InitializeComponent ();
            Inicializar();
        }

        public void Inicializar()
        {
            string user = PageInicio.Usuario, grupo;
            if (PageInicio.Grupo == 0)
            {
                grupo = "Administrador";
                Image.Source = "admin.png";
            }
            else
            {
                grupo = "Estudiante";
                Image.Source = "user.png";
            }

            LbTitulo.Text = "Bienvenido " + grupo + " " + user;
        }
	}
}