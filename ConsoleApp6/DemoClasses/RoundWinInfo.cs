namespace CounterStrikeDB.DemoClasses
{
    public enum RoundWinReason
    {
        Unknown,
        CounterTerroristsEliminated,
        TerroristsEliminated,
        BombExploded,
        BombDefused,
        TimeExpired,
        HostageRescued
    }

    public class RoundWinInfo
    {
        public string Team { get; set; } // team that won
        public RoundWinReason Reason { get; set; } // reason from enum above
        public string ReasonString { get; set; } // raw string of win reason
    }
}