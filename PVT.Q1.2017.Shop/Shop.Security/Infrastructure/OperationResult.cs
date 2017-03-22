namespace PVT.Q1._2017.Shop.Security.Infrastructure
{
    using System.Collections.Generic;

    /// <summary>
    /// Result of operation
    /// </summary>
    public class OperationResult
    {
        private static readonly OperationResult _success = new OperationResult(true);

        /// <summary>
        /// True if the operation was successful
        /// 
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// List of errors
        /// 
        /// </summary>
        public IEnumerable<string> Errors { get; private set; }

        /// <summary>
        /// Static success result
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public static OperationResult Success
        {
            get
            {
                return _success;
            }
        }

        static OperationResult()
        {
        }

        /// <summary>
        /// Failure constructor that takes error messages
        /// 
        /// </summary>
        /// <param name="errors"/>
        public OperationResult(params string[] errors)
            : this((IEnumerable<string>)errors)
        {
        }

        /// <summary>
        /// Failure constructor that takes error messages
        /// 
        /// </summary>
        /// <param name="errors"/>
        public OperationResult(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new string[1]
                {
                    "Shit happens"
                };

            }
            
            Succeeded = false;
            Errors = errors;
        }

        protected OperationResult(bool success)
        {
            this.Succeeded = success;
            this.Errors = new string[0];
        }

        /// <summary>
        /// Failed helper method
        /// 
        /// </summary>
        /// <param name="errors"/>
        /// <returns/>
        public static OperationResult Failed(params string[] errors)
        {
            return new OperationResult(errors);
        }

    }
}