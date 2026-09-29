//namespace Les1
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello, World!");
//        }
//    }
//}

Console.WriteLine("Hello, World!");



// Formule pseudo: r_n+1 = (a*r_n+c)%m (--> m is priemgetal)

// m = 2^31
// a = 214013
// c = 2531011

LCG LCG  = new LCG();
Console.WriteLine(LCG.Next());
Console.WriteLine(LCG.Next());
Console.WriteLine(LCG.Next());