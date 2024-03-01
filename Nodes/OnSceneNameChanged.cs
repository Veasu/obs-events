using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.scenenamechanged", Title = "On Scene Name Changed", Category = "OBS Events")]
  public class OnSceneNameChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string OldSceneName;
    private string SceneName;

    protected override void OnCreate()
    {
      Subscribe<SceneNameChangedEventArgs>(it =>
      {
        OldSceneName = it.OldSceneName;
        SceneName = it.SceneName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Old Scene Name")]
    public string OldSceneNameOutput() => OldSceneName;
    [DataOutput(98)]
    [Label("Scene Name")]
    public string SceneNameOutput() => SceneName;
  }
}