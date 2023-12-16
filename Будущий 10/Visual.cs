using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Будущий_10
{
    public class Visual
    {
       public static string[] MenuA =
       {
            " 0 - Админ " ,
            " 1 - Кассир ",
            " 2 - Кадровик ",          
            " 3 - Склад-менеджер " ,
            " 4 - Бухгалтер",
            "",
            " S - Cохранить изменения",
            " Escape - Вернуться в меню"
       };

        public static string[] MenuB =
        {
            "F10 - изменить пункт",
            "Delete - удалить запись",
            "Escape - вернуться в меню"
        };

        public static string[] MenuF =
        {
            "F1 - Добавить запись",
            "F2 - Найти запись",
        };
        public static string[] MenuAd =
        {
            "  ID" ,"  Логин", "  Пароль", "  Роль"
        };
        static string lineMenu = "";
        public static string getlineMenu(string[] Menu, int index)
        {

            if (index < Menu.Length)
            {
                lineMenu = Menu[index];
            }
            else if (index == Menu.Length)
            {
                lineMenu = new string('-', 27);
            }
            else
            {
                lineMenu = "";
            }
            return lineMenu;
        }
    }
}
