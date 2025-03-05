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

                string input = Console.ReadLine();

                switch (input)
                {
                    case CommandShowTopLevel:
                        ShowTopPlayers(players, player => player.Level, "Топ игроков по уровню:", "Уровень");
                        break;
                    case CommandShowTopStrength:
                        ShowTopPlayers(players, player => player.Strength, "Топ игроков по силе:", "Сила");
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

        private static void ShowTopPlayers(List<Player> players, Func<Player, int> primarySort, string title, string parameterName)
        {
            Console.Clear();

            var topPlayers = players
            .OrderByDescending(primarySort) 
            .Take(TopPlayersCount);

            Console.WriteLine(title);
            foreach (var player in topPlayers)
            {
                if (parameterName == "Уровень")
                {
                    Console.WriteLine($"Имя: {player.Name}, {parameterName}: {player.Level}");
                }
                else if (parameterName == "Сила")
                {
                    Console.WriteLine($"Имя: {player.Name}, {parameterName}: {player.Strength}");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }
    }

    class Player
    {
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
            return $"Имя: {Name}, Уровень: {Level}, Сила: {Strength}";
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
