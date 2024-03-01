using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.replaybuffersaved", Title = "On Replay Buffer Saved", Category = "OBS Events")]
  public class OnReplayBufferSavedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SavedReplayPath;

    protected override void OnCreate()
    {
      Subscribe<ReplayBufferSavedEventArgs>(it =>
      {
        SavedReplayPath = it.SavedReplayPath;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Saved Replay Path")]
    public string SavedReplayPathOutput() => SavedReplayPath;
  }
}