using System;

namespace TPICAP.TechChallenge.Infrastructure.Models
{
    public record RequestLog
    {
        public DateTimeOffset DateAndTime { get; init; }
        public string CallerDetail { get; init; }
        public string PayloadData { get; init; }
        public string RequestUrl { get; init; }
    }
}