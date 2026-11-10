// Les 7 – Logistic Regression (cursus H7, p. 54-58)
// STARTCODE – vul blok 1, 2 en 3 aan. Test telkens met Program.cs.

public class LogisticRegression
{
    public double[] Weights { get; }   // één gewicht per kenmerk (w1, w2, ...)
    public double Bias { get; set; }   // b

    // Een model met gekozen gewichten en bias (zoals de spamfilter uit de cursus).
    public LogisticRegression(double[] weights, double bias)
    {
        Weights = weights;
        Bias = bias;
    }

    // Een leeg model: alle gewichten 0 en bias 0 (startpunt om te trainen).
    public LogisticRegression(int aantalKenmerken) : this(new double[aantalKenmerken], 0) { }

    // =====================================================================
    // BLOK 1 – De gewogen som
    // =====================================================================

    // z = w1·x1 + w2·x2 + ... + b
    public double GewogenSom(double[] x)
    {
        // Stappen:
        // 1  start met z = Bias
        // 2  voor elk kenmerk i: tel Weights[i] · x[i] op bij z
        // 3  geef z terug
        double z = Bias;
        for (int i = 0; i < Weights.Length; i++)
            z += Weights[i] * x[i];
        return z;
    }

    // =====================================================================
    // BLOK 2 – Van som naar kans naar beslissing
    // =====================================================================

    // S(z) = 1 / (1 + e^(−z))
    public static double Sigmoid(double z)
    {
        // Stappen:
        // 1  e^(−z) schrijf je in C# als Math.Exp(-z)
        // 2  geef 1 / (1 + e^(−z)) terug
        return 1 / (1 + Math.Exp(-z));
    }

    public double VoorspelKans(double[] x)
    {
        // Stappen:
        // 1  bereken z met GewogenSom(x)
        // 2  geef Sigmoid(z) terug
        double z = GewogenSom(x);
        return Sigmoid(z);
    }

    public int Voorspel(double[] x)
    {
        // Stappen:
        // 1  bereken de kans met VoorspelKans(x)
        // 2  kans >= 0.5?  ->  1, anders 0
        double kans = VoorspelKans(x);
        return kans >= 0.5 ? 1 : 0;
    }

    // =====================================================================
    // BLOK 3 – Leren: één stapje bergaf
    // =====================================================================

    // Pseudocode: cursus p. 57
    public void TrainStap(double[] x, int label, double leerSnelheid)
    {
        // Stappen:
        // 1  fout = VoorspelKans(x) − label
        // 2  voor elk kenmerk i: Weights[i] = Weights[i] − leerSnelheid · fout · x[i]
        // 3  Bias = Bias − leerSnelheid · fout
        double fout = VoorspelKans(x) - label;
        for (int i = 0; i < Weights.Length; i++)
            Weights[i] = Weights[i] - leerSnelheid * fout * x[i];
        Bias = Bias - leerSnelheid * fout;
    }

    public void Train(List<Sample> data, double leerSnelheid, int aantalStappen)
    {
        // Stappen:
        // 1  herhaal aantalStappen keer:
        // 2      voor elk voorbeeld s in data: TrainStap(s.Features, s.Label, leerSnelheid)
        for (int stap = 0; stap < aantalStappen; stap++)
            foreach (Sample s in data)
                TrainStap(s.Features, s.Label, leerSnelheid);
    }

    // =====================================================================
    // GEGEVEN – niet aanpassen
    // =====================================================================

    // Toont gewichten en bias, bv. "w = (0.15, 0.1)  b = 0.05"
    public override string ToString() =>
        $"w = ({string.Join(", ", Weights.Select(w => w.ToString("0.###")))})  b = {Bias:0.###}";
}