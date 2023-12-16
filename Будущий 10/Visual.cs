namespace Будущий_10
{
    public class Visual
    {
        public static string[] MenuA =
        {
            " 0 - Админ " ,
            " 1 - Кадровик",
            " 2 - Бухгалтер",          
            " 3 - Склад-менеджер ",
            " 4 - Кассир",
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

        public static string[] MenuC =
        {
            "S - Завершить покупку",
            "Escape - Вернуться в меню",
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
