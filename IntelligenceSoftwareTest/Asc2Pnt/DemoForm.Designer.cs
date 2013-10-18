namespace Asc2Pnt
{
	partial class DemoForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlDemo = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnlDemo
			// 
			this.pnlDemo.Location = new System.Drawing.Point(12, 12);
			this.pnlDemo.Name = "pnlDemo";
			this.pnlDemo.Size = new System.Drawing.Size(500, 500);
			this.pnlDemo.TabIndex = 0;
			this.pnlDemo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDemo_Paint);
			// 
			// DemoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(525, 524);
			this.Controls.Add(this.pnlDemo);
			this.Name = "DemoForm";
			this.Text = "Демонстрация процесса (искусственно замедленная)";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlDemo;
	}
}