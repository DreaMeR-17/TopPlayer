using System;
using System.Collections.Generic;
using System.Linq;

namespace TopPlayer
{
    internal class Program
    {
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
                Console.WriteLine("1 - Показать топ-3 игроков по уровню");
                Console.WriteLine("2 - Показать топ-3 игроков по силе");
                Console.WriteLine("0 - Выйти из программы");
                Console.Write("Введите команду: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowTopPlayers(players, sortByLevel: true);
                        break;
                    case "2":
                        ShowTopPlayers(players, sortByLevel: false);
                        break;
                    case "0":
                        Console.WriteLine("Выход из программы...");
                        isWork = false;
                        return;
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
                ? players.OrderByDescending(player => player.Level).Take(3)
                : players.OrderByDescending(player => player.Strength).Take(3);

            Console.WriteLine(sortByLevel ? "Топ-3 игроков по уровню:" : "Топ-3 игроков по силе:");
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
            _level = UserUtils.GetRandomNumber(15, 36); // Уровень от 15 до 35
            _strength = UserUtils.GetRandomNumber(150, 251); // Сила от 150 до 250
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
