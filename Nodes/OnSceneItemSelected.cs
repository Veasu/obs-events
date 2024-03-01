using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sceneitemselected", Title = "On Scene Item Selected", Category = "OBS Events")]
  public class OnSceneItemSelectedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;
    private string SceneItemId;

    protected override void OnCreate()
    {
      Subscribe<SceneItemSelectedEventArgs>(it =>
      {
        SceneName = it.SceneName;
        SceneItemId = it.SceneItemId;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Scene Name")]
    public string SceneNameOutput() => SceneName;
    [DataOutput(98)]
    [Label("Scene Item Id")]
    public string SceneItemIdOutput() => SceneItemId;
  }
}