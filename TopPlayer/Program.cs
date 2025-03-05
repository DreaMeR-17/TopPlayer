using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRanking
{
    internal class Program
    {
        private const string COMMAND_SHOW_TOP_LEVEL = "1";
        private const string COMMAND_SHOW_TOP_STRENGTH = "2";
        private const string COMMAND_EXIT = "0";

        private const int TOP_PLAYERS_COUNT = 3;

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
                Console.WriteLine($"{COMMAND_SHOW_TOP_LEVEL} - Показать топ-{TOP_PLAYERS_COUNT} игроков по уровню");
                Console.WriteLine($"{COMMAND_SHOW_TOP_STRENGTH} - Показать топ-{TOP_PLAYERS_COUNT} игроков по силе");
                Console.WriteLine($"{COMMAND_EXIT} - Выйти из программы");
                Console.Write("Введите команду: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case COMMAND_SHOW_TOP_LEVEL:
                        ShowTopPlayers(players, sortByLevel: true);
                        break;
                    case COMMAND_SHOW_TOP_STRENGTH:
                        ShowTopPlayers(players, sortByLevel: false);
                        break;
                    case COMMAND_EXIT:
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

        private static void ShowTopPlayers(List<Player> players, bool sortByLevel)
        {
            Console.Clear();

            var topPlayers = sortByLevel
            ? players.OrderByDescending(player => player.Level).Take(TOP_PLAYERS_COUNT)
            : players.OrderByDescending(player => player.Strength).Take(TOP_PLAYERS_COUNT);

            Console.WriteLine(sortByLevel ? $"Топ-{TOP_PLAYERS_COUNT} игроков по уровню:" : $"Топ-{TOP_PLAYERS_COUNT} игроков по силе:");
            ShowPlayers(topPlayers);

            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
        }
    }

    class Player
    {
        private string _name;
        private int _level;
        private int _strength;

        public Player(string name)
        {
            _name = name;
            _level = UserUtils.GetRandomNumber(15, 36);
            _strength = UserUtils.GetRandomNumber(150, 251);
        }

        public string Name => _name;
        public int Level => _level;
        public int Strength => _strength;

        public override string ToString()
        {
            return $"Имя: {_name}, Уровень: {_level}, Сила: {_strength}";
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
