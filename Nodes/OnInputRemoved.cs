using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputremoved", Title = "On Input Removed", Category = "OBS Events")]
  public class OnInputRemovedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;

    protected override void OnCreate()
    {
      Subscribe<InputRemovedEventArgs>(it =>
      {
        InputName = it.InputName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
  }
}