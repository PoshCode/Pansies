using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Set", "CursorPosition")]
    [OutputType(typeof(string))]
    public sealed class SetCursorPositionCommand : Cmdlet
    {
        [Parameter(Position = 0)]
        public int? Line { get; set; }

        [Parameter(Position = 1)]
        public int? Column { get; set; }

        [Parameter]
        public SwitchParameter Absolute { get; set; }

        protected override void EndProcessing()
        {
            var position = new Position(Line, Column, Absolute);
            WriteObject(position.ToString());
        }
    }
}
