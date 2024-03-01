using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sceneitemtransformchanged", Title = "On Scene Item Transform Changed", Category = "OBS Events")]
  public class OnSceneItemTransformChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;
    private string SceneItemId;
    private SceneItemTransformInfo Transform;

    protected override void OnCreate()
    {
      Subscribe<SceneItemTransformEventArgs>(it =>
      {
        SceneName = it.SceneName;
        SceneItemId = it.SceneItemId;
        Transform = it.Transform;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Scene Name")]
    public string SceneNameOutput() => SceneName;
    [DataOutput(98)]
    [Label("Scene Item Id")]
    public string SceneItemIdOutput() => SceneItemId;
    [DataOutput(97)]
    [Label("Transform")]
    public SceneItemTransformInfo TransformOutput() => Transform;
  }
}