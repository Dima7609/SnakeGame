using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public class MaxScore
    {
        public static MaxScore GetMaxScore()
        {
            MaxScore maxscore = null;
            string filename = "settings.xml";

            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlSerializer xser = new XmlSerializer(typeof(MaxScore));
                    maxscore = (MaxScore)xser.Deserialize(fs);
                    fs.Close();
                }
            }
            else maxscore = new MaxScore();
            return maxscore;
        }

        public void Save()
        {
            string filename = "settings.xml";
            FileStream fs = new FileStream(filename, FileMode.Create);
            XmlSerializer xser = new XmlSerializer(typeof(MaxScore));
            xser.Serialize(fs, this);
            fs.Close();
        }

        public int MyScore { get; set; }

        public void TestMaxScore()
        {
            Program prog = new Program();
            int score = prog.ReturnScore();

            MaxScore _maxscore = null;
            _maxscore = MaxScore.GetMaxScore();

            if (score > _maxscore.MyScore)
            {
                _maxscore.MyScore = score;
                _maxscore.Save();
            }
        }
    }
}