namespace APP.Views;

public partial class TitleWithArrow : ContentView
{
	public TitleWithArrow()
	{
		InitializeComponent();

        this.Loaded += TitleWithArrow_Loaded;
    }

    private void TitleWithArrow_Loaded(object sender, EventArgs e)
    {
        try
        {
            string value = (string)BindingContext;

            buttonX.Text = value;
        }
        catch (Exception ex) { }
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        try
        {
            // apparently using a while loop and checking for ContentPage 
            //is how you select the containing contentpage
            Navigation.RemovePage((Page)this.Parent.Parent.Parent);
        }
        catch
        {

        }
    }
}