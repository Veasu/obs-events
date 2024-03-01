using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputaudiosyncoffsetchanged", Title = "On Input Audio Sync Offset Changed", Category = "OBS Events")]
  public class OnInputAudioSyncOffsetChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;
    private int InputAudioSyncOffset;

    protected override void OnCreate()
    {
      Subscribe<InputAudioSyncOffsetChangedEventArgs>(it =>
      {
        InputName = it.InputName;
        InputAudioSyncOffset = it.InputAudioSyncOffset;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
    [DataOutput(98)]
    [Label("Input Audio Sync Offset")]
    public int InputAudioSyncOffsetOutput() => InputAudioSyncOffset;
  }
}