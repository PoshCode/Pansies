using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("New", "Hyperlink")]
    [Alias("Url")]
    public class NewHyperlinkCommand : Cmdlet
    {

        /// <summary>
        /// Gets or sets the Uri for the hyperlink.
        /// </summary>
        /// <value>A string</value>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the object to be linked. The Object will be converted to string, and wrapped in a link. Defaults to the Uri
        /// </summary>
        /// <value>A string</value>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, Position = 1)]
        public object Object { get; set; }

        /// <summary>
        /// Gets or Sets the separator text to use if the Object is an array
        /// </summary>
        [Parameter()]
        public object Separator { get; set; } = " ";

        /// <summary>
        /// Gets or Sets the background color for the block
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("Bg")]
        public RgbColor BackgroundColor { get; set; }

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

        [Parameter]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (null != Object) {
                Object = Text.ConvertToString(Object, Separator.ToString());
            } else {
                Object = Uri;
            }

            Object = $"\u001B]8;;{Uri}\a{Object}\u001B]8;;\a";

            var result = new Text()
            {
                BackgroundColor = BackgroundColor,
                ForegroundColor = ForegroundColor,
                Separator = Separator,
                Object = Object,
                Clear = !LeaveColor,
                Entities = !IgnoreEntities,
            };

            if (Passthru) {
                WriteObject(result);
            } else {
                WriteObject(result.ToString());
            }
        }
    }
}
