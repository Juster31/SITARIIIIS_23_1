using SITARIIIIS_23_1.Models;
using SITARIIIIS_23_1.Patterns;


void ShowDoc(Document doc, CommandManager manager, string header)
{
    Console.WriteLine($"  --- {header} ---");
    Console.WriteLine(doc.Render());
    Console.WriteLine($"  (undo={manager.UndoCount}, redo={manager.RedoCount})");
    Console.WriteLine();
}

var doc = new Document();
var manager = new CommandManager();
var caretaker = new HistoryCaretaker();

Console.WriteLine("==== Вариант 1. Текстовый редактор с историей изменений ====");
Console.WriteLine();

ShowDoc(doc, manager, "исходный документ");

// ========== ДЕМО 1: выполнение команд ==========
Console.WriteLine("=== Демо 1: выполнение команд ===");
manager.Execute(new InsertLineCommand(doc, 0, "Привет, мир!"));
manager.Execute(new InsertLineCommand(doc, 1, "Вторая строка"));
manager.Execute(new ReplaceTextCommand(doc, 0, "Hello, World!"));
manager.Execute(new FormatLineCommand(doc, 0, "bold"));
ShowDoc(doc, manager, "после 4 команд");

// ========== ДЕМО 2: многоступенчатая отмена ==========
Console.WriteLine("=== Демо 2: многоступенчатая отмена ===");
manager.Undo(); ShowDoc(doc, manager, "1 undo (снято форматирование)");
manager.Undo(); ShowDoc(doc, manager, "2 undo (снята замена текста)");
manager.Undo(); ShowDoc(doc, manager, "3 undo (удалена 2-я строка)");
manager.Undo(); ShowDoc(doc, manager, "4 undo (документ пуст)");
manager.Undo(); // «нечего отменять»

// ========== ДЕМО 3: повтор ==========
Console.WriteLine();
Console.WriteLine("=== Демо 3: повтор действий ===");
manager.Redo(); ShowDoc(doc, manager, "1 redo");
manager.Redo(); ShowDoc(doc, manager, "2 redo");
manager.Redo(); ShowDoc(doc, manager, "3 redo");
manager.Redo(); ShowDoc(doc, manager, "4 redo (всё повторено)");
manager.Redo(); // «нечего повторять»

// ========== ДЕМО 4: новая команда сбрасывает redo ==========
Console.WriteLine();
Console.WriteLine("=== Демо 4: новая команда очищает redo ===");
manager.Undo();
manager.Undo();
ShowDoc(doc, manager, "после 2 undo");
manager.Execute(new InsertLineCommand(doc, 0, "Новая строка"));
ShowDoc(doc, manager, "после новой команды — redo должен быть пуст");
manager.Redo(); // «нечего повторять»

// ========== ДЕМО 5: чекпоинт (Memento) ==========
Console.WriteLine();
Console.WriteLine("=== Демо 5: чекпоинт и возврат к нему ===");

caretaker.Save("before_edit", doc);
ShowDoc(doc, manager, "снимок \"before_edit\" сохранён");

manager.Execute(new InsertLineCommand(doc, 1, "Заголовок"));
manager.Execute(new FormatLineCommand(doc, 2, "italic"));
ShowDoc(doc, manager, "после двух новых команд");

caretaker.Restore("before_edit", doc);
manager.ClearHistory(); // история команд больше не соответствует состоянию
ShowDoc(doc, manager, "после восстановления чекпоинта");

// несуществующий чекпоинт
caretaker.Restore("no_such", doc);

Console.WriteLine();
Console.WriteLine("Готово. Нажмите Enter для выхода.");
Console.ReadLine();

/*
var doc = new Document();
var manager = new CommandManager();
var caretaker = new HistoryCaretaker();

while (true)
{
    
    Console.WriteLine();
    Console.WriteLine("=== Меню редактора ===");
    Console.WriteLine("1. Показать документ");
    Console.WriteLine("2. Вставить строку");
    Console.WriteLine("3. Удалить строку");
    Console.WriteLine("4. Заменить текст строки");
    Console.WriteLine("5. Форматировать строку");
    Console.WriteLine("6. Undo");
    Console.WriteLine("7. Redo");
    Console.WriteLine("8. Сохранить чекпоинт");
    Console.WriteLine("9. Восстановить чекпоинт");
    Console.WriteLine("10. Прогнать авто-демо всех паттернов");
    Console.WriteLine("0. Выход");
    Console.Write("Выбор: ");

    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                Console.WriteLine(doc.Render());
                Console.WriteLine($"undo={manager.UndoCount}, redo={manager.RedoCount}");
                Console.WriteLine();
                break;

            case "2":
                Console.Write("Индекс: ");
                var insIdx = int.Parse(Console.ReadLine()!);
                Console.Write("Текст: ");
                var insText = Console.ReadLine() ?? "";
                manager.Execute(new InsertLineCommand(doc, insIdx, insText));
                Console.WriteLine();
                break;

            case "3":
                Console.Write("Индекс: ");
                var delIdx = int.Parse(Console.ReadLine()!);
                manager.Execute(new DeleteLineCommand(doc, delIdx));
                Console.WriteLine();
                break;

            case "4":
                Console.Write("Индекс: ");
                var repIdx = int.Parse(Console.ReadLine()!);
                Console.Write("Новый текст: ");
                var repText = Console.ReadLine() ?? "";
                manager.Execute(new ReplaceTextCommand(doc, repIdx, repText));
                Console.WriteLine();
                break;

            case "5":
                Console.Write("Индекс: ");
                var fmtIdx = int.Parse(Console.ReadLine()!);
                Console.Write("Формат (bold/italic/underline/пусто): ");
                var fmt = Console.ReadLine() ?? "";
                manager.Execute(new FormatLineCommand(doc, fmtIdx, fmt));
                Console.WriteLine();
                break;

            case "6":
                manager.Undo();
                Console.WriteLine();
                break;

            case "7":
                manager.Redo();
                Console.WriteLine();
                break;

            case "8":
                Console.Write("Имя чекпоинта: ");
                var name = Console.ReadLine() ?? "default";
                caretaker.Save(name, doc);
                Console.WriteLine(); 
                break;

            case "9":
                Console.Write("Имя чекпоинта: ");
                var rname = Console.ReadLine() ?? "default";
                if (caretaker.Restore(rname, doc))
                    manager.ClearHistory();
                Console.WriteLine();
                break;

            case "10":
                RunAutoDemo(doc, manager, caretaker);
                Console.WriteLine();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Неизвестная команда");
                Console.WriteLine();
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
}

// Авто-демо вынесено в отдельный локальный метод, чтобы не путать с меню
static void RunAutoDemo(Document doc, CommandManager manager, HistoryCaretaker caretaker)
{


    Console.WriteLine("--- авто-демо: команды + undo/redo ---");
    manager.Execute(new InsertLineCommand(doc, 0, "Привет, мир!"));
    manager.Execute(new InsertLineCommand(doc, 1, "Вторая строка"));
    manager.Execute(new FormatLineCommand(doc, 0, "bold"));
    Console.WriteLine(doc.Render());
    manager.Undo();
    manager.Undo();
    manager.Redo();
    Console.WriteLine(doc.Render());

    Console.WriteLine("--- авто-демо: чекпоинт ---");
    caretaker.Save("auto", doc);
    manager.Execute(new ReplaceTextCommand(doc, 0, "Изменено"));
    caretaker.Restore("auto", doc);
    manager.ClearHistory();
    Console.WriteLine(doc.Render());
}

void ShowDoc(Document doc, CommandManager manager, string header)
{
    Console.WriteLine($"  --- {header} ---");
    Console.WriteLine(doc.Render());
    Console.WriteLine($"  (undo={manager.UndoCount}, redo={manager.RedoCount})");
    Console.WriteLine();
}
*/