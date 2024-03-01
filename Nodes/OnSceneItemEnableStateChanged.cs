using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sceneitemenablestatechanged", Title = "On Scene Item Enable State Changed", Category = "OBS Events")]
  public class OnSceneItemEnableStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;
    private int SceneItemId;
    private bool SceneItemEnabled;

    protected override void OnCreate()
    {
      Subscribe<SceneItemEnableStateChangedEventArgs>(it =>
      {
        SceneName = it.SceneName;
        SceneItemId = it.SceneItemId;
        SceneItemEnabled = it.SceneItemEnabled;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Scene Name")]
    public string SceneNameOutput() => SceneName;
    [DataOutput(98)]
    [Label("Scene Item Id")]
    public int SceneItemIdOutput() => SceneItemId;
    [DataOutput(97)]
    [Label("Scene Item Enabled")]
    public bool SceneItemEnabledOutput() => SceneItemEnabled;
  }
}