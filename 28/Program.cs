using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введiть кiлькiсть потокiв:");
        string userInput = Console.ReadLine();
        int Parallel;
        if (!int.TryParse(userInput, out Parallel))
        {
            Console.WriteLine("Невiрний ввод. Використовується стандартна кiлькiсть потокiв.");
            Parallel = Environment.ProcessorCount;
        }

        int[] numbers = Enumerable.Range(1, 100000000).ToArray();

        Stopwatch stopwatch = Stopwatch.StartNew();
        var parallelResult = numbers.AsParallel()
                                    .WithDegreeOfParallelism(Parallel)
                                    .Where(x => x % 2 == 0)
                                    .Select(x => x * x)
                                    .ToList();
        stopwatch.Stop();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Паралельний результат: " + parallelResult.Count);
        Console.WriteLine("Час виконання з PLINQ: " + stopwatch.ElapsedMilliseconds + " мс");

        stopwatch.Restart();
        var Result = numbers.Where(x => x % 2 == 0)
                                      .Select(x => x * x)
                                      .ToList();
        stopwatch.Stop();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Послiдовний результат: " + Result.Count);
        Console.WriteLine("Час виконання без PLINQ: " + stopwatch.ElapsedMilliseconds + " мс");

        Console.ResetColor();
    }
}
