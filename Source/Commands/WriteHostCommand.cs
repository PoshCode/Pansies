using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using PoshCode.Pansies;
using System.Collections;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Write","HostAnsi")]
    public class WriteHostCommand : PSCmdlet
    {

        [Parameter(Position=0, ValueFromPipeline=true, ValueFromRemainingArguments=true)]
        public object Object { get; set; }

        [Parameter()]
        public SwitchParameter NoNewline { get; set; }

        [Parameter()]
        public object Separator { get; set; } = " ";

        [Parameter()]
        public RgbColor ForegroundColor { get; set; }

        [Parameter()]
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
