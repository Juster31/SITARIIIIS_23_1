using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SITARIIIIS_23_1.Models;

namespace SITARIIIIS_23_1.Patterns
{
    public sealed class FormatLineCommand : ICommand
    {
        private readonly Document _doc;
        private readonly int _index;
        private readonly string _newFormat;
        private string _oldFormat = string.Empty;

        public FormatLineCommand(Document doc, int index, string newFormat)
        {
            _doc = doc; _index = index; _newFormat = newFormat;
        }

        public string Description => $"Форматировать строку {_index} как \"{_newFormat}\"";

        public void Execute()
        {
            _oldFormat = _doc.Lines[_index].Format;
            _doc.SetFormat(_index, _newFormat);
        }

        public void Undo() => _doc.SetFormat(_index, _oldFormat);
    }
}
