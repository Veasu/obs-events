using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.scenelistchanged", Title = "On Scene List Changed", Category = "OBS Events")]
  public class OnSceneListChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;



    protected override void OnCreate()
    {
      Subscribe<SceneListChangedEventArgs>(it =>
      {

        InvokeFlow("Exit");
      }, true);
    }


  }
}