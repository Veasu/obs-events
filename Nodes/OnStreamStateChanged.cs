using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.streamstatechanged", Title = "On Stream State Changed", Category = "OBS Events")]
  public class OnStreamStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private OutputStateChanged OutputState;

    protected override void OnCreate()
    {
      Subscribe<StreamStateChangedEventArgs>(it =>
      {
        OutputState = it.OutputState;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Output State")]
    public OutputStateChanged OutputStateOutput() => OutputState;
  }
}