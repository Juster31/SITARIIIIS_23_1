using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITARIIIIS_23_1.Patterns
{
    /// <summary>Invoker: хранит историю команд, выполняет undo/redo.</summary>
    public sealed class CommandManager
    {
        private readonly Stack<ICommand> _undo = new();
        private readonly Stack<ICommand> _redo = new();

        public int UndoCount => _undo.Count;
        public int RedoCount => _redo.Count;

        public void Execute(ICommand command)
        {
            command.Execute();
            _undo.Push(command);
            _redo.Clear();
            Console.WriteLine($"  [команда] {command.Description}");
        }

        public bool Undo()
        {
            if (_undo.Count == 0)
            {
                Console.WriteLine("  [undo] нечего отменять");
                return false;
            }
            var cmd = _undo.Pop();
            cmd.Undo();
            _redo.Push(cmd);
            Console.WriteLine($"  [undo] отменена: {cmd.Description}");
            return true;
        }

        public bool Redo()
        {
            if (_redo.Count == 0)
            {
                Console.WriteLine("  [redo] нечего повторять");
                return false;
            }
            var cmd = _redo.Pop();
            cmd.Execute();
            _undo.Push(cmd);
            Console.WriteLine($"  [redo] повторена: {cmd.Description}");
            return true;
        }

        /// <summary>Сбросить историю (например, после восстановления чекпоинта).</summary>
        public void ClearHistory()
        {
            _undo.Clear();
            _redo.Clear();
            Console.WriteLine("  [history] история команд очищена");
        }
    }
}
