using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRanking
{
    internal class Program
    {
        private const string CommandShowTopLevel = "1";
        private const string CommandShowTopStrength = "2";
        private const string CommandExit = "0";

        private const int TopPlayersCount = 3;

        static void Main(string[] args)
        {
            List<Player> players = new List<Player>
            {
                new Player("Jinx"),
                new Player("Vi"),
                new Player("Charlie"),
                new Player("David"),
                new Player("Eve"),
                new Player("Silko"),
                new Player("Grace"),
                new Player("Hank"),
                new Player("Ivy"),
                new Player("Jack")
            };

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                Console.WriteLine("Список всех игроков:");
                ShowPlayers(players);

                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine($"{CommandShowTopLevel} - Показать топ-{TopPlayersCount} игроков по уровню");
                Console.WriteLine($"{CommandShowTopStrength} - Показать топ-{TopPlayersCount} игроков по силе");
                Console.WriteLine($"{CommandExit} - Выйти из программы");
                Console.Write("Введите команду: ");

                string level = "Уровень";
                string strenght = "Сила";
                string input = Console.ReadLine();

                switch (input)
                {
                    case CommandShowTopLevel:
                        ShowTopPlayers(players, "Топ игроков по уровню:", level, player => player.Level);
                        break;
                    case CommandShowTopStrength:
                        ShowTopPlayers(players, "Топ игроков по силе:", strenght, player => player.Strength);
                        break;
                    case CommandExit:
                        Console.WriteLine("Выход из программы...");
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Неверная команда! Попробуйте снова.");
                        break;
                }
            }
        }

        private static void ShowPlayers(IEnumerable<Player> players)
        {
            foreach (var player in players)
            Console.WriteLine(player);
        }

        private static void ShowTopPlayers(List<Player> players, string title, string parameterName, Func<Player, int> sortParameter)
        {
            Console.Clear();

            var topPlayers = players
                .OrderByDescending(sortParameter)
                .Take(TopPlayersCount);

            Console.WriteLine(title);
            foreach (var player in topPlayers)
            {
                PrintPlayerInfo(player, parameterName);
            }

            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }

        private static void PrintPlayerInfo(Player player, string parameterName)
        {
            string level = "Уровень";
            string name = "Имя";

            int value = parameterName == level ? player.Level : player.Strength;
            Console.WriteLine($"{name}: {player.Name}, {parameterName}: {value}");
        }
    }

    class Player
    {
        string level = "Уровень";
        string strenght = "Сила";
        string name = "Имя";

        public Player(string name)
        {
            Name = name;
            Level = UserUtils.GetRandomNumber(15, 36);
            Strength = UserUtils.GetRandomNumber(150, 251);
        }

        public string Name { get; }
        public int Level { get; }
        public int Strength { get; }

        public override string ToString()
        {
            return $"{name}: {Name}, {level}: {Level}, {strenght}: {Strength}";
        }
    }

    static class UserUtils
    {
        private static Random _random = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
