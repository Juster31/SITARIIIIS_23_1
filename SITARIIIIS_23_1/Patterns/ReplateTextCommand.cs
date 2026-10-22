using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SITARIIIIS_23_1.Models;

namespace SITARIIIIS_23_1.Patterns
{
    public sealed class ReplaceTextCommand : ICommand
    {
        private readonly Document _doc;
        private readonly int _index;
        private readonly string _newText;
        private string _oldText = string.Empty;

        public ReplaceTextCommand(Document doc, int index, string newText)
        {
            _doc = doc; _index = index; _newText = newText;
        }

        public string Description => $"Заменить текст строки {_index} на \"{_newText}\"";

        public void Execute()
        {
            _oldText = _doc.Lines[_index].Text;
            _doc.ReplaceText(_index, _newText);
        }

        public void Undo() => _doc.ReplaceText(_index, _oldText);
    }
}
