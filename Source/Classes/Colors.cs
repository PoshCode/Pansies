// Note: This is a generated file.

 
using ColorMine.ColorSpaces.Conversions;
using ColorMine.ColorSpaces;

namespace PoshCode.Pansies
{
    public partial class RgbColor : ColorMine.ColorSpaces.Rgb
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
		public RgbColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
		}
		public RgbColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
		}
		public RgbColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
		}
		public RgbColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
		}
		public RgbColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
		}
		public RgbColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
		}
		public RgbColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
		}
		public RgbColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
		}
		public RgbColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
		}
		public RgbColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class XyzColor : ColorMine.ColorSpaces.Xyz
    {
		public XyzColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public XyzColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
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
		public XyzColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
		}
		public XyzColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
		}
		public XyzColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
		}
		public XyzColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
		}
		public XyzColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
		}
		public XyzColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
		}
		public XyzColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
		}
		public XyzColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
		}
		public XyzColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class HslColor : ColorMine.ColorSpaces.Hsl
    {
		public HslColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public HslColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public HslColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
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
		public HslColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
		}
		public HslColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
		}
		public HslColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
		}
		public HslColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
		}
		public HslColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
		}
		public HslColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
		}
		public HslColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
		}
		public HslColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class LabColor : ColorMine.ColorSpaces.Lab
    {
		public LabColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public LabColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public LabColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
		}
		public LabColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
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
		public LabColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
		}
		public LabColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
		}
		public LabColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
		}
		public LabColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
		}
		public LabColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
		}
		public LabColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
		}
		public LabColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class LchColor : ColorMine.ColorSpaces.Lch
    {
		public LchColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public LchColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public LchColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
		}
		public LchColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
		}
		public LchColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
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
		public LchColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
		}
		public LchColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
		}
		public LchColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
		}
		public LchColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
		}
		public LchColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
		}
		public LchColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class LuvColor : ColorMine.ColorSpaces.Luv
    {
		public LuvColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public LuvColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public LuvColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
		}
		public LuvColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
		}
		public LuvColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
		}
		public LuvColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
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
		public LuvColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
		}
		public LuvColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
		}
		public LuvColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
		}
		public LuvColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
		}
		public LuvColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class YxyColor : ColorMine.ColorSpaces.Yxy
    {
		public YxyColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public YxyColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public YxyColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
		}
		public YxyColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
		}
		public YxyColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
		}
		public YxyColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
		}
		public YxyColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
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
		public YxyColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
		}
		public YxyColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
		}
		public YxyColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
		}
		public YxyColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class CmyColor : ColorMine.ColorSpaces.Cmy
    {
		public CmyColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public CmyColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public CmyColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
		}
		public CmyColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
		}
		public CmyColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
		}
		public CmyColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
		}
		public CmyColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
		}
		public CmyColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
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
		public CmyColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
		}
		public CmyColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
		}
		public CmyColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class CmykColor : ColorMine.ColorSpaces.Cmyk
    {
		public CmykColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public CmykColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public CmykColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
		}
		public CmykColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
		}
		public CmykColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
		}
		public CmykColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
		}
		public CmykColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
		}
		public CmykColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
		}
		public CmykColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
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
		public CmykColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
		}
		public CmykColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class HsvColor : ColorMine.ColorSpaces.Hsv
    {
		public HsvColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public HsvColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public HsvColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
		}
		public HsvColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
		}
		public HsvColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
		}
		public HsvColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
		}
		public HsvColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
		}
		public HsvColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
		}
		public HsvColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
		}
		public HsvColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
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
		public HsvColor(IHunterLab hunterlab)
		{
			Initialize(hunterlab.ToRgb());
		}

        public IHunterLab ToHunterLab()
		{
            return new HunterLabColor(this);
		}
    }

    public partial class HsbColor : ColorMine.ColorSpaces.Hsb
    {
		public HsbColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public HsbColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public HsbColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
		}
		public HsbColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
		}
		public HsbColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
		}
		public HsbColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
		}
		public HsbColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
		}
		public HsbColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
		}
		public HsbColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
		}
		public HsbColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
		}
		public HsbColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
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
    }

    public partial class HunterLabColor : ColorMine.ColorSpaces.HunterLab
    {
		public HunterLabColor(){}

		// IColorSpace means they have To<IColorSpace>() and Initialize(IRgb)
		// But PowerShell doesn't handle generic methods properly
		// And we want cast capability

		public HunterLabColor(IRgb rgb)
		{
			Initialize(rgb.ToRgb());
		}

		public HunterLabColor(IXyz xyz)
		{
			Initialize(xyz.ToRgb());
		}

        public IXyz ToXyz()
		{
            return new XyzColor(this);
		}
		public HunterLabColor(IHsl hsl)
		{
			Initialize(hsl.ToRgb());
		}

        public IHsl ToHsl()
		{
            return new HslColor(this);
		}
		public HunterLabColor(ILab lab)
		{
			Initialize(lab.ToRgb());
		}

        public ILab ToLab()
		{
            return new LabColor(this);
		}
		public HunterLabColor(ILch lch)
		{
			Initialize(lch.ToRgb());
		}

        public ILch ToLch()
		{
            return new LchColor(this);
		}
		public HunterLabColor(ILuv luv)
		{
			Initialize(luv.ToRgb());
		}

        public ILuv ToLuv()
		{
            return new LuvColor(this);
		}
		public HunterLabColor(IYxy yxy)
		{
			Initialize(yxy.ToRgb());
		}

        public IYxy ToYxy()
		{
            return new YxyColor(this);
		}
		public HunterLabColor(ICmy cmy)
		{
			Initialize(cmy.ToRgb());
		}

        public ICmy ToCmy()
		{
            return new CmyColor(this);
		}
		public HunterLabColor(ICmyk cmyk)
		{
			Initialize(cmyk.ToRgb());
		}

        public ICmyk ToCmyk()
		{
            return new CmykColor(this);
		}
		public HunterLabColor(IHsv hsv)
		{
			Initialize(hsv.ToRgb());
		}

        public IHsv ToHsv()
		{
            return new HsvColor(this);
		}
		public HunterLabColor(IHsb hsb)
		{
			Initialize(hsb.ToRgb());
		}

        public IHsb ToHsb()
		{
            return new HsbColor(this);
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