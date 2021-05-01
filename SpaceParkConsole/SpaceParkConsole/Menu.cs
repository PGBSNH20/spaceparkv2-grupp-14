using System;

namespace SpaceParkConsole
{
    public class Menu
    {
        public static object ShowMenu(string info, object[] options)
        {
            Console.CursorVisible = false;
            Console.WriteLine(info);

            //this is the item in the options that will be highlighted for selection when enter is pressed (starting on 0, top item)
            int indexHighlight = 0;

            //this will go to the HighlightPageOption method which prints all the available options and hightlights currently selected
            HighlightPageOption(info, options, indexHighlight);

            /*get what key the user is pressing 
             * (should only be pagedown, pageup or enter, 
             * however if none of them are pressed the program will do nothing through the while loop) */
            ConsoleKey key = Console.ReadKey().Key;

            while (key != ConsoleKey.Enter)
            {
                //if key is page up we want to go upward on the options, meaning we need to remove 1 from indexhighlight (since 0 is top most value)
                //also if indexhightlight is more than 0 since we can't select an option beyond 0 (ie -1)
                if (key == ConsoleKey.UpArrow && indexHighlight > 0)
                {
                    indexHighlight--;
                    HighlightPageOption(info, options, indexHighlight);
                }
                //if key is page down we want to go downward on the option, so we add 1 to indexhighlight (since highest value is down most value)
                //also check if indexhightlight is less than options.length - 1 since we cant go beyond the highest index of options
                //that would just cause an index out of range exception and crash the program
                else if (key == ConsoleKey.DownArrow && indexHighlight < options.Length - 1)
                {
                    indexHighlight++;
                    HighlightPageOption(info, options, indexHighlight);
                }

                //update key input of the user to determine if we should continue going through the options
                //or check if we are done through Key == enter break the loop
                key = Console.ReadKey().Key;
            }

            //return the highlighted index option
            return options[indexHighlight];
        }

        public static void HighlightPageOption(string info, object[] options, int index)
        {
            //Clear the console so it doesn't print the new values on new lines, but instead replaces current values with new values on respective line
            Console.Clear();

            //print info once again
            Console.WriteLine(info);

            for (int i = 0; i < options.Length; i++)
            {
                //if i equals the index value we are highlighting, print it in green color with an arrow to show that THIS is the value we are on
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("> " + options[i]);
                    //reset text color back to gray
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                //else simply print the value
                else
                {
                    Console.WriteLine(options[i]);
                }
            }
        }

        public static string RequestUserInput(string prompt)
        {
            Console.Clear();
            Console.WriteLine(prompt);

            return Console.ReadLine();
        }
    }
}