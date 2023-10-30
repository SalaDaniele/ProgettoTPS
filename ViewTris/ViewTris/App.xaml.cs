using ViewTris.Views;

namespace ViewTris;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new StartPage());
    }
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Width = 600;
        window.Height = 900;
        window.MaximumHeight = 600;
        window.MaximumWidth = 900;
        window.MinimumHeight = 900;
        window.MinimumWidth = 600;
        return window;
    }
}
