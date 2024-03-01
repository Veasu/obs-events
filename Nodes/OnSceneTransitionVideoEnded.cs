using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.scenetransitionvideoended", Title = "On Scene Transition Video Ended", Category = "OBS Events")]
  public class OnSceneTransitionVideoEndedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string TransitionName;

    protected override void OnCreate()
    {
      Subscribe<SceneTransitionVideoEndedEventArgs>(it =>
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