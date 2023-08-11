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
    /// return message code to client
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// MessageModel
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="ModuleCode"></param>
        public MessageModel(string code, string ModuleCode = GlobalConstant.SYSTEM_MESSAGE_PREFIX)
        {
            Code = $"B{ModuleCode}{code}";
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        #region default message

        #region Common

        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        public static MessageModel Success => new MessageModel("000000");

        /// <summary>
        /// Gets the failure.
        /// </summary>
        /// <value>
        /// The failure.
        /// </value>
        public static MessageModel Failure => new MessageModel("999999");

        #endregion

        #region Permission

        /// <summary>
        /// Gets the system is busy.
        /// </summary>
        /// <value>
        /// The system is busy.
        /// </value>
        public static MessageModel TheSystemIsBusy => new MessageModel("000061");

        /// <summary>
        /// Gets the actions are too frequent.
        /// </summary>
        /// <value>
        /// The actions are too frequent.
        /// </value>
        public static MessageModel ActionsAreTooFrequent => new MessageModel("000062");

        #endregion 

        #region Param

        /// <summary>
        /// Gets the required parameter is empty.
        /// </summary>
        /// <value>
        /// The required parameter is empty.
        /// </value>
        public static MessageModel TheRequiredParameterIsEmpty => new MessageModel("000040");

        /// <summary>
        /// Gets the required parameter is invalid.
        /// </summary>
        /// <value>
        /// The required parameter is invalid.
        /// </value>
        public static MessageModel TheRequiredParameterIsInvalid => new MessageModel("000041");

        /// <summary>
        /// Gets the data is invalid.
        /// </summary>
        /// <value>
        /// The data is invalid.
        /// </value>
        public static MessageModel TheDataIsInvalid => new MessageModel("000042");

        /// <summary>
        /// Gets the signature verification failed.
        /// </summary>
        /// <value>
        /// The signature verification failed.
        /// </value>
        public static MessageModel SignatureVerificationFailed => new MessageModel("000043");

        /// <summary>
        /// Gets the token mismatch.
        /// </summary>
        /// <value>
        /// The token mismatch.
        /// </value>
        public static MessageModel TokenMismatch => new MessageModel("000044");

        /// <summary>
        /// Gets the token has lapsed.
        /// </summary>
        /// <value>
        /// The token has lapsed.
        /// </value>
        public static MessageModel TheTokenHasLapsed => new MessageModel("000045");

        /// <summary>
        /// Gets the request parameter is incorrect.
        /// </summary>
        /// <value>
        /// The request parameter is incorrect.
        /// </value>
        public static MessageModel TheRequestParameterIsIncorrect => new MessageModel("000046");


        #endregion 

        #region System
        /// <summary>
        /// Gets the user does not have permission to do this.
        /// </summary>
        /// <value>
        /// The user does not have permission to do this.
        /// </value>
        public static MessageModel TheUserDoesNotHavePermissionToDoThis => new MessageModel("000090");

        /// <summary>
        /// Gets the downstream system response timed out.
        /// </summary>
        /// <value>
        /// The downstream system response timed out.
        /// </value>
        public static MessageModel TheDownstreamSystemResponseTimedOut => new MessageModel("000091");

        /// <summary>
        /// Gets the downstream system cannot find or cannot reach.
        /// </summary>
        /// <value>
        /// The downstream system cannot find or cannot reach.
        /// </value>
        public static MessageModel TheDownstreamSystemCannotFindOrCannotReach => new MessageModel("000092");

        /// <summary>
        /// Gets the database connection error.
        /// </summary>
        /// <value>
        /// The database connection error.
        /// </value>
        public static MessageModel DatabaseConnectionError => new MessageModel("000093");

        /// <summary>
        /// Gets the database operation exception.
        /// </summary>
        /// <value>
        /// The database operation exception.
        /// </value>
        public static MessageModel DatabaseOperationException => new MessageModel("000094");

        /// <summary>
        /// Gets the calling downstream system is unresponsive.
        /// </summary>
        /// <value>
        /// The calling downstream system is unresponsive.
        /// </value>
        public static MessageModel TheCallingDownstreamSystemIsUnresponsive => new MessageModel("000095");

        /// <summary>
        /// Gets the unknown exception.
        /// </summary>
        /// <value>
        /// The unknown exception.
        /// </value>
        public static MessageModel UnknownException => new MessageModel("000099");
        #endregion

        #endregion
    }
}
