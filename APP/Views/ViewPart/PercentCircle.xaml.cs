using APP.Code.Geometry;
using Microsoft.Maui.Controls.Shapes;
using APP.Code;

namespace APP.Views;

public partial class PercentCircle : ContentView
{
	public PercentCircle()
	{
		InitializeComponent();
    }

    public void UpdateValue(WalletAsset asset, float size, float start = 0)
    {
        float per = (float)(asset.Percentage * 360);

        percentValue.Text = (int)(asset.Percentage * 100) + "%";

        BasicCircle circle = new BasicCircle(new Point(size, size), size);

        string cir = circle.GetCircle(start, per);

        Geometry pathCirc =
            (Geometry)new PathGeometryConverter()
            .ConvertFromString(cir);

        CirclePart.Data = pathCirc;
        CirclePart.Stroke = asset.HtmlColor;

        string full = circle.GetCircle(start + per, 360 - per);

        Geometry pathFull =
            (Geometry)new PathGeometryConverter()
            .ConvertFromString(full);

        CircleFull.Data = pathFull;
    }
}