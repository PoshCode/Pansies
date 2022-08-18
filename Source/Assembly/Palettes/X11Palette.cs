using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using PoshCode.Pansies.ColorSpaces.Comparisons;
using PoshCode.Pansies.Palettes;

namespace PoshCode.Pansies.Palettes
{
    public enum X11ColorName
    {
        AliceBlue,
        AntiqueWhite,
        AntiqueWhite1,
        AntiqueWhite2,
        AntiqueWhite3,
        AntiqueWhite4,
        Aquamarine,
        Aquamarine1,
        Aquamarine2,
        Aquamarine3,
        Aquamarine4,
        Azure,
        Azure1,
        Azure2,
        Azure3,
        Azure4,
        Beige,
        Bisque,
        Bisque1,
        Bisque2,
        Bisque3,
        Bisque4,
        Black,
        BlanchedAlmond,
        Blue,
        Blue1,
        Blue2,
        Blue3,
        Blue4,
        BlueViolet,
        Brown,
        Brown1,
        Brown2,
        Brown3,
        Brown4,
        Burlywood,
        Burlywood1,
        Burlywood2,
        Burlywood3,
        Burlywood4,
        CadetBlue,
        CadetBlue1,
        CadetBlue2,
        CadetBlue3,
        CadetBlue4,
        Chartreuse,
        Chartreuse1,
        Chartreuse2,
        Chartreuse3,
        Chartreuse4,
        Chocolate,
        Chocolate1,
        Chocolate2,
        Chocolate3,
        Chocolate4,
        Coral,
        Coral1,
        Coral2,
        Coral3,
        Coral4,
        CornflowerBlue,
        Cornsilk,
        Cornsilk1,
        Cornsilk2,
        Cornsilk3,
        Cornsilk4,
        Cyan,
        Cyan1,
        Cyan2,
        Cyan3,
        Cyan4,
        DarkBlue,
        DarkCyan,
        DarkGoldenrod,
        DarkGoldenrod1,
        DarkGoldenrod2,
        DarkGoldenrod3,
        DarkGoldenrod4,
        DarkGray,
        DarkGreen,
        DarkGrey,
        DarkKhaki,
        DarkMagenta,
        DarkOliveGreen,
        DarkOliveGreen1,
        DarkOliveGreen2,
        DarkOliveGreen3,
        DarkOliveGreen4,
        DarkOrange,
        DarkOrange1,
        DarkOrange2,
        DarkOrange3,
        DarkOrange4,
        DarkOrchid,
        DarkOrchid1,
        DarkOrchid2,
        DarkOrchid3,
        DarkOrchid4,
        DarkRed,
        DarkSalmon,
        DarkSeaGreen,
        DarkSeaGreen1,
        DarkSeaGreen2,
        DarkSeaGreen3,
        DarkSeaGreen4,
        DarkSlateBlue,
        DarkSlateGray,
        DarkSlateGray1,
        DarkSlateGray2,
        DarkSlateGray3,
        DarkSlateGray4,
        DarkSlateGrey,
        DarkTurquoise,
        DarkViolet,
        DarkYellow,
        DeepPink,
        DeepPink1,
        DeepPink2,
        DeepPink3,
        DeepPink4,
        DeepSkyBlue,
        DeepSkyBlue1,
        DeepSkyBlue2,
        DeepSkyBlue3,
        DeepSkyBlue4,
        DimGray,
        DimGrey,
        DodgerBlue,
        DodgerBlue1,
        DodgerBlue2,
        DodgerBlue3,
        DodgerBlue4,
        Firebrick,
        Firebrick1,
        Firebrick2,
        Firebrick3,
        Firebrick4,
        FloralWhite,
        ForestGreen,
        Gainsboro,
        GhostWhite,
        Gold,
        Gold1,
        Gold2,
        Gold3,
        Gold4,
        Goldenrod,
        Goldenrod1,
        Goldenrod2,
        Goldenrod3,
        Goldenrod4,
        Gray,
        Gray0,
        Gray1,
        Gray2,
        Gray3,
        Gray4,
        Gray5,
        Gray6,
        Gray7,
        Gray8,
        Gray9,
        Gray10,
        Gray11,
        Gray12,
        Gray13,
        Gray14,
        Gray15,
        Gray16,
        Gray17,
        Gray18,
        Gray19,
        Gray20,
        Gray21,
        Gray22,
        Gray23,
        Gray24,
        Gray25,
        Gray26,
        Gray27,
        Gray28,
        Gray29,
        Gray30,
        Gray31,
        Gray32,
        Gray33,
        Gray34,
        Gray35,
        Gray36,
        Gray37,
        Gray38,
        Gray39,
        Gray40,
        Gray41,
        Gray42,
        Gray43,
        Gray44,
        Gray45,
        Gray46,
        Gray47,
        Gray48,
        Gray49,
        Gray50,
        Gray51,
        Gray52,
        Gray53,
        Gray54,
        Gray55,
        Gray56,
        Gray57,
        Gray58,
        Gray59,
        Gray60,
        Gray61,
        Gray62,
        Gray63,
        Gray64,
        Gray65,
        Gray66,
        Gray67,
        Gray68,
        Gray69,
        Gray70,
        Gray71,
        Gray72,
        Gray73,
        Gray74,
        Gray75,
        Gray76,
        Gray77,
        Gray78,
        Gray79,
        Gray80,
        Gray81,
        Gray82,
        Gray83,
        Gray84,
        Gray85,
        Gray86,
        Gray87,
        Gray88,
        Gray89,
        Gray90,
        Gray91,
        Gray92,
        Gray93,
        Gray94,
        Gray95,
        Gray96,
        Gray97,
        Gray98,
        Gray99,
        Gray100,
        Green,
        Green1,
        Green2,
        Green3,
        Green4,
        GreenYellow,
        Grey,
        Grey0,
        Grey1,
        Grey2,
        Grey3,
        Grey4,
        Grey5,
        Grey6,
        Grey7,
        Grey8,
        Grey9,
        Grey10,
        Grey11,
        Grey12,
        Grey13,
        Grey14,
        Grey15,
        Grey16,
        Grey17,
        Grey18,
        Grey19,
        Grey20,
        Grey21,
        Grey22,
        Grey23,
        Grey24,
        Grey25,
        Grey26,
        Grey27,
        Grey28,
        Grey29,
        Grey30,
        Grey31,
        Grey32,
        Grey33,
        Grey34,
        Grey35,
        Grey36,
        Grey37,
        Grey38,
        Grey39,
        Grey40,
        Grey41,
        Grey42,
        Grey43,
        Grey44,
        Grey45,
        Grey46,
        Grey47,
        Grey48,
        Grey49,
        Grey50,
        Grey51,
        Grey52,
        Grey53,
        Grey54,
        Grey55,
        Grey56,
        Grey57,
        Grey58,
        Grey59,
        Grey60,
        Grey61,
        Grey62,
        Grey63,
        Grey64,
        Grey65,
        Grey66,
        Grey67,
        Grey68,
        Grey69,
        Grey70,
        Grey71,
        Grey72,
        Grey73,
        Grey74,
        Grey75,
        Grey76,
        Grey77,
        Grey78,
        Grey79,
        Grey80,
        Grey81,
        Grey82,
        Grey83,
        Grey84,
        Grey85,
        Grey86,
        Grey87,
        Grey88,
        Grey89,
        Grey90,
        Grey91,
        Grey92,
        Grey93,
        Grey94,
        Grey95,
        Grey96,
        Grey97,
        Grey98,
        Grey99,
        Grey100,
        Honeydew,
        Honeydew1,
        Honeydew2,
        Honeydew3,
        Honeydew4,
        HotPink,
        HotPink1,
        HotPink2,
        HotPink3,
        HotPink4,
        IndianRed,
        IndianRed1,
        IndianRed2,
        IndianRed3,
        IndianRed4,
        Ivory,
        Ivory1,
        Ivory2,
        Ivory3,
        Ivory4,
        Khaki,
        Khaki1,
        Khaki2,
        Khaki3,
        Khaki4,
        Lavender,
        LavenderBlush,
        LavenderBlush1,
        LavenderBlush2,
        LavenderBlush3,
        LavenderBlush4,
        LawnGreen,
        LemonChiffon,
        LemonChiffon1,
        LemonChiffon2,
        LemonChiffon3,
        LemonChiffon4,
        LightBlue,
        LightBlue1,
        LightBlue2,
        LightBlue3,
        LightBlue4,
        LightCoral,
        LightCyan,
        LightCyan1,
        LightCyan2,
        LightCyan3,
        LightCyan4,
        LightGoldenrod,
        LightGoldenrod1,
        LightGoldenrod2,
        LightGoldenrod3,
        LightGoldenrod4,
        LightGoldenrodYellow,
        LightGray,
        LightGreen,
        LightGrey,
        LightPink,
        LightPink1,
        LightPink2,
        LightPink3,
        LightPink4,
        LightSalmon,
        LightSalmon1,
        LightSalmon2,
        LightSalmon3,
        LightSalmon4,
        LightSeaGreen,
        LightSkyBlue,
        LightSkyBlue1,
        LightSkyBlue2,
        LightSkyBlue3,
        LightSkyBlue4,
        LightSlateBlue,
        LightSlateGray,
        LightSlateGrey,
        LightSteelBlue,
        LightSteelBlue1,
        LightSteelBlue2,
        LightSteelBlue3,
        LightSteelBlue4,
        LightYellow,
        LightYellow1,
        LightYellow2,
        LightYellow3,
        LightYellow4,
        LimeGreen,
        Linen,
        Magenta,
        Magenta1,
        Magenta2,
        Magenta3,
        Magenta4,
        Maroon,
        Maroon1,
        Maroon2,
        Maroon3,
        Maroon4,
        MediumAquamarine,
        MediumBlue,
        MediumOrchid,
        MediumOrchid1,
        MediumOrchid2,
        MediumOrchid3,
        MediumOrchid4,
        MediumPurple,
        MediumPurple1,
        MediumPurple2,
        MediumPurple3,
        MediumPurple4,
        MediumSeaGreen,
        MediumSlateBlue,
        MediumSpringGreen,
        MediumTurquoise,
        MediumVioletRed,
        MidnightBlue,
        MintCream,
        MistyRose,
        MistyRose1,
        MistyRose2,
        MistyRose3,
        MistyRose4,
        Moccasin,
        NavajoWhite,
        NavajoWhite1,
        NavajoWhite2,
        NavajoWhite3,
        NavajoWhite4,
        Navy,
        NavyBlue,
        OldLace,
        OliveDrab,
        OliveDrab1,
        OliveDrab2,
        OliveDrab3,
        OliveDrab4,
        Orange,
        Orange1,
        Orange2,
        Orange3,
        Orange4,
        OrangeRed,
        OrangeRed1,
        OrangeRed2,
        OrangeRed3,
        OrangeRed4,
        Orchid,
        Orchid1,
        Orchid2,
        Orchid3,
        Orchid4,
        PaleGoldenrod,
        PaleGreen,
        PaleGreen1,
        PaleGreen2,
        PaleGreen3,
        PaleGreen4,
        PaleTurquoise,
        PaleTurquoise1,
        PaleTurquoise2,
        PaleTurquoise3,
        PaleTurquoise4,
        PaleVioletRed,
        PaleVioletRed1,
        PaleVioletRed2,
        PaleVioletRed3,
        PaleVioletRed4,
        PapayaWhip,
        PeachPuff,
        PeachPuff1,
        PeachPuff2,
        PeachPuff3,
        PeachPuff4,
        Peru,
        Pink,
        Pink1,
        Pink2,
        Pink3,
        Pink4,
        Plum,
        Plum1,
        Plum2,
        Plum3,
        Plum4,
        PowderBlue,
        Purple,
        Purple1,
        Purple2,
        Purple3,
        Purple4,
        Red,
        Red1,
        Red2,
        Red3,
        Red4,
        RosyBrown,
        RosyBrown1,
        RosyBrown2,
        RosyBrown3,
        RosyBrown4,
        RoyalBlue,
        RoyalBlue1,
        RoyalBlue2,
        RoyalBlue3,
        RoyalBlue4,
        SaddleBrown,
        Salmon,
        Salmon1,
        Salmon2,
        Salmon3,
        Salmon4,
        SandyBrown,
        SeaGreen,
        SeaGreen1,
        SeaGreen2,
        SeaGreen3,
        SeaGreen4,
        Seashell,
        Seashell1,
        Seashell2,
        Seashell3,
        Seashell4,
        Sienna,
        Sienna1,
        Sienna2,
        Sienna3,
        Sienna4,
        SkyBlue,
        SkyBlue1,
        SkyBlue2,
        SkyBlue3,
        SkyBlue4,
        SlateBlue,
        SlateBlue1,
        SlateBlue2,
        SlateBlue3,
        SlateBlue4,
        SlateGray,
        SlateGray1,
        SlateGray2,
        SlateGray3,
        SlateGray4,
        SlateGrey,
        Snow,
        Snow1,
        Snow2,
        Snow3,
        Snow4,
        SpringGreen,
        SpringGreen1,
        SpringGreen2,
        SpringGreen3,
        SpringGreen4,
        SteelBlue,
        SteelBlue1,
        SteelBlue2,
        SteelBlue3,
        SteelBlue4,
        Tan,
        Tan1,
        Tan2,
        Tan3,
        Tan4,
        Thistle,
        Thistle1,
        Thistle2,
        Thistle3,
        Thistle4,
        Tomato,
        Tomato1,
        Tomato2,
        Tomato3,
        Tomato4,
        Turquoise,
        Turquoise1,
        Turquoise2,
        Turquoise3,
        Turquoise4,
        Violet,
        VioletRed,
        VioletRed1,
        VioletRed2,
        VioletRed3,
        VioletRed4,
        Wheat,
        Wheat1,
        Wheat2,
        Wheat3,
        Wheat4,
        White,
        WhiteSmoke,
        Yellow,
        Yellow1,
        Yellow2,
        Yellow3,
        Yellow4,
        YellowGreen
    }

    public class X11Palette : Palette<RgbColor>, IArgumentCompleter
    {
        public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            WildcardPattern wildcard = new WildcardPattern(wordToComplete + "*", WildcardOptions.IgnoreCase);

            foreach (var name in typeof(X11ColorName).GetEnumValues().Cast<X11ColorName>().Where(s => wildcard.IsMatch(s.ToString())))
            {
                yield return new CompletionResult(name.ToString(), this[(int)name].ToVt(true) + " \u001B[0m " + name.ToString(), CompletionResultType.ParameterValue, name.ToString());
            }
        }

        public override IColorSpaceComparison ComparisonAlgorithm { get; set; } = new CieDe2000Comparison();

        public X11Palette()
        {
            Add(new RgbColor { R = 240, G = 248, B = 255 }); // AliceBlue
            Add(new RgbColor { R = 250, G = 235, B = 215 }); // AntiqueWhite
            Add(new RgbColor { R = 255, G = 239, B = 219 }); // AntiqueWhite1
            Add(new RgbColor { R = 238, G = 223, B = 204 }); // AntiqueWhite2
            Add(new RgbColor { R = 205, G = 192, B = 176 }); // AntiqueWhite3
            Add(new RgbColor { R = 139, G = 131, B = 120 }); // AntiqueWhite4
            Add(new RgbColor { R = 127, G = 255, B = 212 }); // Aquamarine
            Add(new RgbColor { R = 127, G = 255, B = 212 }); // Aquamarine1
            Add(new RgbColor { R = 118, G = 238, B = 198 }); // Aquamarine2
            Add(new RgbColor { R = 102, G = 205, B = 170 }); // Aquamarine3
            Add(new RgbColor { R =  69, G = 139, B = 116 }); // Aquamarine4
            Add(new RgbColor { R = 240, G = 255, B = 255 }); // Azure
            Add(new RgbColor { R = 240, G = 255, B = 255 }); // Azure1
            Add(new RgbColor { R = 224, G = 238, B = 238 }); // Azure2
            Add(new RgbColor { R = 193, G = 205, B = 205 }); // Azure3
            Add(new RgbColor { R = 131, G = 139, B = 139 }); // Azure4
            Add(new RgbColor { R = 245, G = 245, B = 220 }); // Beige
            Add(new RgbColor { R = 255, G = 228, B = 196 }); // Bisque
            Add(new RgbColor { R = 255, G = 228, B = 196 }); // Bisque1
            Add(new RgbColor { R = 238, G = 213, B = 183 }); // Bisque2
            Add(new RgbColor { R = 205, G = 183, B = 158 }); // Bisque3
            Add(new RgbColor { R = 139, G = 125, B = 107 }); // Bisque4
            Add(new RgbColor { R =   0, G =   0, B =   0 }); // Black
            Add(new RgbColor { R = 255, G = 235, B = 205 }); // BlanchedAlmond
            Add(new RgbColor { R =   0, G =   0, B = 255 }); // Blue
            Add(new RgbColor { R =   0, G =   0, B = 255 }); // Blue1
            Add(new RgbColor { R =   0, G =   0, B = 238 }); // Blue2
            Add(new RgbColor { R =   0, G =   0, B = 205 }); // Blue3
            Add(new RgbColor { R =   0, G =   0, B = 139 }); // Blue4
            Add(new RgbColor { R = 138, G =  43, B = 226 }); // BlueViolet
            Add(new RgbColor { R = 165, G =  42, B =  42 }); // Brown
            Add(new RgbColor { R = 255, G =  64, B =  64 }); // Brown1
            Add(new RgbColor { R = 238, G =  59, B =  59 }); // Brown2
            Add(new RgbColor { R = 205, G =  51, B =  51 }); // Brown3
            Add(new RgbColor { R = 139, G =  35, B =  35 }); // Brown4
            Add(new RgbColor { R = 222, G = 184, B = 135 }); // Burlywood
            Add(new RgbColor { R = 255, G = 211, B = 155 }); // Burlywood1
            Add(new RgbColor { R = 238, G = 197, B = 145 }); // Burlywood2
            Add(new RgbColor { R = 205, G = 170, B = 125 }); // Burlywood3
            Add(new RgbColor { R = 139, G = 115, B =  85 }); // Burlywood4
            Add(new RgbColor { R =  95, G = 158, B = 160 }); // CadetBlue
            Add(new RgbColor { R = 152, G = 245, B = 255 }); // CadetBlue1
            Add(new RgbColor { R = 142, G = 229, B = 238 }); // CadetBlue2
            Add(new RgbColor { R = 122, G = 197, B = 205 }); // CadetBlue3
            Add(new RgbColor { R =  83, G = 134, B = 139 }); // CadetBlue4
            Add(new RgbColor { R = 127, G = 255, B =   0 }); // Chartreuse
            Add(new RgbColor { R = 127, G = 255, B =   0 }); // Chartreuse1
            Add(new RgbColor { R = 118, G = 238, B =   0 }); // Chartreuse2
            Add(new RgbColor { R = 102, G = 205, B =   0 }); // Chartreuse3
            Add(new RgbColor { R =  69, G = 139, B =   0 }); // Chartreuse4
            Add(new RgbColor { R = 210, G = 105, B =  30 }); // Chocolate
            Add(new RgbColor { R = 255, G = 127, B =  36 }); // Chocolate1
            Add(new RgbColor { R = 238, G = 118, B =  33 }); // Chocolate2
            Add(new RgbColor { R = 205, G = 102, B =  29 }); // Chocolate3
            Add(new RgbColor { R = 139, G =  69, B =  19 }); // Chocolate4
            Add(new RgbColor { R = 255, G = 127, B =  80 }); // Coral
            Add(new RgbColor { R = 255, G = 114, B =  86 }); // Coral1
            Add(new RgbColor { R = 238, G = 106, B =  80 }); // Coral2
            Add(new RgbColor { R = 205, G =  91, B =  69 }); // Coral3
            Add(new RgbColor { R = 139, G =  62, B =  47 }); // Coral4
            Add(new RgbColor { R = 100, G = 149, B = 237 }); // CornflowerBlue
            Add(new RgbColor { R = 255, G = 248, B = 220 }); // Cornsilk
            Add(new RgbColor { R = 255, G = 248, B = 220 }); // Cornsilk1
            Add(new RgbColor { R = 238, G = 232, B = 205 }); // Cornsilk2
            Add(new RgbColor { R = 205, G = 200, B = 177 }); // Cornsilk3
            Add(new RgbColor { R = 139, G = 136, B = 120 }); // Cornsilk4
            Add(new RgbColor { R =   0, G = 255, B = 255 }); // Cyan
            Add(new RgbColor { R =   0, G = 255, B = 255 }); // Cyan1
            Add(new RgbColor { R =   0, G = 238, B = 238 }); // Cyan2
            Add(new RgbColor { R =   0, G = 205, B = 205 }); // Cyan3
            Add(new RgbColor { R =   0, G = 139, B = 139 }); // Cyan4
            Add(new RgbColor { R =   0, G =   0, B = 139 }); // DarkBlue
            Add(new RgbColor { R =   0, G = 139, B = 139 }); // DarkCyan
            Add(new RgbColor { R = 184, G = 134, B =  11 }); // DarkGoldenrod
            Add(new RgbColor { R = 255, G = 185, B =  15 }); // DarkGoldenrod1
            Add(new RgbColor { R = 238, G = 173, B =  14 }); // DarkGoldenrod2
            Add(new RgbColor { R = 205, G = 149, B =  12 }); // DarkGoldenrod3
            Add(new RgbColor { R = 139, G = 101, B =   8 }); // DarkGoldenrod4
            Add(new RgbColor { R = 169, G = 169, B = 169 }); // DarkGray
            Add(new RgbColor { R =   0, G = 100, B =   0 }); // DarkGreen
            Add(new RgbColor { R = 169, G = 169, B = 169 }); // DarkGrey
            Add(new RgbColor { R = 189, G = 183, B = 107 }); // DarkKhaki
            Add(new RgbColor { R = 139, G =   0, B = 139 }); // DarkMagenta
            Add(new RgbColor { R =  85, G = 107, B =  47 }); // DarkOliveGreen
            Add(new RgbColor { R = 202, G = 255, B = 112 }); // DarkOliveGreen1
            Add(new RgbColor { R = 188, G = 238, B = 104 }); // DarkOliveGreen2
            Add(new RgbColor { R = 162, G = 205, B =  90 }); // DarkOliveGreen3
            Add(new RgbColor { R = 110, G = 139, B =  61 }); // DarkOliveGreen4
            Add(new RgbColor { R = 255, G = 140, B =   0 }); // DarkOrange
            Add(new RgbColor { R = 255, G = 127, B =   0 }); // DarkOrange1
            Add(new RgbColor { R = 238, G = 118, B =   0 }); // DarkOrange2
            Add(new RgbColor { R = 205, G = 102, B =   0 }); // DarkOrange3
            Add(new RgbColor { R = 139, G =  69, B =   0 }); // DarkOrange4
            Add(new RgbColor { R = 153, G =  50, B = 204 }); // DarkOrchid
            Add(new RgbColor { R = 191, G =  62, B = 255 }); // DarkOrchid1
            Add(new RgbColor { R = 178, G =  58, B = 238 }); // DarkOrchid2
            Add(new RgbColor { R = 154, G =  50, B = 205 }); // DarkOrchid3
            Add(new RgbColor { R = 104, G =  34, B = 139 }); // DarkOrchid4
            Add(new RgbColor { R = 139, G =   0, B =   0 }); // DarkRed
            Add(new RgbColor { R = 233, G = 150, B = 122 }); // DarkSalmon
            Add(new RgbColor { R = 143, G = 188, B = 143 }); // DarkSeaGreen
            Add(new RgbColor { R = 193, G = 255, B = 193 }); // DarkSeaGreen1
            Add(new RgbColor { R = 180, G = 238, B = 180 }); // DarkSeaGreen2
            Add(new RgbColor { R = 155, G = 205, B = 155 }); // DarkSeaGreen3
            Add(new RgbColor { R = 105, G = 139, B = 105 }); // DarkSeaGreen4
            Add(new RgbColor { R =  72, G =  61, B = 139 }); // DarkSlateBlue
            Add(new RgbColor { R =  47, G =  79, B =  79 }); // DarkSlateGray
            Add(new RgbColor { R = 151, G = 255, B = 255 }); // DarkSlateGray1
            Add(new RgbColor { R = 141, G = 238, B = 238 }); // DarkSlateGray2
            Add(new RgbColor { R = 121, G = 205, B = 205 }); // DarkSlateGray3
            Add(new RgbColor { R =  82, G = 139, B = 139 }); // DarkSlateGray4
            Add(new RgbColor { R =  47, G =  79, B =  79 }); // DarkSlateGrey
            Add(new RgbColor { R =   0, G = 206, B = 209 }); // DarkTurquoise
            Add(new RgbColor { R = 148, G =   0, B = 211 }); // DarkViolet
            Add(new RgbColor { R = 205, G = 205, B =   0 }); // DarkYellow
            Add(new RgbColor { R = 255, G =  20, B = 147 }); // DeepPink
            Add(new RgbColor { R = 255, G =  20, B = 147 }); // DeepPink1
            Add(new RgbColor { R = 238, G =  18, B = 137 }); // DeepPink2
            Add(new RgbColor { R = 205, G =  16, B = 118 }); // DeepPink3
            Add(new RgbColor { R = 139, G =  10, B =  80 }); // DeepPink4
            Add(new RgbColor { R =   0, G = 191, B = 255 }); // DeepSkyBlue
            Add(new RgbColor { R =   0, G = 191, B = 255 }); // DeepSkyBlue1
            Add(new RgbColor { R =   0, G = 178, B = 238 }); // DeepSkyBlue2
            Add(new RgbColor { R =   0, G = 154, B = 205 }); // DeepSkyBlue3
            Add(new RgbColor { R =   0, G = 104, B = 139 }); // DeepSkyBlue4
            Add(new RgbColor { R = 105, G = 105, B = 105 }); // DimGray
            Add(new RgbColor { R = 105, G = 105, B = 105 }); // DimGrey
            Add(new RgbColor { R =  30, G = 144, B = 255 }); // DodgerBlue
            Add(new RgbColor { R =  30, G = 144, B = 255 }); // DodgerBlue1
            Add(new RgbColor { R =  28, G = 134, B = 238 }); // DodgerBlue2
            Add(new RgbColor { R =  24, G = 116, B = 205 }); // DodgerBlue3
            Add(new RgbColor { R =  16, G =  78, B = 139 }); // DodgerBlue4
            Add(new RgbColor { R = 178, G =  34, B =  34 }); // Firebrick
            Add(new RgbColor { R = 255, G =  48, B =  48 }); // Firebrick1
            Add(new RgbColor { R = 238, G =  44, B =  44 }); // Firebrick2
            Add(new RgbColor { R = 205, G =  38, B =  38 }); // Firebrick3
            Add(new RgbColor { R = 139, G =  26, B =  26 }); // Firebrick4
            Add(new RgbColor { R = 255, G = 250, B = 240 }); // FloralWhite
            Add(new RgbColor { R =  34, G = 139, B =  34 }); // ForestGreen
            Add(new RgbColor { R = 220, G = 220, B = 220 }); // Gainsboro
            Add(new RgbColor { R = 248, G = 248, B = 255 }); // GhostWhite
            Add(new RgbColor { R = 255, G = 215, B =   0 }); // Gold
            Add(new RgbColor { R = 255, G = 215, B =   0 }); // Gold1
            Add(new RgbColor { R = 238, G = 201, B =   0 }); // Gold2
            Add(new RgbColor { R = 205, G = 173, B =   0 }); // Gold3
            Add(new RgbColor { R = 139, G = 117, B =   0 }); // Gold4
            Add(new RgbColor { R = 218, G = 165, B =  32 }); // Goldenrod
            Add(new RgbColor { R = 255, G = 193, B =  37 }); // Goldenrod1
            Add(new RgbColor { R = 238, G = 180, B =  34 }); // Goldenrod2
            Add(new RgbColor { R = 205, G = 155, B =  29 }); // Goldenrod3
            Add(new RgbColor { R = 139, G = 105, B =  20 }); // Goldenrod4
            Add(new RgbColor { R = 190, G = 190, B = 190 }); // Gray
            Add(new RgbColor { R =   0, G =   0, B =   0 }); // Gray0
            Add(new RgbColor { R =   3, G =   3, B =   3 }); // Gray1
            Add(new RgbColor { R =   5, G =   5, B =   5 }); // Gray2
            Add(new RgbColor { R =   8, G =   8, B =   8 }); // Gray3
            Add(new RgbColor { R =  10, G =  10, B =  10 }); // Gray4
            Add(new RgbColor { R =  13, G =  13, B =  13 }); // Gray5
            Add(new RgbColor { R =  15, G =  15, B =  15 }); // Gray6
            Add(new RgbColor { R =  18, G =  18, B =  18 }); // Gray7
            Add(new RgbColor { R =  20, G =  20, B =  20 }); // Gray8
            Add(new RgbColor { R =  23, G =  23, B =  23 }); // Gray9
            Add(new RgbColor { R =  26, G =  26, B =  26 }); // Gray10
            Add(new RgbColor { R =  28, G =  28, B =  28 }); // Gray11
            Add(new RgbColor { R =  31, G =  31, B =  31 }); // Gray12
            Add(new RgbColor { R =  33, G =  33, B =  33 }); // Gray13
            Add(new RgbColor { R =  36, G =  36, B =  36 }); // Gray14
            Add(new RgbColor { R =  38, G =  38, B =  38 }); // Gray15
            Add(new RgbColor { R =  41, G =  41, B =  41 }); // Gray16
            Add(new RgbColor { R =  43, G =  43, B =  43 }); // Gray17
            Add(new RgbColor { R =  46, G =  46, B =  46 }); // Gray18
            Add(new RgbColor { R =  48, G =  48, B =  48 }); // Gray19
            Add(new RgbColor { R =  51, G =  51, B =  51 }); // Gray20
            Add(new RgbColor { R =  54, G =  54, B =  54 }); // Gray21
            Add(new RgbColor { R =  56, G =  56, B =  56 }); // Gray22
            Add(new RgbColor { R =  59, G =  59, B =  59 }); // Gray23
            Add(new RgbColor { R =  61, G =  61, B =  61 }); // Gray24
            Add(new RgbColor { R =  64, G =  64, B =  64 }); // Gray25
            Add(new RgbColor { R =  66, G =  66, B =  66 }); // Gray26
            Add(new RgbColor { R =  69, G =  69, B =  69 }); // Gray27
            Add(new RgbColor { R =  71, G =  71, B =  71 }); // Gray28
            Add(new RgbColor { R =  74, G =  74, B =  74 }); // Gray29
            Add(new RgbColor { R =  77, G =  77, B =  77 }); // Gray30
            Add(new RgbColor { R =  79, G =  79, B =  79 }); // Gray31
            Add(new RgbColor { R =  82, G =  82, B =  82 }); // Gray32
            Add(new RgbColor { R =  84, G =  84, B =  84 }); // Gray33
            Add(new RgbColor { R =  87, G =  87, B =  87 }); // Gray34
            Add(new RgbColor { R =  89, G =  89, B =  89 }); // Gray35
            Add(new RgbColor { R =  92, G =  92, B =  92 }); // Gray36
            Add(new RgbColor { R =  94, G =  94, B =  94 }); // Gray37
            Add(new RgbColor { R =  97, G =  97, B =  97 }); // Gray38
            Add(new RgbColor { R =  99, G =  99, B =  99 }); // Gray39
            Add(new RgbColor { R = 102, G = 102, B = 102 }); // Gray40
            Add(new RgbColor { R = 105, G = 105, B = 105 }); // Gray41
            Add(new RgbColor { R = 107, G = 107, B = 107 }); // Gray42
            Add(new RgbColor { R = 110, G = 110, B = 110 }); // Gray43
            Add(new RgbColor { R = 112, G = 112, B = 112 }); // Gray44
            Add(new RgbColor { R = 115, G = 115, B = 115 }); // Gray45
            Add(new RgbColor { R = 117, G = 117, B = 117 }); // Gray46
            Add(new RgbColor { R = 120, G = 120, B = 120 }); // Gray47
            Add(new RgbColor { R = 122, G = 122, B = 122 }); // Gray48
            Add(new RgbColor { R = 125, G = 125, B = 125 }); // Gray49
            Add(new RgbColor { R = 127, G = 127, B = 127 }); // Gray50
            Add(new RgbColor { R = 130, G = 130, B = 130 }); // Gray51
            Add(new RgbColor { R = 133, G = 133, B = 133 }); // Gray52
            Add(new RgbColor { R = 135, G = 135, B = 135 }); // Gray53
            Add(new RgbColor { R = 138, G = 138, B = 138 }); // Gray54
            Add(new RgbColor { R = 140, G = 140, B = 140 }); // Gray55
            Add(new RgbColor { R = 143, G = 143, B = 143 }); // Gray56
            Add(new RgbColor { R = 145, G = 145, B = 145 }); // Gray57
            Add(new RgbColor { R = 148, G = 148, B = 148 }); // Gray58
            Add(new RgbColor { R = 150, G = 150, B = 150 }); // Gray59
            Add(new RgbColor { R = 153, G = 153, B = 153 }); // Gray60
            Add(new RgbColor { R = 156, G = 156, B = 156 }); // Gray61
            Add(new RgbColor { R = 158, G = 158, B = 158 }); // Gray62
            Add(new RgbColor { R = 161, G = 161, B = 161 }); // Gray63
            Add(new RgbColor { R = 163, G = 163, B = 163 }); // Gray64
            Add(new RgbColor { R = 166, G = 166, B = 166 }); // Gray65
            Add(new RgbColor { R = 168, G = 168, B = 168 }); // Gray66
            Add(new RgbColor { R = 171, G = 171, B = 171 }); // Gray67
            Add(new RgbColor { R = 173, G = 173, B = 173 }); // Gray68
            Add(new RgbColor { R = 176, G = 176, B = 176 }); // Gray69
            Add(new RgbColor { R = 179, G = 179, B = 179 }); // Gray70
            Add(new RgbColor { R = 181, G = 181, B = 181 }); // Gray71
            Add(new RgbColor { R = 184, G = 184, B = 184 }); // Gray72
            Add(new RgbColor { R = 186, G = 186, B = 186 }); // Gray73
            Add(new RgbColor { R = 189, G = 189, B = 189 }); // Gray74
            Add(new RgbColor { R = 191, G = 191, B = 191 }); // Gray75
            Add(new RgbColor { R = 194, G = 194, B = 194 }); // Gray76
            Add(new RgbColor { R = 196, G = 196, B = 196 }); // Gray77
            Add(new RgbColor { R = 199, G = 199, B = 199 }); // Gray78
            Add(new RgbColor { R = 201, G = 201, B = 201 }); // Gray79
            Add(new RgbColor { R = 204, G = 204, B = 204 }); // Gray80
            Add(new RgbColor { R = 207, G = 207, B = 207 }); // Gray81
            Add(new RgbColor { R = 209, G = 209, B = 209 }); // Gray82
            Add(new RgbColor { R = 212, G = 212, B = 212 }); // Gray83
            Add(new RgbColor { R = 214, G = 214, B = 214 }); // Gray84
            Add(new RgbColor { R = 217, G = 217, B = 217 }); // Gray85
            Add(new RgbColor { R = 219, G = 219, B = 219 }); // Gray86
            Add(new RgbColor { R = 222, G = 222, B = 222 }); // Gray87
            Add(new RgbColor { R = 224, G = 224, B = 224 }); // Gray88
            Add(new RgbColor { R = 227, G = 227, B = 227 }); // Gray89
            Add(new RgbColor { R = 229, G = 229, B = 229 }); // Gray90
            Add(new RgbColor { R = 232, G = 232, B = 232 }); // Gray91
            Add(new RgbColor { R = 235, G = 235, B = 235 }); // Gray92
            Add(new RgbColor { R = 237, G = 237, B = 237 }); // Gray93
            Add(new RgbColor { R = 240, G = 240, B = 240 }); // Gray94
            Add(new RgbColor { R = 242, G = 242, B = 242 }); // Gray95
            Add(new RgbColor { R = 245, G = 245, B = 245 }); // Gray96
            Add(new RgbColor { R = 247, G = 247, B = 247 }); // Gray97
            Add(new RgbColor { R = 250, G = 250, B = 250 }); // Gray98
            Add(new RgbColor { R = 252, G = 252, B = 252 }); // Gray99
            Add(new RgbColor { R = 255, G = 255, B = 255 }); // Gray100
            Add(new RgbColor { R =   0, G = 255, B =   0 }); // Green
            Add(new RgbColor { R =   0, G = 255, B =   0 }); // Green1
            Add(new RgbColor { R =   0, G = 238, B =   0 }); // Green2
            Add(new RgbColor { R =   0, G = 205, B =   0 }); // Green3
            Add(new RgbColor { R =   0, G = 139, B =   0 }); // Green4
            Add(new RgbColor { R = 173, G = 255, B =  47 }); // GreenYellow
            Add(new RgbColor { R = 190, G = 190, B = 190 }); // Grey
            Add(new RgbColor { R =   0, G =   0, B =   0 }); // Grey0
            Add(new RgbColor { R =   3, G =   3, B =   3 }); // Grey1
            Add(new RgbColor { R =   5, G =   5, B =   5 }); // Grey2
            Add(new RgbColor { R =   8, G =   8, B =   8 }); // Grey3
            Add(new RgbColor { R =  10, G =  10, B =  10 }); // Grey4
            Add(new RgbColor { R =  13, G =  13, B =  13 }); // Grey5
            Add(new RgbColor { R =  15, G =  15, B =  15 }); // Grey6
            Add(new RgbColor { R =  18, G =  18, B =  18 }); // Grey7
            Add(new RgbColor { R =  20, G =  20, B =  20 }); // Grey8
            Add(new RgbColor { R =  23, G =  23, B =  23 }); // Grey9
            Add(new RgbColor { R =  26, G =  26, B =  26 }); // Grey10
            Add(new RgbColor { R =  28, G =  28, B =  28 }); // Grey11
            Add(new RgbColor { R =  31, G =  31, B =  31 }); // Grey12
            Add(new RgbColor { R =  33, G =  33, B =  33 }); // Grey13
            Add(new RgbColor { R =  36, G =  36, B =  36 }); // Grey14
            Add(new RgbColor { R =  38, G =  38, B =  38 }); // Grey15
            Add(new RgbColor { R =  41, G =  41, B =  41 }); // Grey16
            Add(new RgbColor { R =  43, G =  43, B =  43 }); // Grey17
            Add(new RgbColor { R =  46, G =  46, B =  46 }); // Grey18
            Add(new RgbColor { R =  48, G =  48, B =  48 }); // Grey19
            Add(new RgbColor { R =  51, G =  51, B =  51 }); // Grey20
            Add(new RgbColor { R =  54, G =  54, B =  54 }); // Grey21
            Add(new RgbColor { R =  56, G =  56, B =  56 }); // Grey22
            Add(new RgbColor { R =  59, G =  59, B =  59 }); // Grey23
            Add(new RgbColor { R =  61, G =  61, B =  61 }); // Grey24
            Add(new RgbColor { R =  64, G =  64, B =  64 }); // Grey25
            Add(new RgbColor { R =  66, G =  66, B =  66 }); // Grey26
            Add(new RgbColor { R =  69, G =  69, B =  69 }); // Grey27
            Add(new RgbColor { R =  71, G =  71, B =  71 }); // Grey28
            Add(new RgbColor { R =  74, G =  74, B =  74 }); // Grey29
            Add(new RgbColor { R =  77, G =  77, B =  77 }); // Grey30
            Add(new RgbColor { R =  79, G =  79, B =  79 }); // Grey31
            Add(new RgbColor { R =  82, G =  82, B =  82 }); // Grey32
            Add(new RgbColor { R =  84, G =  84, B =  84 }); // Grey33
            Add(new RgbColor { R =  87, G =  87, B =  87 }); // Grey34
            Add(new RgbColor { R =  89, G =  89, B =  89 }); // Grey35
            Add(new RgbColor { R =  92, G =  92, B =  92 }); // Grey36
            Add(new RgbColor { R =  94, G =  94, B =  94 }); // Grey37
            Add(new RgbColor { R =  97, G =  97, B =  97 }); // Grey38
            Add(new RgbColor { R =  99, G =  99, B =  99 }); // Grey39
            Add(new RgbColor { R = 102, G = 102, B = 102 }); // Grey40
            Add(new RgbColor { R = 105, G = 105, B = 105 }); // Grey41
            Add(new RgbColor { R = 107, G = 107, B = 107 }); // Grey42
            Add(new RgbColor { R = 110, G = 110, B = 110 }); // Grey43
            Add(new RgbColor { R = 112, G = 112, B = 112 }); // Grey44
            Add(new RgbColor { R = 115, G = 115, B = 115 }); // Grey45
            Add(new RgbColor { R = 117, G = 117, B = 117 }); // Grey46
            Add(new RgbColor { R = 120, G = 120, B = 120 }); // Grey47
            Add(new RgbColor { R = 122, G = 122, B = 122 }); // Grey48
            Add(new RgbColor { R = 125, G = 125, B = 125 }); // Grey49
            Add(new RgbColor { R = 127, G = 127, B = 127 }); // Grey50
            Add(new RgbColor { R = 130, G = 130, B = 130 }); // Grey51
            Add(new RgbColor { R = 133, G = 133, B = 133 }); // Grey52
            Add(new RgbColor { R = 135, G = 135, B = 135 }); // Grey53
            Add(new RgbColor { R = 138, G = 138, B = 138 }); // Grey54
            Add(new RgbColor { R = 140, G = 140, B = 140 }); // Grey55
            Add(new RgbColor { R = 143, G = 143, B = 143 }); // Grey56
            Add(new RgbColor { R = 145, G = 145, B = 145 }); // Grey57
            Add(new RgbColor { R = 148, G = 148, B = 148 }); // Grey58
            Add(new RgbColor { R = 150, G = 150, B = 150 }); // Grey59
            Add(new RgbColor { R = 153, G = 153, B = 153 }); // Grey60
            Add(new RgbColor { R = 156, G = 156, B = 156 }); // Grey61
            Add(new RgbColor { R = 158, G = 158, B = 158 }); // Grey62
            Add(new RgbColor { R = 161, G = 161, B = 161 }); // Grey63
            Add(new RgbColor { R = 163, G = 163, B = 163 }); // Grey64
            Add(new RgbColor { R = 166, G = 166, B = 166 }); // Grey65
            Add(new RgbColor { R = 168, G = 168, B = 168 }); // Grey66
            Add(new RgbColor { R = 171, G = 171, B = 171 }); // Grey67
            Add(new RgbColor { R = 173, G = 173, B = 173 }); // Grey68
            Add(new RgbColor { R = 176, G = 176, B = 176 }); // Grey69
            Add(new RgbColor { R = 179, G = 179, B = 179 }); // Grey70
            Add(new RgbColor { R = 181, G = 181, B = 181 }); // Grey71
            Add(new RgbColor { R = 184, G = 184, B = 184 }); // Grey72
            Add(new RgbColor { R = 186, G = 186, B = 186 }); // Grey73
            Add(new RgbColor { R = 189, G = 189, B = 189 }); // Grey74
            Add(new RgbColor { R = 191, G = 191, B = 191 }); // Grey75
            Add(new RgbColor { R = 194, G = 194, B = 194 }); // Grey76
            Add(new RgbColor { R = 196, G = 196, B = 196 }); // Grey77
            Add(new RgbColor { R = 199, G = 199, B = 199 }); // Grey78
            Add(new RgbColor { R = 201, G = 201, B = 201 }); // Grey79
            Add(new RgbColor { R = 204, G = 204, B = 204 }); // Grey80
            Add(new RgbColor { R = 207, G = 207, B = 207 }); // Grey81
            Add(new RgbColor { R = 209, G = 209, B = 209 }); // Grey82
            Add(new RgbColor { R = 212, G = 212, B = 212 }); // Grey83
            Add(new RgbColor { R = 214, G = 214, B = 214 }); // Grey84
            Add(new RgbColor { R = 217, G = 217, B = 217 }); // Grey85
            Add(new RgbColor { R = 219, G = 219, B = 219 }); // Grey86
            Add(new RgbColor { R = 222, G = 222, B = 222 }); // Grey87
            Add(new RgbColor { R = 224, G = 224, B = 224 }); // Grey88
            Add(new RgbColor { R = 227, G = 227, B = 227 }); // Grey89
            Add(new RgbColor { R = 229, G = 229, B = 229 }); // Grey90
            Add(new RgbColor { R = 232, G = 232, B = 232 }); // Grey91
            Add(new RgbColor { R = 235, G = 235, B = 235 }); // Grey92
            Add(new RgbColor { R = 237, G = 237, B = 237 }); // Grey93
            Add(new RgbColor { R = 240, G = 240, B = 240 }); // Grey94
            Add(new RgbColor { R = 242, G = 242, B = 242 }); // Grey95
            Add(new RgbColor { R = 245, G = 245, B = 245 }); // Grey96
            Add(new RgbColor { R = 247, G = 247, B = 247 }); // Grey97
            Add(new RgbColor { R = 250, G = 250, B = 250 }); // Grey98
            Add(new RgbColor { R = 252, G = 252, B = 252 }); // Grey99
            Add(new RgbColor { R = 255, G = 255, B = 255 }); // Grey100
            Add(new RgbColor { R = 240, G = 255, B = 240 }); // Honeydew
            Add(new RgbColor { R = 240, G = 255, B = 240 }); // Honeydew1
            Add(new RgbColor { R = 224, G = 238, B = 224 }); // Honeydew2
            Add(new RgbColor { R = 193, G = 205, B = 193 }); // Honeydew3
            Add(new RgbColor { R = 131, G = 139, B = 131 }); // Honeydew4
            Add(new RgbColor { R = 255, G = 105, B = 180 }); // HotPink
            Add(new RgbColor { R = 255, G = 110, B = 180 }); // HotPink1
            Add(new RgbColor { R = 238, G = 106, B = 167 }); // HotPink2
            Add(new RgbColor { R = 205, G =  96, B = 144 }); // HotPink3
            Add(new RgbColor { R = 139, G =  58, B =  98 }); // HotPink4
            Add(new RgbColor { R = 205, G =  92, B =  92 }); // IndianRed
            Add(new RgbColor { R = 255, G = 106, B = 106 }); // IndianRed1
            Add(new RgbColor { R = 238, G =  99, B =  99 }); // IndianRed2
            Add(new RgbColor { R = 205, G =  85, B =  85 }); // IndianRed3
            Add(new RgbColor { R = 139, G =  58, B =  58 }); // IndianRed4
            Add(new RgbColor { R = 255, G = 255, B = 240 }); // Ivory
            Add(new RgbColor { R = 255, G = 255, B = 240 }); // Ivory1
            Add(new RgbColor { R = 238, G = 238, B = 224 }); // Ivory2
            Add(new RgbColor { R = 205, G = 205, B = 193 }); // Ivory3
            Add(new RgbColor { R = 139, G = 139, B = 131 }); // Ivory4
            Add(new RgbColor { R = 240, G = 230, B = 140 }); // Khaki
            Add(new RgbColor { R = 255, G = 246, B = 143 }); // Khaki1
            Add(new RgbColor { R = 238, G = 230, B = 133 }); // Khaki2
            Add(new RgbColor { R = 205, G = 198, B = 115 }); // Khaki3
            Add(new RgbColor { R = 139, G = 134, B =  78 }); // Khaki4
            Add(new RgbColor { R = 230, G = 230, B = 250 }); // Lavender
            Add(new RgbColor { R = 255, G = 240, B = 245 }); // LavenderBlush
            Add(new RgbColor { R = 255, G = 240, B = 245 }); // LavenderBlush1
            Add(new RgbColor { R = 238, G = 224, B = 229 }); // LavenderBlush2
            Add(new RgbColor { R = 205, G = 193, B = 197 }); // LavenderBlush3
            Add(new RgbColor { R = 139, G = 131, B = 134 }); // LavenderBlush4
            Add(new RgbColor { R = 124, G = 252, B =   0 }); // LawnGreen
            Add(new RgbColor { R = 255, G = 250, B = 205 }); // LemonChiffon
            Add(new RgbColor { R = 255, G = 250, B = 205 }); // LemonChiffon1
            Add(new RgbColor { R = 238, G = 233, B = 191 }); // LemonChiffon2
            Add(new RgbColor { R = 205, G = 201, B = 165 }); // LemonChiffon3
            Add(new RgbColor { R = 139, G = 137, B = 112 }); // LemonChiffon4
            Add(new RgbColor { R = 173, G = 216, B = 230 }); // LightBlue
            Add(new RgbColor { R = 191, G = 239, B = 255 }); // LightBlue1
            Add(new RgbColor { R = 178, G = 223, B = 238 }); // LightBlue2
            Add(new RgbColor { R = 154, G = 192, B = 205 }); // LightBlue3
            Add(new RgbColor { R = 104, G = 131, B = 139 }); // LightBlue4
            Add(new RgbColor { R = 240, G = 128, B = 128 }); // LightCoral
            Add(new RgbColor { R = 224, G = 255, B = 255 }); // LightCyan
            Add(new RgbColor { R = 224, G = 255, B = 255 }); // LightCyan1
            Add(new RgbColor { R = 209, G = 238, B = 238 }); // LightCyan2
            Add(new RgbColor { R = 180, G = 205, B = 205 }); // LightCyan3
            Add(new RgbColor { R = 122, G = 139, B = 139 }); // LightCyan4
            Add(new RgbColor { R = 238, G = 221, B = 130 }); // LightGoldenrod
            Add(new RgbColor { R = 255, G = 236, B = 139 }); // LightGoldenrod1
            Add(new RgbColor { R = 238, G = 220, B = 130 }); // LightGoldenrod2
            Add(new RgbColor { R = 205, G = 190, B = 112 }); // LightGoldenrod3
            Add(new RgbColor { R = 139, G = 129, B =  76 }); // LightGoldenrod4
            Add(new RgbColor { R = 250, G = 250, B = 210 }); // LightGoldenrodYellow
            Add(new RgbColor { R = 211, G = 211, B = 211 }); // LightGray
            Add(new RgbColor { R = 144, G = 238, B = 144 }); // LightGreen
            Add(new RgbColor { R = 211, G = 211, B = 211 }); // LightGrey
            Add(new RgbColor { R = 255, G = 182, B = 193 }); // LightPink
            Add(new RgbColor { R = 255, G = 174, B = 185 }); // LightPink1
            Add(new RgbColor { R = 238, G = 162, B = 173 }); // LightPink2
            Add(new RgbColor { R = 205, G = 140, B = 149 }); // LightPink3
            Add(new RgbColor { R = 139, G =  95, B = 101 }); // LightPink4
            Add(new RgbColor { R = 255, G = 160, B = 122 }); // LightSalmon
            Add(new RgbColor { R = 255, G = 160, B = 122 }); // LightSalmon1
            Add(new RgbColor { R = 238, G = 149, B = 114 }); // LightSalmon2
            Add(new RgbColor { R = 205, G = 129, B =  98 }); // LightSalmon3
            Add(new RgbColor { R = 139, G =  87, B =  66 }); // LightSalmon4
            Add(new RgbColor { R =  32, G = 178, B = 170 }); // LightSeaGreen
            Add(new RgbColor { R = 135, G = 206, B = 250 }); // LightSkyBlue
            Add(new RgbColor { R = 176, G = 226, B = 255 }); // LightSkyBlue1
            Add(new RgbColor { R = 164, G = 211, B = 238 }); // LightSkyBlue2
            Add(new RgbColor { R = 141, G = 182, B = 205 }); // LightSkyBlue3
            Add(new RgbColor { R =  96, G = 123, B = 139 }); // LightSkyBlue4
            Add(new RgbColor { R = 132, G = 112, B = 255 }); // LightSlateBlue
            Add(new RgbColor { R = 119, G = 136, B = 153 }); // LightSlateGray
            Add(new RgbColor { R = 119, G = 136, B = 153 }); // LightSlateGrey
            Add(new RgbColor { R = 176, G = 196, B = 222 }); // LightSteelBlue
            Add(new RgbColor { R = 202, G = 225, B = 255 }); // LightSteelBlue1
            Add(new RgbColor { R = 188, G = 210, B = 238 }); // LightSteelBlue2
            Add(new RgbColor { R = 162, G = 181, B = 205 }); // LightSteelBlue3
            Add(new RgbColor { R = 110, G = 123, B = 139 }); // LightSteelBlue4
            Add(new RgbColor { R = 255, G = 255, B = 224 }); // LightYellow
            Add(new RgbColor { R = 255, G = 255, B = 224 }); // LightYellow1
            Add(new RgbColor { R = 238, G = 238, B = 209 }); // LightYellow2
            Add(new RgbColor { R = 205, G = 205, B = 180 }); // LightYellow3
            Add(new RgbColor { R = 139, G = 139, B = 122 }); // LightYellow4
            Add(new RgbColor { R =  50, G = 205, B =  50 }); // LimeGreen
            Add(new RgbColor { R = 250, G = 240, B = 230 }); // Linen
            Add(new RgbColor { R = 255, G =   0, B = 255 }); // Magenta
            Add(new RgbColor { R = 255, G =   0, B = 255 }); // Magenta1
            Add(new RgbColor { R = 238, G =   0, B = 238 }); // Magenta2
            Add(new RgbColor { R = 205, G =   0, B = 205 }); // Magenta3
            Add(new RgbColor { R = 139, G =   0, B = 139 }); // Magenta4
            Add(new RgbColor { R = 176, G =  48, B =  96 }); // Maroon
            Add(new RgbColor { R = 255, G =  52, B = 179 }); // Maroon1
            Add(new RgbColor { R = 238, G =  48, B = 167 }); // Maroon2
            Add(new RgbColor { R = 205, G =  41, B = 144 }); // Maroon3
            Add(new RgbColor { R = 139, G =  28, B =  98 }); // Maroon4
            Add(new RgbColor { R = 102, G = 205, B = 170 }); // MediumAquamarine
            Add(new RgbColor { R =   0, G =   0, B = 205 }); // MediumBlue
            Add(new RgbColor { R = 186, G =  85, B = 211 }); // MediumOrchid
            Add(new RgbColor { R = 224, G = 102, B = 255 }); // MediumOrchid1
            Add(new RgbColor { R = 209, G =  95, B = 238 }); // MediumOrchid2
            Add(new RgbColor { R = 180, G =  82, B = 205 }); // MediumOrchid3
            Add(new RgbColor { R = 122, G =  55, B = 139 }); // MediumOrchid4
            Add(new RgbColor { R = 147, G = 112, B = 219 }); // MediumPurple
            Add(new RgbColor { R = 171, G = 130, B = 255 }); // MediumPurple1
            Add(new RgbColor { R = 159, G = 121, B = 238 }); // MediumPurple2
            Add(new RgbColor { R = 137, G = 104, B = 205 }); // MediumPurple3
            Add(new RgbColor { R =  93, G =  71, B = 139 }); // MediumPurple4
            Add(new RgbColor { R =  60, G = 179, B = 113 }); // MediumSeaGreen
            Add(new RgbColor { R = 123, G = 104, B = 238 }); // MediumSlateBlue
            Add(new RgbColor { R =   0, G = 250, B = 154 }); // MediumSpringGreen
            Add(new RgbColor { R =  72, G = 209, B = 204 }); // MediumTurquoise
            Add(new RgbColor { R = 199, G =  21, B = 133 }); // MediumVioletRed
            Add(new RgbColor { R =  25, G =  25, B = 112 }); // MidnightBlue
            Add(new RgbColor { R = 245, G = 255, B = 250 }); // MintCream
            Add(new RgbColor { R = 255, G = 228, B = 225 }); // MistyRose
            Add(new RgbColor { R = 255, G = 228, B = 225 }); // MistyRose1
            Add(new RgbColor { R = 238, G = 213, B = 210 }); // MistyRose2
            Add(new RgbColor { R = 205, G = 183, B = 181 }); // MistyRose3
            Add(new RgbColor { R = 139, G = 125, B = 123 }); // MistyRose4
            Add(new RgbColor { R = 255, G = 228, B = 181 }); // Moccasin
            Add(new RgbColor { R = 255, G = 222, B = 173 }); // NavajoWhite
            Add(new RgbColor { R = 255, G = 222, B = 173 }); // NavajoWhite1
            Add(new RgbColor { R = 238, G = 207, B = 161 }); // NavajoWhite2
            Add(new RgbColor { R = 205, G = 179, B = 139 }); // NavajoWhite3
            Add(new RgbColor { R = 139, G = 121, B =  94 }); // NavajoWhite4
            Add(new RgbColor { R =   0, G =   0, B = 128 }); // Navy
            Add(new RgbColor { R =   0, G =   0, B = 128 }); // NavyBlue
            Add(new RgbColor { R = 253, G = 245, B = 230 }); // OldLace
            Add(new RgbColor { R = 107, G = 142, B =  35 }); // OliveDrab
            Add(new RgbColor { R = 192, G = 255, B =  62 }); // OliveDrab1
            Add(new RgbColor { R = 179, G = 238, B =  58 }); // OliveDrab2
            Add(new RgbColor { R = 154, G = 205, B =  50 }); // OliveDrab3
            Add(new RgbColor { R = 105, G = 139, B =  34 }); // OliveDrab4
            Add(new RgbColor { R = 255, G = 165, B =   0 }); // Orange
            Add(new RgbColor { R = 255, G = 165, B =   0 }); // Orange1
            Add(new RgbColor { R = 238, G = 154, B =   0 }); // Orange2
            Add(new RgbColor { R = 205, G = 133, B =   0 }); // Orange3
            Add(new RgbColor { R = 139, G =  90, B =   0 }); // Orange4
            Add(new RgbColor { R = 255, G =  69, B =   0 }); // OrangeRed
            Add(new RgbColor { R = 255, G =  69, B =   0 }); // OrangeRed1
            Add(new RgbColor { R = 238, G =  64, B =   0 }); // OrangeRed2
            Add(new RgbColor { R = 205, G =  55, B =   0 }); // OrangeRed3
            Add(new RgbColor { R = 139, G =  37, B =   0 }); // OrangeRed4
            Add(new RgbColor { R = 218, G = 112, B = 214 }); // Orchid
            Add(new RgbColor { R = 255, G = 131, B = 250 }); // Orchid1
            Add(new RgbColor { R = 238, G = 122, B = 233 }); // Orchid2
            Add(new RgbColor { R = 205, G = 105, B = 201 }); // Orchid3
            Add(new RgbColor { R = 139, G =  71, B = 137 }); // Orchid4
            Add(new RgbColor { R = 238, G = 232, B = 170 }); // PaleGoldenrod
            Add(new RgbColor { R = 152, G = 251, B = 152 }); // PaleGreen
            Add(new RgbColor { R = 154, G = 255, B = 154 }); // PaleGreen1
            Add(new RgbColor { R = 144, G = 238, B = 144 }); // PaleGreen2
            Add(new RgbColor { R = 124, G = 205, B = 124 }); // PaleGreen3
            Add(new RgbColor { R =  84, G = 139, B =  84 }); // PaleGreen4
            Add(new RgbColor { R = 175, G = 238, B = 238 }); // PaleTurquoise
            Add(new RgbColor { R = 187, G = 255, B = 255 }); // PaleTurquoise1
            Add(new RgbColor { R = 174, G = 238, B = 238 }); // PaleTurquoise2
            Add(new RgbColor { R = 150, G = 205, B = 205 }); // PaleTurquoise3
            Add(new RgbColor { R = 102, G = 139, B = 139 }); // PaleTurquoise4
            Add(new RgbColor { R = 219, G = 112, B = 147 }); // PaleVioletRed
            Add(new RgbColor { R = 255, G = 130, B = 171 }); // PaleVioletRed1
            Add(new RgbColor { R = 238, G = 121, B = 159 }); // PaleVioletRed2
            Add(new RgbColor { R = 205, G = 104, B = 137 }); // PaleVioletRed3
            Add(new RgbColor { R = 139, G =  71, B =  93 }); // PaleVioletRed4
            Add(new RgbColor { R = 255, G = 239, B = 213 }); // PapayaWhip
            Add(new RgbColor { R = 255, G = 218, B = 185 }); // PeachPuff
            Add(new RgbColor { R = 255, G = 218, B = 185 }); // PeachPuff1
            Add(new RgbColor { R = 238, G = 203, B = 173 }); // PeachPuff2
            Add(new RgbColor { R = 205, G = 175, B = 149 }); // PeachPuff3
            Add(new RgbColor { R = 139, G = 119, B = 101 }); // PeachPuff4
            Add(new RgbColor { R = 205, G = 133, B =  63 }); // Peru
            Add(new RgbColor { R = 255, G = 192, B = 203 }); // Pink
            Add(new RgbColor { R = 255, G = 181, B = 197 }); // Pink1
            Add(new RgbColor { R = 238, G = 169, B = 184 }); // Pink2
            Add(new RgbColor { R = 205, G = 145, B = 158 }); // Pink3
            Add(new RgbColor { R = 139, G =  99, B = 108 }); // Pink4
            Add(new RgbColor { R = 221, G = 160, B = 221 }); // Plum
            Add(new RgbColor { R = 255, G = 187, B = 255 }); // Plum1
            Add(new RgbColor { R = 238, G = 174, B = 238 }); // Plum2
            Add(new RgbColor { R = 205, G = 150, B = 205 }); // Plum3
            Add(new RgbColor { R = 139, G = 102, B = 139 }); // Plum4
            Add(new RgbColor { R = 176, G = 224, B = 230 }); // PowderBlue
            Add(new RgbColor { R = 160, G =  32, B = 240 }); // Purple
            Add(new RgbColor { R = 155, G =  48, B = 255 }); // Purple1
            Add(new RgbColor { R = 145, G =  44, B = 238 }); // Purple2
            Add(new RgbColor { R = 125, G =  38, B = 205 }); // Purple3
            Add(new RgbColor { R =  85, G =  26, B = 139 }); // Purple4
            Add(new RgbColor { R = 255, G =   0, B =   0 }); // Red
            Add(new RgbColor { R = 255, G =   0, B =   0 }); // Red1
            Add(new RgbColor { R = 238, G =   0, B =   0 }); // Red2
            Add(new RgbColor { R = 205, G =   0, B =   0 }); // Red3
            Add(new RgbColor { R = 139, G =   0, B =   0 }); // Red4
            Add(new RgbColor { R = 188, G = 143, B = 143 }); // RosyBrown
            Add(new RgbColor { R = 255, G = 193, B = 193 }); // RosyBrown1
            Add(new RgbColor { R = 238, G = 180, B = 180 }); // RosyBrown2
            Add(new RgbColor { R = 205, G = 155, B = 155 }); // RosyBrown3
            Add(new RgbColor { R = 139, G = 105, B = 105 }); // RosyBrown4
            Add(new RgbColor { R =  65, G = 105, B = 225 }); // RoyalBlue
            Add(new RgbColor { R =  72, G = 118, B = 255 }); // RoyalBlue1
            Add(new RgbColor { R =  67, G = 110, B = 238 }); // RoyalBlue2
            Add(new RgbColor { R =  58, G =  95, B = 205 }); // RoyalBlue3
            Add(new RgbColor { R =  39, G =  64, B = 139 }); // RoyalBlue4
            Add(new RgbColor { R = 139, G =  69, B =  19 }); // SaddleBrown
            Add(new RgbColor { R = 250, G = 128, B = 114 }); // Salmon
            Add(new RgbColor { R = 255, G = 140, B = 105 }); // Salmon1
            Add(new RgbColor { R = 238, G = 130, B =  98 }); // Salmon2
            Add(new RgbColor { R = 205, G = 112, B =  84 }); // Salmon3
            Add(new RgbColor { R = 139, G =  76, B =  57 }); // Salmon4
            Add(new RgbColor { R = 244, G = 164, B =  96 }); // SandyBrown
            Add(new RgbColor { R =  46, G = 139, B =  87 }); // SeaGreen
            Add(new RgbColor { R =  84, G = 255, B = 159 }); // SeaGreen1
            Add(new RgbColor { R =  78, G = 238, B = 148 }); // SeaGreen2
            Add(new RgbColor { R =  67, G = 205, B = 128 }); // SeaGreen3
            Add(new RgbColor { R =  46, G = 139, B =  87 }); // SeaGreen4
            Add(new RgbColor { R = 255, G = 245, B = 238 }); // Seashell
            Add(new RgbColor { R = 255, G = 245, B = 238 }); // Seashell1
            Add(new RgbColor { R = 238, G = 229, B = 222 }); // Seashell2
            Add(new RgbColor { R = 205, G = 197, B = 191 }); // Seashell3
            Add(new RgbColor { R = 139, G = 134, B = 130 }); // Seashell4
            Add(new RgbColor { R = 160, G =  82, B =  45 }); // Sienna
            Add(new RgbColor { R = 255, G = 130, B =  71 }); // Sienna1
            Add(new RgbColor { R = 238, G = 121, B =  66 }); // Sienna2
            Add(new RgbColor { R = 205, G = 104, B =  57 }); // Sienna3
            Add(new RgbColor { R = 139, G =  71, B =  38 }); // Sienna4
            Add(new RgbColor { R = 135, G = 206, B = 235 }); // SkyBlue
            Add(new RgbColor { R = 135, G = 206, B = 255 }); // SkyBlue1
            Add(new RgbColor { R = 126, G = 192, B = 238 }); // SkyBlue2
            Add(new RgbColor { R = 108, G = 166, B = 205 }); // SkyBlue3
            Add(new RgbColor { R =  74, G = 112, B = 139 }); // SkyBlue4
            Add(new RgbColor { R = 106, G =  90, B = 205 }); // SlateBlue
            Add(new RgbColor { R = 131, G = 111, B = 255 }); // SlateBlue1
            Add(new RgbColor { R = 122, G = 103, B = 238 }); // SlateBlue2
            Add(new RgbColor { R = 105, G =  89, B = 205 }); // SlateBlue3
            Add(new RgbColor { R =  71, G =  60, B = 139 }); // SlateBlue4
            Add(new RgbColor { R = 112, G = 128, B = 144 }); // SlateGray
            Add(new RgbColor { R = 198, G = 226, B = 255 }); // SlateGray1
            Add(new RgbColor { R = 185, G = 211, B = 238 }); // SlateGray2
            Add(new RgbColor { R = 159, G = 182, B = 205 }); // SlateGray3
            Add(new RgbColor { R = 108, G = 123, B = 139 }); // SlateGray4
            Add(new RgbColor { R = 112, G = 128, B = 144 }); // SlateGrey
            Add(new RgbColor { R = 255, G = 250, B = 250 }); // Snow
            Add(new RgbColor { R = 255, G = 250, B = 250 }); // Snow1
            Add(new RgbColor { R = 238, G = 233, B = 233 }); // Snow2
            Add(new RgbColor { R = 205, G = 201, B = 201 }); // Snow3
            Add(new RgbColor { R = 139, G = 137, B = 137 }); // Snow4
            Add(new RgbColor { R =   0, G = 255, B = 127 }); // SpringGreen
            Add(new RgbColor { R =   0, G = 255, B = 127 }); // SpringGreen1
            Add(new RgbColor { R =   0, G = 238, B = 118 }); // SpringGreen2
            Add(new RgbColor { R =   0, G = 205, B = 102 }); // SpringGreen3
            Add(new RgbColor { R =   0, G = 139, B =  69 }); // SpringGreen4
            Add(new RgbColor { R =  70, G = 130, B = 180 }); // SteelBlue
            Add(new RgbColor { R =  99, G = 184, B = 255 }); // SteelBlue1
            Add(new RgbColor { R =  92, G = 172, B = 238 }); // SteelBlue2
            Add(new RgbColor { R =  79, G = 148, B = 205 }); // SteelBlue3
            Add(new RgbColor { R =  54, G = 100, B = 139 }); // SteelBlue4
            Add(new RgbColor { R = 210, G = 180, B = 140 }); // Tan
            Add(new RgbColor { R = 255, G = 165, B =  79 }); // Tan1
            Add(new RgbColor { R = 238, G = 154, B =  73 }); // Tan2
            Add(new RgbColor { R = 205, G = 133, B =  63 }); // Tan3
            Add(new RgbColor { R = 139, G =  90, B =  43 }); // Tan4
            Add(new RgbColor { R = 216, G = 191, B = 216 }); // Thistle
            Add(new RgbColor { R = 255, G = 225, B = 255 }); // Thistle1
            Add(new RgbColor { R = 238, G = 210, B = 238 }); // Thistle2
            Add(new RgbColor { R = 205, G = 181, B = 205 }); // Thistle3
            Add(new RgbColor { R = 139, G = 123, B = 139 }); // Thistle4
            Add(new RgbColor { R = 255, G =  99, B =  71 }); // Tomato
            Add(new RgbColor { R = 255, G =  99, B =  71 }); // Tomato1
            Add(new RgbColor { R = 238, G =  92, B =  66 }); // Tomato2
            Add(new RgbColor { R = 205, G =  79, B =  57 }); // Tomato3
            Add(new RgbColor { R = 139, G =  54, B =  38 }); // Tomato4
            Add(new RgbColor { R =  64, G = 224, B = 208 }); // Turquoise
            Add(new RgbColor { R =   0, G = 245, B = 255 }); // Turquoise1
            Add(new RgbColor { R =   0, G = 229, B = 238 }); // Turquoise2
            Add(new RgbColor { R =   0, G = 197, B = 205 }); // Turquoise3
            Add(new RgbColor { R =   0, G = 134, B = 139 }); // Turquoise4
            Add(new RgbColor { R = 238, G = 130, B = 238 }); // Violet
            Add(new RgbColor { R = 208, G =  32, B = 144 }); // VioletRed
            Add(new RgbColor { R = 255, G =  62, B = 150 }); // VioletRed1
            Add(new RgbColor { R = 238, G =  58, B = 140 }); // VioletRed2
            Add(new RgbColor { R = 205, G =  50, B = 120 }); // VioletRed3
            Add(new RgbColor { R = 139, G =  34, B =  82 }); // VioletRed4
            Add(new RgbColor { R = 245, G = 222, B = 179 }); // Wheat
            Add(new RgbColor { R = 255, G = 231, B = 186 }); // Wheat1
            Add(new RgbColor { R = 238, G = 216, B = 174 }); // Wheat2
            Add(new RgbColor { R = 205, G = 186, B = 150 }); // Wheat3
            Add(new RgbColor { R = 139, G = 126, B = 102 }); // Wheat4
            Add(new RgbColor { R = 255, G = 255, B = 255 }); // White
            Add(new RgbColor { R = 245, G = 245, B = 245 }); // WhiteSmoke
            Add(new RgbColor { R = 255, G = 255, B =   0 }); // Yellow
            Add(new RgbColor { R = 255, G = 255, B =   0 }); // Yellow1
            Add(new RgbColor { R = 238, G = 238, B =   0 }); // Yellow2
            Add(new RgbColor { R = 205, G = 205, B =   0 }); // Yellow3
            Add(new RgbColor { R = 139, G = 139, B =   0 }); // Yellow4
            Add(new RgbColor { R = 154, G = 205, B =  50 }); // YellowGreen
        }
    }
}
