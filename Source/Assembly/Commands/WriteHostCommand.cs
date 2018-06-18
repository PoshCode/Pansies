using System.Collections.Generic;
using System.Linq;
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

            var tags = new string[] { "PSHOST" };
            
            // Discuss: is it worth implementing this, even though Cmdlet.WriteHost won't respect it?
            var value = GetVariableValue("HostPreference", ActionPreference.Continue);
            // NOTE: Anything but Continue and SilentlyContinue (or Ignore) is pointless, since you can set them on Information 
            if (value is ActionPreference preference && (preference != ActionPreference.SilentlyContinue || preference != ActionPreference.Ignore))
            { 
                tags = new string[] { };
            }

            WriteInformation(informationMessage, tags);
            
            // this.Host.UI.TranscribeResult(result);

        }
    }
}
