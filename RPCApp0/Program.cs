// вариант 10: поиск расстояния Левенштейна

using System;
using System.Diagnostics;

public class Program
{
    public static int LevenshteinDistance(string s1, string s2)
    {
        if (s1.Length == 0) return s2.Length;
        if (s2.Length == 0) return s1.Length;

        int cost = s1[0] == s2[0] ? 0 : 1;

        return Math.Min(
            Math.Min(
                LevenshteinDistance(s1.Substring(1), s2) + 1,
                LevenshteinDistance(s1, s2.Substring(1)) + 1),
            LevenshteinDistance(s1.Substring(1), s2.Substring(1)) + cost);
    }
	public static int LevenshteinDistanceModified(string s1, string s2)
    {
        //добавил проверку на null
        if (s1 == null) throw new ArgumentNullException(nameof(s1));
        if (s2 == null) throw new ArgumentNullException(nameof(s2));

        if (s1.Length == 0) return s2.Length;
        if (s2.Length == 0) return s1.Length;

        // добавил проверку на длину строк
        if (s1.Length < s2.Length)
        {
            (s1, s2) = (s2, s1);
        }

        int m = s1.Length; 
        int n = s2.Length; 

        // два динамических массива для хранения предыдущей и текущей строки, сложность O(n) 
        int[] previousRow = new int[n + 1];
        int[] currentRow = new int[n + 1];

        for (int j = 0; j <= n; j++)
        {
            previousRow[j] = j;
        }

        for (int i = 1; i <= m; i++)
        {
            currentRow[0] = i;
            char charFromS1 = s1[i - 1];

            for (int j = 1; j <= n; j++)
            {
                char charFromS2 = s2[j - 1];
                int substitutionCost = (charFromS1 == charFromS2) ? 0 : 1;

                int insertion = currentRow[j - 1] + 1;     
                int deletion = previousRow[j] + 1;         
                int substitution = previousRow[j - 1] + substitutionCost;

                int best = insertion < deletion ? insertion : deletion;
                best = best < substitution ? best : substitution;

                currentRow[j] = best;
            }

            // меняем местами предыдущую и текущую строку
            (previousRow, currentRow) = (currentRow, previousRow);
        }

        return previousRow[n];
    }

    static void Main()
    {
        string s1 = "kittensafsa";
        string s2 = "sittingasfas";
        Stopwatch sw = Stopwatch.StartNew();
        int distance = LevenshteinDistanceModified(s1, s2);
        sw.Stop();
        Console.WriteLine($"Расстояние Левенштейна (модифицированный алгоритм): {distance}");
        Console.WriteLine("Время: " + sw.Elapsed + " мс");
        sw.Reset();
        sw.Start();
        distance = LevenshteinDistance(s1, s2);
        sw.Stop();
        Console.WriteLine($"Расстояние Левенштейна (рекурсивный алгоритм): {distance}");
        Console.WriteLine("Время: " + sw.Elapsed + " мс");
    }
}