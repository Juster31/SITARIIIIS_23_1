using SITARIIIIS_23_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITARIIIIS_23_1.Patterns
{
    /// <summary>Caretaker: хранит снимки документа по именам.</summary>
    public sealed class HistoryCaretaker
    {
        private readonly Dictionary<string, DocumentMemento> _checkpoints = new();

        public void Save(string name, Document doc)
        {
            _checkpoints[name] = doc.CreateMemento();
            Console.WriteLine($"  [memento] сохранён чекпоинт \"{name}\"");
        }

        public bool Restore(string name, Document doc)
        {
            if (!_checkpoints.TryGetValue(name, out var memento))
            {
                Console.WriteLine($"  [memento] чекпоинт \"{name}\" не найден");
                return false;
            }
            doc.Restore(memento);
            Console.WriteLine($"  [memento] восстановлен чекпоинт \"{name}\"");
            return true;
        }
    }
}
