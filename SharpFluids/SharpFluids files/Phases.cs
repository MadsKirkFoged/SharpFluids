namespace SharpFluids
{
    public enum Phases : int
    {
        /// <summary>
        /// Subcritical liquid
        /// </summary>
        Liquid = 0,

        /// <summary>
        /// Supercritical (p>pc, T>Tc)
        /// </summary>
        Supercritical,

        /// <summary>
        /// Supercritical gas (p<pc, T>Tc)
        /// </summary>
        SupercriticalGas,

        /// <summary>
        /// Supercritical liquid (p>pc, T<Tc)
        /// </summary>
        SupercriticalLiquid,

        /// <summary>
        /// At critical point
        /// </summary>
        CriticalPoint,

        /// <summary>
        /// Subcritical gas
        /// </summary>
        Gas,

        /// <summary>
        /// Twophase
        /// </summary>
        Twophase,

        /// <summary>
        /// Unknown phase
        /// </summary>
        Unknown,

        NotImposed
    }
}
