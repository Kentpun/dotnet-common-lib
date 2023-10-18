using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSH.Common.ShareModel.Drug
{
    /// <summary>
    /// DrugDetailRouteCategoryResponse
    /// </summary>
    public class DrugDetailRouteCategoryResponse
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
        /// Gets or sets the color of the font.
        /// </summary>
        /// <value>
        /// The color of the font.
        /// </value>
        public string FontColor { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public string BackgroundColor { get; set; } = string.Empty;
    }
}
