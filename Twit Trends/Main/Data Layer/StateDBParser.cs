using Business_Objects;
using GMap.NET;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Objects.Parsers
{
    public class StateDbParser
    {
        public StateDbParser()
        {
            if (states==null)
            {
                Parse();
            }
        }

        public Dictionary<string, State> states;

        private void Parse()
        {
            string jsonString = new StreamReader("F:/лабы/сем 4/ООП/проект 2/TwitTrends/Data/states.json").ReadToEnd();            
            states = JsonConvert.DeserializeObject<Dictionary<string, State>>(jsonString);
        }

        public void TweetsForStates(List<Tweet> tweets)
        {
            List<string> keys = new List<string>(states.Keys);
            List<PointLatLng> Cordinates = new List<PointLatLng>();


            for (int i = 0; i < keys.Count; i++)
            {
                double avarageState = 0;
                int counterState = 0;
                double valueState = 0;
                List<Tweet> Tweets = new List<Tweet>();

                for (int j = 0; j < states[keys[i]].Polygons.Count; j++)
                {
                    foreach (List<double> item in states[keys[i]].Polygons[j])
                    {
                        Cordinates.Add(new PointLatLng(item[1], item[0]));
                    }
                    for (int k = 0; k < tweets.Count; k++)
                    {
                        GMapPolygon gMapPolygon = new GMapPolygon(Cordinates, keys[i]);
                        if (gMapPolygon.IsInside(tweets[k].Cordinates))
                        {
                            valueState += tweets[k].Weight;
                            counterState++;
                            Tweets.Add(tweets[k]);
                        }
                    }
                    Cordinates.Clear();
                }
                if (counterState != 0)
                {
                    avarageState = valueState / counterState;
                }
                states[keys[i]].Tweets = Tweets;
                states[keys[i]].Weight = avarageState;
            }
        }
    }
}
