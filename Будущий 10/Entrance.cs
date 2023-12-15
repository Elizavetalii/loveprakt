using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Будущий_10
{
    internal class Entrance
    {
        public string Password {  get; set; }
        public string Login { get; set; }

        public string Doljnost { get; set; }   

        public Entrance( string password, string login, string doljnost)
        {
            Password = password;
            Login = login;
            Doljnost = doljnost;
        }
    }
}
