using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.currentscenecollectionchanged", Title = "On Current Scene Collection Changed", Category = "OBS Events")]
  public class OnCurrentSceneCollectionChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneCollectionName;

    protected override void OnCreate()
    {
      Subscribe<CurrentSceneCollectionChangedEventArgs>(it =>
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