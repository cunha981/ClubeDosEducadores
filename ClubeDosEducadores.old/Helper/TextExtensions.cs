using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class TextExtensions
    {
        public static string ReplaceDiacritics(this string source)
        {
            if(source == null)
            {
                return source;
            }

            string sourceInFormD = source.Normalize(NormalizationForm.FormD);

            var output = new StringBuilder();
            foreach (char c in sourceInFormD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    output.Append(c);
            }

            return (output.ToString().Normalize(NormalizationForm.FormC));
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        private static readonly HashSet<char> DefaultNonWordCharacters
          = new HashSet<char> { ',', '.', ':', ';' };

        /// <summary>
        /// Returns a substring from the start of <paramref name="value"/> no 
        /// longer than <paramref name="length"/>.
        /// Returning only whole words is favored over returning a string that 
        /// is exactly <paramref name="length"/> long. 
        /// </summary>
        /// <param name="value">The original string from which the substring 
        /// will be returned.</param>
        /// <param name="length">The maximum length of the substring.</param>
        /// <param name="nonWordCharacters">Characters that, while not whitespace, 
        /// are not considered part of words and therefor can be removed from a 
        /// word in the end of the returned value. 
        /// Defaults to ",", ".", ":" and ";" if null.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="length"/> is negative
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="value"/> is null
        /// </exception>
        public static string CropWholeWords(this string value, int length, string appendText = null, HashSet<char> nonWordCharacters = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (length < 0)
            {
                throw new ArgumentException("Negative values not allowed.", "length");
            }

            if (nonWordCharacters == null)
            {
                nonWordCharacters = DefaultNonWordCharacters;
            }

            if (length >= value.Length)
            {
                return value;
            }
            int end = length;

            for (int i = end; i > 0; i--)
            {
                if (value[i].IsWhitespace())
                {
                    break;
                }

                if (nonWordCharacters.Contains(value[i])
                    && (value.Length == i + 1 || value[i + 1] == ' '))
                {
                    //Removing a character that isn't whitespace but not part 
                    //of the word either (ie ".") given that the character is 
                    //followed by whitespace or the end of the string makes it
                    //possible to include the word, so we do that.
                    break;
                }
                end--;
            }

            if (end == 0)
            {
                //If the first word is longer than the length we favor 
                //returning it as cropped over returning nothing at all.
                end = length;
            }

            var result = value.Substring(0, end);

            return appendText != null ? result + appendText : result;
        }

        private static bool IsWhitespace(this char character)
        {
            return character == ' ' || character == 'n' || character == 't';
        }
    }

    
}
