namespace Example1
{
    /// <summary>
    /// The commands on game screen
    /// </summary>
    class GameComands
    {
        public static string GameStart { get; } = "start";

        public static string GameEnd { get; } = "end__";

        public static string GamePause { get; } = "pause";

        public static string OutRange { get; } = "outrn";

        /// <summary>
        /// When the user get back from out of range
        /// </summary>
        public static string GetBack { get; } = "gtbac";

        /// <summary>
        /// Back to the login Page
        /// </summary>
        public static string Back { get; } = "gtolo";

        public static string CountUp { get; } = "conup";

        public static string CountDown { get; } = "condn";

        /// <summary>
        /// receive when the game end and the user complete the game successfully
        /// </summary>
        public static string GameSucceed { get; } = "gmsuc";

        /// <summary>
        /// receive when the game end and the user complete the game successfully
        /// </summary>
        public static string GameFailed { get; } = "gmfal";
    }
}
