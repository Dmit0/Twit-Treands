using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Objects.Parsers
{
    public class SentDbParser
    {
        public SentDbParser()
        {
            if (sents == null)
            {
                ParseSentiments();
            }
        }

        public Dictionary<string, double> sents;

        public void ParseSentiments()
        {
            sents = new Dictionary<string, double>();
            string sentiments = new StreamReader("F:/лабы/сем 4/ООП/проект 2/TwitTrends/Data/sentiments.csv").ReadToEnd();
            string[] dictionary = sentiments.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);            
            foreach (var str in dictionary)
            {
                string[] temp = str.Split(',');
                sents.Add(temp[0], double.Parse(temp[1].Replace('.', ',')));
            }            
        }
    }
}
