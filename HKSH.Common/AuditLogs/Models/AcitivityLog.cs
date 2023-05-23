using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HKSH.Common.AuditLogs.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class AcitivityLog
    {

        /// <summary>
        /// Gets or sets the action date.
        /// </summary>
        /// <value>
        /// The action date.
        /// </value>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; } = null!;


        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>
        /// The module.
        /// </value>
        public string Module { get; set; } = null!;

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; } = null!;

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        /// <value>
        /// The function.
        /// </value>
        public string Function { get; set; } = null!;




        /// <summary>
        /// Gets or sets the visti file no.
        /// </summary>
        /// <value>
        /// The visti file no.
        /// </value>
        public string VistiFileNo { get; set; } = null!;

    }
}