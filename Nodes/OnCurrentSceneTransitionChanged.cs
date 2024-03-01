using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.currentscenetransitionchanged", Title = "On Current Scene Transition Changed", Category = "OBS Events")]
  public class OnCurrentSceneTransitionChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string TransitionName;

    protected override void OnCreate()
    {
      Subscribe<CurrentSceneTransitionChangedEventArgs>(it =>
      {
        TransitionName = it.TransitionName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Transition Name")]
    public string TransitionNameOutput() => TransitionName;
  }
}