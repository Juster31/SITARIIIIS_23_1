using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITARIIIIS_23_1.Models
{
    /// <summary>
    /// Снимок состояния документа. Поля скрыты (internal),
    /// снаружи доступны только через Document.Restore.
    /// </summary>
    public sealed class DocumentMemento
    {
        internal IReadOnlyList<TextLine> Lines { get; }

        internal DocumentMemento(IReadOnlyList<TextLine> lines) => Lines = lines;
    }
}
