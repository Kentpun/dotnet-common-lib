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
    public abstract class MessageModel
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

    }
}
