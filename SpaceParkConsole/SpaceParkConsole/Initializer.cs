using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkConsole
{
    public class Initializer
    {
        public static void Run()
        {
            var logInChoice = new string[] { "Register", "Log in" };
            var parkingChoice = new string[] { "Register Parking", "Pay for parking", "exit" };
            var option = Menu.ShowMenu("Welcome", logInChoice);
        }
    }
}
