using Business_Objects;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data_Objects.Parsers
{
    public class TweetsDbParser
    {
        private List<Tweet> tweets;

        public List<Tweet> ParseTweets(string fileName, Dictionary<string, double> sents)
        {
            tweets = new List<Tweet>();
            string allTweets = new StreamReader(fileName).ReadToEnd();
            string[] strTweets = allTweets.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            tweets.Clear();
            foreach (var item in strTweets)
            {
                tweets.Add(new Tweet
                {
                    Cordinates = GetCoordinates(item),
                    Message = GetMessage(item),
                    Weight = CountWeight(GetMessage(item), sents)

                }); 
            }
            return tweets;
            
        }



        private PointLatLng GetCoordinates(string tweet)
        {
            var parsedData = Regex.Split(tweet, @"\]");
            var stringNumbers = Regex.Split(parsedData[0].TrimStart('['), @", ");
            PointLatLng coords = new PointLatLng(Convert.ToDouble(Regex.Replace(stringNumbers[0], @"\.", ",")), Convert.ToDouble(Regex.Replace(stringNumbers[1], @"\.", ",")));
            return coords;
        }



        private string GetMessage(string tweet)
        {
            var parsedData = Regex.Split(tweet, @"\t_\t");
            var draftWords = Regex.Split(parsedData[1], "\\t");
            return draftWords[1];
        }


        private double CountWeight(string message, Dictionary<string, double> sents)
        {
            string[] words = message.Split(new char[] { ',', '!', '?', ' ', '.', '\'', '\"', '#', '*', ':', ';', '(', ')', '&' }, StringSplitOptions.RemoveEmptyEntries);
            double sentsValue = 0;
            int counter = 0;
            double avgSentsValue = 0;

            foreach (var word in words)
            {
                if (sents.ContainsKey(word.ToLower()))
                {
                    sentsValue += sents[word.ToLower()];
                    counter++;
                }
            }

            if (counter != 0)
            {
                avgSentsValue = sentsValue / counter;
            }

            return avgSentsValue;
        }

       

    }
}
