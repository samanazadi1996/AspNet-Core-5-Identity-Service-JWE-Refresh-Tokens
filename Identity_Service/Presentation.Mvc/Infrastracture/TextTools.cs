namespace Presentation.Mvc.Infrastracture
{
    public static class TextTools
    {
        public static string GetEnglishNumber(string number)
        {
            string englishNumber = "";

            if (string.IsNullOrEmpty(number))
                return string.Empty;

            foreach (char ch in number)
            {
                englishNumber += char.GetNumericValue(ch);
            }
            return englishNumber;
        }
    }
}
