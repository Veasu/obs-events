using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.currentscenecollectionchanging", Title = "On Current Scene Collection Changing", Category = "OBS Events")]
  public class OnCurrentSceneCollectionChangingNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneCollectionName;

    protected override void OnCreate()
    {
      Subscribe<CurrentSceneCollectionChangingEventArgs>(it =>
      {
        SceneCollectionName = it.SceneCollectionName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Scene Collection Name")]
    public string SceneCollectionNameOutput() => SceneCollectionName;
  }
}