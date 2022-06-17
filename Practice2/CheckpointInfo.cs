using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2
{
    public class CheckpointInfo
    {
        public string Name { get; set; }

        public bool Drinks { get; set; }

        public bool EnergyBars { get; set; }

        public bool Toilets { get; set; }

        public bool Information { get; set; }

        public bool Medical { get; set; }

        public string DrinksText { get { return Drinks ? "Yes" : "No"; } }

        public string EnergyBarsText { get { return EnergyBars ? "Yes" : "No"; } }

        public string ToiletsText { get { return Toilets ? "Yes" : "No"; } }

        public string InformationText { get { return Information ? "Yes" : "No"; } }
        public string MedicalText { get { return Medical ? "Yes" : "No"; } }


        public CheckpointInfo()
        {        
        }

        public CheckpointInfo(string line)
        {
            Parse(line);
        }

        public bool Parse(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return false;
            }

            List<string> words = line.Split().Where(w => w != string.Empty).ToList();            

            if (words.Count < 6)
            {
                return false;
            }            
            Name = words[0];
            words = words.Select(n => n.ToLower()).ToList();
            Drinks = words[1] == "yes";
            EnergyBars = words[2] == "yes";
            Toilets = words[3] == "yes";
            Information = words[4] == "yes";
            Medical = words[5] == "yes";
            return true;
        }
    }
}
