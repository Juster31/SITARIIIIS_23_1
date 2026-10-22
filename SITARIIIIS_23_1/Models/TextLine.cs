using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITARIIIIS_23_1.Models
{
    /// <summary>Строка документа: текст + оформление.</summary>
    public sealed class TextLine
    {
        public string Text { get; set; }
        public string Format { get; set; } // "", "bold", "italic", "underline"

        public TextLine(string text, string format = "")
        {
            Text = text;
            Format = format;
        }

        public TextLine Clone() => new(Text, Format);

        public override string ToString() =>
            string.IsNullOrEmpty(Format) ? Text : $"[{Format}] {Text}";
    }
}
