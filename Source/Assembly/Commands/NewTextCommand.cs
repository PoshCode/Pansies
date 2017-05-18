using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("New","Text")][Alias("Text")]
    public class NewTextCommand : Cmdlet
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
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("Bg")]
        public RgbColor BackgroundColor { get; set; }


        [Parameter()]
        public object Separator { get; set; } = " ";

        /// <summary>
        /// Gets or Sets the foreground color for the block
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("Fg")]
        public RgbColor ForegroundColor { get; set; }

        [Parameter]
        public SwitchParameter LeaveColor { get; set; }

        [Parameter]
        public SwitchParameter IgnoreEntities { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var result = new Text()
            {
                BackgroundColor = BackgroundColor,
                ForegroundColor = ForegroundColor,
                Separator = Separator,
                Object = Object,
                Clear = !LeaveColor,
                Entities = !IgnoreEntities,
            };
            WriteObject(result);
        }
    }
}
