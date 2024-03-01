using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sceneitemlockstatechanged", Title = "On Scene Item Lock State Changed", Category = "OBS Events")]
  public class OnSceneItemLockStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;
    private int SceneItemId;
    private bool SceneItemLocked;

    protected override void OnCreate()
    {
      Subscribe<SceneItemLockStateChangedEventArgs>(it =>
      {
        SceneName = it.SceneName;
        SceneItemId = it.SceneItemId;
        SceneItemLocked = it.SceneItemLocked;
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
    [Label("Scene Item Locked")]
    public bool SceneItemLockedOutput() => SceneItemLocked;
  }
}