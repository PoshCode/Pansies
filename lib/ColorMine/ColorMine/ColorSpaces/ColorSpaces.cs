// Note: This is a generated file.
using ColorMine.ColorSpaces.Conversions;

namespace ColorMine.ColorSpaces
{
	public interface IRgb : IColorSpace
    {
		double R { get; set; }
		double G { get; set; }
		double B { get; set; }
    }

    public class Rgb : ColorSpace, IRgb
    {
		public double R { get; set; }
		public double G { get; set; }
		public double B { get; set; }

        public Rgb() { }

		public Rgb(double r, double g, double b)
		{
			R = r;
			G = g;
			B = b;
		}

		public Rgb(IColorSpace color)
		{
			Ordinals = color.To<Rgb>().Ordinals;
		}

		public Rgb(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            RgbConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "R: " + R,
		       "G: " + G,
		       "B: " + B,
            });
		}

        public override IRgb ToRgb()
        {
            return RgbConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { R, G, B, };
		    }
			set
			{
				R = value[0];
				G = value[1];
				B = value[2];
			}
		}
    }
	public interface IXyz : IColorSpace
    {
		double X { get; set; }
		double Y { get; set; }
		double Z { get; set; }
    }

    public class Xyz : ColorSpace, IXyz
    {
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

        public Xyz() { }

		public Xyz(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Xyz(IColorSpace color)
		{
			Ordinals = color.To<Xyz>().Ordinals;
		}

		public Xyz(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            XyzConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "X: " + X,
		       "Y: " + Y,
		       "Z: " + Z,
            });
		}

        public override IRgb ToRgb()
        {
            return XyzConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { X, Y, Z, };
		    }
			set
			{
				X = value[0];
				Y = value[1];
				Z = value[2];
			}
		}
    }
	public interface IHsl : IColorSpace
    {
		double H { get; set; }
		double S { get; set; }
		double L { get; set; }
    }

    public class Hsl : ColorSpace, IHsl
    {
		public double H { get; set; }
		public double S { get; set; }
		public double L { get; set; }

        public Hsl() { }

		public Hsl(double h, double s, double l)
		{
			H = h;
			S = s;
			L = l;
		}

		public Hsl(IColorSpace color)
		{
			Ordinals = color.To<Hsl>().Ordinals;
		}

		public Hsl(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            HslConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "H: " + H,
		       "S: " + S,
		       "L: " + L,
            });
		}

        public override IRgb ToRgb()
        {
            return HslConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { H, S, L, };
		    }
			set
			{
				H = value[0];
				S = value[1];
				L = value[2];
			}
		}
    }
	public interface ILab : IColorSpace
    {
		double L { get; set; }
		double A { get; set; }
		double B { get; set; }
    }

    public class Lab : ColorSpace, ILab
    {
		public double L { get; set; }
		public double A { get; set; }
		public double B { get; set; }

        public Lab() { }

		public Lab(double l, double a, double b)
		{
			L = l;
			A = a;
			B = b;
		}

		public Lab(IColorSpace color)
		{
			Ordinals = color.To<Lab>().Ordinals;
		}

		public Lab(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            LabConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "L: " + L,
		       "A: " + A,
		       "B: " + B,
            });
		}

        public override IRgb ToRgb()
        {
            return LabConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { L, A, B, };
		    }
			set
			{
				L = value[0];
				A = value[1];
				B = value[2];
			}
		}
    }
	public interface ILch : IColorSpace
    {
		double L { get; set; }
		double C { get; set; }
		double H { get; set; }
    }

    public class Lch : ColorSpace, ILch
    {
		public double L { get; set; }
		public double C { get; set; }
		public double H { get; set; }

        public Lch() { }

		public Lch(double l, double c, double h)
		{
			L = l;
			C = c;
			H = h;
		}

		public Lch(IColorSpace color)
		{
			Ordinals = color.To<Lch>().Ordinals;
		}

		public Lch(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            LchConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "L: " + L,
		       "C: " + C,
		       "H: " + H,
            });
		}

        public override IRgb ToRgb()
        {
            return LchConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { L, C, H, };
		    }
			set
			{
				L = value[0];
				C = value[1];
				H = value[2];
			}
		}
    }
	public interface ILuv : IColorSpace
    {
		double L { get; set; }
		double U { get; set; }
		double V { get; set; }
    }

    public class Luv : ColorSpace, ILuv
    {
		public double L { get; set; }
		public double U { get; set; }
		public double V { get; set; }

        public Luv() { }

		public Luv(double l, double u, double v)
		{
			L = l;
			U = u;
			V = v;
		}

		public Luv(IColorSpace color)
		{
			Ordinals = color.To<Luv>().Ordinals;
		}

		public Luv(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            LuvConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "L: " + L,
		       "U: " + U,
		       "V: " + V,
            });
		}

        public override IRgb ToRgb()
        {
            return LuvConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { L, U, V, };
		    }
			set
			{
				L = value[0];
				U = value[1];
				V = value[2];
			}
		}
    }
	public interface IYxy : IColorSpace
    {
		double Y1 { get; set; }
		double X { get; set; }
		double Y2 { get; set; }
    }

    public class Yxy : ColorSpace, IYxy
    {
		public double Y1 { get; set; }
		public double X { get; set; }
		public double Y2 { get; set; }

        public Yxy() { }

		public Yxy(double y1, double x, double y2)
		{
			Y1 = y1;
			X = x;
			Y2 = y2;
		}

		public Yxy(IColorSpace color)
		{
			Ordinals = color.To<Yxy>().Ordinals;
		}

		public Yxy(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            YxyConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "Y1: " + Y1,
		       "X: " + X,
		       "Y2: " + Y2,
            });
		}

        public override IRgb ToRgb()
        {
            return YxyConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { Y1, X, Y2, };
		    }
			set
			{
				Y1 = value[0];
				X = value[1];
				Y2 = value[2];
			}
		}
    }
	public interface ICmy : IColorSpace
    {
		double C { get; set; }
		double M { get; set; }
		double Y { get; set; }
    }

    public class Cmy : ColorSpace, ICmy
    {
		public double C { get; set; }
		public double M { get; set; }
		public double Y { get; set; }

        public Cmy() { }

		public Cmy(double c, double m, double y)
		{
			C = c;
			M = m;
			Y = y;
		}

		public Cmy(IColorSpace color)
		{
			Ordinals = color.To<Cmy>().Ordinals;
		}

		public Cmy(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            CmyConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "C: " + C,
		       "M: " + M,
		       "Y: " + Y,
            });
		}

        public override IRgb ToRgb()
        {
            return CmyConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { C, M, Y, };
		    }
			set
			{
				C = value[0];
				M = value[1];
				Y = value[2];
			}
		}
    }
	public interface ICmyk : IColorSpace
    {
		double C { get; set; }
		double M { get; set; }
		double Y { get; set; }
		double K { get; set; }
    }

    public class Cmyk : ColorSpace, ICmyk
    {
		public double C { get; set; }
		public double M { get; set; }
		public double Y { get; set; }
		public double K { get; set; }

        public Cmyk() { }

		public Cmyk(double c, double m, double y, double k)
		{
			C = c;
			M = m;
			Y = y;
			K = k;
		}

		public Cmyk(IColorSpace color)
		{
			Ordinals = color.To<Cmyk>().Ordinals;
		}

		public Cmyk(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            CmykConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "C: " + C,
		       "M: " + M,
		       "Y: " + Y,
		       "K: " + K,
            });
		}

        public override IRgb ToRgb()
        {
            return CmykConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { C, M, Y, K, };
		    }
			set
			{
				C = value[0];
				M = value[1];
				Y = value[2];
				K = value[3];
			}
		}
    }
	public interface IHsv : IColorSpace
    {
		double H { get; set; }
		double S { get; set; }
		double V { get; set; }
    }

    public class Hsv : ColorSpace, IHsv
    {
		public double H { get; set; }
		public double S { get; set; }
		public double V { get; set; }

        public Hsv() { }

		public Hsv(double h, double s, double v)
		{
			H = h;
			S = s;
			V = v;
		}

		public Hsv(IColorSpace color)
		{
			Ordinals = color.To<Hsv>().Ordinals;
		}

		public Hsv(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            HsvConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "H: " + H,
		       "S: " + S,
		       "V: " + V,
            });
		}

        public override IRgb ToRgb()
        {
            return HsvConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { H, S, V, };
		    }
			set
			{
				H = value[0];
				S = value[1];
				V = value[2];
			}
		}
    }
	public interface IHsb : IColorSpace
    {
		double H { get; set; }
		double S { get; set; }
		double B { get; set; }
    }

    public class Hsb : ColorSpace, IHsb
    {
		public double H { get; set; }
		public double S { get; set; }
		public double B { get; set; }

        public Hsb() { }

		public Hsb(double h, double s, double b)
		{
			H = h;
			S = s;
			B = b;
		}

		public Hsb(IColorSpace color)
		{
			Ordinals = color.To<Hsb>().Ordinals;
		}

		public Hsb(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            HsbConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "H: " + H,
		       "S: " + S,
		       "B: " + B,
            });
		}

        public override IRgb ToRgb()
        {
            return HsbConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { H, S, B, };
		    }
			set
			{
				H = value[0];
				S = value[1];
				B = value[2];
			}
		}
    }
	public interface IHunterLab : IColorSpace
    {
		double L { get; set; }
		double A { get; set; }
		double B { get; set; }
    }

    public class HunterLab : ColorSpace, IHunterLab
    {
		public double L { get; set; }
		public double A { get; set; }
		public double B { get; set; }

        public HunterLab() { }

		public HunterLab(double l, double a, double b)
		{
			L = l;
			A = a;
			B = b;
		}

		public HunterLab(IColorSpace color)
		{
			Ordinals = color.To<HunterLab>().Ordinals;
		}

		public HunterLab(double[] ordinals)
		{
			Ordinals = ordinals;
		}

        public override void Initialize(IRgb color)
        {
            HunterLabConverter.ToColorSpace(color,this);
        }

        public override string ToString()
		{
		    return string.Join(", ", new []{
		       "L: " + L,
		       "A: " + A,
		       "B: " + B,
            });
		}

        public override IRgb ToRgb()
        {
            return HunterLabConverter.ToColor(this);
        }

		public override sealed double[] Ordinals
		{
		    get
		    {
		        return new[] { L, A, B, };
		    }
			set
			{
				L = value[0];
				A = value[1];
				B = value[2];
			}
		}
    }
}