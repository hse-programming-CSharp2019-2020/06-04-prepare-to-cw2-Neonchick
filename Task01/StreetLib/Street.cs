using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetLib
{
    /// <summary>
    /// Класс улицы.
    /// </summary>
    [Serializable]
    public class Street
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Список номеров домов.
        /// </summary>
        public int[] Houses { set; get; }

        /// <summary>
        /// Оператор, возвращающий количество домов на улице.
        /// </summary>
        /// <param name="street">Улица.</param>
        public static int operator ~(Street street) => street.Houses.Length;

        /// <summary>
        /// Оператор, возвращающий true, если на улице есть хотя бы
        /// один дом, содержащий в своем номере цифру 7, и false – в противном случае.
        /// </summary>
        /// <param name="street">Улица.</param>
        public static bool operator +(Street street) => street.Houses.Any((x) => x == 7);

        /// <summary>
        /// Метод предстовляет улицу в виде строки.
        /// </summary>
        public override string ToString()
        {
            var str = Name;
            foreach (var house in Houses)
                str += $" {house}";
            return str;
        }
    }
}
