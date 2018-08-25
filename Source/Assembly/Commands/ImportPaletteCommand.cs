using ColorMine.Palettes;
using PoshCode.Pansies.Console;
using System;
using System.IO;
using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Import","Palette")]
    public class ImportPaletteCommand : Cmdlet
    {
        /// <summary>
        /// The path to the palette file to import
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
        [Alias("PSPath")]
        [ValidateNotNullOrEmpty()]
        public string Path { get; set; }

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = typeof(ImportPaletteCommand).Assembly.CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }

        protected override void ProcessRecord()
        {
            Palette<RgbColor> palette = null;
            base.ProcessRecord();

            string directory = System.IO.Path.GetDirectoryName(Path);
            string paletteName = System.IO.Path.GetFileNameWithoutExtension(Path);
            string extension = System.IO.Path.GetExtension(Path);

            // if they pass just a palette name, we can look in the module \schemes folder
            if (string.IsNullOrEmpty(directory))
            {
                directory = System.IO.Path.Combine(AssemblyDirectory, "palettes");
                if (!Directory.Exists(directory))
                {
                    ThrowTerminatingError(new ErrorRecord(new FileNotFoundException("Palette directory not found", directory), "PalettesDirectoryNotFound", ErrorCategory.ObjectNotFound, Path));
                }
            }

            // if they pass just a palette name, we can look for any known scheme file types
            if (string.IsNullOrEmpty(extension))
            {
                // For now, just hard-code the parsers:
                extension = ".itermcolors";
            }

            Path = System.IO.Path.Combine(directory, paletteName + extension);

            try
            {
                palette = Parsers.PListParser.Parse(Path);
            }
            catch (FileNotFoundException ex)
            {
                ThrowTerminatingError(new ErrorRecord(ex, "PaletteFileNotFound", ErrorCategory.ObjectNotFound, Path));
            }
            catch (Exception ex)
            {
                ThrowTerminatingError(new ErrorRecord(ex, "PaletteParseException", ErrorCategory.ParserError, Path));
            }

            if (palette == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidDataException("Uknown Palette file format: " + Path), "PaletteParseFailure", ErrorCategory.NotImplemented, Path));
            }

            WriteObject(palette);
        }
    }
}
