using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.scenetransitionstarted", Title = "On Scene Transition Started", Category = "OBS Events")]
  public class OnSceneTransitionStartedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string TransitionName;

    protected override void OnCreate()
    {
      Subscribe<SceneTransitionStartedEventArgs>(it =>
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