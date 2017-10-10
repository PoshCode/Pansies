using System;
using ColorMine.ColorSpaces.Conversions;
using System.Windows.Media;

namespace ColorMine.ColorSpaces
{
    public interface IColorSpaceWithProfile
    {
        IColorSpace Color { get; set; }
        Uri Profile { get; set; }
    }

    public class ColorSpaceWithProfile : IColorSpaceWithProfile
    {
        public IColorSpace Color { get; set; }
        public Uri Profile { get; set; }
    }

    public static class ColorSpaceExtensions
    {
        public static IColorSpaceWithProfile WithProfile(this IColorSpace colorSpace, Uri profile)
        {
            return new ColorSpaceWithProfile
                {
                    Color = colorSpace,
                    Profile = profile
                };
        }

        public static T To<T>(this IColorSpaceWithProfile color) where T : class, IColorSpace, new()
        {
            if (color.Color is ICmyk)
            {
                var rgb = ToColor(color.Color as ICmyk, color.Profile);
                return rgb.To<T>();
            }
            if (typeof(ICmyk).IsAssignableFrom(typeof(T)))
            {
                var rgb = color.Color.To<Rgb>();
                var item = new Cmyk();
                ToColorSpace(rgb, item, color.Profile);
                return item as T;
            }
            throw new ArgumentException("Profiles require that you are converting to or from Cmyk.");
        }

        public static IRgb ToColor(ICmyk item, Uri profile)
        {
            var points = new[] { (float)item.C, (float)item.M, (float)item.Y, (float)item.K };
            var color = Color.FromValues(points, profile);
            return new Rgb
            {
                R = color.R,
                G = color.G,
                B = color.B
            };
        }

        public static void ToColorSpace(IRgb color, ICmyk item, Uri cmykProfile)
        {
            if (cmykProfile == null)
            {
                CmykConverter.ToColorSpace(color, item);
                return;
            }

            var cmyk = CmykProfileConverter.TranslateColor(color, cmykProfile);
            item.C = cmyk.C;
            item.M = cmyk.M;
            item.Y = cmyk.Y;
            item.K = cmyk.K;
        }

        public static void ToColorSpace(IRgb color, ICmyk item, Uri cmykProfile, Uri rgbProfile)
        {
            if (rgbProfile == null)
            {
                ToColorSpace(color, item, cmykProfile);
                return;
            }

            var cmyk = CmykProfileConverter.TranslateColor(color, cmykProfile, rgbProfile);
            item.C = cmyk.C;
            item.M = cmyk.M;
            item.Y = cmyk.Y;
            item.K = cmyk.K;
        }

    }
}