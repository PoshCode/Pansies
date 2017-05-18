using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Write","Host")]
    public class WriteHostCommand : PSCmdlet
    {

        [Parameter(Position=0, ValueFromPipeline=true, ValueFromRemainingArguments=true)]
        public object Object { get; set; }

        [Parameter()]
        public SwitchParameter NoNewline { get; set; }

        [Parameter()]
        public object Separator { get; set; } = " ";

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public RgbColor ForegroundColor { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public RgbColor BackgroundColor { get; set; }

        protected override void ProcessRecord()
        {
            HostInformationMessage informationMessage = new HostInformationMessage();
            informationMessage.Message = Text.GetString(ForegroundColor, BackgroundColor, Object, Separator.ToString(), true, true);
            informationMessage.NoNewLine = NoNewline.IsPresent;

            this.WriteInformation(informationMessage, new string[] { "PSHOST" });
            // this.Host.UI.TranscribeResult(result);

        }
    }
}
