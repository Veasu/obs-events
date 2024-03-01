using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sceneitemlistreindexed", Title = "On Scene Item List Reindexed", Category = "OBS Events")]
  public class OnSceneItemListReindexedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;

    protected override void OnCreate()
    {
      Subscribe<SceneItemListReindexedEventArgs>(it =>
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