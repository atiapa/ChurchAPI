using ChurchApi.Models;
using System.Collections.Generic;

namespace ChurchApi.DTOs
{
    public class StatDto : Stat
    {
    }

    public class StatCardDto
    {
        public int TotalDownloads { get; set; }
        public int TotalUploads { get; set; }
        public int TotalPortalUsers { get; set; }
        public int TotalProcessed { get; set; }
        public int TotalPending { get; set; }
        public List<TrendDto> Trends { get; set; }
        public List<TrendSummaryDto> Summary { get; set; }
    }

    public class TrendDto
    {
        public string Month { get; set; }
        public int TotalProcessed { get; set; }
        public int TotalPending { get; set; }
    }

    public class TrendSummaryDto
    {
        public int Year { get; set; }
        public int TotalDownloads { get; set; }
        public int TotalUploads { get; set; }

    }
}

