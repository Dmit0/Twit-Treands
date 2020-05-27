using Business_Objects;
using Data_Objects.Parsers;
using Data_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Service
    {
        readonly TweetsDbParser tweetsParser = new TweetsDbParser();
        readonly SentDbParser sentimentsParser = new SentDbParser();
        readonly StateDbParser stateParser = new StateDbParser();
        

        public List<Tweet> ParseTweets(string fileName, Dictionary<string, double> sents)
        {
            return tweetsParser.ParseTweets(fileName,sents);
        }

        public Dictionary<string, double> GetSentiments()
        {
            return sentimentsParser.sents;
        }

        public Dictionary<string, State> GetStates()
        {
            return stateParser.states;
        }

 

        public void TweetsForStates(List<Tweet> tweets)
        {
            stateParser.TweetsForStates(tweets);
        }
    }
}
