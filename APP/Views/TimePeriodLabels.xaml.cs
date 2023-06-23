namespace APP.Views;

public partial class TimePeriodLabels : ContentView
{
    public Color BasicColor = Color.FromArgb("#71868F");
    public TimePeriodLabels()
	{
		InitializeComponent();

        this.Loaded += TimePeriodLabels_Loaded;
	}

    private void TimePeriodLabels_Loaded(object sender, EventArgs e)
    {
        if (BindingContext != null)
        {
            BasicColor = (Color)BindingContext;

            ResetColors();

            bToday.TextColor = Colors.White;
        }
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        ResetColors();
        bLive.TextColor = Colors.White;
    }
    private void Button_Today(object sender, EventArgs e)
    {
        ResetColors();
        bToday.TextColor = Colors.White;
    }

    private void Button_W(object sender, EventArgs e)
    {
        ResetColors();
        bW.TextColor = Colors.White;
    }
    private void Button_M(object sender, EventArgs e)
    {
        ResetColors();
        bM.TextColor = Colors.White;
    }
    private void Button_M3(object sender, EventArgs e)
    {
        ResetColors();
        bM3.TextColor = Colors.White;
    }
    private void Button_Y(object sender, EventArgs e)
    {
        ResetColors();
        bY.TextColor = Colors.White;
    }

    public void ResetColors()
    {
        bLive.TextColor = BasicColor;
        bToday.TextColor = BasicColor;
        bW.TextColor = BasicColor;
        bM.TextColor = BasicColor;
        bM3.TextColor = BasicColor;
        bY.TextColor = BasicColor;
    }
}