using NaiveBayes;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> spam = new List<string>();
        List<string> ham = new List<string>();

        foreach (string line in File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "sample_spam_ham_subset.csv")))
        {
            if (line.StartsWith("spam")) spam.Add(line.Substring(5));
            else ham.Add(line.Substring(4));
        }

        Console.WriteLine(String.Join(" ", spam));

        Console.WriteLine(String.Join(" ", ham));
        Hamspam nb = new Hamspam();
        nb.Train_them(spam, ham);

        Console.WriteLine("\nWoorden in spam:");
        foreach (var kv in nb.spam_dict.OrderByDescending(kv => kv.Value)) Console.WriteLine($"  {kv.Key,-15} {kv.Value}");

        Console.WriteLine("\nWoorden in ham:");
        foreach (var kv in nb.ham_dict.OrderByDescending(kv => kv.Value)) Console.WriteLine($"  {kv.Key,-15} {kv.Value}");
    }
}