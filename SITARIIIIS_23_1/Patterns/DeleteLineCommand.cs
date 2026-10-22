using SITARIIIIS_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITARIIIIS_23_1.Patterns
{
    public sealed class DeleteLineCommand : ICommand
    {
        private readonly Document _doc;
        private readonly int _index;
        private TextLine? _deleted;

        public DeleteLineCommand(Document doc, int index)
        {
            _doc = doc; _index = index;
        }

        public string Description => $"Удалить строку {_index}";

        public void Execute()
        {
            _deleted = _doc.Lines[_index].Clone();
            _doc.DeleteLine(_index);
        }

        public void Undo() => _doc.InsertTextLine(_index, _deleted!);
    }
}
