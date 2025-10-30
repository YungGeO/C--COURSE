using System.Globalization;
int vowelCount = 0;
string takis = "takis tsan";
foreach (char c in takis)
{
    char lower = char.ToLower(c);
    if (lower == 'a' || lower == 'e' || lower == 'i' || lower == 'o' || lower == 'u')
    {
        vowelCount++;
    }
    ;

}
Console.Write(vowelCount);