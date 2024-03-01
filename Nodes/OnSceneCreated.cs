using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.scenecreated", Title = "On Scene Created", Category = "OBS Events")]
  public class OnSceneCreatedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;
    private bool IsGroup;

    protected override void OnCreate()
    {
      Subscribe<SceneCreatedEventArgs>(it =>
      {
        SceneName = it.SceneName;
        IsGroup = it.IsGroup;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Scene Name")]
    public string SceneNameOutput() => SceneName;
    [DataOutput(98)]
    [Label("Is Group")]
    public bool IsGroupOutput() => IsGroup;
  }
}