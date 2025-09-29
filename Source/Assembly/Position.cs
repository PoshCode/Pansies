using System;
using System.Globalization;

namespace PoshCode.Pansies
{
    public class Position : IPsMetadataSerializable
    {
        public bool Absolute { get; set; }

        public int? Line { get; set; }

        public int? Column { get; set; }

        public Position()
        {
        }

        public Position(int? line, int? column, bool absolute = false)
        {
            Line = line;
            Column = column;
            Absolute = absolute;
        }

        public Position(string metadata)
        {
            FromPsMetadata(metadata);
        }

        public override string ToString()
        {
            if (Line is null && Column is null)
            {
                return string.Empty;
            }

            if (Absolute)
            {
                if (Line is null)
                {
                    return "\u001b" + $"[{Column}G";
                }

                if (Column is null)
                {
                    return "\u001b" + $"[{Line}d";
                }

                return "\u001b" + $"[{Line};{Column}H";
            }

            if (Column != null)
            {
                if (Column > 0)
                {
                    return "\u001b" + $"[{Column}C";
                }

                if (Column < 0)
                {
                    return "\u001b" + $"[{-Column}D";
                }
            }

            if (Line != null)
            {
                if (Line > 0)
                {
                    return "\u001b" + $"[{Line}B";
                }

                if (Line < 0)
                {
                    return "\u001b" + $"[{-Line}A";
                }
            }

            return string.Empty;
        }

        public string ToPsMetadata()
        {
            return $"{Line};{Column}{(Absolute ? ";1" : string.Empty)}";
        }

        public void FromPsMetadata(string metadata)
        {
            if (string.IsNullOrEmpty(metadata))
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            var data = metadata.Split(';');

            if (data.Length >= 3)
            {
                Absolute = data[2] == "1";
            }
            else
            {
                Absolute = false;
            }

            if (data.Length >= 1 && data[0].Length > 0)
            {
                Line = int.Parse(data[0], NumberFormatInfo.InvariantInfo);
            }
            else
            {
                Line = null;
            }

            if (data.Length >= 2 && data[1].Length > 0)
            {
                Column = int.Parse(data[1], NumberFormatInfo.InvariantInfo);
            }
            else
            {
                Column = null;
            }
        }
    }
}
