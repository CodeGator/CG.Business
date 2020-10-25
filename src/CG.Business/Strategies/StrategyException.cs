using System;
using System.Runtime.Serialization;

namespace CG.Business.Strategys
{
    /// <summary>
    /// This class represents a strategy related exception.
    /// </summary>
    [Serializable]
    public class StrategyException : CGException
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="StrategyException"/>
        /// class.
        /// </summary>
        public StrategyException()
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="StrategyException"/>
        /// class.
        /// </summary>
        /// <param name="message">The message to use for the exception.</param>
        /// <param name="innerException">An optional inner exception reference.</param>
        public StrategyException(
            string message,
            Exception innerException
            ) : base(message, innerException)
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="StrategyException"/>
        /// class.
        /// </summary>
        /// <param name="message">The message to use for the exception.</param>
        public StrategyException(
            string message
            ) : base(message)
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="StrategyException"/>
        /// class.
        /// </summary>
        /// <param name="info">The serialization info to use for the exception.</param>
        /// <param name="context">The streaming context to use for the exception.</param>
        public StrategyException(
            SerializationInfo info,
            StreamingContext context
            ) : base(info, context)
        {

        }

        #endregion
    }
}
