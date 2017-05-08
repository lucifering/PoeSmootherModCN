namespace LibGGPK
{
    using System.IO;
    using System.Text.RegularExpressions;

    public  class Utils
    {
        public static int DirtySortOrder = 0;

        public static FileStream OpenFile(string path, out bool isReadOnly)
        {
            isReadOnly = true;
            try
            {
                var ret = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                isReadOnly = false;
                return ret;
            }
            catch (IOException)
            {
                // File can't be written to, since it's being used (either by the program, or by the game itself)
                return File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
        }
        public static string ConvertGggFormatedText(string input)
        {
            string pattern;
            string replacement;

            pattern = @"<i>{((.|\n)*?)}";
            replacement = "<i>$1</i>";
            input = Regex.Replace(input, pattern, replacement);

            pattern = @"<italic>{((.|\n)*?)}";
            replacement = "<i>$1</i>";
            input = Regex.Replace(input, pattern, replacement);

            pattern = @"<b>{((.|\n)*?)}";
            replacement = "<b>$1</b>";
            input = Regex.Replace(input, pattern, replacement);

            return input;
        }
    }
}