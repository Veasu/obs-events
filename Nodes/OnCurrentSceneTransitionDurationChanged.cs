using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.currentscenetransitiondurationchanged", Title = "On Current Scene Transition Duration Changed", Category = "OBS Events")]
  public class OnCurrentSceneTransitionDurationChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private int TransitionDuration;

    protected override void OnCreate()
    {
      Subscribe<CurrentSceneTransitionDurationChangedEventArgs>(it =>
      {
        TransitionDuration = it.TransitionDuration;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Transition Duration")]
    public int TransitionDurationOutput() => TransitionDuration;
  }
}