/*
    Дисциплина:     "Программирование"
    Группа:         196ПИ/1 (11 подгруппа)
    Студент:        Кузнецов Михаил Федорович
    Задача:         1
    Дата:           30.05.2020 4:59:49
*/

using StreetLib;
using System;
using System.IO;
using System.Security;
using System.Text;
using System.Xml.Serialization;

namespace Task01
{
    class Program
    {
        /// <summary>
        /// Считывает переменную типа int.
        /// </summary>
        /// <param name="x">Переменная, в которую необходимо считать значение.</param>
        /// <param name="pr">Условие, накладываемые на переменную.</param>
        /// <param name="outStr">Сообщение пользователю.</param>
        static void ReadInt(out int x, Predicate<int> pr, string outStr)
        {
            if (outStr != "")
                Console.WriteLine(outStr);
            while (!(int.TryParse(Console.ReadLine(), out x) && pr(x)))
            {
                Console.WriteLine("Ошибка ввода");
                if (outStr != "")
                    Console.WriteLine(outStr);
            }
        }

        static void Main(string[] args)
        {
            do
            {
                // Узнаем колличество улиц.
                ReadInt(out int N, (x) => x >= 0, "Введете число улиц");
                // Получим массив улиц.
                var streetsArray = GetStreets(N);
                // Выводим улицы.
                foreach (var street in streetsArray)
                    Console.WriteLine(street);
                // Сохраняем улицы.
                SaveStreets(streetsArray);
                // Цикл повтора решения.
                Console.WriteLine("Для завершение программы нажмите клавишу 'Esc', или любую другую клавишу для продолжение.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Метод сериализации улиц.
        /// </summary>
        /// <param name="streetsArray">Массив улиц.</param>
        private static void SaveStreets(Street[] streetsArray)
        {
            try
            {
                using (var fs = new FileStream("out.ser", FileMode.Create))
                {
                    var xmlf = new XmlSerializer(typeof(Street[]));
                    xmlf.Serialize(fs, streetsArray);
                }
                Console.WriteLine("streetsArray был сериализован.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            }
            catch (SecurityException ex)
            {
                Console.WriteLine($"Ошибка безопасности: {ex.Message}");
            }
        }

        /// <summary>
        /// Метод получения массива улиц.
        /// </summary>
        /// <param name="n">Колличество улиц.</param>
        private static Street[] GetStreets(int n)
        {
            // Проверяем улицы из файла.
            bool fCorect = LoadStreets(n, out Street[] streets);
            // Если файл некоректный сгенирируем улицы.
            if (!fCorect)
            {
                Console.WriteLine("Файл data.txt некоректный");
                streets = new Street[n];
                for (int i = 0; i < n; i++)
                {
                    streets[i] = new Street();
                    streets[i].Name = GenName();
                    var houses = new int[rnd.Next(2, 10)];
                    for (int j = 0; j < houses.Length; j++)
                        houses[j] = rnd.Next(1, 101);
                    streets[i].Houses = houses;
                }
            }
            else Console.WriteLine("Файл data.txt коректный");
            return streets;
        }

        /// <summary>
        /// Генератор случайных чисел.
        /// </summary>
        static Random rnd = new Random(DateTime.Now.Second);

        /// <summary>
        /// Метод герации имени.
        /// </summary>
        private static string GenName()
        {
            int count = rnd.Next(2, 11);
            string str = "";
            for (int i = 0; i < count; i++)
                str += (char)('a' + rnd.Next(26));
            return str;
        }

        /// <summary>
        /// Загрузка улиц из файла.
        /// </summary>
        /// <param name="n">Колличество улиц.</param>
        /// <param name="streets">Массив улиц.</param>
        /// <returns>true - если файл коректный, false - в ином случае</returns>
        private static bool LoadStreets(int n, out Street[] streets)
        {
            bool fCorect = false;
            streets = new Street[n];
            try
            {
                var strArray = File.ReadAllLines("data.txt", Encoding.GetEncoding(1251));
                // Проверяем длину массива улиц.
                if (strArray.Length < n)
                    return false;
                for (int j=0;j<strArray.Length; j++)
                {
                    var strStreet = strArray[j].Split();
                    // Проверка строки на пустоту.
                    if (strStreet.Length == 0)
                        return false;
                    // Проверка имени.
                    if (!CheckName(strStreet[0]))
                        return false;
                    // Проверка домов.
                    var houses = new int[strStreet.Length - 1];
                    for (int i = 1; i < strStreet.Length; i++)
                    {
                        houses[i - 1] = int.Parse(strStreet[i]);
                        if (houses[i - 1] < 1 || houses[i - 1] > 100)
                            return false;
                    }
                    if (j<n)
                    {
                        streets[j] = new Street();
                        streets[j].Name = strStreet[0];
                        streets[j].Houses = houses;
                    }
                }
                fCorect = true;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            }
            catch (SecurityException ex)
            {
                Console.WriteLine($"Ошибка безопасности: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка формата: {ex.Message}");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Ошибка переполнения: {ex.Message}");
            }
            return fCorect;
        }

        /// <summary>
        /// Метод проверки имени.
        /// </summary>
        /// <param name="v">Имя.</param>
        /// <returns>true - если имя коректно, false - обратном случе.</returns>
        private static bool CheckName(string v)
        {
            bool fl = true;
            foreach (var ch in v)
                if ((ch < 'а' || ch > 'я') && (ch < 'А' || ch > 'Я') && (ch < 'a' || ch > 'z') && (ch < 'A' || ch > 'Z'))
                    fl = false;
            return fl;
        }
    }
}
