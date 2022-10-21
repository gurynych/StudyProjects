using System.Security.RightsManagement;
using System.Windows.Media;

namespace DB_Lab1.Model
{
    public struct Description
    {
        public static readonly Brush RedDescription = Brushes.Red;
        public static readonly Brush CoralDescription = Brushes.Coral;
        public static readonly Brush OrangeDescription = Brushes.Orange;
        public static readonly Brush YellowDescription = Brushes.Yellow;
        public static readonly Brush GreenDescription = Brushes.Green;

        public static readonly Description EmptyDescription = new Description();

        public string Text { get; set; }                

        public Brush ColorDescription { get; set; }

        public bool IsError { get; set; }

        public Description(string text = "", Brush color = null, bool isError = false)
        {
            Text = text;
            ColorDescription = color ?? Brushes.Transparent;            
            IsError = isError;
        }
    }
}
