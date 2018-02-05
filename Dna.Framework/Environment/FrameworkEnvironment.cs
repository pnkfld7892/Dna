namespace Dna
{
    /// <summary>
    /// Details about the current system environment
    /// </summary>
    public class FrameworkEnvironment
    {
        #region Public Properties
        /// <summary>
        /// True if in development
        /// </summary>
        public bool IsDevelopment { get; set; } = true;

        /// <summary>
        /// The configuration of the environment
        /// </summary>
        public string Configuration => IsDevelopment ? "Development" : "Production";
        #endregion

        #region Ctor
        /// <summary>
        /// Default constructor
        /// </summary>
        public FrameworkEnvironment()
        {
#if RELEASE
            IsDevelopment = false;
#endif
        }
        #endregion
    }
}
