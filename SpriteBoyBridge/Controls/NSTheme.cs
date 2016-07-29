using OpenTK;
using OpenTK.Graphics;
using OpenTK.Platform;
using SpriteBoy.Data;
using SpriteBoy.Data.Editing;
using SpriteBoy.Data.Editing.Graphics;
using SpriteBoy.Forms;
using SpriteBoy.Forms.Dialogs;
using SpriteBoy.Forms.Editors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

//IMPORTANT:
//Please leave these comments in place as they help protect intellectual rights and allow
//developers to determine the version of the theme they are using. The preffered method
//of distributing this theme is through the Nimoru Software home page at nimoru.com.

//Name: Net Seal Theme
//Created: 6/21/2013
//Version: 1.0.0.2 beta
//Site: http://nimoru.com/

//This work is licensed under a Creative Commons Attribution 3.0 Unported License.
//To view a copy of this license, please visit http://creativecommons.org/licenses/by/3.0/

//Copyright © 2013 Nimoru Software

namespace SpriteBoy.Controls {


	public static class ThemeModule {

		static ThemeModule() {
			TextBitmap = new Bitmap(1, 1);
			TextGraphics = Graphics.FromImage(TextBitmap);
		}

		private static Bitmap TextBitmap;

		private static Graphics TextGraphics;
		static internal SizeF MeasureString(string text, Font font) {
			return TextGraphics.MeasureString(text, font);
		}

		static internal SizeF MeasureString(string text, Font font, int width) {
			return TextGraphics.MeasureString(text, font, width, StringFormat.GenericTypographic);
		}

		private static GraphicsPath CreateRoundPath;

		private static Rectangle CreateRoundRectangle;
		static internal GraphicsPath CreateRound(int x, int y, int width, int height, int slope) {
			CreateRoundRectangle = new Rectangle(x, y, width, height);
			return CreateRound(CreateRoundRectangle, slope);
		}

		static internal GraphicsPath CreateRound(Rectangle r, int slope) {
			CreateRoundPath = new GraphicsPath(FillMode.Winding);
			CreateRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
			CreateRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
			CreateRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
			CreateRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
			CreateRoundPath.CloseFigure();
			return CreateRoundPath;
		}

		static internal GraphicsPath CreateRoundIncomplete(Rectangle r, int slope, Corners corners) {
			CreateRoundPath = new GraphicsPath(FillMode.Winding);
			if (corners.TopLeft) {
				CreateRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
			} else {
				CreateRoundPath.AddLine(r.X, r.Y, r.X, r.Y);
			}
			if (corners.TopRight) {
				CreateRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
			} else {
				CreateRoundPath.AddLine(r.Right, r.Y, r.Right, r.Y);
			}
			if (corners.BottomRight) {
				CreateRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
			} else {
				CreateRoundPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);
			}
			if (corners.BottomLeft) {
				CreateRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
			} else {
				CreateRoundPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
			}
			CreateRoundPath.CloseFigure();
			return CreateRoundPath;
		}

		[System.Serializable]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class Corners {
			public bool TopLeft { get; set; }
			public bool TopRight { get; set; }
			public bool BottomLeft { get; set; }
			public bool BottomRight { get; set; }

			public Corners() {
				TopLeft = true;
				TopRight = true;
				BottomLeft = true;
				BottomRight = true;
			}
		}
	}

	public class NSTheme : ThemeContainer154 {
		public static Color UI_ACCENT = Color.FromArgb(205, 150, 0);

		private int _AccentOffset = 0;
		public int AccentOffset {
			get { return _AccentOffset; }
			set {
				_AccentOffset = value;
				Invalidate();
			}
		}

		public NSTheme() {
			Header = 30;
			BackColor = Color.FromArgb(50, 50, 50);

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(60, 60, 60));

			B1 = new SolidBrush(Color.FromArgb(50, 50, 50));
		}


		protected override void ColorHook() {
		}


		private Rectangle R1;
		private Pen P1;
		private Pen P2;

		private SolidBrush B1;

		private int Pad;
		protected override void PaintHook() {
			G.Clear(BackColor);
			DrawBorders(P2, 1);

			G.DrawLine(P1, 0, 26, Width, 26);
			G.DrawLine(P2, 0, 25, Width, 25);

			Pad = Math.Max(Measure().Width + 20, 80);
			R1 = new Rectangle(Pad, 17, Width - (Pad * 2) + _AccentOffset, 8);

			G.DrawRectangle(P2, R1);
			G.DrawRectangle(P1, R1.X + 1, R1.Y + 1, R1.Width - 2, R1.Height);

			G.DrawLine(P1, 0, 29, Width, 29);
			G.DrawLine(P2, 0, 30, Width, 30);

			DrawText(Brushes.Black, HorizontalAlignment.Left, 8, 1);
			DrawText(Brushes.White, HorizontalAlignment.Left, 7, 0);

			G.FillRectangle(B1, 0, 27, Width, 2);
			DrawBorders(Pens.Black);
		}

	}

	public class NSButton : Control {

		public NSButton() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(65, 65, 65));
		}


		private bool IsMouseDown;
		private GraphicsPath GP1;

		private GraphicsPath GP2;
		private SizeF SZ1;

		private PointF PT1;
		private Pen P1;

		private Pen P2;
		private PathGradientBrush PB1;

		private LinearGradientBrush GB1;

		private Graphics G;
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
			GP2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

			if (IsMouseDown) {
				PB1 = new PathGradientBrush(GP1);
				PB1.CenterColor = Color.FromArgb(60, 60, 60);
				PB1.SurroundColors = new Color[] { Color.FromArgb(55, 55, 55) };
				PB1.FocusScales = new PointF(0.8f, 0.5f);

				G.FillPath(PB1, GP1);
			} else {
				GB1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
				G.FillPath(GB1, GP1);
			}

			G.DrawPath(P1, GP1);
			G.DrawPath(P2, GP2);

			SZ1 = G.MeasureString(Text, Font);
			PT1 = new PointF(Width / 2 - SZ1.Width / 2, Height / 2 - SZ1.Height / 2);

			if (IsMouseDown) {
				PT1.X += 1f;
				PT1.Y += 1f;
			}

			G.DrawString(Text, Font, Brushes.Black, PT1.X + 1, PT1.Y + 1);
			G.DrawString(Text, Font, Enabled ? Brushes.White : Brushes.Gray, PT1);
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			IsMouseDown = true;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			IsMouseDown = false;
			Invalidate();
		}

	}

	public class NSIconicButton : Control {

		public NSIconicButton() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(65, 65, 65));
		}

		private ThemeModule.Corners corners = new ThemeModule.Corners();

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ThemeModule.Corners Corners {
			get { return corners; }
			set { corners = value; Invalidate(); }
		}

		private Image _Icon, _IconShadow, _IconDisabled;
		public Image IconImage {
			get { return _Icon; }
			set {
				_Icon = value;
				if (_Icon != null) {
					_IconSize = _Icon.Size;
				}
				GenerateExtIcons();
				Invalidate();
			}
		}

		private Size _IconSize;
		public Size IconSize {
			get { return _IconSize; }
			set { _IconSize = value; Invalidate(); }
		}

		private bool _Large;
		public bool Large {
			get { return _Large; }
			set { _Large = value; Invalidate(); }
		}



		private bool IsMouseDown;
		private GraphicsPath GP1;

		private GraphicsPath GP2;
		private SizeF SZ1;

		private PointF PT1;
		private Pen P1;

		private Pen P2;
		private PathGradientBrush PB1;

		private LinearGradientBrush GB1;

		private Graphics G;
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRoundIncomplete(new Rectangle(0, 0, Width - 1, Height - 1), _Large ? 15 : 7, corners);
			GP2 = ThemeModule.CreateRoundIncomplete(new Rectangle(1, 1, Width - 3, Height - 3), _Large ? 15 : 7, corners);

			if (IsMouseDown) {
				PB1 = new PathGradientBrush(GP1);
				PB1.CenterColor = Color.FromArgb(60, 60, 60);
				PB1.SurroundColors = new Color[] { Color.FromArgb(55, 55, 55) };
				PB1.FocusScales = new PointF(0.8f, 0.5f);

				G.FillPath(PB1, GP1);
			} else {
				GB1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
				G.FillPath(GB1, GP1);
			}

			G.DrawPath(P1, GP1);
			G.DrawPath(P2, GP2);

			int pad = 5;
			PointF ICP = Point.Empty;
			if (_Icon != null) {
				pad = 13 + _IconSize.Width;
				ICP = new PointF(
					7, Height / 2 - _IconSize.Height / 2
				);
				if (Text == "") {
					ICP = new PointF(
						Width / 2 - _IconSize.Width / 2,
						Height / 2 - _IconSize.Height / 2
					);
				}
			}

			SZ1 = G.MeasureString(Text, Font);
			PT1 = new PointF(pad, Height / 2 - SZ1.Height / 2);

			if (IsMouseDown) {
				PT1.X += 1f;
				PT1.Y += 1f;
				ICP.X += 1f;
				ICP.Y += 1f;
			}

			if (Text != "") {
				G.DrawString(Text, Font, Brushes.Black, PT1.X + 1, PT1.Y + 1);
				G.DrawString(Text, Font, Enabled ? Brushes.White : Brushes.Gray, PT1);
			}

			if (_Icon != null) {
				G.DrawImage(_IconShadow, new RectangleF(ICP.X + 1, ICP.Y + 1, _IconSize.Width, _IconSize.Height));
				G.DrawImage(Enabled ? _Icon : _IconDisabled, new RectangleF(ICP, _IconSize));
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			IsMouseDown = true;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			IsMouseDown = false;
			Invalidate();
		}

		protected override void OnEnabledChanged(EventArgs e) {
			base.OnEnabledChanged(e);
			Invalidate();
		}

		void GenerateExtIcons() {
			_IconShadow = null;
			if (_Icon != null) {
				_IconDisabled = new Bitmap(_Icon.Width, _Icon.Height);
				using (Graphics g = Graphics.FromImage(_IconDisabled)) {
					ImageAttributes attr = new ImageAttributes();
					attr.SetColorMatrix(new ColorMatrix(
						new float[][] { 
						new float[] {0.5f,  0,  0,  0, 0},        // red scaling factor of 2
						new float[] {0,  0.5f,  0,  0, 0},        // green scaling factor of 1
						new float[] {0,  0,  0.5f,  0, 0},        // blue scaling factor of 1
						new float[] {0,  0,  0,  1, 0},        // alpha scaling factor of 1
						new float[] {0,  0,  0,  0, 1}
					}
					), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

					g.DrawImage(_Icon,
						new Rectangle(0, 0, _Icon.Width, _Icon.Height),  // destination rectangle 
						0, 0,        // upper-left corner of source rectangle 
						_Icon.Width,       // width of source rectangle
						_Icon.Height,      // height of source rectangle
						GraphicsUnit.Pixel,
						attr
					);
				}
				_IconShadow = new Bitmap(_Icon.Width, _Icon.Height);
				using (Graphics g = Graphics.FromImage(_IconShadow)) {
					ImageAttributes attr = new ImageAttributes();
					attr.SetColorMatrix(new ColorMatrix(
						new float[][] { 
						new float[] {0,  0,  0,  0, 0},        // red scaling factor of 2
						new float[] {0,  0,  0,  0, 0},        // green scaling factor of 1
						new float[] {0,  0,  0,  0, 0},        // blue scaling factor of 1
						new float[] {0,  0,  0,  1, 0},        // alpha scaling factor of 1
						new float[] {0,  0,  0,  0, 1}
					}
					), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

					g.DrawImage(_Icon,
						new Rectangle(0, 0, _Icon.Width, _Icon.Height),  // destination rectangle 
						0, 0,        // upper-left corner of source rectangle 
						_Icon.Width,       // width of source rectangle
						_Icon.Height,      // height of source rectangle
						GraphicsUnit.Pixel,
						attr
					);
				}
			}
		}
	}

	[DefaultEvent("CheckedChanged")]
	public class NSRadioIconicButton : Control {

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);

		public NSRadioIconicButton() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(65, 65, 65));
			P3 = new Pen(NSTheme.UI_ACCENT);
			B2 = new SolidBrush(NSTheme.UI_ACCENT);
		}

		private ThemeModule.Corners corners = new ThemeModule.Corners();

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ThemeModule.Corners Corners {
			get { return corners; }
			set { corners = value; Invalidate(); }
		}

		private Image _Icon, _IconShadow;
		public Image IconImage {
			get { return _Icon; }
			set {
				_Icon = value;
				if (_Icon != null) {
					_IconSize = _Icon.Size;
				}
				GenerateShadow();
				Invalidate();
			}
		}

		private Size _IconSize;
		public Size IconSize {
			get { return _IconSize; }
			set { _IconSize = value; Invalidate(); }
		}

		private bool _Large;
		public bool Large {
			get { return _Large; }
			set { _Large = value; Invalidate(); }
		}


		private bool _Checked;
		public bool Checked {
			get { return _Checked; }
			set {
				_Checked = value;

				if (_Checked) {
					InvalidateParent();
				}

				if (CheckedChanged != null) {
					CheckedChanged(this);
				}
				Invalidate();
			}
		}

		private void InvalidateParent() {
			if (Parent == null)
				return;

			foreach (Control C in Parent.Controls) {
				if ((!object.ReferenceEquals(C, this)) && (C is NSRadioIconicButton)) {
					((NSRadioIconicButton)C).Checked = false;
				}
			}
		}


		private bool IsMouseDown;
		private GraphicsPath GP1;

		private GraphicsPath GP2;
		private SizeF SZ1;

		private PointF PT1;
		private Pen P1;
		private Pen P2;
		private Pen P3;
		private PathGradientBrush PB1;
		private Brush B2;

		private LinearGradientBrush GB1;

		private Graphics G;
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;
			G.InterpolationMode = InterpolationMode.High;

			GP1 = ThemeModule.CreateRoundIncomplete(new Rectangle(0, 0, Width - 1, Height - 1), _Large ? 15 : 7, corners);
			GP2 = ThemeModule.CreateRoundIncomplete(new Rectangle(1, 1, Width - 3, Height - 3), _Large ? 15 : 7, corners);

			if (Checked) {
				G.FillPath(B2, GP1);
			} else {
				if (IsMouseDown) {
					PB1 = new PathGradientBrush(GP1);
					PB1.CenterColor = Color.FromArgb(60, 60, 60);
					PB1.SurroundColors = new Color[] { Color.FromArgb(55, 55, 55) };
					PB1.FocusScales = new PointF(0.8f, 0.5f);

					G.FillPath(PB1, GP1);
				} else {
					GB1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
					G.FillPath(GB1, GP1);
				}
				G.DrawPath(P1, GP1);
				G.DrawPath(P2, GP2);
			}


			int pad = 5;
			PointF ICP = Point.Empty;
			if (_Icon != null) {
				pad = 13 + _IconSize.Width;
				ICP = new PointF(
					7, Height / 2 - _IconSize.Height / 2
				);
				if (Text == "") {
					ICP = new PointF(
						Width / 2 - _IconSize.Width / 2,
						Height / 2 - _IconSize.Height / 2
					);
				}
			}

			SZ1 = G.MeasureString(Text, Font);
			PT1 = new PointF(pad, Height / 2 - SZ1.Height / 2);

			if (IsMouseDown) {
				PT1.X += 1f;
				PT1.Y += 1f;
				ICP.X += 1f;
				ICP.Y += 1f;
			}

			if (Text != "") {
				G.DrawString(Text, Font, Brushes.Black, PT1.X + 1, PT1.Y + 1);
				G.DrawString(Text, Font, Brushes.White, PT1);
			}

			if (_Icon != null) {
				G.DrawImage(_IconShadow, new RectangleF(ICP.X + 1, ICP.Y + 1, _IconSize.Width, _IconSize.Height));
				G.DrawImage(_Icon, new RectangleF(ICP, _IconSize));
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			IsMouseDown = true;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			IsMouseDown = false;
			Invalidate();
		}

		protected override void OnClick(EventArgs e) {
			base.OnClick(e);
			Checked = true;
		}

		void GenerateShadow() {
			_IconShadow = null;
			if (_Icon != null) {
				_IconShadow = new Bitmap(_Icon.Width, _Icon.Height);
				using (Graphics g = Graphics.FromImage(_IconShadow)) {
					ImageAttributes attr = new ImageAttributes();
					attr.SetColorMatrix(new ColorMatrix(
						new float[][] { 
						new float[] {0,  0,  0,  0, 0},        // red scaling factor of 2
						new float[] {0,  0,  0,  0, 0},        // green scaling factor of 1
						new float[] {0,  0,  0,  0, 0},        // blue scaling factor of 1
						new float[] {0,  0,  0,  1, 0},        // alpha scaling factor of 1
						new float[] {0,  0,  0,  0, 1}
					}
					), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

					g.DrawImage(_Icon,
						new Rectangle(0, 0, _Icon.Width, _Icon.Height),  // destination rectangle 
						0, 0,        // upper-left corner of source rectangle 
						_Icon.Width,       // width of source rectangle
						_Icon.Height,      // height of source rectangle
						GraphicsUnit.Pixel,
						attr
					);
				}
			}
		}
	}

	[DefaultEvent("CheckedChanged")]
	public class NSCheckboxIconicButton : Control {

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);

		public NSCheckboxIconicButton() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(65, 65, 65));
			P3 = new Pen(NSTheme.UI_ACCENT);
			B2 = new SolidBrush(NSTheme.UI_ACCENT);
		}

		private ThemeModule.Corners corners = new ThemeModule.Corners();

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ThemeModule.Corners Corners {
			get { return corners; }
			set { corners = value; Invalidate(); }
		}

		private Image _Icon, _IconShadow;
		public Image IconImage {
			get { return _Icon; }
			set {
				_Icon = value;
				if (_Icon != null) {
					_IconSize = _Icon.Size;
				}
				GenerateShadow();
				Invalidate();
			}
		}

		private Size _IconSize;
		public Size IconSize {
			get { return _IconSize; }
			set { _IconSize = value; Invalidate(); }
		}

		private bool _Large;
		public bool Large {
			get { return _Large; }
			set { _Large = value; Invalidate(); }
		}


		private bool _Checked;
		public bool Checked {
			get { return _Checked; }
			set {
				_Checked = value;

				if (CheckedChanged != null) {
					CheckedChanged(this);
				}
				Invalidate();
			}
		}


		private bool IsMouseDown;
		private GraphicsPath GP1;

		private GraphicsPath GP2;
		private SizeF SZ1;

		private PointF PT1;
		private Pen P1;
		private Pen P2;
		private Pen P3;
		private PathGradientBrush PB1;
		private Brush B2;

		private LinearGradientBrush GB1;

		private Graphics G;
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;
			G.InterpolationMode = InterpolationMode.High;

			GP1 = ThemeModule.CreateRoundIncomplete(new Rectangle(0, 0, Width - 1, Height - 1), _Large ? 15 : 7, corners);
			GP2 = ThemeModule.CreateRoundIncomplete(new Rectangle(1, 1, Width - 3, Height - 3), _Large ? 15 : 7, corners);

			if (Checked) {
				G.FillPath(B2, GP1);
			} else {
				if (IsMouseDown) {
					PB1 = new PathGradientBrush(GP1);
					PB1.CenterColor = Color.FromArgb(60, 60, 60);
					PB1.SurroundColors = new Color[] { Color.FromArgb(55, 55, 55) };
					PB1.FocusScales = new PointF(0.8f, 0.5f);

					G.FillPath(PB1, GP1);
				} else {
					GB1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
					G.FillPath(GB1, GP1);
				}
				G.DrawPath(P1, GP1);
				G.DrawPath(P2, GP2);
			}


			int pad = 5;
			PointF ICP = Point.Empty;
			if (_Icon != null) {
				pad = 13 + _IconSize.Width;
				ICP = new PointF(
					7, Height / 2 - _IconSize.Height / 2
				);
				if (Text == "") {
					ICP = new PointF(
						Width / 2 - _IconSize.Width / 2,
						Height / 2 - _IconSize.Height / 2
					);
				}
			}

			SZ1 = G.MeasureString(Text, Font);
			PT1 = new PointF(pad, Height / 2 - SZ1.Height / 2);

			if (IsMouseDown) {
				PT1.X += 1f;
				PT1.Y += 1f;
				ICP.X += 1f;
				ICP.Y += 1f;
			}

			if (Text != "") {
				G.DrawString(Text, Font, Brushes.Black, PT1.X + 1, PT1.Y + 1);
				G.DrawString(Text, Font, Brushes.White, PT1);
			}

			if (_Icon != null) {
				G.DrawImage(_IconShadow, new RectangleF(ICP.X + 1, ICP.Y + 1, _IconSize.Width, _IconSize.Height));
				G.DrawImage(_Icon, new RectangleF(ICP, _IconSize));
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			IsMouseDown = true;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			IsMouseDown = false;
			Invalidate();
		}

		protected override void OnClick(EventArgs e) {
			base.OnClick(e);
			Checked = !Checked;
		}

		void GenerateShadow() {
			_IconShadow = null;
			if (_Icon != null) {
				_IconShadow = new Bitmap(_Icon.Width, _Icon.Height);
				using (Graphics g = Graphics.FromImage(_IconShadow)) {
					ImageAttributes attr = new ImageAttributes();
					attr.SetColorMatrix(new ColorMatrix(
						new float[][] { 
						new float[] {0,  0,  0,  0, 0},        // red scaling factor of 2
						new float[] {0,  0,  0,  0, 0},        // green scaling factor of 1
						new float[] {0,  0,  0,  0, 0},        // blue scaling factor of 1
						new float[] {0,  0,  0,  1, 0},        // alpha scaling factor of 1
						new float[] {0,  0,  0,  0, 1}
					}
					), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

					g.DrawImage(_Icon,
						new Rectangle(0, 0, _Icon.Width, _Icon.Height),  // destination rectangle 
						0, 0,        // upper-left corner of source rectangle 
						_Icon.Width,       // width of source rectangle
						_Icon.Height,      // height of source rectangle
						GraphicsUnit.Pixel,
						attr
					);
				}
			}
		}
	}

	public class NSProgressBar : Control {

		private int _Minimum;
		public int Minimum {
			get { return _Minimum; }
			set {
				if (value < 0) {
					throw new Exception("Property value is not valid.");
				}

				_Minimum = value;
				if (value > _Value)
					_Value = value;
				if (value > _Maximum)
					_Maximum = value;
				Invalidate();
			}
		}

		private int _Maximum = 100;
		public int Maximum {
			get { return _Maximum; }
			set {
				if (value < 0) {
					throw new Exception("Property value is not valid.");
				}

				_Maximum = value;
				if (value < _Value)
					_Value = value;
				if (value < _Minimum)
					_Minimum = value;
				Invalidate();
			}
		}

		private int _Value;
		public int Value {
			get { return _Value; }
			set {
				if (value > _Maximum || value < _Minimum) {
					throw new Exception("Property value is not valid.");
				}

				_Value = value;
				Invalidate();
			}
		}

		private void Increment(int amount) {
			Value += amount;
		}

		public NSProgressBar() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(55, 55, 55));
			B1 = new SolidBrush(Color.FromArgb(200, 160, 0));
		}

		private GraphicsPath GP1;
		private GraphicsPath GP2;

		private GraphicsPath GP3;
		private Rectangle R1;

		private Rectangle R2;
		private Pen P1;
		private Pen P2;
		private SolidBrush B1;
		private LinearGradientBrush GB1;

		private LinearGradientBrush GB2;

		private int I1;
		private Graphics G;

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
			GP2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

			R1 = new Rectangle(0, 2, Width - 1, Height - 1);
			GB1 = new LinearGradientBrush(R1, Color.FromArgb(45, 45, 45), Color.FromArgb(50, 50, 50), 90f);

			G.SetClip(GP1);
			G.FillRectangle(GB1, R1);

			I1 = (int)Math.Round((float)(_Value - _Minimum) / (float)(_Maximum - _Minimum) * (float)(Width-3));
			System.Diagnostics.Debug.WriteLine("Data: {0}, min {1}, max {2}", I1, _Minimum, _Maximum);

			if (I1 > 1) {
				GP3 = ThemeModule.CreateRound(1, 1, I1, Height - 3, 7);

				R2 = new Rectangle(1, 1, I1, Height - 3);
				GB2 = new LinearGradientBrush(R2, NSTheme.UI_ACCENT, Color.FromArgb(150, 110, 0), 90f);

				G.FillPath(GB2, GP3);
				G.DrawPath(P1, GP3);

				G.SetClip(GP3);
				G.SmoothingMode = SmoothingMode.None;

				G.FillRectangle(B1, R2.X, R2.Y + 1, R2.Width, R2.Height / 2);

				G.SmoothingMode = SmoothingMode.AntiAlias;
				G.ResetClip();
			}

			G.DrawPath(P2, GP1);
			G.DrawPath(P1, GP2);
		}

	}



	public class NSLabel : Control {

		public NSLabel() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			Font = new Font("Tahoma", 8.25f);
		}

		ContentAlignment align = ContentAlignment.MiddleLeft;

		[Category("Appearance")]
		public ContentAlignment TextAlign {
			get {
				return align;
			}
			set {
				align = value;
				Invalidate();
			}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			Graphics G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			G.Clear(BackColor);

			float w = ClientSize.Width - 4;
			float h = ClientSize.Height - 4;
			StringFormat f = new StringFormat();
			f.Alignment = StringAlignment.Center;
			f.LineAlignment = StringAlignment.Center;

			// Горизонталь
			switch (align) {
				case ContentAlignment.BottomCenter:
					f.LineAlignment = StringAlignment.Far;
					break;
				case ContentAlignment.BottomLeft:
					f.Alignment = StringAlignment.Near;
					f.LineAlignment = StringAlignment.Far;
					break;
				case ContentAlignment.BottomRight:
					f.Alignment = StringAlignment.Far;
					f.LineAlignment = StringAlignment.Far;
					break;
				case ContentAlignment.MiddleLeft:
					f.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.MiddleRight:
					f.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.TopCenter:
					f.LineAlignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopLeft:
					f.Alignment = StringAlignment.Near;
					f.LineAlignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopRight:
					f.Alignment = StringAlignment.Far;
					f.LineAlignment = StringAlignment.Near;
					break;
			}

			G.DrawString(Text, Font, Brushes.Black, new RectangleF(3, 3, w, h), f);
			G.DrawString(Text, Font, new SolidBrush(ForeColor), new RectangleF(2, 2, w, h), f);
		}

		protected override void OnTextChanged(EventArgs e) {
			base.OnTextChanged(e);
			Invalidate();
		}
	}

	[DefaultEvent("TextChanged")]
	public class NSTextBox : Control {

		private HorizontalAlignment _TextAlign = HorizontalAlignment.Left;
		public HorizontalAlignment TextAlign {
			get { return _TextAlign; }
			set {
				_TextAlign = value;
				if (Base != null) {
					Base.TextAlign = value;
				}
			}
		}

		private ThemeModule.Corners corners = new ThemeModule.Corners();
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ThemeModule.Corners Corners {
			get { return corners; }
			set { corners = value; Invalidate(); }
		}

		private int _MaxLength = 32767;
		public int MaxLength {
			get { return _MaxLength; }
			set {
				_MaxLength = value;
				if (Base != null) {
					Base.MaxLength = value;
				}
			}
		}

		private bool _ReadOnly;
		public bool ReadOnly {
			get { return _ReadOnly; }
			set {
				_ReadOnly = value;
				if (Base != null) {
					Base.ReadOnly = value;
				}
			}
		}

		private bool _UseSystemPasswordChar;
		public bool UseSystemPasswordChar {
			get { return _UseSystemPasswordChar; }
			set {
				_UseSystemPasswordChar = value;
				if (Base != null) {
					Base.UseSystemPasswordChar = value;
				}
			}
		}

		private bool _Multiline;
		public bool Multiline {
			get { return _Multiline; }
			set {
				_Multiline = value;
				if (Base != null) {
					Base.Multiline = value;

					if (value) {
						Base.Height = Height - 11;
					} else {
						Height = Base.Height + 11;
					}
				}
			}
		}

		public override string Text {
			get { return base.Text; }
			set {
				base.Text = value;
				if (Base != null) {
					Base.Text = value;
				}
			}
		}

		public override Font Font {
			get { return base.Font; }
			set {
				base.Font = value;
				if (Base != null) {
					Base.Font = value;
					Base.Location = new Point(5, 5);
					Base.Width = Width - 8;

					if (!_Multiline) {
						Height = Base.Height + 11;
					}
				}
			}
		}

		protected override void OnHandleCreated(EventArgs e) {
			if (!Controls.Contains(Base)) {
				Controls.Add(Base);
			}

			base.OnHandleCreated(e);
		}

		private TextBox Base;
		public NSTextBox() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, true);

			Cursor = Cursors.IBeam;

			Base = new TextBox();
			Base.Font = Font;
			Base.Text = Text;
			Base.MaxLength = _MaxLength;
			Base.Multiline = _Multiline;
			Base.ReadOnly = _ReadOnly;
			Base.UseSystemPasswordChar = _UseSystemPasswordChar;

			Base.ForeColor = Color.White;
			Base.BackColor = Color.FromArgb(50, 50, 50);

			Base.BorderStyle = BorderStyle.None;

			Base.Location = new Point(5, 5);
			Base.Width = Width - 14;

			if (_Multiline) {
				Base.Height = Height - 11;
			} else {
				Height = Base.Height + 11;
			}

			Base.TextChanged += OnBaseTextChanged;
			Base.KeyDown += OnBaseKeyDown;

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(55, 55, 55));
		}

		private GraphicsPath GP1;

		private GraphicsPath GP2;
		private Pen P1;
		private Pen P2;

		private PathGradientBrush PB1;
		private Graphics G;

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRoundIncomplete(new Rectangle(0, 0, Width - 1, Height - 1), 7, corners);
			GP2 = ThemeModule.CreateRoundIncomplete(new Rectangle(1, 1, Width - 3, Height - 3), 7, corners);

			PB1 = new PathGradientBrush(GP1);
			PB1.CenterColor = Color.FromArgb(50, 50, 50);
			PB1.SurroundColors = new Color[] { Color.FromArgb(45, 45, 45) };
			PB1.FocusScales = new PointF(0.9f, 0.5f);

			G.FillPath(PB1, GP1);

			G.DrawPath(P2, GP1);
			G.DrawPath(P1, GP2);
		}

		private void OnBaseTextChanged(object s, EventArgs e) {
			Text = Base.Text;
		}

		private void OnBaseKeyDown(object s, KeyEventArgs e) {
			if (e.Control && e.KeyCode == Keys.A) {
				Base.SelectAll();
				e.SuppressKeyPress = true;
			}
		}

		protected override void OnResize(EventArgs e) {
			Base.Location = new Point(5, 5);

			Base.Width = Width - 10;
			Base.Height = Height - 11;

			base.OnResize(e);
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			Base.Focus();
			base.OnMouseDown(e);
		}

		protected override void OnEnter(EventArgs e) {
			Base.Focus();
			Invalidate();
			base.OnEnter(e);
		}

		protected override void OnLeave(EventArgs e) {
			Invalidate();
			base.OnLeave(e);
		}

	}

	[DefaultEvent("CheckedChanged")]
	public class NSCheckBox : Control {

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);

		public NSCheckBox() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			P11 = new Pen(Color.FromArgb(55, 55, 55));
			P22 = new Pen(Color.FromArgb(35, 35, 35));
			P3 = new Pen(Color.Black, 2f);
			P4 = new Pen(Color.White, 2f);
		}

		private bool _Checked;
		public bool Checked {
			get { return _Checked; }
			set {
				_Checked = value;
				if (CheckedChanged != null) {
					CheckedChanged(this);
				}

				Invalidate();
			}
		}

		private GraphicsPath GP1;

		private GraphicsPath GP2;
		private SizeF SZ1;

		private PointF PT1;
		private Pen P11;
		private Pen P22;
		private Pen P3;

		private Pen P4;

		private PathGradientBrush PB1;
		private Graphics G;
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRound(0, 2, Height - 5, Height - 5, 5);
			GP2 = ThemeModule.CreateRound(1, 3, Height - 7, Height - 7, 5);

			PB1 = new PathGradientBrush(GP1);
			PB1.CenterColor = Color.FromArgb(50, 50, 50);
			PB1.SurroundColors = new Color[] { Color.FromArgb(45, 45, 45) };
			PB1.FocusScales = new PointF(0.3f, 0.3f);

			G.FillPath(PB1, GP1);
			G.DrawPath(P11, GP1);
			G.DrawPath(P22, GP2);

			if (_Checked) {
				G.DrawLine(P3, 5, Height - 9, 8, Height - 7);
				G.DrawLine(P3, 7, Height - 7, Height - 8, 7);

				G.DrawLine(P4, 4, Height - 10, 7, Height - 8);
				G.DrawLine(P4, 6, Height - 8, Height - 9, 6);
			}

			SZ1 = G.MeasureString(Text, Font);
			PT1 = new PointF(Height - 3, Height / 2 - SZ1.Height / 2);

			G.DrawString(Text, Font, Brushes.Black, PT1.X + 1, PT1.Y + 1);
			G.DrawString(Text, Font, Brushes.White, PT1);
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			Checked = !Checked;
		}

	}

	[DefaultEvent("CheckedChanged")]
	public class NSRadioButton : Control {

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);

		public NSRadioButton() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			P1 = new Pen(Color.FromArgb(55, 55, 55));
			P2 = new Pen(Color.FromArgb(35, 35, 35));
		}

		private bool _Checked;
		public bool Checked {
			get { return _Checked; }
			set {
				_Checked = value;

				if (_Checked) {
					InvalidateParent();
				}

				if (CheckedChanged != null) {
					CheckedChanged(this);
				}
				Invalidate();
			}
		}

		private void InvalidateParent() {
			if (Parent == null)
				return;

			foreach (Control C in Parent.Controls) {
				if ((!object.ReferenceEquals(C, this)) && (C is NSRadioButton)) {
					((NSRadioButton)C).Checked = false;
				}
			}
		}


		private GraphicsPath GP1;
		private SizeF SZ1;

		private PointF PT1;
		private Pen P1;

		private Pen P2;

		private PathGradientBrush PB1;
		private Graphics G;
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = new GraphicsPath();
			GP1.AddEllipse(0, 2, Height - 5, Height - 5);

			PB1 = new PathGradientBrush(GP1);
			PB1.CenterColor = Color.FromArgb(50, 50, 50);
			PB1.SurroundColors = new Color[] { Color.FromArgb(45, 45, 45) };
			PB1.FocusScales = new PointF(0.3f, 0.3f);

			G.FillPath(PB1, GP1);

			G.DrawEllipse(P1, 0, 2, Height - 5, Height - 5);
			G.DrawEllipse(P2, 1, 3, Height - 7, Height - 7);

			if (_Checked) {
				G.FillEllipse(Brushes.Black, 6, 8, Height - 15, Height - 15);
				G.FillEllipse(Brushes.White, 5, 7, Height - 15, Height - 15);
			}

			SZ1 = G.MeasureString(Text, Font);
			PT1 = new PointF(Height - 3, Height / 2 - SZ1.Height / 2);

			G.DrawString(Text, Font, Brushes.Black, PT1.X + 1, PT1.Y + 1);
			G.DrawString(Text, Font, Brushes.White, PT1);
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			Checked = true;
			base.OnMouseDown(e);
		}

	}

	public class NSComboBox : Control {

		/// <summary>
		/// Выбранный элемент
		/// </summary>
		public int SelectedIndex {
			get {
				return selected;
			}
			set {
				selected = value;
				if (items!=null) {
					if (selected<0) {
						selected = 0;
					}else if(selected >= items.Length){
						selected = items.Length - 1;
					}
				} else {
					selected = -1;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Строки комбобокса
		/// </summary>
		[EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design", "System.Drawing.Design.UITypeEditor")]
		public string Items {
			get {
				if (items!=null) {
					return string.Join("\n", items);
				}
				return "";
			}
			set {
				items = value.Replace("\r", "").Split('\n');

				// Пересоздание контекстного меню
				context.Items.Clear();
				for (int i = 0; i < items.Length; i++) {
					if (items[i].Length>0) {
						context.Items.Add(items[i]);
					}else{
						context.Items.Add(new ToolStripSeparator());
					}
				}
				if (selected < 0) {
					selected = 0;
				} else if (selected >= items.Length) {
					selected = items.Length - 1;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Элементы списка
		/// </summary>
		string[] items;

		/// <summary>
		/// Выбранный пункт
		/// </summary>
		int selected;

		/// <summary>
		/// Контекстное меню
		/// </summary>
		NSContextMenu context;

		public NSComboBox() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);		

			BackColor = Color.FromArgb(50, 50, 50);
			ForeColor = Color.White;

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.White, 2f);
			P3 = new Pen(Brushes.Black, 2f);
			P4 = new Pen(Color.FromArgb(65, 65, 65));

			B1 = new SolidBrush(Color.FromArgb(65, 65, 65));
			B2 = new SolidBrush(Color.FromArgb(55, 55, 55));

			context = new NSContextMenu();
			context.ItemClicked += context_ItemClicked;
		}

		private GraphicsPath GP1;

		private GraphicsPath GP2;
		private SizeF SZ1;

		private PointF PT1;
		private Pen P1;
		private Pen P2;
		private Pen P3;
		private Pen P4;
		private SolidBrush B1;

		private SolidBrush B2;

		private LinearGradientBrush GB1;
		private Graphics G;
		protected override void OnPaint(PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
			GP2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

			GB1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
			G.SetClip(GP1);
			G.FillRectangle(GB1, ClientRectangle);
			G.ResetClip();

			G.DrawPath(P1, GP1);
			G.DrawPath(P4, GP2);

			if (selected!=-1 && items != null) {
				SZ1 = G.MeasureString(items[selected], Font);
				PT1 = new PointF(5, Height / 2 - SZ1.Height / 2);
				G.DrawString(items[selected], Font, Brushes.Black, PT1.X + 1, PT1.Y + 1);
				G.DrawString(items[selected], Font, Brushes.White, PT1);
			}
			

			G.DrawLine(P3, Width - 15, 10, Width - 11, 13);
			G.DrawLine(P3, Width - 7, 10, Width - 11, 13);
			G.DrawLine(Pens.Black, Width - 11, 13, Width - 11, 14);

			G.DrawLine(P2, Width - 16, 9, Width - 12, 12);
			G.DrawLine(P2, Width - 8, 9, Width - 12, 12);
			G.DrawLine(Pens.White, Width - 12, 12, Width - 12, 13);

			G.DrawLine(P1, Width - 22, 0, Width - 22, Height);
			G.DrawLine(P4, Width - 23, 1, Width - 23, Height - 2);
			G.DrawLine(P4, Width - 21, 1, Width - 21, Height - 2);
		}

		protected override void OnClick(EventArgs e) {
			

			Point tp = PointToScreen(new Point(0, Height));
			Screen scr = Screen.FromPoint(tp);
			if (tp.Y+context.Height > scr.Bounds.Height) {
				context.Show(this, Point.Empty, ToolStripDropDownDirection.AboveRight);
			} else {
				context.Show(this, new Point(0, Height), ToolStripDropDownDirection.BelowRight);
			}
		}

		void context_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
			SelectedIndex = context.Items.IndexOf(e.ClickedItem);
		}
	}

	public class NSTabControl : TabControl {

		public NSTabControl() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			SizeMode = TabSizeMode.Fixed;
			Alignment = TabAlignment.Left;
			ItemSize = new Size(28, 115);

			DrawMode = TabDrawMode.OwnerDrawFixed;

			P1 = new Pen(Color.FromArgb(55, 55, 55));
			P2 = new Pen(Color.FromArgb(35, 35, 35));
			P3 = new Pen(Color.FromArgb(45, 45, 45), 2);

			B1 = new SolidBrush(Color.FromArgb(50, 50, 50));
			B2 = new SolidBrush(Color.FromArgb(35, 35, 35));
			B3 = new SolidBrush(NSTheme.UI_ACCENT);
			B4 = new SolidBrush(Color.FromArgb(65, 65, 65));

			SF1 = new StringFormat();
			SF1.LineAlignment = StringAlignment.Center;
		}

		protected override void OnControlAdded(ControlEventArgs e) {
			if (e.Control is TabPage) {
				e.Control.BackColor = Color.FromArgb(50, 50, 50);
			}

			base.OnControlAdded(e);
		}

		private GraphicsPath GP1;
		private GraphicsPath GP2;
		private GraphicsPath GP3;

		private GraphicsPath GP4;
		private Rectangle R1;

		private Rectangle R2;
		private Pen P1;
		private Pen P2;
		private Pen P3;
		private SolidBrush B1;
		private SolidBrush B2;
		private SolidBrush B3;

		private SolidBrush B4;

		private PathGradientBrush PB1;
		private TabPage TP1;

		private StringFormat SF1;
		private int Offset;

		private int ItemHeight;
		private Graphics G;

		protected override void OnPaint(PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(Color.FromArgb(50, 50, 50));
			G.SmoothingMode = SmoothingMode.AntiAlias;

			ItemHeight = ItemSize.Height + 2;

			GP1 = ThemeModule.CreateRound(0, 0, ItemHeight + 3, Height - 1, 7);
			GP2 = ThemeModule.CreateRound(1, 1, ItemHeight + 3, Height - 3, 7);

			PB1 = new PathGradientBrush(GP1);
			PB1.CenterColor = Color.FromArgb(50, 50, 50);
			PB1.SurroundColors = new Color[] { Color.FromArgb(45, 45, 45) };
			PB1.FocusScales = new PointF(0.8f, 0.95f);

			G.FillPath(PB1, GP1);

			G.DrawPath(P1, GP1);
			G.DrawPath(P2, GP2);

			for (int I = 0; I <= TabCount - 1; I++) {
				R1 = GetTabRect(I);
				R1.Y += 2;
				R1.Height -= 3;
				R1.Width += 1;
				R1.X -= 1;

				TP1 = TabPages[I];
				Offset = 0;

				if (SelectedIndex == I) {
					G.FillRectangle(B1, R1);

					for (int J = 0; J <= 1; J++) {
						G.FillRectangle(B2, R1.X + 5 + (J * 5), R1.Y + 6, 2, R1.Height - 9);

						G.SmoothingMode = SmoothingMode.None;
						G.FillRectangle(B3, R1.X + 5 + (J * 5), R1.Y + 5, 2, R1.Height - 9);
						G.SmoothingMode = SmoothingMode.AntiAlias;

						Offset += 5;
					}

					G.DrawRectangle(P3, R1.X + 1, R1.Y - 1, R1.Width, R1.Height + 2);
					G.DrawRectangle(P1, R1.X + 1, R1.Y + 1, R1.Width - 2, R1.Height - 2);
					G.DrawRectangle(P2, R1);
				} else {
					for (int J = 0; J <= 1; J++) {
						G.FillRectangle(B2, R1.X + 5 + (J * 5), R1.Y + 6, 2, R1.Height - 9);

						G.SmoothingMode = SmoothingMode.None;
						G.FillRectangle(B4, R1.X + 5 + (J * 5), R1.Y + 5, 2, R1.Height - 9);
						G.SmoothingMode = SmoothingMode.AntiAlias;

						Offset += 5;
					}
				}

				R1.X += 5 + Offset;

				R2 = R1;
				R2.Y += 1;
				R2.X += 1;

				G.DrawString(TP1.Text, Font, Brushes.Black, R2, SF1);
				G.DrawString(TP1.Text, Font, Brushes.White, R1, SF1);
			}

			GP3 = ThemeModule.CreateRound(ItemHeight, 0, Width - ItemHeight - 1, Height - 1, 7);
			GP4 = ThemeModule.CreateRound(ItemHeight + 1, 1, Width - ItemHeight - 3, Height - 3, 7);

			G.DrawPath(P2, GP3);
			G.DrawPath(P1, GP4);
		}

	}

	public class NSProjectControl : TabControl {

		public delegate void TabCloseEventHandler(object sender, TabCloseEventArgs e);

		public event TabCloseEventHandler TabClose;

		public NSProjectControl() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			SizeMode = TabSizeMode.Fixed;
			Alignment = TabAlignment.Top;

			DrawMode = TabDrawMode.OwnerDrawFixed;

			P1 = new Pen(Color.FromArgb(55, 55, 55));
			P2 = new Pen(Color.FromArgb(35, 35, 35));
			P3 = new Pen(Color.FromArgb(45, 45, 45), 2);
			P4 = new Pen(Color.FromArgb(100, 100, 100), 2);
			P5 = new Pen(Color.FromArgb(35, 35, 35), 2);

			B1 = new SolidBrush(Color.FromArgb(50, 50, 50));
			B2 = new SolidBrush(Color.FromArgb(35, 35, 35));
			B3 = new SolidBrush(NSTheme.UI_ACCENT);
			B4 = new SolidBrush(Color.FromArgb(65, 65, 65));

			SF1 = new StringFormat();
			SF1.LineAlignment = StringAlignment.Center;

			boldFont = new Font(Font, FontStyle.Bold);

			this.Scroller.ScrollLeft += new EventHandler(Scroller_ScrollLeft);
			this.Scroller.ScrollRight += new EventHandler(Scroller_ScrollRight);
			this.Scroller.TabList += new EventHandler(Scroller_TabList);
		}

		protected override void OnControlAdded(ControlEventArgs e) {
			if (e.Control is TabPage) {
				e.Control.BackColor = Color.FromArgb(50, 50, 50);
			}

			base.OnControlAdded(e);
		}

		private Rectangle R1;

		private Rectangle R2;
		private Pen P1;
		private Pen P2;
		private Pen P3;
		private Pen P4;
		private Pen P5;
		private SolidBrush B1;
		private SolidBrush B2;
		private SolidBrush B3;
		private Font boldFont;

		private SolidBrush B4;

		private TabPage TP1;

		private StringFormat SF1;

		private int ItemHeight;

		private TabPage ClosingPage;

		//private TabPage DraggingPage;
		//private bool DraggingActive;

		protected override void OnPaint(PaintEventArgs e) {
			Graphics G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(Color.FromArgb(40, 40, 40));
			G.SmoothingMode = SmoothingMode.AntiAlias;

			ItemHeight = ItemSize.Height + 2;
			G.DrawLine(P2, 0, ItemHeight, Width, ItemHeight);

			for (int I = 0; I < TabCount; I++) {
				R1 = GetTabRect(I);
				R1.Y += 2;
				R1.Height -= 3;
				R1.Width += 1;
				R1.X -= 1;

				TP1 = TabPages[I];

				Rectangle iconSize = new Rectangle(
					R1.X + 5, R1.Y + 3, ItemHeight - 10, ItemHeight - 10
				);

				if (SelectedIndex == I) {
					G.FillRectangle(B1, R1.X, R1.Y, R1.Width, R1.Height + 8);
					G.DrawRectangle(P2, R1.X, R1.Y, R1.Width, R1.Height + 8);

					// Крестик
					if (TabCount > 1) {
						Rectangle CR1 = new Rectangle(R1.X + R1.Width - 19, R1.Y + 4, 15, 15);
						Pen CPR1 = P4;
						R1.Width -= 24;

						if (ClosingPage == TP1) {
							G.FillRectangle(B2, CR1);
							CPR1 = P1;
						} else {
							G.DrawRectangle(P2, CR1);
						}

						G.DrawLine(CPR1,
							new Point(CR1.X + 4, CR1.Y + 4),
							new Point(CR1.X + CR1.Width - 4, CR1.Y + CR1.Height - 4)
						);
						G.DrawLine(CPR1,
							new Point(CR1.X + CR1.Width - 4, CR1.Y + 4),
							new Point(CR1.X + 4, CR1.Y + CR1.Height - 4)
						);

					}
				}

				Font labelFont = Font;
				string label = "";
				BaseForm frm = TabPages[I].Tag as BaseForm;
				if (frm!=null) {
					label = frm.FileEditor.Title;
					frm.DrawIcon(G, iconSize.X, iconSize.Y);
					R1.X += iconSize.Width + 8;
					R1.Width -= iconSize.Width + 8;
					if (!frm.FileEditor.Saved) {
						labelFont = boldFont;
					}
				}


				R1.X += 0;
				R1.Y += 1;

				R2 = R1;
				R2.Y += 1;
				R2.X += 1;

				SizeF strz = G.MeasureString(label, labelFont);
				if (strz.Width > R1.Width) {
					for (int ln = label.Length - 1; ln > 0; ln--) {
						string lg = label.Substring(0, ln) + "...";
						strz = G.MeasureString(lg, labelFont);
						if (strz.Width < R1.Width) {
							label = lg;
							break;
						}
					}
				}

				G.DrawString(label, labelFont, Brushes.Black, R2, SF1);
				G.DrawString(label, labelFont, Brushes.White, R1, SF1);
			}
			G.FillRectangle(B1, new Rectangle(0, ItemHeight + 1, Width, Height - ItemHeight - 1));

		}

		protected override void OnMouseDown(MouseEventArgs e) {
			ClosingPage = null;
			//DraggingActive = false;
			//DraggingPage = null;
			if (TabCount > 1) {
				for (int I = 0; I < TabCount; I++) {
					if (I == SelectedIndex) {
						R1 = GetTabRect(I);
						R1.Y += 2;
						R1.Height -= 3;
						R1.Width += 1;
						R1.X -= 1;
						Rectangle CR1 = new Rectangle(R1.X + R1.Width - 18, R1.Y + 3, 15, 15);
						if (CR1.Contains(e.Location)) {
							ClosingPage = TabPages[I];
							break;
						}
					}
				}
			}
			if (ClosingPage == null) {

			}
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			if (ClosingPage != null) {
				for (int I = 0; I < TabCount; I++) {
					if (I == SelectedIndex) {
						R1 = GetTabRect(I);
						R1.Y += 2;
						R1.Height -= 3;
						R1.Width += 1;
						R1.X -= 1;
						Rectangle CR1 = new Rectangle(R1.X + R1.Width - 18, R1.Y + 3, 15, 15);
						if (CR1.Contains(e.Location)) {
							TabCloseEventArgs ea = new TabCloseEventArgs() {
								Cancel = false,
								Page = ClosingPage
							};
							if (TabClose != null) {
								TabClose((object)this, ea);
							}
							if (!ea.Cancel) {
								int sa = SelectedIndex;
								TabPages.Remove(ea.Page);
								SelectedIndex = (TabCount - 1 < sa) ? TabCount - 1 : sa;
							}
							break;
						}
					}
				}
			}
			ClosingPage = null;
			Invalidate();
		}

		public void AddTab(TabPage p, bool select = false) {
			TabPages.Add(p);
			if (select) {
				SelectedTab = p;
			}
		}

		public void RemoveTab(TabPage p) {
			int sa = SelectedIndex;
			bool sel = p == SelectedTab;
			TabPages.Remove(p);
			if (sel) {
				SelectedIndex = (TabCount - 1 < sa) ? TabCount - 1 : sa;
			}
		}

		public class TabCloseEventArgs : EventArgs {
			public bool Cancel;
			public TabPage Page;
		}

		#region Windows Form Designer generated code

		//TabControl overrides dispose to clean up the component list.
		[PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (UpDown != null)
					UpDown.ReleaseHandle();
			}
			base.Dispose(disposing);
		}


		#region  PInvoke Declarations

		[DllImport("User32", CallingConvention = CallingConvention.Cdecl)]
		private static extern int RealGetWindowClass(IntPtr hwnd, System.Text.StringBuilder pszType, int cchType);

		[DllImport("user32")]
		private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		private const int WM_Create = 0x1;
		private const int WM_PARENTNOTIFY = 0x210;
		private const int WM_HSCROLL = 0x114;

		#endregion

		private NSNativeUpDown UpDown = null;
		private NSTabScroller Scroller = new NSTabScroller();

		private int ScrollPosition {
			get {
				int multiplier = -1;
				Rectangle tabRect;
				do {
					tabRect = GetTabRect(multiplier + 1);
					multiplier++;
				}
				while (tabRect.Left < 0 && multiplier < this.TabCount);
				return multiplier;
			}
		}


		[PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
		protected override void WndProc(ref System.Windows.Forms.Message m) {
			if (m.Msg == WM_PARENTNOTIFY) {
				if ((ushort)(m.WParam.ToInt32() & 0xFFFF) == WM_Create) {
					System.Text.StringBuilder WindowName = new System.Text.StringBuilder(16);
					RealGetWindowClass(m.LParam, WindowName, 16);
					if (WindowName.ToString() == "msctls_updown32") {
						//unhook the existing updown control as it will be recreated if 
						//the tabcontrol is recreated (alignment, visible changed etc..)
						if (UpDown != null)
							UpDown.ReleaseHandle();
						//and hook it.
						UpDown = new NSNativeUpDown();
						UpDown.AssignHandle(m.LParam);
					}
				}
			}
			base.WndProc(ref m);
		}


		protected override void OnHandleCreated(System.EventArgs e) {
			base.OnHandleCreated(e);
			if (this.Multiline == false) {
				SetParent(Scroller.Handle, this.Handle);
			}
			this.OnFontChanged(EventArgs.Empty);
		}


		protected override void OnFontChanged(System.EventArgs e) {
			base.OnFontChanged(e);
			this.OnResize(EventArgs.Empty);
		}


		protected override void OnResize(System.EventArgs e) {
			base.OnResize(e);
			Invalidate(true);
			if (this.Multiline)
				return;
			if (this.Alignment == TabAlignment.Top)
				Scroller.Location = new Point(this.Width - Scroller.Width, 4);
			else
				Scroller.Location = new Point(this.Width - Scroller.Width, this.Height - Scroller.Height - 2);
		}


		private void Scroller_ScrollLeft(Object sender, System.EventArgs e) {
			if (this.TabCount == 0)
				return;
			int scrollPos = Math.Max(0, (ScrollPosition - 1) * 0x10000);
			SendMessage(this.Handle, WM_HSCROLL, (IntPtr)(scrollPos | 0x4), IntPtr.Zero);
			SendMessage(this.Handle, WM_HSCROLL, (IntPtr)(scrollPos | 0x8), IntPtr.Zero);
		}


		private void Scroller_ScrollRight(Object sender, System.EventArgs e) {
			if (this.TabCount == 0)
				return;
			if (GetTabRect(this.TabCount - 1).Right <= this.Scroller.Left)
				return;
			int scrollPos = Math.Max(0, (ScrollPosition + 1) * 0x10000);
			SendMessage(this.Handle, WM_HSCROLL, (IntPtr)(scrollPos | 0x4), IntPtr.Zero);
			SendMessage(this.Handle, WM_HSCROLL, (IntPtr)(scrollPos | 0x8), IntPtr.Zero);
		}


		private void Scroller_TabList(Object sender, System.EventArgs e) {
			if (TabPages.Count==0) {
				return;
			}
			
			NSContextMenu cm = new NSContextMenu();
			foreach (TabPage t in TabPages) {
				BaseForm frm = (BaseForm)t.Tag;
				if (frm!=null) {
					cm.Items.Add(frm.FileEditor.Title, frm.Icon, (sn, ev) => {
						SelectedTab = t;
					});
				}
			}
			
			Point showPos = Scroller.ListButton.PointToScreen(new Point(0, Scroller.ListButton.Height));
			cm.Show(showPos.X, showPos.Y);
		}


		#endregion


	}

	public class NSGraphicsCanvas : Control {

		/// <summary>
		/// Держать мышь в пределах
		/// </summary>
		public bool LockMouse {
			get {
				return locked;
			}
			set {
				locked = value;
				prevPos = pos;
			}
		}

		/// <summary>
		/// Скорость мыши
		/// </summary>
		public PointF MouseSpeed {
			get {
				PointF p = new PointF(pos.X - prevPos.X, pos.Y - prevPos.Y);
				prevPos = pos;
				return p;
			}
		}

		/// <summary>
		/// Координаты мыши
		/// </summary>
		public Point MouseLocation;

		/// <summary>
		/// Находится ли мышь внутри
		/// </summary>
		public bool MouseInside {
			get;
			private set;
		}

		/// <summary>
		/// Контекст
		/// </summary>
		GraphicsContext context;

		/// <summary>
		/// Информация об окне
		/// </summary>
		IWindowInfo info;

		/// <summary>
		/// Находится ли контрол в режиме дизайна
		/// </summary>
		bool design;

		/// <summary>
		/// Мышь была передвинута кодом
		/// </summary>
		bool jumping;

		/// <summary>
		/// Предыдущая позиция мыши
		/// </summary>
		Point prevPos;

		/// <summary>
		/// Позиция мыши
		/// </summary>
		Point pos;

		/// <summary>
		/// Занята ли мышь
		/// </summary>
		bool locked;

		/// <summary>
		/// Конструктор
		/// </summary>
		public NSGraphicsCanvas() {
			BackColor = Color.FromArgb(50, 50, 50);
			SetStyle(ControlStyles.Opaque, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			DoubleBuffered = false;
			LockMouse = false;

			design = DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
			if (!design) {
				info = Utilities.CreateWindowsWindowInfo(Handle);
				context = new GraphicsContext(
					new GraphicsMode(new ColorFormat(32), 16, 0, 0),
					info,
					2, 4,
					GraphicsContextFlags.Default
				);
				context.MakeCurrent(info);
				(context as IGraphicsContext).LoadAll();
				context.SwapInterval = 0;
				context.Update(info);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			Focus();
			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			if (!jumping || !LockMouse) {
				prevPos = pos;
				pos = Cursor.Position;

				if (LockMouse) {
					Rectangle r = RectangleToScreen(new Rectangle(Point.Empty, Size));

					Point oms = pos;
					int gap = 20;

					if (pos.X-gap < r.Left) {
						pos.X += r.Width - gap * 2;
						prevPos.X += r.Width - gap * 2;
					} else if (pos.X + gap > r.Right) {
						pos.X -= r.Width - gap * 2;
						prevPos.X -= r.Width - gap * 2;
					}
					if (pos.Y - gap < r.Top) {
						pos.Y += r.Height - gap * 2;
						prevPos.Y += r.Height - gap * 2;
					} else if (pos.Y + gap > r.Bottom) {
						pos.Y -= r.Height - gap * 2;
						prevPos.Y -= r.Height - gap * 2;
					}

					if (pos.X != oms.X || pos.Y != oms.Y) {
						jumping = true;
						Cursor.Position = pos;
						jumping = false;
					}
				}

				base.OnMouseMove(e);

			}
		}

		/// <summary>
		/// Установка контекста в качестве текущего
		/// </summary>
		public void MakeCurrent() {
			if (!context.IsCurrent) {
				context.MakeCurrent(info);
			}
		}

		/// <summary>
		/// Смена кадров
		/// </summary>
		public void Swap() {
			context.SwapBuffers();
		}

		protected override void OnPaint(PaintEventArgs e) {
			if (design) {
				e.Graphics.Clear(Color.FromArgb(20, 20, 20));
			}
		}
	}

	[DefaultEvent("FileChanged")]
	public class NSFileDropControl : Control {

		/// <summary>
		/// Типы для открытия изображений
		/// </summary>
		public const string ImageTypes = ".png|.jpg|.jpeg|.gif|.bmp";

		/// <summary>
		/// Файл изменился
		/// </summary>
		public event EventHandler FileChanged;

		/// <summary>
		/// Ссылка на файл
		/// </summary>
		public Project.Entry File {
			get {
				return file;
			}
			set {
				file = value;
				if (file!=null) {
					if (file.Icon == null) {
						file.Icon = Preview.Get(file.FullPath);
					}
				}
				Invalidate();
				if (FileChanged != null) {
					FileChanged(this as object, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Доступные для перетаскивания файлы
		/// </summary>
		[DefaultValue("")]
		public string AllowedTypes {
			get {
				if (types == null) {
					return "";
				} else {
					return string.Join("|", types);
				}
			}
			set {
				if (value != null) {
					string d = value;
					d = d.Replace("[images]", ImageTypes);
					types = d.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
				} else {
					types = new string[0];
				}
			}
		}

		/// <summary>
		/// Кнопка для выбора изображения
		/// </summary>
		NSIconicButton pickerButton;

		/// <summary>
		/// Окно-пикер
		/// </summary>
		ProjectPickerDialog picker;

		/// <summary>
		/// Выбраный файл
		/// </summary>
		Project.Entry file;

		/// <summary>
		/// Доступные типы файлов
		/// </summary>
		string[] types;

		/// <summary>
		/// Создание контрола
		/// </summary>
		public NSFileDropControl() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			BackColor = Color.FromArgb(50, 50, 50);
			Font = new System.Drawing.Font("Tahoma", 8);

			pickerButton = new NSIconicButton();
			pickerButton.Size = new Size(30, 18);
			pickerButton.IconImage = ControlImages.pickdots;
			pickerButton.IconSize = new System.Drawing.Size(17, 5);
			pickerButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			pickerButton.Location = new Point(-31, -18);
			pickerButton.Corners = new ThemeModule.Corners() {
				BottomLeft = true,
				BottomRight = true,
				TopLeft = false,
				TopRight = false
			};
			Controls.Add(pickerButton);
			Size = new System.Drawing.Size(100, 100);
			AllowDrop = true;
			Preview.PreviewsReady += Preview_PreviewsReady;

			pickerButton.Click += pickerButton_Click;
		}

		/// <summary>
		/// Поддерживается ли имя файла
		/// </summary>
		/// <param name="fn"></param>
		public bool FileSupported(string fn) {
			string ex = System.IO.Path.GetExtension(fn).ToLower();
			foreach (string ds in types) {
				if (ds.ToLower() == ex) {
					return true;
				}
			}
			return false;
		}

		void pickerButton_Click(object sender, EventArgs e) {
			picker = new ProjectPickerDialog();
			picker.Dropper = this;

			// Вычисление расположения
			Point loc = pickerButton.PointToScreen(Point.Empty);
			Screen scr = Screen.FromPoint(loc);
			picker.Location = new Point(
				loc.X, loc.Y + pickerButton.Height
			);

			if (picker.Location.Y + picker.Height > scr.Bounds.Bottom) {
				picker.Location = new Point(picker.Location.X, loc.Y - picker.Height); 
			}
			if (picker.Location.X + picker.Width > scr.Bounds.Right) {
				picker.Location = new Point(loc.X - picker.Width + pickerButton.Width, picker.Location.Y); 
			}


			picker.Show();
		}

		void Preview_PreviewsReady(Events.Data.PreviewReadyEventArgs e) {
			if (file != null) {
				foreach (Preview p in e.ReadyPreviews) {
					if (p == file.Icon) {
						Invalidate();
						break;
					}
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			Graphics G = e.Graphics;
			G.Clear(BackColor);

			ThemeModule.Corners corners = new ThemeModule.Corners() {
				TopLeft = true, TopRight = true,
				BottomLeft = true, BottomRight = false
			};

			GraphicsPath GP1 = ThemeModule.CreateRoundIncomplete(new Rectangle(0, 0, Width - 1, Height - 1 - 18), 7, corners);
			GraphicsPath GP2 = ThemeModule.CreateRoundIncomplete(new Rectangle(1, 1, Width - 3, Height - 3 - 18), 7, corners);
			Pen P1 = new Pen(Color.FromArgb(35, 35, 35));
			Pen P2 = new Pen(Color.FromArgb(55, 55, 55));

			PathGradientBrush PB1 = new PathGradientBrush(GP1);
			PB1.CenterColor = Color.FromArgb(50, 50, 50);
			PB1.SurroundColors = new Color[] { Color.FromArgb(45, 45, 45) };
			PB1.FocusScales = new PointF(0.7f, 0.7f);

			G.FillPath(PB1, GP1);

			// Лейбл
			string label = "";

			// Отрисовка иконки
			if (file != null) {
				Rectangle rc = ClientRectangle;
				rc.X = 8;
				rc.Y = 8;
				rc.Width -= 16;
				rc.Height -= 16 + 18;
				if (file.Icon!=null) {
					file.Icon.LargeIcon.Draw(G, rc);
				}
				label = System.IO.Path.GetFileNameWithoutExtension(file.Name);
			}

			int mw = ClientRectangle.Width - 32;
			SizeF sz = G.MeasureString(label, Font);
			if (sz.Width > mw) {
				for (int j = label.Length; j > 0; j--) {
					sz = G.MeasureString(label.Substring(0, j) + "...", Font);
					if (sz.Width <= mw) {
						label = label.Substring(0, j) + "...";
						break;
					}
				}
			}
			PointF txtp = new PointF(2 /*mw / 2 - sz.Width / 2*/, ClientSize.Height - 9 - sz.Height / 2);
			G.DrawString(label, Font, Brushes.Black, txtp.X + 1, txtp.Y + 1);
			G.DrawString(label, Font, Brushes.White, txtp.X, txtp.Y);

			G.DrawPath(P2, GP1);
			G.DrawPath(P1, GP2);
		}

		protected override void OnDragEnter(DragEventArgs drgevent) {
			DragDropEffects de = DragDropEffects.None;
			if (drgevent.Data.GetData(typeof(Project.DraggingEntry)) != null) {
				Project.Entry e = (drgevent.Data.GetData(typeof(Project.DraggingEntry)) as Project.DraggingEntry).File;
				string ex = System.IO.Path.GetExtension(e.Name);
				foreach (string ds in types) {
					if (ds.ToLower() == ex) {
						de = DragDropEffects.Link;
						break;
					}
				}
			}
			drgevent.Effect = de;
		}

		protected override void OnDragDrop(DragEventArgs drgevent) {
			if (drgevent.Data.GetData(typeof(Project.DraggingEntry)) != null) {
				Project.Entry e = (drgevent.Data.GetData(typeof(Project.DraggingEntry)) as Project.DraggingEntry).File;
				string ex = System.IO.Path.GetExtension(e.Name);
				foreach (string ds in types) {
					if (ds.ToLower() == ex) {
						file = e;
						Invalidate();
						if (FileChanged!=null) {
							FileChanged(this as object, EventArgs.Empty);
						}
						break;
					}
				}
			}
		}

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			Preview.PreviewsReady -= Preview_PreviewsReady;
		}
	}

	#region   Custom Scroller with Close Button

	internal class NSTabScroller : Control {

		#region   Windows Form Designer generated code

		public NSTabScroller()
			: base() {
			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

		}


		//TabScroller overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}


		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;


		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal NSIconicButton LeftScroller;
		internal NSIconicButton RightScroller;
		internal NSIconicButton ListButton;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent() {
			this.LeftScroller = new NSIconicButton();
			this.RightScroller = new NSIconicButton();
			this.ListButton = new NSIconicButton();
			this.SuspendLayout();
			//
			//LeftScroller
			//
			this.LeftScroller.Dock = System.Windows.Forms.DockStyle.Right;
			this.LeftScroller.Location = new System.Drawing.Point(0, 0);
			this.LeftScroller.Name = "LeftScroller";
			this.LeftScroller.Size = new System.Drawing.Size(22, 22);
			this.LeftScroller.IconImage = SpriteBoy.ControlImages.left;
			this.LeftScroller.IconSize = new Size(8, 8);
			this.LeftScroller.Corners.TopRight = false;
			this.LeftScroller.Corners.BottomRight = false;
			this.LeftScroller.TabIndex = 0;
			this.LeftScroller.Click += new EventHandler(LeftScroller_Click);
			//
			//RightScroller
			//
			this.RightScroller.Dock = System.Windows.Forms.DockStyle.Right;
			this.RightScroller.Location = new System.Drawing.Point(21, 0);
			this.RightScroller.Name = "RightScroller";
			this.RightScroller.Size = new System.Drawing.Size(22, 22);
			this.RightScroller.IconImage = SpriteBoy.ControlImages.right;
			this.RightScroller.IconSize = new Size(8, 8);
			this.RightScroller.Corners.TopLeft = false;
			this.RightScroller.Corners.BottomLeft = false;
			this.RightScroller.TabIndex = 1;
			this.RightScroller.Click += new EventHandler(RightScroller_Click);
			//
			//CloseButton
			//
			this.ListButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.ListButton.Location = new System.Drawing.Point(44, 0);
			this.ListButton.Name = "ListButton";
			this.ListButton.Size = new System.Drawing.Size(22, 22);
			this.ListButton.IconImage = SpriteBoy.ControlImages.list;
			this.ListButton.IconSize = new Size(10, 10);
			this.ListButton.TabIndex = 2;
			this.ListButton.Click += new EventHandler(CloseButton_Click);
			//
			//TabScroller
			//
			this.Controls.Add(this.LeftScroller);
			this.Controls.Add(this.RightScroller);
			this.Controls.Add(this.ListButton);
			this.Name = "TabScroller";
			this.Size = new System.Drawing.Size(66, 22);
			this.BackColor = Color.FromArgb(40, 40, 40);
			this.Resize += new EventHandler(TabScroller_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		public event EventHandler TabList;
		public event EventHandler ScrollLeft;
		public event EventHandler ScrollRight;

		private void TabScroller_Resize(Object sender, System.EventArgs e) {
			//LeftScroller.Width = this.Width / 3;
			//RightScroller.Width = this.Width / 3;
			//ListButton.Width = this.Width / 3;
		}


		private void LeftScroller_Click(Object sender, System.EventArgs e) {
			if (ScrollLeft != null)
				ScrollLeft(this, EventArgs.Empty);
		}


		private void RightScroller_Click(Object sender, System.EventArgs e) {
			if (ScrollRight != null)
				ScrollRight(this, EventArgs.Empty);
		}


		private void CloseButton_Click(Object sender, System.EventArgs e) {
			if (TabList != null)
				TabList(this, EventArgs.Empty);
		}


	}

	#endregion

	#region  UpDown Control Subclasser

	internal class NSNativeUpDown : System.Windows.Forms.NativeWindow {

		public NSNativeUpDown() : base() { }

		private const int WM_DESTROY = 0x2;
		private const int WM_NCDESTROY = 0x82;
		private const int WM_WINDOWPOSCHANGING = 0x46;

		[StructLayout(LayoutKind.Sequential)]
		private struct WINDOWPOS {
			public IntPtr hwnd, hwndInsertAfter;
			public int x, y, cx, cy, flags;
		}

		[PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
		protected override void WndProc(ref System.Windows.Forms.Message m) {
			if (m.Msg == WM_DESTROY || m.Msg == WM_NCDESTROY)
				this.ReleaseHandle();
			else if (m.Msg == WM_WINDOWPOSCHANGING) {
				//Move the updown control off the edge so it's not visible
				WINDOWPOS wp = (WINDOWPOS)(m.GetLParam(typeof(WINDOWPOS)));
				wp.x += wp.cx;
				Marshal.StructureToPtr(wp, m.LParam, true);
				_bounds = new Rectangle(wp.x, wp.y, wp.cx, wp.cy);
			}
			base.WndProc(ref m);
		}


		private Rectangle _bounds;
		internal Rectangle Bounds {
			get { return _bounds; }
		}

	}

	#endregion

	public class NSDirectoryInspector : Control {

		public event SelectionChangedEventHandler SelectionChanged;
		public delegate void SelectionChangedEventHandler(object sender);

		public NSDirectoryInspector() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, true);

			P1 = new Pen(Color.FromArgb(55, 55, 55));
			P2 = new Pen(Color.FromArgb(35, 35, 35));
			P3 = new Pen(Color.FromArgb(45, 45, 45), 2);
			P4 = new Pen(Color.FromArgb(100, 100, 100), 2);

			B1 = new SolidBrush(Color.FromArgb(50, 50, 50));
			B2 = new SolidBrush(Color.FromArgb(40, 40, 40));
			B3 = new SolidBrush(NSTheme.UI_ACCENT);
			B4 = new SolidBrush(Color.FromArgb(65, 65, 65));
			B5 = new SolidBrush(Color.FromArgb((int)((float)NSTheme.UI_ACCENT.R * 0.85f), (int)((float)NSTheme.UI_ACCENT.G * 0.85f), (int)((float)NSTheme.UI_ACCENT.B * 0.85f)));

			SF1 = new StringFormat();
			SF1.LineAlignment = StringAlignment.Center;

			Font = new System.Drawing.Font("Tahoma", 8);
			empltyMessage = ControlStrings.InspectorEmptyMessage;

			Entries = new ObservableCollection<Entry>();
			Entries.CollectionChanged += Entries_CollectionChanged;

			scroller = new NSVScrollBar();
			scroller.Dock = DockStyle.Right;
			scroller.Width = 18;
			scroller.Minimum = 0;
			scroller.Maximum = 1;
			scroller.Value = 0;
			scroller.Scroll += scroller_Scroll;
			Controls.Add(scroller);
		}

		private NSVScrollBar scroller;

		private int offset;
		public int Offset {
			get { return offset; }
			set { offset = value; Invalidate(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ObservableCollection<Entry> Entries { get; private set; }

		[DefaultValue(false)]
		public bool AllowDragging {
			get;
			set;
		}

		private Entry selectedEntry;
		public Entry SelectedEntry {
			get { return selectedEntry; }
			set { 
				selectedEntry = value;
				if (selectedEntry!=null) {
					ScrollIntoView();
				}
				if (SelectionChanged!=null) {
					SelectionChanged(this);
				}
				Invalidate(); 
			}
		}

		private Entry hoverEntry;
		private bool mouseInside;
		private bool mouseDown;
		private Entry clickedEntry;
		private bool dragStarted;
		string empltyMessage;
		bool heightOverflow;

		private Pen P1;
		private Pen P2;
		private Pen P3;
		private Pen P4;
		private SolidBrush B1;
		private SolidBrush B2;
		private SolidBrush B3;
		private SolidBrush B4;
		private SolidBrush B5;


		private StringFormat SF1;

		private int ItemWidth;
		private int ItemHeight;
		private int ItemsStride;
		private int AreaWidth;
		private int AreaHeight;

		private int ItemSize = 82;
		private int IconSize = 64;

		protected override void OnPaint(PaintEventArgs e) {
			Graphics G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			G.Clear(Color.FromArgb(40, 40, 40));
			G.SmoothingMode = SmoothingMode.AntiAlias;

			if (Entries.Count > 0) {
				float delta = (float)ItemWidth / (float)ItemSize;
				int skip = (int)Math.Floor((float)offset / (float)ItemHeight) * ItemsStride;
				if (ItemHeight == 0) {
					ItemHeight = 1;
				}
				int dx = 0, dy = 0;
				int off = offset % ItemHeight;

				for (int i = skip; i < Entries.Count; i++) {
					Entry en = Entries[i];
					Rectangle rect = new Rectangle(dx * ItemWidth + 3, dy * ItemHeight + 1 - off, ItemWidth, ItemHeight);
					Rectangle iconRect = new Rectangle((int)((float)rect.X + (float)rect.Width / 2f - (float)IconSize * delta / 2f), (int)((float)rect.Y + (float)rect.Width / 2f - (float)IconSize * delta / 2f), (int)((float)IconSize * delta), (int)((float)IconSize * delta));

					GraphicsPath fullPath = ThemeModule.CreateRound(rect, 7);
					GraphicsPath iconPath = ThemeModule.CreateRound(rect.X + 3, rect.Y + 3, rect.Width - 6, rect.Width - 6, 7);

					// Обводка
					if (en == selectedEntry) {
						G.FillPath(B3, fullPath);
						G.FillPath(B5, iconPath);
					} else if (en == hoverEntry) {
						G.FillPath(B1, fullPath);
						G.FillPath(B2, iconPath);
					}

					// Иконка
					if (en.IsDirectory) {
						Preview.FolderIcon.SmallIcon.Draw(G, iconRect);
					} else {
						if (en.Icon != null) {
							en.Icon.SmallIcon.Draw(G, iconRect);
							if (en.Icon.Ready && en.Icon.ProxyBullet && en.Icon.Proxy != null) {
								Rectangle bulletBox = new Rectangle(
									iconRect.Right - 23,
									iconRect.Bottom - 23,
									24, 24
								);
								Rectangle bulletRect = new Rectangle(
									bulletBox.X + 4,
									bulletBox.Y + 4,
									bulletBox.Width - 8,
									bulletBox.Height - 8
								);

								GraphicsPath bgp1 = ThemeModule.CreateRoundIncomplete(bulletBox, 3, new ThemeModule.Corners() {
									BottomLeft = true,
									BottomRight = true,
									TopLeft = true,
									TopRight = true
								});
								G.FillPath(new SolidBrush(Color.FromArgb(128, 0, 0, 0)), bgp1);

								en.Icon.Proxy.BulletIcon.Draw(G, bulletRect, 2f);
							}
						}
					}

					// Подпись
					SizeF sz = G.MeasureString(en.Name, Font);
					string txt = en.Name;
					if (sz.Width > rect.Width - 6) {
						for (int j = txt.Length; j > 0; j--) {
							sz = G.MeasureString(en.Name.Substring(0, j) + "...", Font);
							if (sz.Width < rect.Width - 6) {
								txt = en.Name.Substring(0, j) + "...";
								break;
							}
						}
					}
					PointF txtp = new PointF(rect.X + rect.Width / 2 - sz.Width / 2, rect.Y + rect.Height - (rect.Height - rect.Width) / 2 - sz.Height / 2 - 2);
					G.DrawString(txt, Font, Brushes.Black, txtp.X + 1, txtp.Y + 1);
					G.DrawString(txt, Font, Brushes.White, txtp.X, txtp.Y);

					dx++;
					if (dx >= ItemsStride) {
						dx = 0;
						dy++;
						if ((dy - 1) * ItemSize - off > Height - 2) {
							break;
						}
					}
				}

			} else {

				// Строка что файлов нет
				string txt = empltyMessage;
				SizeF sz = G.MeasureString(txt, Font);
				PointF txtp = new PointF(Width / 2 - sz.Width / 2, 20);
				G.DrawString(txt, Font, Brushes.Black, txtp.X + 1, txtp.Y + 1);
				G.DrawString(txt, Font, Brushes.DarkGray, txtp.X, txtp.Y);

			}


			G.FillRectangle(B1, 0, 0, 2, AreaHeight);
			G.DrawRectangle(P2, 3, 0, AreaWidth - 4, AreaHeight - 1);
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);

			if (ItemHeight == 0) {
				ItemHeight = 1;
			}
			int scrolledItem = (int)Math.Floor((float)offset / (float)ItemHeight) * ItemsStride;
			int scrolledDepth = offset % ItemHeight;

			CalculateItemSize();
			if (mouseInside) {
				Point cur = PointToClient(MousePosition);
				FindHoveredItem(cur.X, cur.Y, false);
			}

			float height = (int)Math.Ceiling((float)Entries.Count / (float)ItemsStride) * ItemHeight;
			if (height > Height - 4) {
				offset = (int)Math.Floor((float)scrolledItem / (float)ItemsStride) * ItemHeight + scrolledDepth;
				scroller.Scroll -= scroller_Scroll;
				scroller.Maximum = (int)height;
				scroller.Value = (int)Math.Ceiling((float)scroller.Maximum * ((offset) / (float)(height - Height + 4)));
				scroller.Scroll += scroller_Scroll;
			} else {
				scroller.Maximum = 1;
			}
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e) {
			base.OnMouseEnter(e);
			Point cur = PointToClient(MousePosition);
			FindHoveredItem(cur.X, cur.Y, true);
			mouseInside = true;
			//Focus();
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			FindHoveredItem(e.X, e.Y, true);
			if (mouseDown && AllowDragging) {
				if (clickedEntry != hoverEntry && !dragStarted) {
					dragStarted = true;
					Project.DraggingEntry de = new Project.DraggingEntry(){
						File = clickedEntry.Tag as Project.Entry
					};
					DoDragDrop((object)de, DragDropEffects.Link);
				}
			}
		}

		protected override void OnMouseLeave(EventArgs e) {
			base.OnMouseLeave(e);
			hoverEntry = null;
			Invalidate();
			mouseInside = false;
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			Focus();
			selectedEntry = hoverEntry;
			if (e.Button == System.Windows.Forms.MouseButtons.Left && selectedEntry!=null) {
				if (!selectedEntry.IsDirectory) {
					mouseDown = true;
					dragStarted = false;
					clickedEntry = selectedEntry;
				}
			}
			Invalidate();
			if (SelectionChanged!=null) {
				SelectionChanged(this);
			}
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				mouseDown = false;
				clickedEntry = null;
			}
			base.OnMouseUp(e);
		}

		protected override void OnMouseWheel(MouseEventArgs e) {
			float height = (int)Math.Ceiling((float)Entries.Count / (float)ItemsStride) * ItemHeight;
			if (height > Height - 4) {
				offset -= e.Delta / 4;
				if (offset < 0) {
					offset = 0;
				} else if (offset > height - Height + 4) {
					offset = (int)(height - Height + 4);
				}
				FindHoveredItem(e.X, e.Y, false);
				scroller.Scroll -= scroller_Scroll;
				scroller.Value = (int)Math.Ceiling((float)scroller.Maximum * ((offset) / (float)(height - Height + 4)));
				Invalidate();
				scroller.Scroll += scroller_Scroll;
			}
			base.OnMouseWheel(e);
		}

		void CalculateItemSize() {
			AreaHeight = Height;
			AreaWidth = Width - scroller.Width;

			ItemsStride = 1;
			for (int i = 64; i > 0; i--) {
				if (ItemSize * i < AreaWidth - 6) {
					ItemsStride = i + 1;
					break;
				}
			}
			ItemWidth = (AreaWidth - 6) / ItemsStride;
			ItemHeight = (int)Math.Ceiling(ItemWidth * 1.2f);
		}

		void FindHoveredItem(int x, int y, bool redraw = false) {
			int dx = 0, dy = 0;
			Entry hover = hoverEntry;
			hoverEntry = null;

			foreach (Entry e in Entries) {
				Rectangle r = new Rectangle(dx * ItemWidth + 3, dy * ItemHeight + 1 - offset, ItemWidth, ItemHeight);
				if (r.Contains(x, y)) {
					hoverEntry = e;
					break;
				}
				dx++;
				if (dx >= ItemsStride) {
					dx = 0;
					dy++;
				}
			}

			if (hover != hoverEntry && redraw) {
				Invalidate();
			}
		}

		void CheckScroll() {
			float height = (int)Math.Ceiling((float)Entries.Count / (float)ItemsStride) * ItemHeight;
			if (heightOverflow != (height > Height)) {
				heightOverflow = (height > Height);
				if (heightOverflow) {
					scroller.Maximum = (int)height;
					scroller.Value = 0;
				} else {
					scroller.Maximum = 1;
				}
			}
		}

		void ScrollIntoView() {
			if (ItemsStride == 0) {
				return;
			}
			int idx = Entries.IndexOf(selectedEntry);
			int row = (int)Math.Floor((float)idx / (float)ItemsStride);
			int y = row * ItemHeight;
			if (y<offset) {
				offset = y;
			}else if(y > offset+Height-ItemHeight){
				offset = y - Height + ItemHeight+4;
			}
		}

		void scroller_Scroll(object sender) {
			float height = (int)Math.Ceiling((float)Entries.Count / (float)ItemsStride) * ItemHeight;
			offset = (int)((height - Height + 4) * ((float)scroller.Value / (float)scroller.Maximum));
			Invalidate();
		}

		void Entries_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
			if (!Entries.Contains(selectedEntry) && selectedEntry != null) {
				selectedEntry = null;
				if (SelectionChanged != null) {
					SelectionChanged(this);
				}
			}
			CheckScroll();
			Invalidate();
		}


		public class Entry {
			public string Name;
			public bool IsDirectory;
			public Preview Icon;
			public object Tag;
		}

	}

	public class NSFileInfo : Control {

		/// <summary>
		/// Ссылка на файл
		/// </summary>
		public NSDirectoryInspector.Entry File {
			get {
				return file;
			}
			set {
				file = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Скрытое поле файла
		/// </summary>
		NSDirectoryInspector.Entry file;

		/// <summary>
		/// Конструктор
		/// </summary>
		public NSFileInfo() {

			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			DoubleBuffered = true;
		}

		/// <summary>
		/// Отрисовка
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e) {
			Graphics G = e.Graphics;
			G.Clear(Color.FromArgb(50, 50, 50));
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			G.SmoothingMode = SmoothingMode.AntiAlias;

			if (file!=null) {
				int frameSize = Height - 6;
				if (frameSize > Width / 3) {
					frameSize = Width / 3;
				}

				GraphicsPath GP1 = ThemeModule.CreateRound(new Rectangle(3, 3, frameSize, frameSize), 7);
				GraphicsPath GP2 = ThemeModule.CreateRound(new Rectangle(4, 4, frameSize - 2, frameSize - 2), 7);
				Pen P1 = new Pen(Color.FromArgb(35, 35, 35));
				Pen P2 = new Pen(Color.FromArgb(55, 55, 55));

				PathGradientBrush PB1 = new PathGradientBrush(GP1);
				PB1.CenterColor = Color.FromArgb(50, 50, 50);
				PB1.SurroundColors = new Color[] { Color.FromArgb(45, 45, 45) };
				PB1.FocusScales = new PointF(0.7f, 0.7f);


				G.FillPath(PB1, GP1);
				G.DrawPath(P2, GP1);
				G.DrawPath(P1, GP2);

				Rectangle rc = new Rectangle(3, 3, frameSize, frameSize);
				rc.X += 8;
				rc.Y += 8;
				rc.Width -= 16;
				rc.Height -= 16;
				if (file != null) {
					if (file.IsDirectory) {
						Preview.FolderIcon.LargeIcon.Draw(G, rc);
					} else {
						file.Icon.LargeIcon.Draw(G, rc);
					}
				}

				string[] lines = new string[3];
				lines[0] = file.Name;
				if (file.IsDirectory) {
					lines[1] = ControlStrings.FileInfoFolder;
					lines[2] = ControlStrings.FileInfoFolderFiles.Replace("%COUNT%", (file.Tag as Project.Dir).Entries.Length.ToString());
				} else {
					Project.Entry en = (file.Tag as Project.Entry);
					string ext = System.IO.Path.GetExtension(en.Name).ToLower();
					lines[1] = ControlStrings.FileInfoUnknown.Replace("%EXT%", ext.ToUpper());
					if (Editor.Extensions.ContainsKey(ext)) {
						lines[1] = Editor.Extensions[ext].Description;
					}
					lines[2] = CalculateFileSize(en.FullPath);


				}



				int textWidth = Width - frameSize - 10;
				int textX = Width - textWidth;
				int textY = 8;

				for (int i = 0; i < lines.Length; i++) {
					SizeF sz = G.MeasureString(lines[i], Font, textWidth);
					G.DrawString(lines[i], Font, Brushes.Black, new RectangleF(textX + 1, textY + 1, sz.Width, sz.Height));
					G.DrawString(lines[i], Font, i == 0 ? Brushes.White : Brushes.LightGray, new RectangleF(textX, textY, sz.Width, sz.Height));
					textY += (int)sz.Height + 3;
				}
			}
		}

		/// <summary>
		/// Удаление объекта
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			Preview.PreviewsReady -= Preview_PreviewsReady;
		}

		/// <summary>
		/// Событие загрузки превью
		/// </summary>
		/// <param name="e"></param>
		void Preview_PreviewsReady(Events.Data.PreviewReadyEventArgs e) {
			if (file != null) {
				foreach (Preview p in e.ReadyPreviews) {
					if (p == file.Icon) {
						Invalidate();
						break;
					}
				}
			}
		}

		/// <summary>
		/// Строка с размером файла
		/// </summary>
		/// <returns>Размер файла в виде строки</returns>
		string CalculateFileSize(string path) {
			string[] sizes = { "b", "kb", "mb", "gb", "pb" };
			double len = new System.IO.FileInfo(path).Length;
			int order = 0;
			while (len >= 1024 && ++order < sizes.Length) {
				len = len / 1024;
			}
			return String.Format("{0:0.##} {1}", len, sizes[order]);
		}
	}

	public class NSMenuStrip : MenuStrip {

		public NSMenuStrip() {
			Renderer = new NSToolStripRenderer(new NSColorTable());
			ForeColor = Color.White;
			BackColor = Color.FromArgb(50, 50, 50);
		}

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.DrawLine(new Pen(Color.FromArgb(40, 40, 40), 1f), 0, Height - 1, Width, Height - 1);
			base.OnPaint(e);
		}

	}

	[DefaultEvent("CheckedChanged")]
	public class NSOnOffBox : Control {

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);

		public NSOnOffBox() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			P1 = new Pen(Color.FromArgb(55, 55, 55));
			P2 = new Pen(Color.FromArgb(35, 35, 35));
			P3 = new Pen(Color.FromArgb(65, 65, 65));

			B1 = new SolidBrush(Color.FromArgb(35, 35, 35));
			B2 = new SolidBrush(Color.FromArgb(85, 85, 85));
			B3 = new SolidBrush(Color.FromArgb(65, 65, 65));
			B4 = new SolidBrush(NSTheme.UI_ACCENT);
			B5 = new SolidBrush(Color.FromArgb(40, 40, 40));

			SF1 = new StringFormat();
			SF1.LineAlignment = StringAlignment.Center;
			SF1.Alignment = StringAlignment.Near;

			SF2 = new StringFormat();
			SF2.LineAlignment = StringAlignment.Center;
			SF2.Alignment = StringAlignment.Far;

			Size = new Size(56, 24);
			MinimumSize = Size;
			MaximumSize = Size;
		}

		private bool _Checked;
		public bool Checked {
			get { return _Checked; }
			set {
				_Checked = value;
				if (CheckedChanged != null) {
					CheckedChanged(this);
				}

				Invalidate();
			}
		}

		private GraphicsPath GP1;
		private GraphicsPath GP2;
		private GraphicsPath GP3;

		private GraphicsPath GP4;
		private Pen P1;
		private Pen P2;
		private Pen P3;
		private SolidBrush B1;
		private SolidBrush B2;
		private SolidBrush B3;
		private SolidBrush B4;

		private SolidBrush B5;
		private PathGradientBrush PB1;

		private LinearGradientBrush GB1;
		private Rectangle R1;
		private Rectangle R2;
		private Rectangle R3;
		private StringFormat SF1;

		private StringFormat SF2;

		private int Offset;
		private Graphics G;

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
			GP2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

			PB1 = new PathGradientBrush(GP1);
			PB1.CenterColor = Color.FromArgb(50, 50, 50);
			PB1.SurroundColors = new Color[] { Color.FromArgb(45, 45, 45) };
			PB1.FocusScales = new PointF(0.3f, 0.3f);

			G.FillPath(PB1, GP1);
			G.DrawPath(P1, GP1);
			G.DrawPath(P2, GP2);

			R1 = new Rectangle(5, 0, Width - 10, Height + 2);
			R2 = new Rectangle(6, 1, Width - 10, Height + 2);

			R3 = new Rectangle(1, 1, (Width / 2) - 1, Height - 3);

			if (_Checked) {
				G.DrawString("On", Font, Brushes.Black, R2, SF1);
				G.DrawString("On", Font, Brushes.White, R1, SF1);

				R3.X += (Width / 2) - 1;
			} else {
				G.DrawString("Off", Font, B1, R2, SF2);
				G.DrawString("Off", Font, B2, R1, SF2);
			}

			GP3 = ThemeModule.CreateRound(R3, 7);
			GP4 = ThemeModule.CreateRound(R3.X + 1, R3.Y + 1, R3.Width - 2, R3.Height - 2, 7);

			GB1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);

			G.FillPath(GB1, GP3);
			G.DrawPath(P2, GP3);
			G.DrawPath(P3, GP4);

			Offset = R3.X + (R3.Width / 2) - 3;

			for (int I = 0; I <= 1; I++) {
				if (_Checked) {
					G.FillRectangle(B1, Offset + (I * 5), 7, 2, Height - 14);
				} else {
					G.FillRectangle(B3, Offset + (I * 5), 7, 2, Height - 14);
				}

				G.SmoothingMode = SmoothingMode.None;

				if (_Checked) {
					G.FillRectangle(B4, Offset + (I * 5), 7, 2, Height - 14);
				} else {
					G.FillRectangle(B5, Offset + (I * 5), 7, 2, Height - 14);
				}

				G.SmoothingMode = SmoothingMode.AntiAlias;
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			Checked = !Checked;
			base.OnMouseDown(e);
		}

	}

	public class NSControlButton : Control {

		public enum Button : byte {
			None = 0,
			Minimize = 1,
			MaximizeRestore = 2,
			Close = 3
		}

		private Button _ControlButton = Button.Close;
		public Button ControlButton {
			get { return _ControlButton; }
			set {
				_ControlButton = value;
				Invalidate();
			}
		}

		public NSControlButton() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			Anchor = AnchorStyles.Top | AnchorStyles.Right;

			Width = 18;
			Height = 20;

			MinimumSize = Size;
			MaximumSize = Size;

			Margin = new Padding(0);
		}

		private Graphics G;
		protected override void OnPaint(PaintEventArgs e) {
			G = e.Graphics;
			G.Clear(BackColor);

			switch (_ControlButton) {
				case Button.Minimize:
					DrawMinimize(3, 10);
					break;
				case Button.MaximizeRestore:
					if (FindForm().WindowState == FormWindowState.Normal) {
						DrawMaximize(3, 5);
					} else {
						DrawRestore(3, 4);
					}
					break;
				case Button.Close:
					DrawClose(4, 5);
					break;
			}
		}

		private void DrawMinimize(int x, int y) {
			G.FillRectangle(Brushes.White, x, y, 12, 5);
			G.DrawRectangle(Pens.Black, x, y, 11, 4);
		}

		private void DrawMaximize(int x, int y) {
			G.DrawRectangle(new Pen(Color.White, 2), x + 2, y + 2, 8, 6);
			G.DrawRectangle(Pens.Black, x, y, 11, 9);
			G.DrawRectangle(Pens.Black, x + 3, y + 3, 5, 3);
		}

		private void DrawRestore(int x, int y) {
			G.FillRectangle(Brushes.White, x + 3, y + 1, 8, 4);
			G.FillRectangle(Brushes.White, x + 7, y + 5, 4, 4);
			G.DrawRectangle(Pens.Black, x + 2, y + 0, 9, 9);

			G.FillRectangle(Brushes.White, x + 1, y + 3, 2, 6);
			G.FillRectangle(Brushes.White, x + 1, y + 9, 8, 2);
			G.DrawRectangle(Pens.Black, x, y + 2, 9, 9);
			G.DrawRectangle(Pens.Black, x + 3, y + 5, 3, 3);
		}

		private GraphicsPath ClosePath;
		private void DrawClose(int x, int y) {
			if (ClosePath == null) {
				ClosePath = new GraphicsPath();
				ClosePath.AddLine(x + 1, y, x + 3, y);
				ClosePath.AddLine(x + 5, y + 2, x + 7, y);
				ClosePath.AddLine(x + 9, y, x + 10, y + 1);
				ClosePath.AddLine(x + 7, y + 4, x + 7, y + 5);
				ClosePath.AddLine(x + 10, y + 8, x + 9, y + 9);
				ClosePath.AddLine(x + 7, y + 9, x + 5, y + 7);
				ClosePath.AddLine(x + 3, y + 9, x + 1, y + 9);
				ClosePath.AddLine(x + 0, y + 8, x + 3, y + 5);
				ClosePath.AddLine(x + 3, y + 4, x + 0, y + 1);
			}

			G.FillPath(Brushes.White, ClosePath);
			G.DrawPath(Pens.Black, ClosePath);
		}

		protected override void OnMouseClick(MouseEventArgs e) {

			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				Form F = FindForm();

				switch (_ControlButton) {
					case Button.Minimize:
						F.WindowState = FormWindowState.Minimized;
						break;
					case Button.MaximizeRestore:
						if (F.WindowState == FormWindowState.Normal) {
							F.WindowState = FormWindowState.Maximized;
						} else {
							F.WindowState = FormWindowState.Normal;
						}
						break;
					case Button.Close:
						F.Close();
						break;
				}

			}

			Invalidate();
			base.OnMouseClick(e);
		}

	}

	public class NSGroupBox : ContainerControl {

		private bool _DrawSeperator;
		public bool DrawSeperator {
			get { return _DrawSeperator; }
			set {
				_DrawSeperator = value;
				Invalidate();
			}
		}

		private string _Title = "GroupBox";
		public string Title {
			get { return _Title; }
			set {
				_Title = value;
				Invalidate();
			}
		}

		private string _SubTitle = "Details";
		public string SubTitle {
			get { return _SubTitle; }
			set {
				_SubTitle = value;
				Invalidate();
			}
		}

		private Font _TitleFont;

		private Font _SubTitleFont;
		public NSGroupBox() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			_TitleFont = new Font("Verdana", 10f);
			_SubTitleFont = new Font("Verdana", 6.5f);

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(55, 55, 55));

			B1 = new SolidBrush(NSTheme.UI_ACCENT);
		}

		private GraphicsPath GP1;

		private GraphicsPath GP2;
		private PointF PT1;
		private SizeF SZ1;

		private SizeF SZ2;
		private Pen P1;
		private Pen P2;

		private SolidBrush B1;
		private Graphics G;

		protected override void OnPaint(PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
			GP2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

			G.DrawPath(P1, GP1);
			G.DrawPath(P2, GP2);

			SZ1 = G.MeasureString(_Title, _TitleFont, Width, StringFormat.GenericTypographic);
			SZ2 = G.MeasureString(_SubTitle, _SubTitleFont, Width, StringFormat.GenericTypographic);

			G.DrawString(_Title, _TitleFont, Brushes.Black, 6, 6);
			G.DrawString(_Title, _TitleFont, B1, 5, 5);

			PT1 = new PointF(6f, SZ1.Height + 4f);

			G.DrawString(_SubTitle, _SubTitleFont, Brushes.Black, PT1.X + 1, PT1.Y + 1);
			G.DrawString(_SubTitle, _SubTitleFont, Brushes.White, PT1.X, PT1.Y);

			if (_DrawSeperator) {
				int Y = Convert.ToInt32(PT1.Y + SZ2.Height) + 8;

				G.DrawLine(P1, 4, Y, Width - 5, Y);
				G.DrawLine(P2, 4, Y + 1, Width - 5, Y + 1);
			}
		}

	}

	public class NSSeperator : Control {

		public NSSeperator() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			Height = 10;

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(55, 55, 55));
		}

		private Pen P1;

		private Pen P2;
		private Graphics G;

		protected override void OnPaint(PaintEventArgs e) {
			G = e.Graphics;
			G.Clear(BackColor);

			G.DrawLine(P1, 0, 5, Width, 5);
			G.DrawLine(P2, 0, 6, Width, 6);
		}

	}

	[DefaultEvent("Scroll")]
	public class NSTrackBar : Control {

		public event ScrollEventHandler Scroll;
		public delegate void ScrollEventHandler(object sender);

		private int _Minimum;
		public int Minimum {
			get { return _Minimum; }
			set {
				if (value < 0) {
					throw new Exception("Property value is not valid.");
				}

				_Minimum = value;
				if (value > _Value)
					_Value = value;
				if (value > _Maximum)
					_Maximum = value;
				Invalidate();
			}
		}

		private int _Maximum = 10;
		public int Maximum {
			get { return _Maximum; }
			set {
				if (value < 0) {
					throw new Exception("Property value is not valid.");
				}

				_Maximum = value;
				if (value < _Value)
					_Value = value;
				if (value < _Minimum)
					_Minimum = value;
				Invalidate();
			}
		}

		private int _Value;
		public int Value {
			get { return _Value; }
			set {
				if (value == _Value)
					return;

				if (value > _Maximum || value < _Minimum) {
					throw new Exception("Property value is not valid.");
				}

				_Value = value;
				Invalidate();

				if (Scroll != null) {
					Scroll(this);
				}
			}
		}

		public NSTrackBar() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			Height = 17;

			P1 = new Pen(Color.FromArgb(150, 110, 0), 2);
			P2 = new Pen(Color.FromArgb(55, 55, 55));
			P3 = new Pen(Color.FromArgb(35, 35, 35));
			P4 = new Pen(Color.FromArgb(65, 65, 65));
		}

		private GraphicsPath GP1;
		private GraphicsPath GP2;
		private GraphicsPath GP3;

		private GraphicsPath GP4;
		private Rectangle R1;
		private Rectangle R2;
		private Rectangle R3;

		private int I1;
		private Pen P1;
		private Pen P2;
		private Pen P3;

		private Pen P4;
		private LinearGradientBrush GB1;
		private LinearGradientBrush GB2;

		private LinearGradientBrush GB3;
		private Graphics G;

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRound(0, 5, Width - 1, 10, 5);
			GP2 = ThemeModule.CreateRound(1, 6, Width - 3, 8, 5);

			R1 = new Rectangle(0, 7, Width - 1, 5);
			GB1 = new LinearGradientBrush(R1, Color.FromArgb(45, 45, 45), Color.FromArgb(50, 50, 50), 90f);

			I1 = Convert.ToInt32((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum) * (Width - 11));
			R2 = new Rectangle(I1, 0, 10, 20);

			G.SetClip(GP2);
			G.FillRectangle(GB1, R1);

			R3 = new Rectangle(1, 7, R2.X + R2.Width - 2, 8);
			GB2 = new LinearGradientBrush(R3, NSTheme.UI_ACCENT, Color.FromArgb(150, 110, 0), 90f);

			G.SmoothingMode = SmoothingMode.None;
			G.FillRectangle(GB2, R3);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			for (int I = 0; I <= R3.Width - 15; I += 5) {
				G.DrawLine(P1, I, 0, I + 15, Height);
			}

			G.ResetClip();

			G.DrawPath(P2, GP1);
			G.DrawPath(P3, GP2);

			GP3 = ThemeModule.CreateRound(R2, 5);
			GP4 = ThemeModule.CreateRound(R2.X + 1, R2.Y + 1, R2.Width - 2, R2.Height - 2, 5);
			GB3 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);

			G.FillPath(GB3, GP3);
			G.DrawPath(P3, GP3);
			G.DrawPath(P4, GP4);
		}

		private bool TrackDown;
		protected override void OnMouseDown(MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				I1 = Convert.ToInt32((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum) * (Width - 11));
				R2 = new Rectangle(I1, 0, 10, 20);

				TrackDown = R2.Contains(e.Location);
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			if (TrackDown && e.X > -1 && e.X < (Width + 1)) {
				Value = _Minimum + Convert.ToInt32((_Maximum - _Minimum) * ((double)e.X / (double)Width));
			}

			base.OnMouseMove(e);
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			TrackDown = false;
			base.OnMouseUp(e);
		}

	}

	[DefaultEvent("ValueChanged")]
	public class NSRandomPool : Control {

		public event ValueChangedEventHandler ValueChanged;
		public delegate void ValueChangedEventHandler(object sender);

		private StringBuilder _Value = new StringBuilder();
		public string Value {
			get { return _Value.ToString(); }
		}

		public string FullValue {
			get { return BitConverter.ToString(Table).Replace("-", ""); }
		}


		private Random RNG = new Random();
		private int ItemSize = 9;

		private int DrawSize = 8;

		private Rectangle WA;
		private int RowSize;

		private int ColumnSize;
		public NSRandomPool() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			P1 = new Pen(Color.FromArgb(55, 55, 55));
			P2 = new Pen(Color.FromArgb(35, 35, 35));

			B1 = new SolidBrush(Color.FromArgb(30, 30, 30));
		}

		protected override void OnHandleCreated(EventArgs e) {
			UpdateTable();
			base.OnHandleCreated(e);
		}

		private byte[] Table;
		private void UpdateTable() {
			WA = new Rectangle(5, 5, Width - 10, Height - 10);

			RowSize = WA.Width / ItemSize;
			ColumnSize = WA.Height / ItemSize;

			WA.Width = RowSize * ItemSize;
			WA.Height = ColumnSize * ItemSize;

			WA.X = (Width / 2) - (WA.Width / 2);
			WA.Y = (Height / 2) - (WA.Height / 2);

			Table = new byte[(RowSize * ColumnSize)];

			for (int I = 0; I <= Table.Length - 1; I++) {
				Table[I] = Convert.ToByte(RNG.Next(100));
			}

			Invalidate();
		}

		protected override void OnSizeChanged(EventArgs e) {
			UpdateTable();
		}

		private int Index1 = -1;

		private int Index2;

		private bool InvertColors;
		protected override void OnMouseMove(MouseEventArgs e) {
			HandleDraw(e);
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			HandleDraw(e);
			base.OnMouseDown(e);
		}

		private void HandleDraw(MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right) {
				if (!WA.Contains(e.Location))
					return;

				InvertColors = (e.Button == System.Windows.Forms.MouseButtons.Right);

				Index1 = GetIndex(e.X, e.Y);
				if (Index1 == Index2)
					return;

				bool L = !(Index1 % RowSize == 0);
				bool R = !(Index1 % RowSize == (RowSize - 1));

				Randomize(Index1 - RowSize);
				if (L)
					Randomize(Index1 - 1);
				Randomize(Index1);
				if (R)
					Randomize(Index1 + 1);
				Randomize(Index1 + RowSize);

				_Value.Append(Table[Index1].ToString("X"));
				if (_Value.Length > 32)
					_Value.Remove(0, 2);

				if (ValueChanged != null) {
					ValueChanged(this);
				}

				Index2 = Index1;
				Invalidate();
			}
		}

		private GraphicsPath GP1;

		private GraphicsPath GP2;
		private Pen P1;
		private Pen P2;
		private SolidBrush B1;

		private SolidBrush B2;

		private PathGradientBrush PB1;
		private Graphics G;

		protected override void OnPaint(PaintEventArgs e) {
			G = e.Graphics;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GP1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
			GP2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

			PB1 = new PathGradientBrush(GP1);
			PB1.CenterColor = Color.FromArgb(50, 50, 50);
			PB1.SurroundColors = new Color[] { Color.FromArgb(45, 45, 45) };
			PB1.FocusScales = new PointF(0.9f, 0.5f);

			G.FillPath(PB1, GP1);

			G.DrawPath(P1, GP1);
			G.DrawPath(P2, GP2);

			G.SmoothingMode = SmoothingMode.None;

			for (int I = 0; I <= Table.Length - 1; I++) {
				int C = Math.Max(Table[I], (byte)75);

				int X = ((I % RowSize) * ItemSize) + WA.X;
				int Y = ((I / RowSize) * ItemSize) + WA.Y;

				B2 = new SolidBrush(Color.FromArgb(C, C, C));

				G.FillRectangle(B1, X + 1, Y + 1, DrawSize, DrawSize);
				G.FillRectangle(B2, X, Y, DrawSize, DrawSize);

				B2.Dispose();
			}

		}

		private int GetIndex(int x, int y) {
			return (((y - WA.Y) / ItemSize) * RowSize) + ((x - WA.X) / ItemSize);
		}

		private void Randomize(int index) {
			if (index > -1 && index < Table.Length) {
				if (InvertColors) {
					Table[index] = Convert.ToByte(RNG.Next(100));
				} else {
					Table[index] = Convert.ToByte(RNG.Next(100, 256));
				}
			}
		}

	}

	public class NSKeyboard : Control {

		private Bitmap TextBitmap;

		private Graphics TextGraphics;
		const string LowerKeys = "1234567890-=qwertyuiop[]asdfghjkl\\;'zxcvbnm,./`";

		const string UpperKeys = "!@#$%^&*()_+QWERTYUIOP{}ASDFGHJKL|:\"ZXCVBNM<>?~";
		public NSKeyboard() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			Font = new Font("Verdana", 8.25f);

			TextBitmap = new Bitmap(1, 1);
			TextGraphics = Graphics.FromImage(TextBitmap);

			MinimumSize = new Size(386, 162);
			MaximumSize = new Size(386, 162);

			Lower = LowerKeys.ToCharArray();
			Upper = UpperKeys.ToCharArray();

			PrepareCache();

			P1 = new Pen(Color.FromArgb(45, 45, 45));
			P2 = new Pen(Color.FromArgb(65, 65, 65));
			P3 = new Pen(Color.FromArgb(35, 35, 35));

			B1 = new SolidBrush(Color.FromArgb(100, 100, 100));
		}

		private Control _Target;
		public Control Target {
			get { return _Target; }
			set { _Target = value; }
		}


		private bool Shift;
		private int Pressed = -1;

		private Rectangle[] Buttons;
		private char[] Lower;
		private char[] Upper;
		private string[] Other = {
		"Shift",
		"Space",
		"Back"

	};
		private PointF[] UpperCache;

		private PointF[] LowerCache;
		private void PrepareCache() {
			Buttons = new Rectangle[51];
			UpperCache = new PointF[Upper.Length];
			LowerCache = new PointF[Lower.Length];

			int I = 0;

			SizeF S = default(SizeF);
			Rectangle R = default(Rectangle);

			for (int Y = 0; Y <= 3; Y++) {
				for (int X = 0; X <= 11; X++) {
					I = (Y * 12) + X;
					R = new Rectangle(X * 32, Y * 32, 32, 32);

					Buttons[I] = R;

					if (!(I == 47) && !char.IsLetter(Upper[I])) {
						S = TextGraphics.MeasureString(Upper[I].ToString(), Font);
						UpperCache[I] = new PointF(R.X + (R.Width / 2 - S.Width / 2), R.Y + R.Height - S.Height - 2);

						S = TextGraphics.MeasureString(Lower[I].ToString(), Font);
						LowerCache[I] = new PointF(R.X + (R.Width / 2 - S.Width / 2), R.Y + R.Height - S.Height - 2);
					}
				}
			}

			Buttons[48] = new Rectangle(0, 4 * 32, 2 * 32, 32);
			Buttons[49] = new Rectangle(Buttons[48].Right, 4 * 32, 8 * 32, 32);
			Buttons[50] = new Rectangle(Buttons[49].Right, 4 * 32, 2 * 32, 32);
		}


		private GraphicsPath GP1;
		private SizeF SZ1;

		private PointF PT1;
		private Pen P1;
		private Pen P2;
		private Pen P3;

		private SolidBrush B1;
		private PathGradientBrush PB1;

		private LinearGradientBrush GB1;
		private Graphics G;
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);

			Rectangle R = default(Rectangle);

			int Offset = 0;
			G.DrawRectangle(P1, 0, 0, (12 * 32) + 1, (5 * 32) + 1);

			for (int I = 0; I <= Buttons.Length - 1; I++) {
				R = Buttons[I];

				Offset = 0;
				if (I == Pressed) {
					Offset = 1;

					GP1 = new GraphicsPath();
					GP1.AddRectangle(R);

					PB1 = new PathGradientBrush(GP1);
					PB1.CenterColor = Color.FromArgb(60, 60, 60);
					PB1.SurroundColors = new Color[] { Color.FromArgb(55, 55, 55) };
					PB1.FocusScales = new PointF(0.8f, 0.5f);

					G.FillPath(PB1, GP1);
				} else {
					GB1 = new LinearGradientBrush(R, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
					G.FillRectangle(GB1, R);
				}

				switch (I) {
					case 48:
					case 49:
					case 50:
						SZ1 = G.MeasureString(Other[I - 48], Font);
						G.DrawString(Other[I - 48], Font, Brushes.Black, R.X + (R.Width / 2 - SZ1.Width / 2) + Offset + 1, R.Y + (R.Height / 2 - SZ1.Height / 2) + Offset + 1);
						G.DrawString(Other[I - 48], Font, Brushes.White, R.X + (R.Width / 2 - SZ1.Width / 2) + Offset, R.Y + (R.Height / 2 - SZ1.Height / 2) + Offset);
						break;
					case 47:
						DrawArrow(Color.Black, R.X + Offset + 1, R.Y + Offset + 1);
						DrawArrow(Color.White, R.X + Offset, R.Y + Offset);
						break;
					default:
						if (Shift) {
							G.DrawString(Upper[I].ToString(), Font, Brushes.Black, R.X + 3 + Offset + 1, R.Y + 2 + Offset + 1);
							G.DrawString(Upper[I].ToString(), Font, Brushes.White, R.X + 3 + Offset, R.Y + 2 + Offset);

							if (!char.IsLetter(Lower[I])) {
								PT1 = LowerCache[I];
								G.DrawString(Lower[I].ToString(), Font, B1, PT1.X + Offset, PT1.Y + Offset);
							}
						} else {
							G.DrawString(Lower[I].ToString(), Font, Brushes.Black, R.X + 3 + Offset + 1, R.Y + 2 + Offset + 1);
							G.DrawString(Lower[I].ToString(), Font, Brushes.White, R.X + 3 + Offset, R.Y + 2 + Offset);

							if (!char.IsLetter(Upper[I])) {
								PT1 = UpperCache[I];
								G.DrawString(Upper[I].ToString(), Font, B1, PT1.X + Offset, PT1.Y + Offset);
							}
						}
						break;
				}

				G.DrawRectangle(P2, R.X + 1 + Offset, R.Y + 1 + Offset, R.Width - 2, R.Height - 2);
				G.DrawRectangle(P3, R.X + Offset, R.Y + Offset, R.Width, R.Height);

				if (I == Pressed) {
					G.DrawLine(P1, R.X, R.Y, R.Right, R.Y);
					G.DrawLine(P1, R.X, R.Y, R.X, R.Bottom);
				}
			}
		}

		private void DrawArrow(Color color, int rx, int ry) {
			Rectangle R = new Rectangle(rx + 8, ry + 8, 16, 16);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			Pen P = new Pen(color, 1);
			AdjustableArrowCap C = new AdjustableArrowCap(3, 2);
			P.CustomEndCap = C;

			G.DrawArc(P, R, 0f, 290f);

			P.Dispose();
			C.Dispose();
			G.SmoothingMode = SmoothingMode.None;
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			int Index = ((e.Y / 32) * 12) + (e.X / 32);

			if (Index > 47) {
				for (int I = 48; I <= Buttons.Length - 1; I++) {
					if (Buttons[I].Contains(e.X, e.Y)) {
						Pressed = I;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
			} else {
				Pressed = Index;
			}

			HandleKey();
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			Pressed = -1;
			Invalidate();
		}

		private void HandleKey() {
			if (_Target == null)
				return;
			if (Pressed == -1)
				return;

			switch (Pressed) {
				case 47:
					_Target.Text = string.Empty;
					break;
				case 48:
					Shift = !Shift;
					break;
				case 49:
					_Target.Text += " ";
					break;
				case 50:
					if (!(_Target.Text.Length == 0)) {
						_Target.Text = _Target.Text.Remove(_Target.Text.Length - 1);
					}
					break;
				default:
					if (Shift) {
						_Target.Text += Upper[Pressed];
					} else {
						_Target.Text += Lower[Pressed];
					}
					break;
			}
		}

	}

	[DefaultEvent("SelectedIndexChanged")]
	public class NSPaginator : Control {

		public event SelectedIndexChangedEventHandler SelectedIndexChanged;
		public delegate void SelectedIndexChangedEventHandler(object sender, EventArgs e);

		private Bitmap TextBitmap;

		private Graphics TextGraphics;
		public NSPaginator() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			Size = new Size(202, 26);

			TextBitmap = new Bitmap(1, 1);
			TextGraphics = Graphics.FromImage(TextBitmap);

			InvalidateItems();

			B1 = new SolidBrush(Color.FromArgb(50, 50, 50));
			B2 = new SolidBrush(Color.FromArgb(55, 55, 55));

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(55, 55, 55));
			P3 = new Pen(Color.FromArgb(65, 65, 65));
		}

		private int _SelectedIndex;
		public int SelectedIndex {
			get { return _SelectedIndex; }
			set {
				_SelectedIndex = Math.Max(Math.Min(value, MaximumIndex), 0);
				Invalidate();
			}
		}

		private int _NumberOfPages;
		public int NumberOfPages {
			get { return _NumberOfPages; }
			set {
				_NumberOfPages = value;
				_SelectedIndex = Math.Max(Math.Min(_SelectedIndex, MaximumIndex), 0);
				Invalidate();
			}
		}

		public int MaximumIndex {
			get { return NumberOfPages - 1; }
		}


		private int ItemWidth;
		public override Font Font {
			get { return base.Font; }
			set {
				base.Font = value;

				InvalidateItems();
				Invalidate();
			}
		}

		private void InvalidateItems() {
			Size S = TextGraphics.MeasureString("000 ..", Font).ToSize();
			ItemWidth = S.Width + 10;
		}

		private GraphicsPath GP1;

		private GraphicsPath GP2;

		private Rectangle R1;
		private Size SZ1;

		private Point PT1;
		private Pen P1;
		private Pen P2;
		private Pen P3;
		private SolidBrush B1;

		private SolidBrush B2;
		private Graphics G;
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			bool LeftEllipse = false;
			bool RightEllipse = false;

			if (_SelectedIndex < 4) {
				for (int I = 0; I <= Math.Min(MaximumIndex, 4); I++) {
					RightEllipse = (I == 4) && (MaximumIndex > 4);
					DrawBox(I * ItemWidth, I, false, RightEllipse);
				}
			} else if (_SelectedIndex > 3 && _SelectedIndex < (MaximumIndex - 3)) {
				for (int I = 0; I <= 4; I++) {
					LeftEllipse = (I == 0);
					RightEllipse = (I == 4);
					DrawBox(I * ItemWidth, _SelectedIndex + I - 2, LeftEllipse, RightEllipse);
				}
			} else {
				for (int I = 0; I <= 4; I++) {
					LeftEllipse = (I == 0) && (MaximumIndex > 4);
					DrawBox(I * ItemWidth, MaximumIndex - (4 - I), LeftEllipse, false);
				}
			}
		}

		private void DrawBox(int x, int index, bool leftEllipse, bool rightEllipse) {
			R1 = new Rectangle(x, 0, ItemWidth - 4, Height - 1);

			GP1 = ThemeModule.CreateRound(R1, 7);
			GP2 = ThemeModule.CreateRound(R1.X + 1, R1.Y + 1, R1.Width - 2, R1.Height - 2, 7);

			string T = Convert.ToString(index + 1);

			if (leftEllipse)
				T = ".. " + T;
			if (rightEllipse)
				T = T + " ..";

			SZ1 = G.MeasureString(T, Font).ToSize();
			PT1 = new Point(R1.X + (R1.Width / 2 - SZ1.Width / 2), R1.Y + (R1.Height / 2 - SZ1.Height / 2));

			if (index == _SelectedIndex) {
				G.FillPath(B1, GP1);

				Font F = new Font(Font, FontStyle.Underline);
				G.DrawString(T, F, Brushes.Black, PT1.X + 1, PT1.Y + 1);
				G.DrawString(T, F, Brushes.White, PT1);
				F.Dispose();

				G.DrawPath(P1, GP2);
				G.DrawPath(P2, GP1);
			} else {
				G.FillPath(B2, GP1);

				G.DrawString(T, Font, Brushes.Black, PT1.X + 1, PT1.Y + 1);
				G.DrawString(T, Font, Brushes.White, PT1);

				G.DrawPath(P3, GP2);
				G.DrawPath(P1, GP1);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				int NewIndex = 0;
				int OldIndex = _SelectedIndex;

				if (_SelectedIndex < 4) {
					NewIndex = (e.X / ItemWidth);
				} else if (_SelectedIndex > 3 && _SelectedIndex < (MaximumIndex - 3)) {
					NewIndex = (e.X / ItemWidth);

					if (NewIndex == 2) {
						NewIndex = OldIndex;
					} else if (NewIndex < 2) {
						NewIndex = OldIndex - (2 - NewIndex);
					} else if (NewIndex > 2) {
						NewIndex = OldIndex + (NewIndex - 2);
					}
				} else {
					NewIndex = MaximumIndex - (4 - (e.X / ItemWidth));
				}

				if ((NewIndex < _NumberOfPages) && (!(NewIndex == OldIndex))) {
					SelectedIndex = NewIndex;
					if (SelectedIndexChanged != null) {
						SelectedIndexChanged(this, null);
					}
				}
			}

			base.OnMouseDown(e);
		}

	}

	[DefaultEvent("Scroll")]
	public class NSVScrollBar : Control {

		public event ScrollEventHandler Scroll;
		public delegate void ScrollEventHandler(object sender);

		private int _Minimum;
		public int Minimum {
			get { return _Minimum; }
			set {
				if (value < 0) {
					throw new Exception("Property value is not valid.");
				}

				_Minimum = value;
				if (value > _Value)
					_Value = value;
				if (value > _Maximum)
					_Maximum = value;

				InvalidateLayout();
			}
		}

		private int _Maximum = 100;
		public int Maximum {
			get { return _Maximum; }
			set {
				if (value < 1)
					value = 1;

				_Maximum = value;
				if (value < _Value)
					_Value = value;
				if (value < _Minimum)
					_Minimum = value;

				InvalidateLayout();
			}
		}

		private int _Value;
		public int Value {
			get {
				if (!ShowThumb)
					return _Minimum;
				return _Value;
			}
			set {
				if (value == _Value)
					return;

				if (value > _Maximum || value < _Minimum) {
					throw new Exception("Property value is not valid.");
				}

				_Value = value;
				InvalidatePosition();

				if (Scroll != null) {
					Scroll(this);
				}
			}
		}

		public double _Percent { get; set; }
		public double Percent {
			get {
				if (!ShowThumb)
					return 0;
				return GetProgress();
			}
		}

		private int _SmallChange = 1;
		public int SmallChange {
			get { return _SmallChange; }
			set {
				if (value < 1) {
					throw new Exception("Property value is not valid.");
				}

				_SmallChange = value;
			}
		}

		private int _LargeChange = 10;
		public int LargeChange {
			get { return _LargeChange; }
			set {
				if (value < 1) {
					throw new Exception("Property value is not valid.");
				}

				_LargeChange = value;
			}
		}

		private int ButtonSize = 16;
		// 14 minimum
		private int ThumbSize = 24;

		private Rectangle TSA;
		private Rectangle BSA;
		private Rectangle Shaft;

		private Rectangle Thumb;
		private bool ShowThumb;

		private bool ThumbDown;
		public NSVScrollBar() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			Width = 18;

			B1 = new SolidBrush(Color.FromArgb(55, 55, 55));
			B2 = new SolidBrush(Color.FromArgb(35, 35, 35));

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(65, 65, 65));
			P3 = new Pen(Color.FromArgb(55, 55, 55));
			P4 = new Pen(Color.FromArgb(40, 40, 40));
		}

		private GraphicsPath GP1;
		private GraphicsPath GP2;
		private GraphicsPath GP3;

		private GraphicsPath GP4;
		private Pen P1;
		private Pen P2;
		private Pen P3;
		private Pen P4;
		private SolidBrush B1;

		private SolidBrush B2;

		int I1;
		private Graphics G;

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.Clear(BackColor);

			GP1 = DrawArrow(4, 6, false);
			GP2 = DrawArrow(5, 7, false);

			G.FillPath(B1, GP2);
			G.FillPath(B2, GP1);

			GP3 = DrawArrow(4, Height - 11, true);
			GP4 = DrawArrow(5, Height - 10, true);

			G.FillPath(B1, GP4);
			G.FillPath(B2, GP3);

			if (ShowThumb && Enabled) {
				G.FillRectangle(B1, Thumb);
				G.DrawRectangle(P1, Thumb);
				G.DrawRectangle(P2, Thumb.X + 1, Thumb.Y + 1, Thumb.Width - 2, Thumb.Height - 2);

				int Y = 0;
				int LY = Thumb.Y + (Thumb.Height / 2) - 3;

				for (int I = 0; I <= 2; I++) {
					Y = LY + (I * 3);

					G.DrawLine(P1, Thumb.X + 5, Y, Thumb.Right - 5, Y);
					G.DrawLine(P2, Thumb.X + 5, Y + 1, Thumb.Right - 5, Y + 1);
				}
			}

			G.DrawRectangle(P3, 0, 0, Width - 1, Height - 1);
			G.DrawRectangle(P4, 1, 1, Width - 3, Height - 3);
		}

		private GraphicsPath DrawArrow(int x, int y, bool flip) {
			GraphicsPath GP = new GraphicsPath();

			int W = 9;
			int H = 5;

			if (flip) {
				GP.AddLine(x + 1, y, x + W + 1, y);
				GP.AddLine(x + W, y, x + H, y + H - 1);
			} else {
				GP.AddLine(x, y + H, x + W, y + H);
				GP.AddLine(x + W, y + H, x + H, y);
			}

			GP.CloseFigure();
			return GP;
		}

		protected override void OnSizeChanged(EventArgs e) {
			InvalidateLayout();
		}

		private void InvalidateLayout() {
			TSA = new Rectangle(0, 0, Width, ButtonSize);
			BSA = new Rectangle(0, Height - ButtonSize, Width, ButtonSize);
			Shaft = new Rectangle(0, TSA.Bottom + 1, Width, Height - (ButtonSize * 2) - 1);

			ShowThumb = ((_Maximum - _Minimum) > Shaft.Height);

			if (ShowThumb) {
				//ThumbSize = Math.Max(0, 14) 'TODO: Implement this.
				Thumb = new Rectangle(1, 0, Width - 3, ThumbSize);
			}

			if (Scroll != null) {
				Scroll(this);
			}
			InvalidatePosition();
		}

		private void InvalidatePosition() {
			Thumb.Y = Convert.ToInt32(GetProgress() * (Shaft.Height - ThumbSize)) + TSA.Height;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left && ShowThumb && Enabled) {
				if (TSA.Contains(e.Location)) {
					I1 = _Value - _SmallChange;
				} else if (BSA.Contains(e.Location)) {
					I1 = _Value + _SmallChange;
				} else {
					if (Thumb.Contains(e.Location)) {
						ThumbDown = true;
						base.OnMouseDown(e);
						return;
					} else {
						if (e.Y < Thumb.Y) {
							I1 = _Value - _LargeChange;
						} else {
							I1 = _Value + _LargeChange;
						}
					}
				}

				Value = Math.Min(Math.Max(I1, _Minimum), _Maximum);
				InvalidatePosition();
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			if (ThumbDown && ShowThumb) {
				int ThumbPosition = e.Y - TSA.Height - (ThumbSize / 2);
				int ThumbBounds = Shaft.Height - ThumbSize;

				I1 = Convert.ToInt32(((double)ThumbPosition / (double)ThumbBounds) * (_Maximum - _Minimum)) + _Minimum;

				Value = Math.Min(Math.Max(I1, _Minimum), _Maximum);
				InvalidatePosition();
			}

			base.OnMouseMove(e);
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			ThumbDown = false;
			base.OnMouseUp(e);
		}

		private double GetProgress() {
			return (double)(_Value - _Minimum) / (double)(_Maximum - _Minimum);
		}

	}

	[DefaultEvent("Scroll")]
	public class NSHScrollBar : Control {

		public event ScrollEventHandler Scroll;
		public delegate void ScrollEventHandler(object sender);

		private int _Minimum;
		public int Minimum {
			get { return _Minimum; }
			set {
				if (value < 0) {
					throw new Exception("Property value is not valid.");
				}

				_Minimum = value;
				if (value > _Value)
					_Value = value;
				if (value > _Maximum)
					_Maximum = value;

				InvalidateLayout();
			}
		}

		private int _Maximum = 100;
		public int Maximum {
			get { return _Maximum; }
			set {
				if (value < 0) {
					throw new Exception("Property value is not valid.");
				}

				_Maximum = value;
				if (value < _Value)
					_Value = value;
				if (value < _Minimum)
					_Minimum = value;

				InvalidateLayout();
			}
		}

		private int _Value;
		public int Value {
			get {
				if (!ShowThumb)
					return _Minimum;
				return _Value;
			}
			set {
				if (value == _Value)
					return;

				if (value > _Maximum || value < _Minimum) {
					throw new Exception("Property value is not valid.");
				}

				_Value = value;
				InvalidatePosition();

				if (Scroll != null) {
					Scroll(this);
				}
			}
		}

		private int _SmallChange = 1;
		public int SmallChange {
			get { return _SmallChange; }
			set {
				if (value < 1) {
					throw new Exception("Property value is not valid.");
				}

				_SmallChange = value;
			}
		}

		private int _LargeChange = 10;
		public int LargeChange {
			get { return _LargeChange; }
			set {
				if (value < 1) {
					throw new Exception("Property value is not valid.");
				}

				_LargeChange = value;
			}
		}

		private int ButtonSize = 16;
		// 14 minimum
		private int ThumbSize = 24;

		private Rectangle LSA;
		private Rectangle RSA;
		private Rectangle Shaft;

		private Rectangle Thumb;
		private bool ShowThumb;

		private bool ThumbDown;
		public NSHScrollBar() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);

			Height = 18;

			B1 = new SolidBrush(Color.FromArgb(55, 55, 55));
			B2 = new SolidBrush(Color.FromArgb(35, 35, 35));

			P1 = new Pen(Color.FromArgb(35, 35, 35));
			P2 = new Pen(Color.FromArgb(65, 65, 65));
			P3 = new Pen(Color.FromArgb(55, 55, 55));
			P4 = new Pen(Color.FromArgb(40, 40, 40));
		}

		private GraphicsPath GP1;
		private GraphicsPath GP2;
		private GraphicsPath GP3;

		private GraphicsPath GP4;
		private Pen P1;
		private Pen P2;
		private Pen P3;
		private Pen P4;
		private SolidBrush B1;

		private SolidBrush B2;

		int I1;
		private Graphics G;
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			G = e.Graphics;
			G.Clear(BackColor);

			GP1 = DrawArrow(6, 4, false);
			GP2 = DrawArrow(7, 5, false);

			G.FillPath(B1, GP2);
			G.FillPath(B2, GP1);

			GP3 = DrawArrow(Width - 11, 4, true);
			GP4 = DrawArrow(Width - 10, 5, true);

			G.FillPath(B1, GP4);
			G.FillPath(B2, GP3);

			if (ShowThumb) {
				G.FillRectangle(B1, Thumb);
				G.DrawRectangle(P1, Thumb);
				G.DrawRectangle(P2, Thumb.X + 1, Thumb.Y + 1, Thumb.Width - 2, Thumb.Height - 2);

				int X = 0;
				int LX = Thumb.X + (Thumb.Width / 2) - 3;

				for (int I = 0; I <= 2; I++) {
					X = LX + (I * 3);

					G.DrawLine(P1, X, Thumb.Y + 5, X, Thumb.Bottom - 5);
					G.DrawLine(P2, X + 1, Thumb.Y + 5, X + 1, Thumb.Bottom - 5);
				}
			}

			G.DrawRectangle(P3, 0, 0, Width - 1, Height - 1);
			G.DrawRectangle(P4, 1, 1, Width - 3, Height - 3);
		}

		private GraphicsPath DrawArrow(int x, int y, bool flip) {
			GraphicsPath GP = new GraphicsPath();

			int W = 5;
			int H = 9;

			if (flip) {
				GP.AddLine(x, y + 1, x, y + H + 1);
				GP.AddLine(x, y + H, x + W - 1, y + W);
			} else {
				GP.AddLine(x + W, y, x + W, y + H);
				GP.AddLine(x + W, y + H, x + 1, y + W);
			}

			GP.CloseFigure();
			return GP;
		}

		protected override void OnSizeChanged(EventArgs e) {
			InvalidateLayout();
		}

		private void InvalidateLayout() {
			LSA = new Rectangle(0, 0, ButtonSize, Height);
			RSA = new Rectangle(Width - ButtonSize, 0, ButtonSize, Height);
			Shaft = new Rectangle(LSA.Right + 1, 0, Width - (ButtonSize * 2) - 1, Height);

			ShowThumb = ((_Maximum - _Minimum) > Shaft.Width);

			if (ShowThumb) {
				//ThumbSize = Math.Max(0, 14) 'TODO: Implement this.
				Thumb = new Rectangle(0, 1, ThumbSize, Height - 3);
			}

			if (Scroll != null) {
				Scroll(this);
			}
			InvalidatePosition();
		}

		private void InvalidatePosition() {
			Thumb.X = Convert.ToInt32(GetProgress() * (Shaft.Width - ThumbSize)) + LSA.Width;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left && ShowThumb) {
				if (LSA.Contains(e.Location)) {
					I1 = _Value - _SmallChange;
				} else if (RSA.Contains(e.Location)) {
					I1 = _Value + _SmallChange;
				} else {
					if (Thumb.Contains(e.Location)) {
						ThumbDown = true;
						base.OnMouseDown(e);
						return;
					} else {
						if (e.X < Thumb.X) {
							I1 = _Value - _LargeChange;
						} else {
							I1 = _Value + _LargeChange;
						}
					}
				}

				Value = Math.Min(Math.Max(I1, _Minimum), _Maximum);
				InvalidatePosition();
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			if (ThumbDown && ShowThumb) {
				int ThumbPosition = e.X - LSA.Width - (ThumbSize / 2);
				int ThumbBounds = Shaft.Width - ThumbSize;

				I1 = Convert.ToInt32(((double)ThumbPosition / (double)ThumbBounds) * (_Maximum - _Minimum)) + _Minimum;

				Value = Math.Min(Math.Max(I1, _Minimum), _Maximum);
				InvalidatePosition();
			}

			base.OnMouseMove(e);
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			ThumbDown = false;
			base.OnMouseUp(e);
		}

		private double GetProgress() {
			return (double)(_Value - _Minimum) / (double)(_Maximum - _Minimum);
		}

	}

	/// <summary>
	/// Кольцо выбора цвета
	/// </summary>
	public class NSColorPicker : Control {

		/// <summary>
		/// Конструктор
		/// </summary>
		public NSColorPicker() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);
			SelectedColor = Color.Red;
			

		}

		/// <summary>
		/// Выбранный цвет
		/// </summary>
		public Color SelectedColor {
			get {
				return color;
			}
			set {
				color = value;
				hue = (float)color.GetHue() / 360f * (float)(Math.PI * 2);
				saturation = color.GetSaturation();
				brightness = color.GetBrightness();
				GenerateTriangle();
				Invalidate();
			}
		}

		/// <summary>
		/// Изменяется оттенок
		/// </summary>
		bool draggingHue;

		/// <summary>
		/// Изменяется цвет
		/// </summary>
		bool draggingTriangle;

		/// <summary>
		/// Отрисовка кольца Hue
		/// </summary>
		Bitmap hueRing;

		/// <summary>
		/// Изображение треугольника
		/// </summary>
		Bitmap triangle;

		/// <summary>
		/// Выбранный цвет
		/// </summary>
		Color color;

		/// <summary>
		/// Оттенок
		/// </summary>
		float hue;

		/// <summary>
		/// Насыщенность
		/// </summary>
		float saturation;

		/// <summary>
		/// Осветленность
		/// </summary>
		float brightness;

		/// <summary>
		/// Графический путь для треугольника
		/// </summary>
		PointF[] trianglePoly;

		/// <summary>
		/// Отрисовка
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {

			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;

			G.Clear(Color.FromArgb(50,50,50));
			Pen P1 = new Pen(Color.FromArgb(65, 65, 65));
			Pen P2 = new Pen(Color.FromArgb(50, 50, 50));
			Pen P3 = new Pen(Color.FromArgb(45, 45, 45));
			Pen P4 = new Pen(Color.FromArgb(30, 30, 30));
			Pen FP1 = new Pen(Color.FromArgb(45, 45, 45), 2f);
			Pen FP2 = new Pen(Color.FromArgb(0, 0, 0), 2f);
			
			if (hueRing!=null) {
				G.DrawImage(hueRing, 8, 8);
				G.DrawArc(FP1, new Rectangle(8, 8, hueRing.Width, hueRing.Height), 0, 360);
				G.DrawArc(FP2, new Rectangle(27, 27, hueRing.Width-38, hueRing.Height-38), 0, 360);
			}

			if (triangle != null) {
				G.DrawImage(triangle, 28, 28);
				G.TranslateTransform(28, 28);
				G.DrawPolygon(FP2, trianglePoly);
				G.ResetTransform();
			}

			// Выбор Hue
			Rectangle hueSelect = new Rectangle(
				hueRing.Width / 2 - 22, - 4, 24, 8
			);
			if (draggingHue) {
				hueSelect.X -= 2;
				hueSelect.Y -= 2;
				hueSelect.Width += 4;
				hueSelect.Height += 4;
			}

			int tsize = 10;
			if (draggingTriangle) {
				tsize = 26;
			}
			Rectangle triangleOval = new Rectangle(TriangleToCoords(), new Size(tsize, tsize));
			triangleOval.X += 28 - tsize / 2;
			triangleOval.Y += 28 - tsize / 2;

			double dr, dg, db;
			
			if (!draggingTriangle) {
				HSVToRGB(hue / ((float)Math.PI * 2), saturation, brightness, out dr, out dg, out db);
				G.FillPie(new SolidBrush(Color.FromArgb((byte)(dr * 255), (byte)(dg * 255), (byte)(db * 255))), triangleOval, 0, 360);
				G.DrawArc(FP1, triangleOval, 0, 360);
				triangleOval.X -= 1;
				triangleOval.Y -= 1;
				triangleOval.Width += 2;
				triangleOval.Height += 2;
				G.DrawArc(P1, triangleOval, 0, 360);
			}

			HSVToRGB(hue / ((float)Math.PI * 2), 1.0, 1.0, out dr, out dg, out db);
			G.TranslateTransform(Width / 2, Height / 2);
			G.RotateTransform(hue * 360f / (float)(Math.PI * 2));
			G.FillRectangle(new SolidBrush(Color.FromArgb((byte)(dr * 255), (byte)(dg * 255), (byte)(db * 255))), hueSelect);
			G.DrawRectangle(FP1, hueSelect);
			hueSelect.X += 1;
			hueSelect.Y += 1;
			hueSelect.Width -= 2;
			hueSelect.Height -= 2;
			G.DrawRectangle(P4, hueSelect);
			G.ResetTransform();

			if (draggingTriangle) {
				HSVToRGB(hue / ((float)Math.PI * 2), saturation, brightness, out dr, out dg, out db);
				G.FillPie(new SolidBrush(Color.FromArgb((byte)(dr * 255), (byte)(dg * 255), (byte)(db * 255))), triangleOval, 0, 360);
				G.DrawArc(FP1, triangleOval, 0, 360);
				triangleOval.X -= 1;
				triangleOval.Y -= 1;
				triangleOval.Width += 2;
				triangleOval.Height += 2;
				G.DrawArc(P1, triangleOval, 0, 360);
			}
		}

		protected override void OnSizeChanged(EventArgs e) {
			GenerateHueRing();
			GenerateTriangle();
			base.OnSizeChanged(e);
		}

		protected override void OnMouseDown(MouseEventArgs e) {

			Point hueCenter = new Point(8 + hueRing.Width / 2, 8 + hueRing.Height / 2);
			Point centeredPos = new Point(e.X - hueCenter.X, e.Y - hueCenter.Y);
			int radius = hueRing.Width/2;
			int innerRadius = radius - 20;
			int pos = centeredPos.X * centeredPos.X + centeredPos.Y * centeredPos.Y;

			if (pos >= innerRadius * innerRadius && pos <= radius * radius) {
				draggingHue = true;
				CalculateHue(centeredPos);
			} else {
				Point p = new Point(e.X - 28, e.Y - 28);
				if (InsideTriangle(trianglePoly[0], trianglePoly[1], trianglePoly[2], p)) {
					draggingTriangle = true;
					CalculateTriangle(centeredPos);
				}

			}
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			draggingHue = false;
			draggingTriangle = false;
			Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			Point hueCenter = new Point(8 + hueRing.Width / 2, 8 + hueRing.Height / 2);
			Point centeredPos = new Point(e.X - hueCenter.X, e.Y - hueCenter.Y);
			if (draggingHue) {
				CalculateHue(centeredPos);
				Invalidate();
			}else if(draggingTriangle){
				CalculateTriangle(centeredPos);
				Invalidate();
			}

		}

		/// <summary>
		/// Вычисление оттенка
		/// </summary>
		void CalculateHue(Point c) {
			hue = (float)Math.Atan2(c.Y, c.X);
			if (hue<0f) {
				hue += (float)Math.PI * 2f;
			}
			GenerateTriangle();
		}

		/// <summary>
		/// Рассчёт значений треугольника
		/// </summary>
		/// <param name="c"></param>
		void CalculateTriangle(Point c) {

			Matrix mtx = new Matrix();
			mtx.Rotate(-hue * 360f / (float)(Math.PI * 2));
			PointF[] tp = new PointF[]{c};
			mtx.TransformVectors(tp);

			PointF p = tp[0];
			float radius = triangle.Width / 2;

			// Собираем треугольник
			PointF[] tri = new PointF[3];
			tri[0].X = radius;
			tri[0].Y = 0;
			tri[1].X = (float)Math.Cos(Math.PI * 2 / 3f) * radius;
			tri[1].Y = (float)Math.Sin(Math.PI * 2 / 3f) * radius;
			tri[2].X = tri[1].X;
			tri[2].Y = -tri[1].Y;

			// Ставим точку в пределах треугольника
			if (!InsideTriangle(tri[0], tri[1], tri[2], p)) {
				PointF dp = p;
				float dist = float.MaxValue;
				for (int i = 0; i < 3; i++) {
					PointF np = ClosestPointToSegment(p, tri[i], tri[(i + 1) % 3]);
					float dst = (float)Math.Sqrt(Math.Pow(p.X - np.X, 2) + Math.Pow(p.Y - np.Y, 2));
					if (dst<dist) {
						dist = dst;
						dp = np;
					}
				}
				p = dp;
			}

			// Вычисление барицентрических координат
			PointF v0 = Sub(tri[1], tri[0]), v1 = Sub(tri[2], tri[0]), v2 = Sub(p, tri[0]);
			float d00 = Dot(v0, v0);
			float d01 = Dot(v0, v1);
			float d11 = Dot(v1, v1);
			float d20 = Dot(v2, v0);
			float d21 = Dot(v2, v1);
			float denom = d00 * d11 - d01 * d01;
			float v = (d11 * d20 - d01 * d21) / denom;
			float w = (d00 * d21 - d01 * d20) / denom;
			float u = 1f - v - w;

			// Установка значений
			brightness = 1.0f - v;
			saturation = u;
			GenerateTriangle();
		}


		/// <summary>
		/// Вычитание двух точек
		/// </summary>
		/// <returns></returns>
		PointF Sub(PointF a, PointF b) {
			return new PointF(a.X - b.X, a.Y - b.Y);
		}

		/// <summary>
		/// Дотпродукт двух точек
		/// </summary>
		float Dot(PointF a, PointF b) {
			return a.X * b.X + a.Y * b.Y;
		}

		/// <summary>
		/// Ближайшее расстояние от линии до точки
		/// </summary>
		PointF ClosestPointToSegment(PointF P, PointF A, PointF B) {
			PointF a_to_p = new PointF(), a_to_b = new PointF();
			a_to_p.X = P.X - A.X;
			a_to_p.Y = P.Y - A.Y;
			a_to_b.X = B.X - A.X;
			a_to_b.Y = B.Y - A.Y;
			float atb2 = a_to_b.X * a_to_b.X + a_to_b.Y * a_to_b.Y;
			float atp_dot_atb = a_to_p.X * a_to_b.X + a_to_p.Y * a_to_b.Y;
			float t = Clamp(atp_dot_atb / atb2, 0f, 1f);
			return new PointF(A.X + a_to_b.X * t, A.Y + a_to_b.Y * t);
		}

		/// <summary>
		/// Сжатие значения в установленные пределы
		/// </summary>
		float Clamp(float b, float min, float max) {
			if (b>max) {
				return max;
			}else if(b<min){
				return min;
			}
			return b;
		}

		/// <summary>
		/// Координаты на треугольнике
		/// </summary>
		/// <returns></returns>
		Point TriangleToCoords() {
			float u = saturation;
			float v = 1.0f - brightness;
			float w = 1f - u - v;

			return new Point(
				(int)(
					trianglePoly[0].X * u + 
					trianglePoly[1].X * v +
					trianglePoly[2].X * w
				),
				(int)(
					trianglePoly[0].Y * u +
					trianglePoly[1].Y * v +
					trianglePoly[2].Y * w
				)
			);
		}



		/// <summary>
		/// Генерация изображения кольца
		/// </summary>
		void GenerateHueRing() {
			int sz = Width;
			if (Height<Width) {
				sz = Height;
			}
			sz -= 16;
			if (sz <= 0) {
				return;
			}
			hueRing = new Bitmap(sz, sz);
			
			// Радиусы
			int radius = sz / 2;
			int innerRadius = radius - 19;
			Point center = new Point(radius, radius);
			radius *= radius;
			innerRadius *= innerRadius;

			Color c = Color.Red;

			BitmapData bd = hueRing.LockBits(new Rectangle(0, 0, sz, sz), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			byte[] pixels = new byte[sz*sz*4];
			for (int y = 0; y < sz; y++) {
				int pY = center.Y - y;
				for (int x = 0; x < sz; x++) {
					int pX = center.X - x;
					float dist = pX * pX + pY * pY;

					if (dist <= radius) {
						int pix = (y * sz + x) * 4;
						if (dist >= innerRadius) {
							double theta = Math.Atan2(pY, pX);
							double hue = (theta + Math.PI) / (2 * Math.PI);

							double dr, dg, db;
							HSVToRGB(hue, 1.0, 1.0, out dr, out dg, out db);

							pixels[pix + 0] = (byte)(db * 255);		// Синий
							pixels[pix + 1] = (byte)(dg * 255);		// Зеленый
							pixels[pix + 2] = (byte)(dr * 255);		// Красный
						} else {
							pixels[pix + 0] = (byte)65;		// Синий
							pixels[pix + 1] = (byte)65;		// Зеленый
							pixels[pix + 2] = (byte)65;		// Красный
						}
						pixels[pix + 3] = 255;
					}
				}
			}

			Marshal.Copy(pixels, 0, bd.Scan0, pixels.Length);
			hueRing.UnlockBits(bd);
		}

		/// <summary>
		/// Генерация изображения с треугольником
		/// </summary>
		void GenerateTriangle() {
			int sz = Width;
			if (Height < Width) {
				sz = Height;
			}
			sz -= 56;
			if (sz <= 0) {
				return;
			}
			triangle = new Bitmap(sz, sz);

			float angle = hue;
			float step = (float)Math.PI * 2f / 3f;
			float radius = sz / 2;
			PointF center = new PointF(radius, radius);
			trianglePoly = new PointF[3];
			for (int i = 0; i < 3; i++) {
				trianglePoly[i] = new PointF(
					center.X + (float)Math.Cos(angle) * radius,
					center.Y + (float)Math.Sin(angle) * radius
				);
				angle += step;
			}

			double dr, dg, db;
			HSVToRGB(hue/((float)Math.PI*2), 1.0, 1.0, out dr, out dg, out db);

			GraphicsPath gp = new GraphicsPath();
			gp.AddPolygon(trianglePoly);
			PathGradientBrush gb = new PathGradientBrush(gp);

			Color[] colors = new Color[3]{
				Color.FromArgb((byte)(dr * 255), (byte)(dg * 255), (byte)(db * 255)),
				Color.Black,
				Color.White
			};
			gb.SurroundColors = colors;
			gb.CenterColor = Color.FromArgb((255 + colors[0].R) / 3, (255 + colors[0].G) / 3, (255 + colors[0].B) / 3);
			gb.CenterPoint = center;

			using (Graphics g = Graphics.FromImage(triangle)) {
				g.FillPath(gb, gp);
			}
		}

		/// <summary>
		/// Точка внутри треугольника
		/// </summary>
		bool InsideTriangle(PointF a, PointF b, PointF c, PointF p) {
			PointF pa = new PointF(
				p.X - a.X,
				p.Y - a.Y
			);

			bool s = (b.X - a.X) * pa.Y - (b.Y - a.Y) * pa.X > 0;
			if ((c.X - a.X) * pa.Y - (c.Y - a.Y) * pa.X > 0 == s) return false;
			if ((c.X - b.X) * (p.Y - b.Y) - (c.Y - b.Y) * (p.X - b.X) > 0 != s) return false;
			return true;
		}

		/// <summary>
		/// Конвертация HSV в RGB
		/// </summary>
		void HSVToRGB(double H, double S, double V, out double R, out double G, out double B) {
			if (H == 1.0) {
				H = 0.0;
			}
			double step = 1.0 / 6.0;
			double vh = H / step;
			int i = (int)System.Math.Floor(vh);
			double f = vh - i;
			double p = V * (1.0 - S);
			double q = V * (1.0 - (S * f));
			double t = V * (1.0 - (S * (1.0 - f)));
			switch (i) {
				case 1:
					R = q;
					G = V;
					B = p;
					break;
				case 2:
					R = p;
					G = V;
					B = t;
					break;
				case 3:
					R = p;
					G = q;
					B = V;
					break;
				case 4:
					R = t;
					G = p;
					B = V;
					break;
				case 5:
					R = V;
					G = p;
					B = q;
					break;
				default:
					R = V;
					G = t;
					B = p;
					break;
			}
		}
	}

	public class NSColorPickerButton : Control {

		/// <summary>
		/// Конструктор
		/// </summary>
		public NSColorPickerButton() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, false);
			color = Color.White;
		}

		/// <summary>
		/// Выбранный цвет
		/// </summary>
		public Color SelectedColor {
			get {
				return color;
			}
			set {
				color = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Выбранный цвет
		/// </summary>
		Color color;
		
		/// <summary>
		/// Мышь нажата
		/// </summary>
		bool IsMouseDown;

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			Graphics G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			Pen P1 = new Pen(Color.FromArgb(35, 35, 35));
			Pen P2 = new Pen(Color.FromArgb(65, 65, 65));

			G.Clear(BackColor);
			G.SmoothingMode = SmoothingMode.AntiAlias;

			GraphicsPath GP1 = ThemeModule.CreateRound(0, 0, Width - 1, Height - 1, 7);
			GraphicsPath GP2 = ThemeModule.CreateRound(1, 1, Width - 3, Height - 3, 7);

			if (IsMouseDown) {
				PathGradientBrush PB1 = new PathGradientBrush(GP1);
				PB1.CenterColor = Color.FromArgb(60, 60, 60);
				PB1.SurroundColors = new Color[] { Color.FromArgb(55, 55, 55) };
				PB1.FocusScales = new PointF(0.8f, 0.5f);

				G.FillPath(PB1, GP1);
			} else {
				LinearGradientBrush GB1 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
				G.FillPath(GB1, GP1);
			}

			G.DrawPath(P1, GP1);
			G.DrawPath(P2, GP2);


		}

		protected override void OnMouseDown(MouseEventArgs e) {
			IsMouseDown = true;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			IsMouseDown = false;
			Invalidate();
		}

	}

	public class NSContextMenu : ContextMenuStrip {

		public NSContextMenu() {
			Renderer = new NSToolStripRenderer(new NSColorTable());
			ForeColor = Color.White;
		}

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			base.OnPaint(e);
		}

	}

	public class NSToolStripRenderer : ToolStripProfessionalRenderer {

		public NSToolStripRenderer(ProfessionalColorTable ct)
			: base(ct) {

		}

		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e) {
			var tsMenuItem = e.Item as ToolStripMenuItem;
			if (tsMenuItem != null) {
				e.ArrowColor = Color.White;
			}
			base.OnRenderArrow(e);
		}
	}

	public class NSColorTable : ProfessionalColorTable {
		private Color BackColor = Color.FromArgb(55, 55, 55);

		public override Color ButtonSelectedBorder {
			get { return BackColor; }
		}

		public override Color CheckBackground {
			get { return BackColor; }
		}

		public override Color CheckPressedBackground {
			get { return BackColor; }
		}

		public override Color CheckSelectedBackground {
			get { return BackColor; }
		}

		public override Color ImageMarginGradientBegin {
			get { return BackColor; }
		}

		public override Color ImageMarginGradientEnd {
			get { return BackColor; }
		}

		public override Color ImageMarginGradientMiddle {
			get { return BackColor; }
		}

		public override Color MenuBorder {
			get { return Color.FromArgb(35, 35, 35); }
		}

		public override Color MenuItemBorder {
			get { return BackColor; }
		}

		public override Color MenuItemSelected {
			get { return Color.FromArgb(65, 65, 65); }
		}

		public override Color SeparatorDark {
			get { return Color.FromArgb(35, 35, 35); }
		}

		public override Color ToolStripDropDownBackground {
			get { return BackColor; }
		}

		public override Color MenuStripGradientBegin { get { return Color.Salmon; } }

		public override Color MenuStripGradientEnd { get { return Color.OrangeRed; } }
	}

	//If you have made it this far it's not too late to turn back, you must not continue on! If you are trying to fullfill some 
	//sick act of masochism by studying the source of the ListView then, may god have mercy on your soul.
	public class NSListView : Control {

		public class NSListViewItem {
			public string Text { get; set; }
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
			public List<NSListViewSubItem> SubItems { get; set; }


			protected Guid UniqueId;
			public NSListViewItem() {
				UniqueId = Guid.NewGuid();
			}

			public override string ToString() {
				return Text;
			}

			public override bool Equals(object obj) {
				if (obj is NSListViewItem) {
					return (((NSListViewItem)obj).UniqueId == UniqueId);
				}

				return false;
			}

			public override int GetHashCode() {
				return base.GetHashCode();
			}

		}

		public class NSListViewSubItem {
			public string Text { get; set; }

			public override string ToString() {
				return Text;
			}
		}

		public class NSListViewColumnHeader {
			public string Text { get; set; }
			public int Width { get; set; }

			public override string ToString() {
				return Text;
			}
		}

		private List<NSListViewItem> _Items = new List<NSListViewItem>();
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public NSListViewItem[] Items {
			get { return _Items.ToArray(); }
			set {
				_Items = new List<NSListViewItem>(value);
				InvalidateScroll();
			}
		}

		private List<NSListViewItem> _SelectedItems = new List<NSListViewItem>();
		public NSListViewItem[] SelectedItems {
			get { return _SelectedItems.ToArray(); }
		}

		private List<NSListViewColumnHeader> _Columns = new List<NSListViewColumnHeader>();
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public NSListViewColumnHeader[] Columns {
			get { return _Columns.ToArray(); }
			set {
				_Columns = new List<NSListViewColumnHeader>(value);
				InvalidateColumns();
			}
		}

		private bool _MultiSelect = true;
		public bool MultiSelect {
			get { return _MultiSelect; }
			set {
				_MultiSelect = value;

				if (_SelectedItems.Count > 1) {
					_SelectedItems.RemoveRange(1, _SelectedItems.Count - 1);
				}

				Invalidate();
			}
		}

		private int ItemHeight = 24;
		public override Font Font {
			get { return base.Font; }
			set {
				ItemHeight = Convert.ToInt32(Graphics.FromHwnd(Handle).MeasureString("@", Font).Height) + 6;

				if (VS != null) {
					VS.SmallChange = ItemHeight;
					VS.LargeChange = ItemHeight;
				}

				base.Font = value;
				InvalidateLayout();
			}
		}

		#region " Item Helper Methods "

		//Ok, you've seen everything of importance at this point; I am begging you to spare yourself. You must not read any further!

		public void AddItem(string text, params string[] subItems) {
			List<NSListViewSubItem> Items = new List<NSListViewSubItem>();
			foreach (string I in subItems) {
				NSListViewSubItem SubItem = new NSListViewSubItem();
				SubItem.Text = I;
				Items.Add(SubItem);
			}

			NSListViewItem Item = new NSListViewItem();
			Item.Text = text;
			Item.SubItems = Items;

			_Items.Add(Item);
			InvalidateScroll();
		}

		public void RemoveItemAt(int index) {
			_Items.RemoveAt(index);
			InvalidateScroll();
		}

		public void RemoveItem(NSListViewItem item) {
			_Items.Remove(item);
			InvalidateScroll();
		}

		public void RemoveItems(NSListViewItem[] items) {
			foreach (NSListViewItem I in items) {
				_Items.Remove(I);
			}

			InvalidateScroll();
		}

		#endregion


		private NSVScrollBar VS;
		public NSListView() {
			SetStyle((ControlStyles)139286, true);
			SetStyle(ControlStyles.Selectable, true);

			P1 = new Pen(Color.FromArgb(55, 55, 55));
			P2 = new Pen(Color.FromArgb(35, 35, 35));
			P3 = new Pen(Color.FromArgb(65, 65, 65));

			B1 = new SolidBrush(Color.FromArgb(62, 62, 62));
			B2 = new SolidBrush(Color.FromArgb(65, 65, 65));
			B3 = new SolidBrush(Color.FromArgb(47, 47, 47));
			B4 = new SolidBrush(Color.FromArgb(50, 50, 50));

			VS = new NSVScrollBar();
			VS.SmallChange = ItemHeight;
			VS.LargeChange = ItemHeight;

			VS.Scroll += HandleScroll;
			VS.MouseDown += VS_MouseDown;
			Controls.Add(VS);

			InvalidateLayout();
		}

		protected override void OnSizeChanged(EventArgs e) {
			InvalidateLayout();
			base.OnSizeChanged(e);
		}

		private void HandleScroll(object sender) {
			Invalidate();
		}

		private void InvalidateScroll() {
			VS.Maximum = (_Items.Count * ItemHeight);
			Invalidate();
		}

		private void InvalidateLayout() {
			VS.Location = new Point(Width - VS.Width - 1, 1);
			VS.Size = new Size(18, Height - 2);

			Invalidate();
		}

		private int[] ColumnOffsets;
		public void InvalidateColumns() {
			int Width = 3;
			ColumnOffsets = new int[_Columns.Count];

			for (int I = 0; I <= _Columns.Count - 1; I++) {
				ColumnOffsets[I] = Width;
				Width += Columns[I].Width;
			}

			Invalidate();
		}

		private void VS_MouseDown(object sender, MouseEventArgs e) {
			Focus();
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			Focus();

			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				int Offset = Convert.ToInt32(VS.Percent * (VS.Maximum - (Height - (ItemHeight * 2))));
				int Index = ((e.Y + Offset - ItemHeight) / ItemHeight);

				if (Index > _Items.Count - 1)
					Index = -1;

				if (!(Index == -1)) {
					//TODO: Handle Shift key

					if (ModifierKeys == Keys.Control && _MultiSelect) {
						if (_SelectedItems.Contains(_Items[Index])) {
							_SelectedItems.Remove(_Items[Index]);
						} else {
							_SelectedItems.Add(_Items[Index]);
						}
					} else {
						_SelectedItems.Clear();
						_SelectedItems.Add(_Items[Index]);
					}
				}

				Invalidate();
			}

			base.OnMouseDown(e);
		}

		private Pen P1;
		private Pen P2;
		private Pen P3;
		private SolidBrush B1;
		private SolidBrush B2;
		private SolidBrush B3;
		private SolidBrush B4;

		private LinearGradientBrush GB1;
		//I am so sorry you have to witness this. I tried warning you. ;.;

		private Graphics G;
		protected override void OnPaint(PaintEventArgs e) {
			G = e.Graphics;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);

			int X = 0;
			int Y = 0;
			float H = 0;

			G.DrawRectangle(P1, 1, 1, Width - 3, Height - 3);

			Rectangle R1 = default(Rectangle);
			NSListViewItem CI = null;

			int Offset = Convert.ToInt32(VS.Percent * (VS.Maximum - (Height - (ItemHeight * 2))));

			int StartIndex = 0;
			if (Offset == 0)
				StartIndex = 0;
			else
				StartIndex = (Offset / ItemHeight);

			int EndIndex = Math.Min(StartIndex + (Height / ItemHeight), _Items.Count - 1);

			for (int I = StartIndex; I <= EndIndex; I++) {
				CI = Items[I];

				R1 = new Rectangle(0, ItemHeight + (I * ItemHeight) + 1 - Offset, Width, ItemHeight - 1);

				H = G.MeasureString(CI.Text, Font).Height;
				Y = R1.Y + Convert.ToInt32((ItemHeight / 2) - (H / 2));

				if (_SelectedItems.Contains(CI)) {
					if (I % 2 == 0) {
						G.FillRectangle(B1, R1);
					} else {
						G.FillRectangle(B2, R1);
					}
				} else {
					if (I % 2 == 0) {
						G.FillRectangle(B3, R1);
					} else {
						G.FillRectangle(B4, R1);
					}
				}

				G.DrawLine(P2, 0, R1.Bottom, Width, R1.Bottom);

				if (Columns.Length > 0) {
					R1.Width = Columns[0].Width;
					G.SetClip(R1);
				}

				//TODO: Ellipse text that overhangs seperators.
				G.DrawString(CI.Text, Font, Brushes.Black, 10, Y + 1);
				G.DrawString(CI.Text, Font, Brushes.White, 9, Y);

				if (CI.SubItems != null) {
					for (int I2 = 0; I2 <= Math.Min(CI.SubItems.Count, _Columns.Count) - 1; I2++) {
						X = ColumnOffsets[I2 + 1] + 4;

						R1.X = X;
						R1.Width = Columns[I2].Width;
						G.SetClip(R1);

						G.DrawString(CI.SubItems[I2].Text, Font, Brushes.Black, X + 1, Y + 1);
						G.DrawString(CI.SubItems[I2].Text, Font, Brushes.White, X, Y);
					}
				}

				G.ResetClip();
			}

			R1 = new Rectangle(0, 0, Width, ItemHeight);

			GB1 = new LinearGradientBrush(R1, Color.FromArgb(60, 60, 60), Color.FromArgb(55, 55, 55), 90f);
			G.FillRectangle(GB1, R1);
			G.DrawRectangle(P3, 1, 1, Width - 22, ItemHeight - 2);

			int LH = Math.Min(VS.Maximum + ItemHeight - Offset, Height);

			NSListViewColumnHeader CC = null;
			for (int I = 0; I <= _Columns.Count - 1; I++) {
				CC = Columns[I];

				H = G.MeasureString(CC.Text, Font).Height;
				Y = Convert.ToInt32((ItemHeight / 2) - (H / 2));
				X = ColumnOffsets[I];

				G.DrawString(CC.Text, Font, Brushes.Black, X + 1, Y + 1);
				G.DrawString(CC.Text, Font, Brushes.White, X, Y);

				G.DrawLine(P2, X - 3, 0, X - 3, LH);
				G.DrawLine(P3, X - 2, 0, X - 2, ItemHeight);
			}

			G.DrawRectangle(P2, 0, 0, Width - 1, Height - 1);

			G.DrawLine(P2, 0, ItemHeight, Width, ItemHeight);
			G.DrawLine(P2, VS.Location.X - 1, 0, VS.Location.X - 1, Height);
		}

		protected override void OnMouseWheel(MouseEventArgs e) {
			int Move = -((e.Delta * SystemInformation.MouseWheelScrollLines / 120) * (ItemHeight / 2));

			int Value = Math.Max(Math.Min(VS.Value + Move, VS.Maximum), VS.Minimum);
			VS.Value = Value;

			base.OnMouseWheel(e);
		}

	}

}