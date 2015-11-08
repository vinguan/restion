using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restion
{
    /// <summary>
    /// Represents the options of the <see cref="IRestionClient"/>
    /// </summary>
    public class RestionClientOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether its going to be allowed raw content on the response
        /// </summary>
        public bool AllowRawContent { get; set; }

        /// <summary>
        /// Gets or sets the date format to be used in serialization and deserialization
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="RestionClientOptions"/>
        /// </summary>
        public RestionClientOptions()
        {
            AllowRawContent = false;

            DateFormat = null;
        }
    }
}
