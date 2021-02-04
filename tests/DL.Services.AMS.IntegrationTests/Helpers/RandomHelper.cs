using System;

namespace DL.Services.AMS.IntegrationTests.Helpers
{
    public static class RandomHelper
    {
        private static Random _rand = new Random();
        private const string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string GenerateString(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = _alphabet[_rand.Next(_alphabet.Length)];
            }
            return new string(chars);
        }

        public static DateTime GenerateDate(int startYear, 
            int minAge)
        {
            return new DateTime(_rand.Next(startYear, DateTime.Now.Year - minAge),
                _rand.Next(1, 12), _rand.Next(1, 28));
        }
    }
}
