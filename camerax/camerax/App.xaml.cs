using Microsoft.Maui.Controls;

namespace camerax
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
