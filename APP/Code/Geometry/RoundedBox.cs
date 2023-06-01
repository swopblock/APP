using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code.Geometry
{
    public class RoundedBox
    {
        private float Width;
        private float Height;
        private float Round;
        private float inset = 0;

        public RoundedBox(float width, float height, float roundWidth, float insetSize)
        {
            Width = width;
            Height = height;
            Round = roundWidth;
            inset = insetSize;
        }

        public string GetShape(float ScreenWidth, float ScreenHeight)
        {
            string shape = string.Empty;

            float partWidth = (ScreenWidth / 2) - (inset - (Round * 2));
            float partHeight = (ScreenHeight / 2) - (inset - (Round * 2));

            PointF cent = new PointF(ScreenWidth/2, ScreenHeight/2);
            PointF pt1 = new PointF(ScreenWidth/2, inset);

            shape += BuildSVG.BoxValue(pt1, cent, ScreenWidth, ScreenHeight);
            shape += BuildSVG.LineTo(pt1.X + partWidth , pt1.Y);

            PointF first = new PointF(pt1.X + partWidth + Round, pt1.Y + Round);

            shape += BuildSVG.CurveTo(
                first.X, first.Y,
                pt1.X + partWidth + (Round / 2), pt1.Y + Round,
                pt1.X + partWidth + Round, pt1.Y + (Round / 2));

            shape += BuildSVG.LineTo(first.X, first.Y + partHeight);

            PointF second = new PointF(pt1.X + partWidth + Round, pt1.Y + partHeight + Round);

            shape += BuildSVG.CurveTo(
                second.X, second.Y,
                second.X + Round, second.Y - (Round/2),
                second.X + (Round/2), second.Y);

            shape += BuildSVG.LineTo(second.X + partWidth, second.Y);

            shape += " Z ";

            //shape +=

            return shape;
        }
    }
}
