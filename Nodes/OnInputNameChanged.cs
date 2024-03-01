using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputnamechanged", Title = "On Input Name Changed", Category = "OBS Events")]
  public class OnInputNameChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string OldInputName;
    private string InputName;

    protected override void OnCreate()
    {
      Subscribe<InputNameChangedEventArgs>(it =>
      {
        OldInputName = it.OldInputName;
        InputName = it.InputName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Old Input Name")]
    public string OldInputNameOutput() => OldInputName;
    [DataOutput(98)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
  }
}