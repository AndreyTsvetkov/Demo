using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Asc2Pnt.Model;

namespace Asc2Pnt
{
	public partial class DemoForm : Form
	{
		public DemoForm()
		{
			InitializeComponent();
		}

		public void ShowPoint(DiscretePoint point)
		{
			_points.Add(point);

			var rectangle = GetRectangleForPoint(point);

			// это нужно для появления прокрутки на форме, чтобы вся картинка была доступна
			pnlDemo.ExtendBoundsToInclude(rectangle);

			pnlDemo.Invalidate(rectangle);
		}

		public void ShowTurn(DiscretePoint point)
		{
			_turns.Add(point);
			pnlDemo.Invalidate(GetRectangleForPoint(point));
		}

		private void pnlDemo_Paint(object sender, PaintEventArgs e)
		{
			// можно было бы не все точки перерисовывать, а лишь пересекающиеся с e.ClipRectangle, но пока что это была бы избыточная оптимизация;
			// холст и так все рисование за границей этого прямоугольника игнорирует; 
			// а вот то, что я инвалидирую только нужные кусочки — вот это сильно влияет на качество анимации в лучшую сторону.
			_points.ForEach(point => e.Graphics.FillRectangle(Brushes.Black, GetRectangleForPoint(point)));
			_turns.ForEach(point => e.Graphics.FillRectangle(Brushes.Red, GetRectangleForPoint(point)));
		}

		private static Rectangle GetRectangleForPoint(DiscretePoint point)
		{
			const int scale = 10;
			return new Rectangle(point.X * scale, point.Y * scale, scale, scale);
		}

		private readonly List<DiscretePoint> _points = new List<DiscretePoint>();
		private readonly List<DiscretePoint> _turns = new List<DiscretePoint>();
	}

	internal static class ControlEx
	{
		public static void ExtendBoundsToInclude(this Control ctrl, Rectangle rectangle)
		{
			const int padding = 10;
			if (ctrl.Width < rectangle.X + rectangle.Width)
				ctrl.Invoke((Action)(() => ctrl.Width = rectangle.X + rectangle.Width + padding));
			if (ctrl.Height < rectangle.Y + rectangle.Height)
				ctrl.Invoke((Action)(() => ctrl.Height = rectangle.Y + rectangle.Height + padding));
		}
	}
}