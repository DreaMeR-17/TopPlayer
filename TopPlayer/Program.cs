using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRanking
{
    internal class Program
    {
        private static void Main()
        {
            ProgramRunner.Run();
        }
    }

    class ProgramRunner
    {
        private const string CommandShowTopLevel = "1";
        private const string CommandShowTopStrength = "2";
        private const string CommandExit = "0";

        private static readonly Database s_database = new Database();

        public static void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("Список всех игроков:");
                s_database.ShowAllPlayers();

                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine($"{CommandShowTopLevel} - Показать топ-{Database.TopPlayersCount} игроков по уровню");
                Console.WriteLine($"{CommandShowTopStrength} - Показать топ-{Database.TopPlayersCount} игроков по силе");
                Console.WriteLine($"{CommandExit} - Выйти из программы");
                Console.Write("Введите команду: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case CommandShowTopLevel:
                        s_database.ShowTopPlayersByLevel();
                        break;

                    case CommandShowTopStrength:
                        s_database.ShowTopPlayersByStrength();
                        break;

                    case CommandExit:
                        Console.WriteLine("🚪 Выход из программы...");
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("❌ Неверная команда! Попробуйте снова.");
                        break;
                }
            }
        }
    }

    class Database
    {
        public const int TopPlayersCount = 3;
        private const int MinLevel = 15;
        private const int MaxLevel = 35;
        private const int MinStrength = 150;
        private const int MaxStrength = 250;

        private readonly List<Player> _players;

        public Database()
        {
            _players = GeneratePlayers();
        }

        public void ShowAllPlayers()
        {
            foreach (Player player in _players)
            {
                Console.WriteLine(player);
            }
        }

        public void ShowTopPlayersByLevel()
        {
            ShowTopPlayers("Топ игроков по уровню:", "Уровень", _players.OrderByDescending(player => player.Level).Take(TopPlayersCount).ToList());
        }

        public void ShowTopPlayersByStrength()
        {
            ShowTopPlayers("Топ игроков по силе:", "Сила", _players.OrderByDescending(player => player.Strength).Take(TopPlayersCount).ToList());
        }

        private static void ShowTopPlayers(string title, string parameterName, List<Player> topPlayers)
        {
            Console.Clear();
            Console.WriteLine(title);

            foreach (Player player in topPlayers)
            {
                Console.WriteLine($"Имя: {player.Name}, {parameterName}: {player.GetParameterValue(parameterName)}");
            }

            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }

        private static List<Player> GeneratePlayers()
        {
            string[] names = { "Jinx", "Vi", "Charlie", "David", "Eve", "Silko", "Grace", "Hank", "Ivy", "Jack" };
            List<Player> players = new List<Player>();

            foreach (string name in names)
            {
                int level = UserUtils.GetRandomNumber(MinLevel, MaxLevel + 1);
                int strength = UserUtils.GetRandomNumber(MinStrength, MaxStrength + 1);
                players.Add(new Player(name, level, strength));
            }

            return players;
        }
    }

    class Player
    {
        public Player(string name, int level, int strength)
        {
            Name = name;
            Level = level;
            Strength = strength;
        }

        public string Name { get; }
        public int Level { get; }
        public int Strength { get; }

        public int GetParameterValue(string parameterName)
        {
            return parameterName == "Уровень" ? Level : Strength;
        }

        public override string ToString()
        {
            return $"Имя: {Name}, Уровень: {Level}, Сила: {Strength}";
        }
    }

    class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}
