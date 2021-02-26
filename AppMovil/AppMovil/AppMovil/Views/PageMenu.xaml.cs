using AppMovil.Views;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMovil
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageMenu : MasterDetailPage
    {
		public PageMenu ()
		{
            InitializeComponent();
            Inicializar();
		}

        public void Inicializar()
        {
            Detail = new NavigationPage(new PagePrincipal());
            if (PageInicio.Grupo == 0)
            {
                List<Menu> menu = new List<Menu>
                {
                new Menu{ Page = new PagePerfil(), MenuTitulo="Mi prefil", MenuDetalle="Cambiar contraseña", Icon = "user.png"},
                new Menu{ Page = new PageRegistrar(), MenuTitulo="Añadir usuario", MenuDetalle="Añadir un nuevo usuario", Icon = "adduser.png"},
                new Menu{ Page = new PageSemestre(), MenuTitulo="Crear semestre", MenuDetalle="Crear un nuevo semestre", Icon = "addsem.png"},
                new Menu{ Page = new PageMateria(), MenuTitulo="Crear materia", MenuDetalle="Crear una nueva materia", Icon = "addsub.png"},
                new Menu{ Page = new PageMateriaSemestre(), MenuTitulo="Materia y semestre", MenuDetalle="Añadir una materia a un semestre", Icon = "subsem.png"},
                new Menu{ Page = new PagePlan(), MenuTitulo="Crear plan de materia", MenuDetalle="Crear un nuevo plan de una materia", Icon = "addplansub.png"},
                new Menu{ Page = new PageMateriaEstudiante(), MenuTitulo="Registrar materia a estudiante", MenuDetalle="Añade una nueva materia a un estudiante", Icon = "addsubuser.png"},
                new Menu{ Page = new PageSubirNota(), MenuTitulo="Subir nota", MenuDetalle="Subir una nota de una materia a un estudiante", Icon = "addnote.png"},
                new Menu{ Page = new PageInicio(), MenuTitulo="Salir", MenuDetalle="Cerrar sesion", Icon = "salir.png"}
                };
                LtMenu.ItemsSource = menu;
            }
            else
            {
                List<Menu> menu = new List<Menu>
                {
                new Menu{ Page = new PagePerfil(), MenuTitulo="Mi prefil", MenuDetalle="Cambiar contraseña", Icon = "user.png"},
                new Menu{ Page = new PageNotasGenerales(), MenuTitulo="Notas generales", MenuDetalle="Visualizar las notas generales", Icon = "notes.png"},
                new Menu{ Page = new PageInicio(), MenuTitulo="Salir", MenuDetalle="Cerrar sesion", Icon = "salir.png"}
                };
                LtMenu.ItemsSource = menu;
            }
            
        }

        public class Menu
        {
            public string MenuTitulo { get; set; }
            public string MenuDetalle { get; set; }
            public ImageSource Icon { get; set; }
            public Page Page { get; set; }
        }

        private void LtMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Inicializar();
            var menu = e.SelectedItem as Menu;
            if(menu != null)
            {
                IsPresented = false;
                if (menu.MenuTitulo.Equals("Salir")) Navigation.PopToRootAsync();
                else Detail = new NavigationPage(menu.Page);
            }
            else Detail = new NavigationPage(new PagePrincipal());
        }
    }
}