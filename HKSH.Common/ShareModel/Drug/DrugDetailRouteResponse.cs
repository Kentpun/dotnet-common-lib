using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSH.Common.ShareModel.Drug
{
    /// <summary>
    /// DrugDetailRouteResponse
    /// </summary>
    public class DrugDetailRouteResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the route category.
        /// </summary>
        /// <value>
        /// The route category.
        /// </value>
        public string RouteCategory { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is parenteral.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is parenteral; otherwise, <c>false</c>.
        /// </value>
        public bool IsParenteral { get; set; }

        /// <summary>
        /// isFunctionalRoute
        /// </summary>
        public bool IsFunctionalRoute { get; set; }
    }
}
