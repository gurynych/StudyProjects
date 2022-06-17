using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Practice1
{
    internal class Equation
    {
        private readonly List<double> coefficients;
        private readonly List<double> degrees;

        public Equation()
        {
            coefficients = new List<double>();
            degrees = new List<double>();
        }

        public List<Run> GetEquation()
        {
            List<Run> result = new List<Run>();
            int countEl = coefficients.Count() - 1;
            for (int i = countEl; i >= 0 ; i--)
            {
                if (coefficients[i] == 0)
                {
                    coefficients.RemoveAt(i);
                }
                else break;
            }

            countEl = coefficients.Count() - 1;

            for (int i = countEl; i >= 0; i--)
            {
                if (coefficients[i] == 0)
                {                   
                    degrees.Add(i);
                    continue;
                }
                if (i == countEl && i != 0)
                {
                    Run degree = new Run(i.ToString());
                    degree.Typography.Variants = System.Windows.FontVariants.Superscript;                    
                    result.Add(new Run(((coefficients[i] * coefficients[i] == 1) ?
                        Math.Sign(coefficients[i]).ToString().Replace("1", string.Empty) : 
                        coefficients[i].ToString()) + "x"));

                    if (countEl > 1)
                    {
                        result.Add(degree);
                    }                    
                    degrees.Add(i);
                }
                else if ((coefficients[i] == 1 || coefficients[i] == -1) && i != 0)
                {
                    Run degree = new Run(i.ToString());
                    degree.Typography.Variants = System.Windows.FontVariants.Superscript;
                    result.Add(new Run(((coefficients[i] == 1) ? " +" : " -") + "x"));
                    if (i > 1)
                    { 
                        result.Add(degree);
                    }
                    degrees.Add(i);
                }                
                else if (i > 1)
                {
                    Run degree = new Run(i.ToString());
                    degree.Typography.Variants = System.Windows.FontVariants.Superscript;
                    result.Add(new Run(((coefficients[i] > 0) ?
                        (" +" + coefficients[i].ToString()) :
                        (" " + coefficients[i].ToString())) + "x"));
                    result.Add(degree);            
                    degrees.Add(i);
                }
                else if (i == 1)
                {
                    result.Add(new Run(((coefficients[i] > 0) ?
                        (" +" + coefficients[i].ToString()) :
                        (" " + coefficients[i].ToString())) + "x"));
                    degrees.Add(i);
                }
                else if (i == 0)
                {
                    result.Add(new Run((coefficients[i] > 0) ?
                        (" +" + coefficients[i].ToString()) :
                        (" " + coefficients[i].ToString())));
                    degrees.Add(i);
                }
            }           
            degrees.Reverse();
            return result;
        }

        public bool IsCorrect(string text)
        {
            try
            {
                foreach (string num in text.Split())
                {
                    double.Parse(num.Replace(".", ","));
                }                
                return true;
            }
            catch { return false; }
        }

        public void ConvertText(string text)
        {
            coefficients.AddRange(text.Split().Select(n => double.Parse(n.Replace(".", ","))));
            coefficients.Reverse();
        }

        public List<Segment> FindSegments()
        {
            double step = 0.1;
            List<Segment> result = new List<Segment>();

            for (double i = -20; i < 19.9; i += step)
            {
                Segment segment = new Segment();
                segment.Begin = Math.Round(i, 1);
                segment.End = Math.Round(i + step, 1);
                if (Math.Sign(FindY(segment.Begin)) != Math.Sign(FindY(segment.End)))
                {
                    result.Add(segment);
                }
            }
            return result;
        }

        private double FindY(double x)
        {
            double y = 0;
            for (int i = 0; i < coefficients.Count(); i++)
            {
                y += coefficients[i] * Math.Pow(x, degrees[i]);
            }
            return y;
        }
    }    
}
