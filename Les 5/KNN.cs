namespace _05_KNN
{
    internal class KNN
    {
        public Dictionary<double[], string> data;
        double[] min;
        double[] max;

        public KNN(string file)
        {
            //19,520,actie

            data = new Dictionary<double[], string>();

            foreach (string line in File.ReadAllLines(file))
            {
                string[] parts = line.Split(',');
                double[] features = new double[parts.Length - 1];
                for (int i = 0; i < features.Length; i++)
                {
                    features[i] = double.Parse(parts[i]);
                }
                data[features] = parts.Last();
            }
        }

        // ===== BLOK 1 =====================================================

        // Euclidisch: wortel van de som van (p[i] - q[i])²
        public double Distance(double[] p, double[] q)
        {
            if (p.Length != q.Length) throw new Exception("Arrays must be of the same length.");
            double som = 0;
            for (int i = 0; i < p.Length; i++)
            {
                som += (p[i] - q[i]) * (p[i] - q[i]);
            }
            return Math.Sqrt(som);
        }

        // Manhattan: som van |p[i] - q[i]|   (tip: Math.Abs)
        public double Manhattan(double[] p, double[] q)
        {
            if (p.Length != q.Length) throw new Exception("Arrays must be of the same length.");
            double som = 0;
            for (int i = 0; i < p.Length; i++)
            {
                som += Math.Abs(p[i] - q[i]);
            }
            return som;
        }

        // ===== BLOK 2 =====================================================

        public string Classify(Dictionary<double[], string> data, double[] point, int k)
        {
            // Stap 1: twee arrays: de afstanden en de punten zelf
            double[] distances = new double[data.Count];
            double[][] points = new double[data.Count][];

            // Stap 2: vul ze met een foreach over data.Keys (hou zelf een teller i bij)
            int i = 0;
            foreach (double[] p in data.Keys)
            {
                distances[i] = Distance(point, p);
                points[i] = p;
                i++;
            }

            // Stap 3: sorteer beide arrays tegelijk op afstand (staat er al)
            Array.Sort(distances, points);

            // Stap 4: toon de k dichtste buren, bv. "47  440  -> romantiek (afstand 30.48)"
            //         het label van een punt vind je met data[points[i]]
            for (int j = 0; j < k; j++)
            {
                string tekst = "  ";
                for (int m = 0; m < points[j].Length; m++)
                {
                    tekst += points[j][m] + "  ";
                }
                tekst += "-> " + data[points[j]] + " (afstand " + distances[j] + ")";
                Console.WriteLine(tekst);
            }

            // Stap 5: tel de labels van de eerste k buren in een Dictionary<string, int>
            Dictionary<string, int> telling = new Dictionary<string, int>();
            for (int j = 0; j < k; j++)
            {
                string label = data[points[j]];
                if (telling.ContainsKey(label))
                    telling[label]++;
                else
                    telling[label] = 1;
            }

            // Stap 6: geef het label met de hoogste telling terug
            string beste = "";
            int hoogste = 0;
            foreach (string label in telling.Keys)
            {
                if (telling[label] > hoogste)
                {
                    hoogste = telling[label];
                    beste = label;
                }
            }
            return beste;
        }

        // ===== BLOK 3 =====================================================

        // Zoek per kenmerk de kleinste en grootste waarde in data
        // en schaal daarna alle data naar [0, 1]:  (waarde - min) / (max - min)
        public void Normalize()
        {
            int n = data.Keys.First().Length;   // aantal kenmerken
            min = new double[n];
            max = new double[n];

            // vul min en max
            for (int i = 0; i < n; i++)
            {
                min[i] = double.MaxValue;
                max[i] = double.MinValue;
            }
            foreach (double[] p in data.Keys)
            {
                for (int i = 0; i < n; i++)
                {
                    if (p[i] < min[i])
                    {
                        min[i] = p[i];
                    } 
                    if (p[i] > max[i])
                    {
                        max[i] = p[i];
                    } 
                }
            }

            // schaal elke waarde in data (foreach over data.Keys)
            foreach (double[] p in data.Keys)
            {
                for (int i = 0; i < n; i++)
                {
                    p[i] = (p[i] - min[i]) / (max[i] - min[i]);
                }
            }
        }

        // Schaal een nieuw punt met dezelfde min en max
        public double[] Scale(double[] point)
        {
            double[] geschaald = new double[point.Length];
            for (int i = 0; i < point.Length; i++)
            {
                geschaald[i] = (point[i] - min[i]) / (max[i] - min[i]);
            }
            return geschaald;
        }

        // ===== BLOK 4 =====================================================

        // Cosine: (som van p[i] * q[i]) / (wortel(som p[i]²) * wortel(som q[i]²))
        public double Cosine(double[] p, double[] q)
        {
            double product = 0;
            double somP = 0;
            double somQ = 0;
            for (int i = 0; i < p.Length; i++)
            {
                product += p[i] * q[i];
                somP += p[i] * p[i];
                somQ += q[i] * q[i];
            }
            return product / (Math.Sqrt(somP) * Math.Sqrt(somQ));
        }
    }
}
