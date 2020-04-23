namespace NsauT.Shared.Enums
{
    public enum PeriodOption
    {
        /// <summary>
        /// Common period
        /// </summary>
        None = 0,

        /// <summary>
        /// Period exists once at *date*
        /// </summary>
        Once = 1,

        /// <summary>
        /// Period exists since the *date*
        /// </summary>
        Since = 2,

        /// <summary>
        /// Period exists before the *date*
        /// </summary>
        Until = 3,

        /// <summary>
        /// Period will be in different *cabinet* once at *date*
        /// </summary>
        OnceDifferentCabinet = 4
    }
}
