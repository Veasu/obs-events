using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.currentpreviewscenechanged", Title = "On Current Preview Scene Changed", Category = "OBS Events")]
  public class OnCurrentPreviewSceneChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;

    protected override void OnCreate()
    {
      Subscribe<CurrentPreviewSceneChangedEventArgs>(it =>
      {
        SceneName = it.SceneName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Scene Name")]
    public string SceneNameOutput() => SceneName;
  }
}