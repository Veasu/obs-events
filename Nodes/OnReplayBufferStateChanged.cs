using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.replaybufferstatechanged", Title = "On Replay Buffer State Changed", Category = "OBS Events")]
  public class OnReplayBufferStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private OutputStateChanged OutputState;

    protected override void OnCreate()
    {
      Subscribe<ReplayBufferStateChangedEventArgs>(it =>
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