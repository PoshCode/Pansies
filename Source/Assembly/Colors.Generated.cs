using PoshCode.Pansies.ColorSpaces.Conversions;
using PoshCode.Pansies.ColorSpaces;

// Note: This is a generated file. The source is in Colors.tt
namespace PoshCode.Pansies
{
	public partial class RgbColor : PoshCode.Pansies.ColorSpaces.Rgb
	{
		public RgbColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public RgbColor(IRgb rgb)
		{
			Ordinals = rgb.Ordinals;
		}

		public RgbColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator RgbColor(Xyz xyz)
		{
			return new RgbColor(xyz);
		}

		public static implicit operator RgbColor(XyzColor xyz)
		{
			return new RgbColor(xyz);
		}
		public RgbColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator RgbColor(Hsl hsl)
		{
			return new RgbColor(hsl);
		}

		public static implicit operator RgbColor(HslColor hsl)
		{
			return new RgbColor(hsl);
		}
		public RgbColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator RgbColor(Lab lab)
		{
			return new RgbColor(lab);
		}

		public static implicit operator RgbColor(LabColor lab)
		{
			return new RgbColor(lab);
		}
		public RgbColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator RgbColor(Lch lch)
		{
			return new RgbColor(lch);
		}

		public static implicit operator RgbColor(LchColor lch)
		{
			return new RgbColor(lch);
		}
		public RgbColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator RgbColor(Luv luv)
		{
			return new RgbColor(luv);
		}

		public static implicit operator RgbColor(LuvColor luv)
		{
			return new RgbColor(luv);
		}
		public RgbColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator RgbColor(Yxy yxy)
		{
			return new RgbColor(yxy);
		}

		public static implicit operator RgbColor(YxyColor yxy)
		{
			return new RgbColor(yxy);
		}
		public RgbColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator RgbColor(Cmy cmy)
		{
			return new RgbColor(cmy);
		}

		public static implicit operator RgbColor(CmyColor cmy)
		{
			return new RgbColor(cmy);
		}
		public RgbColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator RgbColor(Cmyk cmyk)
		{
			return new RgbColor(cmyk);
		}

		public static implicit operator RgbColor(CmykColor cmyk)
		{
			return new RgbColor(cmyk);
		}
		public RgbColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator RgbColor(Hsv hsv)
		{
			return new RgbColor(hsv);
		}

		public static implicit operator RgbColor(HsvColor hsv)
		{
			return new RgbColor(hsv);
		}
		public RgbColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator RgbColor(Hsb hsb)
		{
			return new RgbColor(hsb);
		}

		public static implicit operator RgbColor(HsbColor hsb)
		{
			return new RgbColor(hsb);
		}
		public RgbColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator RgbColor(HunterLab hunterlab)
		{
			return new RgbColor(hunterlab);
		}

		public static implicit operator RgbColor(HunterLabColor hunterlab)
		{
			return new RgbColor(hunterlab);
		}
	}
	public partial class XyzColor : PoshCode.Pansies.ColorSpaces.Xyz
	{
		public XyzColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public XyzColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator XyzColor(Rgb rgb)
		{
			return new XyzColor(rgb);
		}

		public static implicit operator XyzColor(RgbColor rgb)
		{
			return new XyzColor(rgb);
		}
		public XyzColor(IXyz xyz)
		{
			Ordinals = xyz.Ordinals;
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public XyzColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator XyzColor(Hsl hsl)
		{
			return new XyzColor(hsl);
		}

		public static implicit operator XyzColor(HslColor hsl)
		{
			return new XyzColor(hsl);
		}
		public XyzColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator XyzColor(Lab lab)
		{
			return new XyzColor(lab);
		}

		public static implicit operator XyzColor(LabColor lab)
		{
			return new XyzColor(lab);
		}
		public XyzColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator XyzColor(Lch lch)
		{
			return new XyzColor(lch);
		}

		public static implicit operator XyzColor(LchColor lch)
		{
			return new XyzColor(lch);
		}
		public XyzColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator XyzColor(Luv luv)
		{
			return new XyzColor(luv);
		}

		public static implicit operator XyzColor(LuvColor luv)
		{
			return new XyzColor(luv);
		}
		public XyzColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator XyzColor(Yxy yxy)
		{
			return new XyzColor(yxy);
		}

		public static implicit operator XyzColor(YxyColor yxy)
		{
			return new XyzColor(yxy);
		}
		public XyzColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator XyzColor(Cmy cmy)
		{
			return new XyzColor(cmy);
		}

		public static implicit operator XyzColor(CmyColor cmy)
		{
			return new XyzColor(cmy);
		}
		public XyzColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator XyzColor(Cmyk cmyk)
		{
			return new XyzColor(cmyk);
		}

		public static implicit operator XyzColor(CmykColor cmyk)
		{
			return new XyzColor(cmyk);
		}
		public XyzColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator XyzColor(Hsv hsv)
		{
			return new XyzColor(hsv);
		}

		public static implicit operator XyzColor(HsvColor hsv)
		{
			return new XyzColor(hsv);
		}
		public XyzColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator XyzColor(Hsb hsb)
		{
			return new XyzColor(hsb);
		}

		public static implicit operator XyzColor(HsbColor hsb)
		{
			return new XyzColor(hsb);
		}
		public XyzColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator XyzColor(HunterLab hunterlab)
		{
			return new XyzColor(hunterlab);
		}

		public static implicit operator XyzColor(HunterLabColor hunterlab)
		{
			return new XyzColor(hunterlab);
		}
	}
	public partial class HslColor : PoshCode.Pansies.ColorSpaces.Hsl
	{
		public HslColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public HslColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator HslColor(Rgb rgb)
		{
			return new HslColor(rgb);
		}

		public static implicit operator HslColor(RgbColor rgb)
		{
			return new HslColor(rgb);
		}
		public HslColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator HslColor(Xyz xyz)
		{
			return new HslColor(xyz);
		}

		public static implicit operator HslColor(XyzColor xyz)
		{
			return new HslColor(xyz);
		}
		public HslColor(IHsl hsl)
		{
			Ordinals = hsl.Ordinals;
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public HslColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator HslColor(Lab lab)
		{
			return new HslColor(lab);
		}

		public static implicit operator HslColor(LabColor lab)
		{
			return new HslColor(lab);
		}
		public HslColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator HslColor(Lch lch)
		{
			return new HslColor(lch);
		}

		public static implicit operator HslColor(LchColor lch)
		{
			return new HslColor(lch);
		}
		public HslColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator HslColor(Luv luv)
		{
			return new HslColor(luv);
		}

		public static implicit operator HslColor(LuvColor luv)
		{
			return new HslColor(luv);
		}
		public HslColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator HslColor(Yxy yxy)
		{
			return new HslColor(yxy);
		}

		public static implicit operator HslColor(YxyColor yxy)
		{
			return new HslColor(yxy);
		}
		public HslColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator HslColor(Cmy cmy)
		{
			return new HslColor(cmy);
		}

		public static implicit operator HslColor(CmyColor cmy)
		{
			return new HslColor(cmy);
		}
		public HslColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator HslColor(Cmyk cmyk)
		{
			return new HslColor(cmyk);
		}

		public static implicit operator HslColor(CmykColor cmyk)
		{
			return new HslColor(cmyk);
		}
		public HslColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator HslColor(Hsv hsv)
		{
			return new HslColor(hsv);
		}

		public static implicit operator HslColor(HsvColor hsv)
		{
			return new HslColor(hsv);
		}
		public HslColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator HslColor(Hsb hsb)
		{
			return new HslColor(hsb);
		}

		public static implicit operator HslColor(HsbColor hsb)
		{
			return new HslColor(hsb);
		}
		public HslColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator HslColor(HunterLab hunterlab)
		{
			return new HslColor(hunterlab);
		}

		public static implicit operator HslColor(HunterLabColor hunterlab)
		{
			return new HslColor(hunterlab);
		}
	}
	public partial class LabColor : PoshCode.Pansies.ColorSpaces.Lab
	{
		public LabColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public LabColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator LabColor(Rgb rgb)
		{
			return new LabColor(rgb);
		}

		public static implicit operator LabColor(RgbColor rgb)
		{
			return new LabColor(rgb);
		}
		public LabColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator LabColor(Xyz xyz)
		{
			return new LabColor(xyz);
		}

		public static implicit operator LabColor(XyzColor xyz)
		{
			return new LabColor(xyz);
		}
		public LabColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator LabColor(Hsl hsl)
		{
			return new LabColor(hsl);
		}

		public static implicit operator LabColor(HslColor hsl)
		{
			return new LabColor(hsl);
		}
		public LabColor(ILab lab)
		{
			Ordinals = lab.Ordinals;
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public LabColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator LabColor(Lch lch)
		{
			return new LabColor(lch);
		}

		public static implicit operator LabColor(LchColor lch)
		{
			return new LabColor(lch);
		}
		public LabColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator LabColor(Luv luv)
		{
			return new LabColor(luv);
		}

		public static implicit operator LabColor(LuvColor luv)
		{
			return new LabColor(luv);
		}
		public LabColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator LabColor(Yxy yxy)
		{
			return new LabColor(yxy);
		}

		public static implicit operator LabColor(YxyColor yxy)
		{
			return new LabColor(yxy);
		}
		public LabColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator LabColor(Cmy cmy)
		{
			return new LabColor(cmy);
		}

		public static implicit operator LabColor(CmyColor cmy)
		{
			return new LabColor(cmy);
		}
		public LabColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator LabColor(Cmyk cmyk)
		{
			return new LabColor(cmyk);
		}

		public static implicit operator LabColor(CmykColor cmyk)
		{
			return new LabColor(cmyk);
		}
		public LabColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator LabColor(Hsv hsv)
		{
			return new LabColor(hsv);
		}

		public static implicit operator LabColor(HsvColor hsv)
		{
			return new LabColor(hsv);
		}
		public LabColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator LabColor(Hsb hsb)
		{
			return new LabColor(hsb);
		}

		public static implicit operator LabColor(HsbColor hsb)
		{
			return new LabColor(hsb);
		}
		public LabColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator LabColor(HunterLab hunterlab)
		{
			return new LabColor(hunterlab);
		}

		public static implicit operator LabColor(HunterLabColor hunterlab)
		{
			return new LabColor(hunterlab);
		}
	}
	public partial class LchColor : PoshCode.Pansies.ColorSpaces.Lch
	{
		public LchColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public LchColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator LchColor(Rgb rgb)
		{
			return new LchColor(rgb);
		}

		public static implicit operator LchColor(RgbColor rgb)
		{
			return new LchColor(rgb);
		}
		public LchColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator LchColor(Xyz xyz)
		{
			return new LchColor(xyz);
		}

		public static implicit operator LchColor(XyzColor xyz)
		{
			return new LchColor(xyz);
		}
		public LchColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator LchColor(Hsl hsl)
		{
			return new LchColor(hsl);
		}

		public static implicit operator LchColor(HslColor hsl)
		{
			return new LchColor(hsl);
		}
		public LchColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator LchColor(Lab lab)
		{
			return new LchColor(lab);
		}

		public static implicit operator LchColor(LabColor lab)
		{
			return new LchColor(lab);
		}
		public LchColor(ILch lch)
		{
			Ordinals = lch.Ordinals;
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public LchColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator LchColor(Luv luv)
		{
			return new LchColor(luv);
		}

		public static implicit operator LchColor(LuvColor luv)
		{
			return new LchColor(luv);
		}
		public LchColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator LchColor(Yxy yxy)
		{
			return new LchColor(yxy);
		}

		public static implicit operator LchColor(YxyColor yxy)
		{
			return new LchColor(yxy);
		}
		public LchColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator LchColor(Cmy cmy)
		{
			return new LchColor(cmy);
		}

		public static implicit operator LchColor(CmyColor cmy)
		{
			return new LchColor(cmy);
		}
		public LchColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator LchColor(Cmyk cmyk)
		{
			return new LchColor(cmyk);
		}

		public static implicit operator LchColor(CmykColor cmyk)
		{
			return new LchColor(cmyk);
		}
		public LchColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator LchColor(Hsv hsv)
		{
			return new LchColor(hsv);
		}

		public static implicit operator LchColor(HsvColor hsv)
		{
			return new LchColor(hsv);
		}
		public LchColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator LchColor(Hsb hsb)
		{
			return new LchColor(hsb);
		}

		public static implicit operator LchColor(HsbColor hsb)
		{
			return new LchColor(hsb);
		}
		public LchColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator LchColor(HunterLab hunterlab)
		{
			return new LchColor(hunterlab);
		}

		public static implicit operator LchColor(HunterLabColor hunterlab)
		{
			return new LchColor(hunterlab);
		}
	}
	public partial class LuvColor : PoshCode.Pansies.ColorSpaces.Luv
	{
		public LuvColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public LuvColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator LuvColor(Rgb rgb)
		{
			return new LuvColor(rgb);
		}

		public static implicit operator LuvColor(RgbColor rgb)
		{
			return new LuvColor(rgb);
		}
		public LuvColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator LuvColor(Xyz xyz)
		{
			return new LuvColor(xyz);
		}

		public static implicit operator LuvColor(XyzColor xyz)
		{
			return new LuvColor(xyz);
		}
		public LuvColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator LuvColor(Hsl hsl)
		{
			return new LuvColor(hsl);
		}

		public static implicit operator LuvColor(HslColor hsl)
		{
			return new LuvColor(hsl);
		}
		public LuvColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator LuvColor(Lab lab)
		{
			return new LuvColor(lab);
		}

		public static implicit operator LuvColor(LabColor lab)
		{
			return new LuvColor(lab);
		}
		public LuvColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator LuvColor(Lch lch)
		{
			return new LuvColor(lch);
		}

		public static implicit operator LuvColor(LchColor lch)
		{
			return new LuvColor(lch);
		}
		public LuvColor(ILuv luv)
		{
			Ordinals = luv.Ordinals;
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public LuvColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator LuvColor(Yxy yxy)
		{
			return new LuvColor(yxy);
		}

		public static implicit operator LuvColor(YxyColor yxy)
		{
			return new LuvColor(yxy);
		}
		public LuvColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator LuvColor(Cmy cmy)
		{
			return new LuvColor(cmy);
		}

		public static implicit operator LuvColor(CmyColor cmy)
		{
			return new LuvColor(cmy);
		}
		public LuvColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator LuvColor(Cmyk cmyk)
		{
			return new LuvColor(cmyk);
		}

		public static implicit operator LuvColor(CmykColor cmyk)
		{
			return new LuvColor(cmyk);
		}
		public LuvColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator LuvColor(Hsv hsv)
		{
			return new LuvColor(hsv);
		}

		public static implicit operator LuvColor(HsvColor hsv)
		{
			return new LuvColor(hsv);
		}
		public LuvColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator LuvColor(Hsb hsb)
		{
			return new LuvColor(hsb);
		}

		public static implicit operator LuvColor(HsbColor hsb)
		{
			return new LuvColor(hsb);
		}
		public LuvColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator LuvColor(HunterLab hunterlab)
		{
			return new LuvColor(hunterlab);
		}

		public static implicit operator LuvColor(HunterLabColor hunterlab)
		{
			return new LuvColor(hunterlab);
		}
	}
	public partial class YxyColor : PoshCode.Pansies.ColorSpaces.Yxy
	{
		public YxyColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public YxyColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator YxyColor(Rgb rgb)
		{
			return new YxyColor(rgb);
		}

		public static implicit operator YxyColor(RgbColor rgb)
		{
			return new YxyColor(rgb);
		}
		public YxyColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator YxyColor(Xyz xyz)
		{
			return new YxyColor(xyz);
		}

		public static implicit operator YxyColor(XyzColor xyz)
		{
			return new YxyColor(xyz);
		}
		public YxyColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator YxyColor(Hsl hsl)
		{
			return new YxyColor(hsl);
		}

		public static implicit operator YxyColor(HslColor hsl)
		{
			return new YxyColor(hsl);
		}
		public YxyColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator YxyColor(Lab lab)
		{
			return new YxyColor(lab);
		}

		public static implicit operator YxyColor(LabColor lab)
		{
			return new YxyColor(lab);
		}
		public YxyColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator YxyColor(Lch lch)
		{
			return new YxyColor(lch);
		}

		public static implicit operator YxyColor(LchColor lch)
		{
			return new YxyColor(lch);
		}
		public YxyColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator YxyColor(Luv luv)
		{
			return new YxyColor(luv);
		}

		public static implicit operator YxyColor(LuvColor luv)
		{
			return new YxyColor(luv);
		}
		public YxyColor(IYxy yxy)
		{
			Ordinals = yxy.Ordinals;
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public YxyColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator YxyColor(Cmy cmy)
		{
			return new YxyColor(cmy);
		}

		public static implicit operator YxyColor(CmyColor cmy)
		{
			return new YxyColor(cmy);
		}
		public YxyColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator YxyColor(Cmyk cmyk)
		{
			return new YxyColor(cmyk);
		}

		public static implicit operator YxyColor(CmykColor cmyk)
		{
			return new YxyColor(cmyk);
		}
		public YxyColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator YxyColor(Hsv hsv)
		{
			return new YxyColor(hsv);
		}

		public static implicit operator YxyColor(HsvColor hsv)
		{
			return new YxyColor(hsv);
		}
		public YxyColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator YxyColor(Hsb hsb)
		{
			return new YxyColor(hsb);
		}

		public static implicit operator YxyColor(HsbColor hsb)
		{
			return new YxyColor(hsb);
		}
		public YxyColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator YxyColor(HunterLab hunterlab)
		{
			return new YxyColor(hunterlab);
		}

		public static implicit operator YxyColor(HunterLabColor hunterlab)
		{
			return new YxyColor(hunterlab);
		}
	}
	public partial class CmyColor : PoshCode.Pansies.ColorSpaces.Cmy
	{
		public CmyColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public CmyColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator CmyColor(Rgb rgb)
		{
			return new CmyColor(rgb);
		}

		public static implicit operator CmyColor(RgbColor rgb)
		{
			return new CmyColor(rgb);
		}
		public CmyColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator CmyColor(Xyz xyz)
		{
			return new CmyColor(xyz);
		}

		public static implicit operator CmyColor(XyzColor xyz)
		{
			return new CmyColor(xyz);
		}
		public CmyColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator CmyColor(Hsl hsl)
		{
			return new CmyColor(hsl);
		}

		public static implicit operator CmyColor(HslColor hsl)
		{
			return new CmyColor(hsl);
		}
		public CmyColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator CmyColor(Lab lab)
		{
			return new CmyColor(lab);
		}

		public static implicit operator CmyColor(LabColor lab)
		{
			return new CmyColor(lab);
		}
		public CmyColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator CmyColor(Lch lch)
		{
			return new CmyColor(lch);
		}

		public static implicit operator CmyColor(LchColor lch)
		{
			return new CmyColor(lch);
		}
		public CmyColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator CmyColor(Luv luv)
		{
			return new CmyColor(luv);
		}

		public static implicit operator CmyColor(LuvColor luv)
		{
			return new CmyColor(luv);
		}
		public CmyColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator CmyColor(Yxy yxy)
		{
			return new CmyColor(yxy);
		}

		public static implicit operator CmyColor(YxyColor yxy)
		{
			return new CmyColor(yxy);
		}
		public CmyColor(ICmy cmy)
		{
			Ordinals = cmy.Ordinals;
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public CmyColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator CmyColor(Cmyk cmyk)
		{
			return new CmyColor(cmyk);
		}

		public static implicit operator CmyColor(CmykColor cmyk)
		{
			return new CmyColor(cmyk);
		}
		public CmyColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator CmyColor(Hsv hsv)
		{
			return new CmyColor(hsv);
		}

		public static implicit operator CmyColor(HsvColor hsv)
		{
			return new CmyColor(hsv);
		}
		public CmyColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator CmyColor(Hsb hsb)
		{
			return new CmyColor(hsb);
		}

		public static implicit operator CmyColor(HsbColor hsb)
		{
			return new CmyColor(hsb);
		}
		public CmyColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator CmyColor(HunterLab hunterlab)
		{
			return new CmyColor(hunterlab);
		}

		public static implicit operator CmyColor(HunterLabColor hunterlab)
		{
			return new CmyColor(hunterlab);
		}
	}
	public partial class CmykColor : PoshCode.Pansies.ColorSpaces.Cmyk
	{
		public CmykColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public CmykColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator CmykColor(Rgb rgb)
		{
			return new CmykColor(rgb);
		}

		public static implicit operator CmykColor(RgbColor rgb)
		{
			return new CmykColor(rgb);
		}
		public CmykColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator CmykColor(Xyz xyz)
		{
			return new CmykColor(xyz);
		}

		public static implicit operator CmykColor(XyzColor xyz)
		{
			return new CmykColor(xyz);
		}
		public CmykColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator CmykColor(Hsl hsl)
		{
			return new CmykColor(hsl);
		}

		public static implicit operator CmykColor(HslColor hsl)
		{
			return new CmykColor(hsl);
		}
		public CmykColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator CmykColor(Lab lab)
		{
			return new CmykColor(lab);
		}

		public static implicit operator CmykColor(LabColor lab)
		{
			return new CmykColor(lab);
		}
		public CmykColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator CmykColor(Lch lch)
		{
			return new CmykColor(lch);
		}

		public static implicit operator CmykColor(LchColor lch)
		{
			return new CmykColor(lch);
		}
		public CmykColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator CmykColor(Luv luv)
		{
			return new CmykColor(luv);
		}

		public static implicit operator CmykColor(LuvColor luv)
		{
			return new CmykColor(luv);
		}
		public CmykColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator CmykColor(Yxy yxy)
		{
			return new CmykColor(yxy);
		}

		public static implicit operator CmykColor(YxyColor yxy)
		{
			return new CmykColor(yxy);
		}
		public CmykColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator CmykColor(Cmy cmy)
		{
			return new CmykColor(cmy);
		}

		public static implicit operator CmykColor(CmyColor cmy)
		{
			return new CmykColor(cmy);
		}
		public CmykColor(ICmyk cmyk)
		{
			Ordinals = cmyk.Ordinals;
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public CmykColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator CmykColor(Hsv hsv)
		{
			return new CmykColor(hsv);
		}

		public static implicit operator CmykColor(HsvColor hsv)
		{
			return new CmykColor(hsv);
		}
		public CmykColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator CmykColor(Hsb hsb)
		{
			return new CmykColor(hsb);
		}

		public static implicit operator CmykColor(HsbColor hsb)
		{
			return new CmykColor(hsb);
		}
		public CmykColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator CmykColor(HunterLab hunterlab)
		{
			return new CmykColor(hunterlab);
		}

		public static implicit operator CmykColor(HunterLabColor hunterlab)
		{
			return new CmykColor(hunterlab);
		}
	}
	public partial class HsvColor : PoshCode.Pansies.ColorSpaces.Hsv
	{
		public HsvColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public HsvColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator HsvColor(Rgb rgb)
		{
			return new HsvColor(rgb);
		}

		public static implicit operator HsvColor(RgbColor rgb)
		{
			return new HsvColor(rgb);
		}
		public HsvColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator HsvColor(Xyz xyz)
		{
			return new HsvColor(xyz);
		}

		public static implicit operator HsvColor(XyzColor xyz)
		{
			return new HsvColor(xyz);
		}
		public HsvColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator HsvColor(Hsl hsl)
		{
			return new HsvColor(hsl);
		}

		public static implicit operator HsvColor(HslColor hsl)
		{
			return new HsvColor(hsl);
		}
		public HsvColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator HsvColor(Lab lab)
		{
			return new HsvColor(lab);
		}

		public static implicit operator HsvColor(LabColor lab)
		{
			return new HsvColor(lab);
		}
		public HsvColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator HsvColor(Lch lch)
		{
			return new HsvColor(lch);
		}

		public static implicit operator HsvColor(LchColor lch)
		{
			return new HsvColor(lch);
		}
		public HsvColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator HsvColor(Luv luv)
		{
			return new HsvColor(luv);
		}

		public static implicit operator HsvColor(LuvColor luv)
		{
			return new HsvColor(luv);
		}
		public HsvColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator HsvColor(Yxy yxy)
		{
			return new HsvColor(yxy);
		}

		public static implicit operator HsvColor(YxyColor yxy)
		{
			return new HsvColor(yxy);
		}
		public HsvColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator HsvColor(Cmy cmy)
		{
			return new HsvColor(cmy);
		}

		public static implicit operator HsvColor(CmyColor cmy)
		{
			return new HsvColor(cmy);
		}
		public HsvColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator HsvColor(Cmyk cmyk)
		{
			return new HsvColor(cmyk);
		}

		public static implicit operator HsvColor(CmykColor cmyk)
		{
			return new HsvColor(cmyk);
		}
		public HsvColor(IHsv hsv)
		{
			Ordinals = hsv.Ordinals;
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public HsvColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator HsvColor(Hsb hsb)
		{
			return new HsvColor(hsb);
		}

		public static implicit operator HsvColor(HsbColor hsb)
		{
			return new HsvColor(hsb);
		}
		public HsvColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator HsvColor(HunterLab hunterlab)
		{
			return new HsvColor(hunterlab);
		}

		public static implicit operator HsvColor(HunterLabColor hunterlab)
		{
			return new HsvColor(hunterlab);
		}
	}
	public partial class HsbColor : PoshCode.Pansies.ColorSpaces.Hsb
	{
		public HsbColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public HsbColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator HsbColor(Rgb rgb)
		{
			return new HsbColor(rgb);
		}

		public static implicit operator HsbColor(RgbColor rgb)
		{
			return new HsbColor(rgb);
		}
		public HsbColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator HsbColor(Xyz xyz)
		{
			return new HsbColor(xyz);
		}

		public static implicit operator HsbColor(XyzColor xyz)
		{
			return new HsbColor(xyz);
		}
		public HsbColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator HsbColor(Hsl hsl)
		{
			return new HsbColor(hsl);
		}

		public static implicit operator HsbColor(HslColor hsl)
		{
			return new HsbColor(hsl);
		}
		public HsbColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator HsbColor(Lab lab)
		{
			return new HsbColor(lab);
		}

		public static implicit operator HsbColor(LabColor lab)
		{
			return new HsbColor(lab);
		}
		public HsbColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator HsbColor(Lch lch)
		{
			return new HsbColor(lch);
		}

		public static implicit operator HsbColor(LchColor lch)
		{
			return new HsbColor(lch);
		}
		public HsbColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator HsbColor(Luv luv)
		{
			return new HsbColor(luv);
		}

		public static implicit operator HsbColor(LuvColor luv)
		{
			return new HsbColor(luv);
		}
		public HsbColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator HsbColor(Yxy yxy)
		{
			return new HsbColor(yxy);
		}

		public static implicit operator HsbColor(YxyColor yxy)
		{
			return new HsbColor(yxy);
		}
		public HsbColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator HsbColor(Cmy cmy)
		{
			return new HsbColor(cmy);
		}

		public static implicit operator HsbColor(CmyColor cmy)
		{
			return new HsbColor(cmy);
		}
		public HsbColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator HsbColor(Cmyk cmyk)
		{
			return new HsbColor(cmyk);
		}

		public static implicit operator HsbColor(CmykColor cmyk)
		{
			return new HsbColor(cmyk);
		}
		public HsbColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator HsbColor(Hsv hsv)
		{
			return new HsbColor(hsv);
		}

		public static implicit operator HsbColor(HsvColor hsv)
		{
			return new HsbColor(hsv);
		}
		public HsbColor(IHsb hsb)
		{
			Ordinals = hsb.Ordinals;
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public HsbColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
		public static implicit operator HsbColor(HunterLab hunterlab)
		{
			return new HsbColor(hunterlab);
		}

		public static implicit operator HsbColor(HunterLabColor hunterlab)
		{
			return new HsbColor(hunterlab);
		}
	}
	public partial class HunterLabColor : PoshCode.Pansies.ColorSpaces.HunterLab
	{
		public HunterLabColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability
		public HunterLabColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public static implicit operator HunterLabColor(Rgb rgb)
		{
			return new HunterLabColor(rgb);
		}

		public static implicit operator HunterLabColor(RgbColor rgb)
		{
			return new HunterLabColor(rgb);
		}
		public HunterLabColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

		public IXyz ToXyz()
		{
			return new XyzColor(this);
		}
		public static implicit operator HunterLabColor(Xyz xyz)
		{
			return new HunterLabColor(xyz);
		}

		public static implicit operator HunterLabColor(XyzColor xyz)
		{
			return new HunterLabColor(xyz);
		}
		public HunterLabColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

		public IHsl ToHsl()
		{
			return new HslColor(this);
		}
		public static implicit operator HunterLabColor(Hsl hsl)
		{
			return new HunterLabColor(hsl);
		}

		public static implicit operator HunterLabColor(HslColor hsl)
		{
			return new HunterLabColor(hsl);
		}
		public HunterLabColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

		public ILab ToLab()
		{
			return new LabColor(this);
		}
		public static implicit operator HunterLabColor(Lab lab)
		{
			return new HunterLabColor(lab);
		}

		public static implicit operator HunterLabColor(LabColor lab)
		{
			return new HunterLabColor(lab);
		}
		public HunterLabColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

		public ILch ToLch()
		{
			return new LchColor(this);
		}
		public static implicit operator HunterLabColor(Lch lch)
		{
			return new HunterLabColor(lch);
		}

		public static implicit operator HunterLabColor(LchColor lch)
		{
			return new HunterLabColor(lch);
		}
		public HunterLabColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

		public ILuv ToLuv()
		{
			return new LuvColor(this);
		}
		public static implicit operator HunterLabColor(Luv luv)
		{
			return new HunterLabColor(luv);
		}

		public static implicit operator HunterLabColor(LuvColor luv)
		{
			return new HunterLabColor(luv);
		}
		public HunterLabColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

		public IYxy ToYxy()
		{
			return new YxyColor(this);
		}
		public static implicit operator HunterLabColor(Yxy yxy)
		{
			return new HunterLabColor(yxy);
		}

		public static implicit operator HunterLabColor(YxyColor yxy)
		{
			return new HunterLabColor(yxy);
		}
		public HunterLabColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

		public ICmy ToCmy()
		{
			return new CmyColor(this);
		}
		public static implicit operator HunterLabColor(Cmy cmy)
		{
			return new HunterLabColor(cmy);
		}

		public static implicit operator HunterLabColor(CmyColor cmy)
		{
			return new HunterLabColor(cmy);
		}
		public HunterLabColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

		public ICmyk ToCmyk()
		{
			return new CmykColor(this);
		}
		public static implicit operator HunterLabColor(Cmyk cmyk)
		{
			return new HunterLabColor(cmyk);
		}

		public static implicit operator HunterLabColor(CmykColor cmyk)
		{
			return new HunterLabColor(cmyk);
		}
		public HunterLabColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

		public IHsv ToHsv()
		{
			return new HsvColor(this);
		}
		public static implicit operator HunterLabColor(Hsv hsv)
		{
			return new HunterLabColor(hsv);
		}

		public static implicit operator HunterLabColor(HsvColor hsv)
		{
			return new HunterLabColor(hsv);
		}
		public HunterLabColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

		public IHsb ToHsb()
		{
			return new HsbColor(this);
		}
		public static implicit operator HunterLabColor(Hsb hsb)
		{
			return new HunterLabColor(hsb);
		}

		public static implicit operator HunterLabColor(HsbColor hsb)
		{
			return new HunterLabColor(hsb);
		}
		public HunterLabColor(IHunterLab hunterlab)
		{
			Ordinals = hunterlab.Ordinals;
		}

		public IHunterLab ToHunterLab()
		{
			return new HunterLabColor(this);
		}
	}
}
