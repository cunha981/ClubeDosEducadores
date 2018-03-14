using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helper.RegexHelper
{
    public static class RegexExtensions
    {
        public static bool StartsWithNumber(this string value, int? numberOfNumbers = null)
        {
            if(numberOfNumbers.HasValue)
            {
                return Regex.IsMatch(value, @"^[0-9]{5}.*$");
            }

            return Regex.IsMatch(value, @"^[0-9].*$");
        }

        public static bool IsValidEmail(this string email)
        {
            return email != null && Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsValidCep(this string cep)
        {
            return cep != null && Regex.IsMatch(cep, @"\d{5}[-]\d{3}");
        }

        /// <summary>
        /// Check if string is a valid telephone or celphone number
        /// </summary>
        /// <param name="telephone">(99) 9999-9999</param>
        /// <returns>true if valid</returns>
        public static bool IsValidTelephone(this string telephone)
        {
            return telephone != null && Regex.IsMatch(telephone, @"^(\([0-9]{2}\))\s([9]{1})?([0-9]{4})-([0-9]{4})$");
        }

        public static string Extract(this string value, string pattern)
        {
            var regex = new Regex(pattern);
            var match = regex.Match(value);

            if(match.Success)
            {
                return match.Groups[0].Value;
            }

            throw new ArgumentOutOfRangeException("Regex not match any value in value");
        }
    }
}
