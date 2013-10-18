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

		public void ShowPoint(object sender, DiscretePointEventArgs e)
		{
			_points.Add(e.Point);
			const int padding = 10;

			var rectangle = GetRectangleForPoint(e.Point);

			if (pnlDemo.Width < rectangle.X + rectangle.Width)
				pnlDemo.Invoke((Action)(() => pnlDemo.Width = rectangle.X + rectangle.Width + padding));
			if (pnlDemo.Height < rectangle.Y + rectangle.Height)
				pnlDemo.Invoke((Action)(() => pnlDemo.Height = rectangle.Y + rectangle.Height + padding));

			pnlDemo.Invalidate(rectangle);
		}

		public void ShowTurn(object sender, DiscretePointEventArgs e)
		{
			_turns.Add(e.Point);
			pnlDemo.Invalidate(GetRectangleForPoint(e.Point));
		}

		private readonly List<DiscretePoint> _points = new List<DiscretePoint>();
		private readonly List<DiscretePoint> _turns = new List<DiscretePoint>();

		private void pnlDemo_Paint(object sender, PaintEventArgs e)
		{
			_points.ForEach(point => e.Graphics.FillRectangle(Brushes.Black, GetRectangleForPoint(point)));
			_turns.ForEach(point => e.Graphics.FillRectangle(Brushes.Red, GetRectangleForPoint(point)));
		}

		private static Rectangle GetRectangleForPoint(DiscretePoint point)
		{
			const int scale = 10;
			return new Rectangle(point.X * scale, point.Y * scale, scale, scale);
		}
	}
}