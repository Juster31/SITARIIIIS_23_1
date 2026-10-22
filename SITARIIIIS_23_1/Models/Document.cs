using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace SITARIIIIS_23_1.Models
{
    /// <summary>
    /// Документ: список строк. Состояние скрыто за методами;
    /// снаружи можно получить только копию через Memento.
    /// </summary>
    public sealed class Document
    {
        private readonly List<TextLine> _lines = new();
        public IReadOnlyList<TextLine> Lines => _lines;
        public int LineCount => _lines.Count;

        public void InsertTextLine(int index, TextLine line)
        {
            if (index < 0 || index > _lines.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
            _lines.Insert(index, line.Clone());
        }
        public void DeleteLine(int index)
        {
            if (index < 0 || index >= _lines.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
            _lines.RemoveAt(index);
        }
        public void ReplaceText(int index, string newText)
        {
            if (index < 0 || index >= _lines.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
            _lines[index].Text = newText;
        }
        public void SetFormat(int index, string format)
        {
            if (index < 0 || index >= _lines.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
            _lines[index].Format = format;
        }

        // ----- Memento -----

        public DocumentMemento CreateMemento()
        {
            var snapshot = _lines.Select(l => l.Clone()).ToList();
            return new DocumentMemento(snapshot);
        }

        public void Restore(DocumentMemento memento)
        {
            _lines.Clear();
            foreach (var line in memento.Lines)
                _lines.Add(line.Clone());
        }

        public string Render()
        {
            if (_lines.Count == 0) return "  (пустой документ)";
            var sb = new StringBuilder();
            for (int i = 0; i < _lines.Count; i++)
                sb.AppendLine($"  {i}: {_lines[i]}");
            return sb.ToString().TrimEnd();
        }
    }
}
