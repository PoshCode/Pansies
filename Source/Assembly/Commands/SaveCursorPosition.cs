using System;
using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Save", "CursorPosition")]
    public sealed class SaveCursorPositionCommand : Cmdlet
    {
        protected override void EndProcessing()
        {
            Console.Write("\u001b[s");
        }
    }
}
