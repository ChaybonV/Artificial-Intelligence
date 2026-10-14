using System.Text.RegularExpressions;

namespace NaiveBayes
{
    public class Hamspam
    {
        public Dictionary<string, int> spam_dict { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> ham_dict { get; set; } = new Dictionary<string, int>();

        public void Train(string message, bool isSpam)
        {
            string[] words = message.Split(" ");
            foreach(string w in words)
            {
                if (isSpam)
                {
                    if (!spam_dict.ContainsKey(w))
                    {
                        spam_dict[w] = 2;
                    }
                    else
                    {
                        spam_dict[w] += 1;
                    }
                }
                else
                {
                    if (!ham_dict.ContainsKey(w))
                    {
                        ham_dict[w] = 2;
                    }
                    else
                    {
                        ham_dict[w] += 1;
                    }
                }
            }
        }

        public void Train_them(List<string> spam, List<string> ham)
        {
            Regex r = new Regex(@"\w+");
            foreach (string w in spam)
            {
                MatchCollection m = r.Matches(w);
                foreach (Match woord in m)
                {
                    Train(woord.Value.ToLower(), true);
                }
            }
            foreach (string w in ham)
            {
                MatchCollection m = r.Matches(w);
                foreach (Match woord in m)
                {
                    Train(woord.Value.ToLower(), false);
                }
            }
        }


    }
}