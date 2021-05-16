using System;
using System.Collections.Generic;

namespace Testovoe
{
    public class RollingRetentionRequestDTO
    {
        public int Days { get; set; }
    }

    public class RollingRetentionResponseDTO
    {
        public double RollingRetention { get; set; }
        public IList<UserLife> HistogramData { get; set; }
    }
    public class UserLife
    {
        public int UserId { get; set; } 
        public int LifeDays { get; set; }
    }
}
