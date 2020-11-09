using System;
using h73.GroundFault;

namespace h73.Core
{
    public class Event : AmiMeter
    {
        public Event() {}

        public string EventIdGuid { get; set; }
        public string Type { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public double? Duration => 
            End == null || Start == null ? 
                (double?) null : 
                End.Value.Subtract(Start.Value).TotalMinutes;
    }
}