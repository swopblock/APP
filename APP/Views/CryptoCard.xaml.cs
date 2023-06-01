namespace APP.Views;

public partial class CryptoCard : ContentView
{
	public CryptoCard()
	{
		InitializeComponent();

        LinearGradientBrush brush = new LinearGradientBrush(
            GetLineGradient(Color.FromArgb("#f2a900")),
            new Point(0.1, 0), 
            new Point(0.8, 1.1));

        LinearGradientBrush brushl1 = new LinearGradientBrush(
            GetLineGradientLayer1(Color.FromArgb("#f2a900")),
            new Point(0, 0.5),
            new Point(1, 0.5));

        LinearGradientBrush brushl2 = new LinearGradientBrush(
            GetLineGradientLayer2(Color.FromArgb("#f2a900")),
            new Point(0.5, 0),
            new Point(0.5, 1));

        LinearGradientBrush brushl3 = new LinearGradientBrush(
            GetLineGradientLayer3(Color.FromArgb("#f2a900")),
            new Point(0, 0.5),
            new Point(1, 0.5));

        rectTest.Fill = brush;
        rectTest1.Fill = brushl1;
        rectTest2.Fill = brushl2;
        rectTest3.Fill = brushl3;
	}

    public GradientStopCollection GetLineGradient(Color clr1)
    {
        
        GradientStop g1 = new GradientStop(GetColorAtOpacity(clr1,0.19f), 0f);
        GradientStop g2 = new GradientStop(GetColorAtOpacity(clr1, 0.1f), 0.1f);
        GradientStop g3 = new GradientStop(GetColorAtOpacity(clr1, 0.05f), 0.19f);
        GradientStop g4 = new GradientStop(GetColorAtOpacity(clr1, 0.01f), 0.31f);
        GradientStop g5 = new GradientStop(GetColorAtOpacity(clr1, 0.0f), 0.5f);
        GradientStop g6 = new GradientStop(GetColorAtOpacity(clr1, 0.01f), 0.66f);
        GradientStop g7 = new GradientStop(GetColorAtOpacity(clr1, 0.06f), 0.79f);
        GradientStop g8 = new GradientStop(GetColorAtOpacity(clr1, 0.13f), 0.92f);
        GradientStop g9 = new GradientStop(GetColorAtOpacity(clr1, 0.2f), 1f);

        GradientStopCollection collection = new GradientStopCollection
        {
           g1, g2, g3, g4, g5, g6, g7, g8, g9
        };

        return collection;
    }
    public GradientStopCollection GetLineGradientLayer1(Color clr1)
    {

        GradientStop g1 = new GradientStop(GetColorAtOpacity(clr1, 0.19f), 0f);
        GradientStop g2 = new GradientStop(GetColorAtOpacity(clr1, 0.1f), 0.1f);
        GradientStop g3 = new GradientStop(GetColorAtOpacity(clr1, 0.05f), 0.19f);
        GradientStop g4 = new GradientStop(GetColorAtOpacity(clr1, 0.01f), 0.31f);
        GradientStop g5 = new GradientStop(GetColorAtOpacity(clr1, 0.0f), 0.5f);
        GradientStop g6 = new GradientStop(GetColorAtOpacity(clr1, 0.01f), 0.63f);
        GradientStop g7 = new GradientStop(GetColorAtOpacity(clr1, 0.06f), 0.74f);
        GradientStop g8 = new GradientStop(GetColorAtOpacity(clr1, 0.13f), 0.85f);
        GradientStop g9 = new GradientStop(GetColorAtOpacity(clr1, 0.24f), 0.95f);
        GradientStop g10 = new GradientStop(GetColorAtOpacity(clr1, 0.3f), 1f);

        GradientStopCollection collection = new GradientStopCollection
        {
           g1, g2, g3, g4, g5, g6, g7, g8, g9, g10
        };

        return collection;
    }
    public GradientStopCollection GetLineGradientLayer2(Color clr1)
    {

        GradientStop g1 = new GradientStop(GetColorAtOpacity(clr1, 0.1f), 0f);
        GradientStop g2 = new GradientStop(GetColorAtOpacity(clr1, 0.05f), 0.01f);
        GradientStop g3 = new GradientStop(GetColorAtOpacity(clr1, 0.01f), 0.03f);
        GradientStop g4 = new GradientStop(GetColorAtOpacity(clr1, 0.0f), 0.06f);
        GradientStop g5 = new GradientStop(GetColorAtOpacity(clr1, 0.0f), 0.9f);
        GradientStop g6 = new GradientStop(GetColorAtOpacity(clr1, 0.0f), 0.96f);
        GradientStop g7 = new GradientStop(GetColorAtOpacity(clr1, 0.01f), 0.98f);
        GradientStop g8 = new GradientStop(GetColorAtOpacity(clr1, 0.06f), 0.99f);
        GradientStop g9 = new GradientStop(GetColorAtOpacity(clr1, 0.1f), 1f);

        GradientStopCollection collection = new GradientStopCollection
        {
           g1, g2, g3, g4, g5, g6, g7, g8, g9
        };

        return collection;
    }
    public GradientStopCollection GetLineGradientLayer3(Color clr1)
    {
        GradientStop g1 = new GradientStop(GetColorAtOpacity(clr1, 0.1f), 0f);
        GradientStop g2 = new GradientStop(GetColorAtOpacity(clr1, 0.05f), 0.01f);
        GradientStop g3 = new GradientStop(GetColorAtOpacity(clr1, 0.01f), 0.03f);
        GradientStop g4 = new GradientStop(GetColorAtOpacity(clr1, 0.0f), 0.06f);
        GradientStop g5 = new GradientStop(GetColorAtOpacity(clr1, 0.0f), 0.88f);
        GradientStop g6 = new GradientStop(GetColorAtOpacity(clr1, 0.0f), 0.93f);
        GradientStop g7 = new GradientStop(GetColorAtOpacity(clr1, 0.01f), 0.95f);
        GradientStop g8 = new GradientStop(GetColorAtOpacity(clr1, 0.06f), 0.97f);
        GradientStop g9 = new GradientStop(GetColorAtOpacity(clr1, 0.13f), 0.99f);
        GradientStop g10 = new GradientStop(GetColorAtOpacity(clr1, 0.2f), 1f);

        GradientStopCollection collection = new GradientStopCollection
        {
           g1, g2, g3, g4, g5, g6, g7, g8, g9, g10
        };

        return collection;
    }

    public Color GetColorAtOpacity(Color clr, float alpha)
    {
        return new Color(clr.Red, clr.Green, clr.Blue, alpha);
    }
}