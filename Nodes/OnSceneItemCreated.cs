using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sceneitemcreated", Title = "On Scene Item Created", Category = "OBS Events")]
  public class OnSceneItemCreatedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;
    private string SourceName;
    private int SceneItemId;
    private int SceneItemIndex;

    protected override void OnCreate()
    {
      Subscribe<SceneItemCreatedEventArgs>(it =>
      {
        SceneName = it.SceneName;
        SourceName = it.SourceName;
        SceneItemId = it.SceneItemId;
        SceneItemIndex = it.SceneItemIndex;
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
    [DataOutput(96)]
    [Label("Scene Item Index")]
    public int SceneItemIndexOutput() => SceneItemIndex;
  }
}