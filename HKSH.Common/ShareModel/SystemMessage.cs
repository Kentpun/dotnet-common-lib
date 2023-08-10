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

        public SystemMessage(string code) : base(code, GlobalConstant.SYSTEM_MESSAGE_PREFIX)
        {
        }

        #region Common

        public static MessageModel Success => new SystemMessage("000000");

        public static MessageModel Failure => new SystemMessage("999999");

        #endregion

        #region Permission

        public static MessageModel TheSystemIsBusy => new SystemMessage("000061");

        public static MessageModel ActionsAreTooFrequent => new SystemMessage("000062");

        #endregion 

        #region Param

        public static MessageModel TheRequiredParameterIsEmpty => new SystemMessage("000040");

        public static MessageModel TheRequiredParameterIsInvalid => new SystemMessage("000041");

        public static MessageModel TheDataIsInvalid => new SystemMessage("000042");

        public static MessageModel SignatureVerificationFailed => new SystemMessage("000043");

        public static MessageModel TokenMismatch => new SystemMessage("000044");

        public static MessageModel TheTokenHasLapsed => new SystemMessage("000045");

        public static MessageModel TheRequestParameterIsIncorrect => new SystemMessage("000046");


        #endregion 

        #region System
        public static MessageModel TheUserDoesNotHavePermissionToDoThis => new SystemMessage("000090");

        public static MessageModel TheDownstreamSystemResponseTimedOut => new SystemMessage("000091");

        public static MessageModel TheDownstreamSystemCannotFindOrCannotReach => new SystemMessage("000092");

        public static MessageModel DatabaseConnectionError => new SystemMessage("000093");

        public static MessageModel DatabaseOperationException => new SystemMessage("000094");

        public static MessageModel TheCallingDownstreamSystemIsUnresponsive => new SystemMessage("000095");

        public static MessageModel UnknownException => new SystemMessage("000099");
        #endregion

    }
}
