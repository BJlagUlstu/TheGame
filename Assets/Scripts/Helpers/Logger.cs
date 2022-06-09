using System.IO;
using System.Globalization;
using System;

namespace Assets.Scripts.Helpers
{
    class Logger
    {
        private const string cultureName = "ru-RU";

        public void WriteLog(string message)
        {
            DateTime localDate = DateTime.Now;
            CultureInfo culture = new(cultureName);

            StreamWriter sw = new(@"log.log", append: true);
            sw.WriteLine("{0} — {1}", localDate.ToString(culture), message);
            sw.Close();
        }
    }
}