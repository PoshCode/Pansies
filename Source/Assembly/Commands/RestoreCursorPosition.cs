using System;
using System.Management.Automation;

namespace PoshCode.Pansies.Commands
{
    [Cmdlet("Restore", "CursorPosition")]
    public sealed class RestoreCursorPositionCommand : Cmdlet
    {
        protected override void EndProcessing()
        {
            Console.Write("\u001b8");
        }
    }
}
