using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.recordstatechanged", Title = "On Record State Changed", Category = "OBS Events")]
  public class OnRecordStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private RecordStateChanged OutputState;

    protected override void OnCreate()
    {
      Subscribe<RecordStateChangedEventArgs>(it =>
      {
        OutputState = it.OutputState;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Output State")]
    public RecordStateChanged OutputStateOutput() => OutputState;
  }
}