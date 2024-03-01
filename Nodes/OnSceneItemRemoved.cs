using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sceneitemremoved", Title = "On Scene Item Removed", Category = "OBS Events")]
  public class OnSceneItemRemovedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;
    private string SourceName;
    private int SceneItemId;

    protected override void OnCreate()
    {
      Subscribe<SceneItemRemovedEventArgs>(it =>
      {
        SceneName = it.SceneName;
        SourceName = it.SourceName;
        SceneItemId = it.SceneItemId;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Scene Name")]
    public string SceneNameOutput() => SceneName;
    [DataOutput(98)]
    [Label("Source Name")]
    public string SourceNameOutput() => SourceName;
    [DataOutput(97)]
    [Label("Scene Item Id")]
    public int SceneItemIdOutput() => SceneItemId;
  }
}