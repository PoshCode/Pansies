using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ColorMine.ColorSpaces.Conversions
{
    // Code lovingly copied from StackOverflow (and tweaked a bit)
    // Question/Answer: http://stackoverflow.com/questions/5237104/c-sharp-convert-Rgb-value-to-cmyk-using-an-icc-profile/5251318#5251318
    // Submitter: Codo http://stackoverflow.com/users/413337/codo
    // License: http://creativecommons.org/licenses/by-sa/3.0/
    internal class CmykProfileConverter
    {
        internal static ICmyk TranslateColor(IRgb rgb, Uri cmykProfile, Uri rgbProfile)
        {
            const double dividend = 65535;
            var profileName = new StringBuilder(256);
            uint size = (uint)profileName.Capacity * 2;

            ProfileFilename sRgbFilename;
            if (rgbProfile == null)
            {
                GetStandardColorSpaceProfile(0, LogicalColorSpace.sRgb, profileName, ref size);
                sRgbFilename = new ProfileFilename(profileName.ToString());
            }
            else
            {
                sRgbFilename = new ProfileFilename(rgbProfile.LocalPath);
            }

            IntPtr hSRgbProfile = OpenColorProfile(sRgbFilename, ProfileRead, FileShare.Read,
                                                   CreateDisposition.OpenExisting);
            var isoCoatedFilename = new ProfileFilename(cmykProfile.LocalPath);
            IntPtr hIsoCoatedProfile = OpenColorProfile(isoCoatedFilename, ProfileRead, FileShare.Read,
                                                        CreateDisposition.OpenExisting);

            var profiles = new[] { hSRgbProfile, hIsoCoatedProfile };
            var intents = new[] { IntentPerceptual };
            IntPtr transform = CreateMultiProfileTransform(profiles, 2, intents, 1, ColorTransformMode.BestMode,
                                                           IndexDontCare);

            var rgbColors = new RgbColor[1];
            rgbColors[0] = new RgbColor();

            var cmykColors = new CmykColor[1];
            cmykColors[0] = new CmykColor();

            rgbColors[0].red = (ushort)(rgb.R * dividend / 255.0);
            rgbColors[0].green = (ushort)(rgb.G * dividend / 255.0);
            rgbColors[0].blue = (ushort)(rgb.B * dividend / 255.0);

            TranslateColors(transform, rgbColors, 1, ColorType.Rgb, cmykColors, ColorType.CMYK);

            DeleteColorTransform(transform);
            CloseColorProfile(hSRgbProfile);
            CloseColorProfile(hIsoCoatedProfile);

            return new Cmyk
            {
                C = cmykColors[0].cyan / dividend,
                M = cmykColors[0].magenta / dividend,
                Y = cmykColors[0].yellow / dividend,
                K = cmykColors[0].black / dividend
            };
        }

        internal static ICmyk TranslateColor(IRgb rgb, Uri cmykProfile)
        {
            var profileName = new StringBuilder(256);
            uint size = (uint)profileName.Capacity * 2;
            GetStandardColorSpaceProfile(0, LogicalColorSpace.sRgb, profileName, ref size);
            var rgbFilename = new ProfileFilename(profileName.ToString());
            return TranslateColor(rgb, cmykProfile, new Uri(rgbFilename.profileData));
        }

        #region Private
        private const uint ProfileFilenameType = 1;
        private const uint ProfileMembufferType = 2;

        private const uint ProfileRead = 1;
        private const uint ProfileReadWrite = 2;

        private const uint IntentPerceptual = 0;
        private const uint IntentRelativeColorimetric = 1;
        private const uint IntentSaturation = 2;
        private const uint IntentAbsoluteColorimetric = 3;

        private const uint IndexDontCare = 0;

        [DllImport("mscms.dll", SetLastError = true, EntryPoint = "OpenColorProfileW",CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr OpenColorProfile(
            [MarshalAs(UnmanagedType.LPStruct)] ProfileFilename profile,
            uint desiredAccess,
            FileShare shareMode,
            CreateDisposition creationMode);

        [DllImport("mscms.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool CloseColorProfile(IntPtr hProfile);

        [DllImport("mscms.dll", SetLastError = true, EntryPoint = "GetStandardColorSpaceProfileW",CallingConvention = CallingConvention.Winapi)]
        private static extern bool GetStandardColorSpaceProfile(
            uint machineName,
            LogicalColorSpace profileID,
            [MarshalAs(UnmanagedType.LPTStr), In, Out] StringBuilder profileName,
            ref uint size);

        [DllImport("mscms.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr CreateMultiProfileTransform(
            [In] IntPtr[] profiles,
            uint nProfiles,
            [In] uint[] intents,
            uint nIntents,
            ColorTransformMode flags,
            uint indexPreferredCMM);

        [DllImport("mscms.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool DeleteColorTransform(IntPtr hTransform);

        [DllImport("mscms.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool TranslateColors(
            IntPtr hColorTransform,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), In] RgbColor[] inputColors,
            uint nColors,
            ColorType ctInput,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Out] CmykColor[] outputColors,
            ColorType ctOutput);

        [StructLayout(LayoutKind.Sequential)]
        private struct CmykColor
        {
            internal ushort cyan;
            internal ushort magenta;
            internal ushort yellow;
            internal ushort black;
        };

        private enum ColorTransformMode : uint
        {
            ProofMode = 0x00000001,
            NormalMode = 0x00000002,
            BestMode = 0x00000003,
            EnableGamutChecking = 0x00010000,
            UseRelativeColorimetric = 0x00020000,
            FastTranslate = 0x00040000,
            PreserveBlack = 0x00100000,
            WCSAlways = 0x00200000
        };

        private enum ColorType
        {
            Gray = 1,
            Rgb = 2,
            XYZ = 3,
            Yxy = 4,
            Lab = 5,
            _3_Channel = 6,
            CMYK = 7,
            _5_Channel = 8,
            _6_Channel = 9,
            _7_Channel = 10,
            _8_Channel = 11,
            Named = 12
        };

        private enum CreateDisposition : uint
        {
            CreateNew = 1,
            CreateAlways = 2,
            OpenExisting = 3,
            OpenAlways = 4,
            TruncateExisting = 5
        };

        private enum FileShare : uint
        {
            Read = 1,
            Write = 2,
            Delete = 4
        };

        private enum LogicalColorSpace : uint
        {
            CalibratedRgb = 0x00000000,
            sRgb = 0x73524742,
            WindowsColorSpace = 0x57696E20
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private class ProfileFilename
        {
            internal uint type;
            [MarshalAs(UnmanagedType.LPTStr)]internal string profileData;
            private uint dataSize;

            internal ProfileFilename(string filename)
            {
                type = ProfileFilenameType;
                profileData = filename;
                dataSize = (uint) filename.Length*2 + 2;
            }
        };

        [StructLayout(LayoutKind.Sequential)]
        private struct RgbColor
        {
            internal ushort red;
            internal ushort green;
            internal ushort blue;
            internal ushort pad;
        };
        #endregion Private
    }
}