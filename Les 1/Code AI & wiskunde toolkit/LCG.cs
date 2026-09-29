class LCG{
    long a = 214013;
    long m = (long)Math.Pow(2,31);
    long c = 2531011;
    long start;

    public LCG() // constructor
    {
        start = System.DateTime.Now.Ticks;
        Console.WriteLine(start);
    }

    public long Next()
    {
        long next = (a * start + c) % m;
        start = next;
        return start;
    }
}

