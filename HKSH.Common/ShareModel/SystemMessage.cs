using Amazon.Runtime;
using HKSH.Common.Constants;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// SystemMessage
    /// </summary>
    /// <seealso cref="HKSH.Common.ShareModel.MessageModel" />
    public class SystemMessage : MessageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMessage" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public SystemMessage(string code) : base(code, GlobalConstant.SYSTEM_MESSAGE_PREFIX)
        {
        }

        #region Common

        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        public static MessageModel Success => new SystemMessage("000000");

        /// <summary>
        /// Gets the failure.
        /// </summary>
        /// <value>
        /// The failure.
        /// </value>
        public static MessageModel Failure => new SystemMessage("999999");

        #endregion

        #region Permission

        /// <summary>
        /// Gets the user does not have permission to do this.
        /// </summary>
        /// <value>
        /// The user does not have permission to do this.
        /// </value>
        public static MessageModel TheUserDoesNotHavePermissionToDoThis => new SystemMessage("000061");

        /// <summary>
        /// Gets the actions are too frequent.
        /// </summary>
        /// <value>
        /// The actions are too frequent.
        /// </value>
        public static MessageModel ActionsAreTooFrequent => new SystemMessage("000062");

        #endregion 

        #region Param

        /// <summary>
        /// Gets the required parameter is empty.
        /// </summary>
        /// <value>
        /// The required parameter is empty.
        /// </value>
        public static MessageModel TheRequiredParameterIsEmpty => new SystemMessage("000040");

        /// <summary>
        /// Gets the required parameter is invalid.
        /// </summary>
        /// <value>
        /// The required parameter is invalid.
        /// </value>
        public static MessageModel TheRequiredParameterIsInvalid => new SystemMessage("000041");

        /// <summary>
        /// Gets the data is invalid.
        /// </summary>
        /// <value>
        /// The data is invalid.
        /// </value>
        public static MessageModel TheDataIsInvalid => new SystemMessage("000042");

        /// <summary>
        /// Gets the signature verification failed.
        /// </summary>
        /// <value>
        /// The signature verification failed.
        /// </value>
        public static MessageModel SignatureVerificationFailed => new SystemMessage("000043");

        /// <summary>
        /// Gets the token mismatch.
        /// </summary>
        /// <value>
        /// The token mismatch.
        /// </value>
        public static MessageModel TokenMismatch => new SystemMessage("000044");

        /// <summary>
        /// Gets the token has lapsed.
        /// </summary>
        /// <value>
        /// The token has lapsed.
        /// </value>
        public static MessageModel TheTokenHasLapsed => new SystemMessage("000045");

        /// <summary>
        /// Gets the request parameter is incorrect.
        /// </summary>
        /// <value>
        /// The request parameter is incorrect.
        /// </value>
        public static MessageModel TheRequestParameterIsIncorrect => new SystemMessage("000046");


        #endregion 

        #region System
        /// <summary>
        /// Gets the system is busy.
        /// </summary>
        /// <value>
        /// The system is busy.
        /// </value>
        public static MessageModel TheSystemIsBusy => new SystemMessage("000090");

        /// <summary>
        /// Gets the downstream system response timed out.
        /// </summary>
        /// <value>
        /// The downstream system response timed out.
        /// </value>
        public static MessageModel TheDownstreamSystemResponseTimedOut => new SystemMessage("000091");

        /// <summary>
        /// Gets the downstream system cannot find or cannot reach.
        /// </summary>
        /// <value>
        /// The downstream system cannot find or cannot reach.
        /// </value>
        public static MessageModel TheDownstreamSystemCannotFindOrCannotReach => new SystemMessage("000092");

        /// <summary>
        /// Gets the database connection error.
        /// </summary>
        /// <value>
        /// The database connection error.
        /// </value>
        public static MessageModel DatabaseConnectionError => new SystemMessage("000093");

        /// <summary>
        /// Gets the database operation exception.
        /// </summary>
        /// <value>
        /// The database operation exception.
        /// </value>
        public static MessageModel DatabaseOperationException => new SystemMessage("000094");

        /// <summary>
        /// Gets the calling downstream system is unresponsive.
        /// </summary>
        /// <value>
        /// The calling downstream system is unresponsive.
        /// </value>
        public static MessageModel TheCallingDownstreamSystemIsUnresponsive => new SystemMessage("000095");

        /// <summary>
        /// Gets the unknown exception.
        /// </summary>
        /// <value>
        /// The unknown exception.
        /// </value>
        public static MessageModel UnknownException => new SystemMessage("000099");
        #endregion
    }
}
