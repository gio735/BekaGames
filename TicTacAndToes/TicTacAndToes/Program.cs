namespace TicTacAndToes
{
    internal class Program
    {
        static List<TicTacToe> Games { get; set; } = new();
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Click anything to continue!");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("1. Start game\n2. View previous games\n3. Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Whats name of player X");
                        var nameX = Console.ReadLine();
                        Console.WriteLine("Whats name of player O");
                        var nameY = Console.ReadLine();
                        Games.Add(new(TakeNumber("Choose table size (3-34): "), nameX, nameY));
                        break;
                    case "2":
                        int index = 1;
                        if (Games.Count == 0)
                        {
                            Console.WriteLine("There are no records!");
                            continue;
                        }
                        foreach (var item in Games)
                        {
                            Console.WriteLine($"{index++}. X - {item.PlayerXName} VS {item.PlayerOName} - O");
                        }
                        int targetId = TakeNumber("Give ID to check game table: ");
                        if (targetId > 0 && targetId <= Games.Count)
                        {
                            Games[targetId - 1].Info();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public static int TakeNumber(string message)
        {
            while (true)
            {
                Console.Write(message);
                bool parsed = int.TryParse(Console.ReadLine(), out int result);
                if (parsed)
                {
                    return result;
                }
                else
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop--;
                    string emptyLine = new(' ', Console.WindowWidth);
                    Console.Write(emptyLine);
                    Console.CursorLeft = 0;
                }
            }
        }
    }
}