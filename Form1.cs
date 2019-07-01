///
/// A simple study on DateTime's parsing and storing.
/// Special emphasis on using timezones and timezone conversion.
///

using System;
using System.Text;
using System.Windows.Forms;

namespace Test {
    public struct DateTimeWithZone
    {
        private readonly DateTime utcDateTime;
        private readonly TimeZoneInfo timeZone;
        
        public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone)
        {
            var dateTimeUnspec = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTimeUnspec, timeZone); 
            this.timeZone = timeZone;
        }

        public DateTime UniversalTime { get { return utcDateTime; } }

        public TimeZoneInfo TimeZone { get { return timeZone; } }

        public DateTime LocalTime
        { 
            get 
            { 
                return TimeZoneInfo.ConvertTime(utcDateTime, timeZone); 
            }
        }        
    }

    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {

            StringBuilder sb = new StringBuilder();

            DateTime dt0 = DateTime.Parse("2019-02-04T12:00:00");
            sb.Append("dt0 DateTime.Parse: " + dt0.ToString("yyyy-MM-ddTHH:mm:ssK"))
                .Append(" Kind: ").Append(dt0.Kind).Append("\r\n");
            

            DateTime dt1 = DateTime.Now;
            sb.Append("dt1 DateTime.Now: " + dt1.ToString("yyyy-MM-ddTHH:mm:ssK"))
                .Append(" Kind: ").Append(dt1.Kind).Append("\r\n");
            

            DateTime dt2 = DateTime.Parse("2019-02-04T12:00:00-05:00");
            sb.Append("dt2 DateTime.Parse: " + dt2.ToString("yyyy-MM-ddTHH:mm:ssK"))
                .Append(" Kind: ").Append(dt2.Kind).Append("\r\n");
            

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            sb.Append("tzi TimeZone: ").Append(tzi).Append("\r\n");
            DateTime dt3 = TimeZoneInfo.ConvertTime(DateTime.Parse("2019-02-04T12:00:00"), tzi);
            sb.Append("dt3 DateTime.Parse with TimeZone: " + dt3.ToString("yyyy-MM-ddTHH:mm:ssK"))
                .Append(" Kind: ").Append(dt3.Kind).Append("\r\n");


            DateTime dt4 = DateTime.SpecifyKind(DateTime.Parse("2019-02-04T12:00:00"), DateTimeKind.Local);
            sb.Append("dt4 DateTime.Parse with TimeZone: " + dt4.ToString("yyyy-MM-ddTHH:mm:ssK"))
                .Append(" Kind: ").Append(dt4.Kind).Append("\r\n");


            DateTime dt5 = DateTime.SpecifyKind(DateTime.Parse("Feb 4, 2019 1:00 PM"), DateTimeKind.Local);
            sb.Append("dt5 DateTime.Parse with TimeZone: " + dt5.ToString("yyyy-MM-ddTHH:mm:ssK"))
                .Append(" Kind: ").Append(dt5.Kind).Append("\r\n");

            
            DateTimeWithZone dtwz = new DateTimeWithZone(DateTime.Parse("2019-02-04T12:00:00"), tzi);
            sb.Append("dtwz LocalTime: ").Append(dtwz.LocalTime.ToString("yyyy-MM-ddTHH:mm:ssK")).Append("\r\n");
            sb.Append("dtwz TimeZone: ").Append(dtwz.TimeZone).Append("\r\n");
            sb.Append("dtwz UTC2: ").Append(dtwz.UniversalTime.ToString("yyyy-MM-ddTHH:mm:ssK")).Append("\r\n");

            textBox1.Text = sb.ToString();
        }
    }
}
