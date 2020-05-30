/*
    Дисциплина:     "Программирование"
    Группа:         196ПИ/1 (11 подгруппа)
    Студент:        Кузнецов Михаил Федорович
    Задача:         2
    Дата:           30.05.2020 8:52:16
*/

using StreetLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                // Загружаем улицы.
                var streets = LoadOrchestra();
                // Находим волшебные.
                var magicStreets = from str in streets
                                   where ~str % 2 == 1
                                   where +str
                                   select str;
                // Выводим их.
                Console.WriteLine("Список волшебных улиц:");
                foreach (var str in magicStreets)
                    Console.WriteLine(str);
                // Цикл повтора решения.
                Console.WriteLine("Для завершение программы нажмите клавишу 'Esc', или любую другую клавишу для продолжение.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Метод для десереализациии улицы.
        /// </summary>
        private static Street[] LoadOrchestra()
        {
            var streets = new Street[0];
            try
            {
                using (var fs = new FileStream("../../../Task01/bin/Debug/out.ser", FileMode.Open))
                {
                    var xmlf = new XmlSerializer(typeof(Street[]));
                    streets = (Street[])xmlf.Deserialize(fs);
                }
                Console.WriteLine("Улицы были десериализованы.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка открытия файла: {ex.Message}");
            }
            catch (SecurityException ex)
            {
                Console.WriteLine($"Ошибка безопасности: {ex.Message}");
            }
            return streets;
        }
    }
}
