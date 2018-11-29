namespace CounterStrikeDB.DemoClasses
{
    public class Award
    {
        public string Title { get; set; } // award title
        public string Description { get; set; } // award description

        public string Name { get; set; } // display name of player that recieved it
        public string UID { get; set; } // UID of the player who recieved it
        public double Value { get; set; } // value for the award (ex: most kills, value = kills the player got)
    }
}