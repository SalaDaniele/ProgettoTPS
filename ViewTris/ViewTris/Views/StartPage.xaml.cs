namespace ViewTris.Views;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}
    private async void Button_Clicked(object sender, EventArgs e)
    {
        string nome;
        do
        {
            nome = await DisplayPromptAsync("Attenzione", "Inserisci il tuo nome:");

        } while (nome == null || nome == "");
        await Navigation.PushAsync(new MultiPlayerPage(nome));
    }


    private async void BotClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BotPage());
    }
}