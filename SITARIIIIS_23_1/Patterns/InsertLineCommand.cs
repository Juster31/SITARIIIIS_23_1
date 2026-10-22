using SITARIIIIS_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITARIIIIS_23_1.Patterns
{
    public sealed class InsertLineCommand : ICommand
    {
        private readonly Document _doc;
        private readonly int _index;
        private readonly string _text;

        public InsertLineCommand(Document doc, int index, string text)
        {
            _doc = doc; _index = index; _text = text;
        }

        public string Description => $"Вставить строку {_index}: \"{_text}\"";

        public void Execute() => _doc.InsertTextLine(_index, new TextLine(_text));

        public void Undo() => _doc.DeleteLine(_index);
    }
}
