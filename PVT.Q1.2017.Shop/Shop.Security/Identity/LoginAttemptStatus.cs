namespace PVT.Q1._2017.Shop.Security.Identity
{
    /// <summary>
    /// Login status
    /// </summary>
    public enum LoginAttemptStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        Success,

        /// <summary>
        /// LockedOut
        /// </summary>
        LockedOut,

        /// <summary>
        /// RequiresVerification
        /// </summary>
        RequiresVerification,

        /// <summary>
        /// Failure
        /// </summary>
        Failure
    }
}