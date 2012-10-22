namespace StringCalculator.Unit.Tests
{
    public class DataBuilder
    {
        public static string GetCharDelimitedData(char delimiter, params int[] numbers)
        {
            return string.Format("//{0}\n{1}", delimiter, string.Join(delimiter.ToString(), numbers));
        }

        public static string GetStringDelimitedData(string delimiter, params int[] numbers)
        {
            return string.Format("//[{0}]\n{1}", delimiter, string.Join(delimiter, numbers));
        }
    }
}