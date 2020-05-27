using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.ToolTips;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using Services;
using Business_Objects;

namespace Task2
{
    public partial class Form1 : Form
    {
     

        public Service service = new Service();
     

        public Form1()
        {
            InitializeComponent();            
        }       

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.CanDragMap = true;
            gMapControl1.DragButton = MouseButtons.Left; 
            gMapControl1.MaxZoom = 18;
            gMapControl1.MinZoom = 2;
            gMapControl1.Zoom = 4;
            gMapControl1.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter; 
            gMapControl1.Dock = DockStyle.None; 
            gMapControl1.MapProvider = GMapProviders.GoogleMap; 
            gMapControl1.Position = new PointLatLng(40, -100);

            if (Convert.ToString(filesComboBox.SelectedItem) != "")
            {
                ViewFunc("F:/лабы/сем 4/ООП/проект 2/TwitTrends/Data/" + filesComboBox.SelectedItem); 
            }
                     

        }       

      private void ViewFunc(string fileName)
        {
            
            
          
            Dictionary<string, double> sents = service.GetSentiments();
            List<Tweet> tweets = new List<Tweet>();
            tweets = service.ParseTweets(fileName,sents);
            Dictionary<string, State> states = service.GetStates();
            service.TweetsForStates(tweets);
            List<string> keys = new List<string>(states.Keys);
            List<PointLatLng> Cordinates = new List<PointLatLng>();


            for (int i = 0; i < keys.Count; i++)
            {

                for (int j = 0; j < states[keys[i]].Polygons.Count; j++)
                {
                    foreach (List<double> item in states[keys[i]].Polygons[j])
                    {
                        Cordinates.Add(new PointLatLng(item[1], item[0]));
                    }
                    if (states[keys[i]].Weight > 0)
                    {
                        Color color = Color.FromArgb(100, 255,140, 0);
                        DrawPolygon(Cordinates, keys[i], color);
                    }

                    if (states[keys[i]].Weight < 0)
                    {
                        Color color = Color.FromArgb(200, 0, 0,140 );
                        DrawPolygon(Cordinates, keys[i], color);
                    }

                    if (states[keys[i]].Tweets.Count == 0)
                    {
                        Color color = Color.FromArgb(200, 130, 130, 130);
                        DrawPolygon(Cordinates, keys[i], color);
                    }

                    if (states[keys[i]].Weight == 0 && states[keys[i]].Tweets.Count != 0)
                    {
                        Color color = Color.FromArgb(200,250, 250, 250);
                        DrawPolygon(Cordinates, keys[i], color);
                    }
                    
                   
                    Cordinates.Clear();
                }

            }



        }

       private void DrawPolygon(List<PointLatLng> points,string state,Color color)
        {
            GMapOverlay overlay = new GMapOverlay("polygon");           
            GMapPolygon mapPolygon = new GMapPolygon(points, state); 
            mapPolygon.Tag = state;
            mapPolygon.Fill = new SolidBrush(color);             
            mapPolygon.Stroke = new Pen(Color.Gray, 1); 
            overlay.Polygons.Add(mapPolygon);
            gMapControl1.Overlays.Add(overlay); 
        }

       

        private void filesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            gMapControl1.Overlays.Clear();
            gMapControl1.Refresh();
            ViewFunc("F:/лабы/сем 4/ООП/проект 2/TwitTrends/Data/" + filesComboBox.SelectedItem);
            gMapControl1.Zoom += 0.5;
            gMapControl1.Zoom -= 0.5;
        }

       

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
