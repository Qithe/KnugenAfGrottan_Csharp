using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KnugenAfGrottan
{
    public class Save
    {
        public static void SaveFile(int[] playerStats)
        {
            StreamWriter sw = new StreamWriter("savegame.txt");

            foreach (int stat in GameElements.playerStats)
            {
                sw.WriteLine(stat);
            }

            sw.Close();
        }
    }
    public class Load
    {
        public static void LoadFile(string[] args)
        {
            StreamReader sr = new StreamReader("savegame.txt");

            sr.Close();
        }
    }
}

