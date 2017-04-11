using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("New","Text")]
    public class NewText : Cmdlet
    {
        /// <summary>
        /// Gets or sets the object. The Object will be converted to string when it's set, and this property always returns a string.
        /// </summary>
        /// <value>A string</value>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
        public object Object { get; set; }

        /// <summary>
        /// Gets or Sets the background color for the block
        /// </summary>
        [Parameter]
        [Alias("Bg")]
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or Sets the foreground color for the block
        /// </summary>
        [Parameter]
        [Alias("Fg")]
        public Color ForegroundColor { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var result = new Text()
            {
                BackgroundColor = BackgroundColor,
                ForegroundColor = ForegroundColor,
                Object = Object
            };
            WriteObject(result);
        }
    }
}
