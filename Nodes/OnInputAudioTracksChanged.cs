using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputaudiotrackschanged", Title = "On Input Audio Tracks Changed", Category = "OBS Events")]
  public class OnInputAudioTracksChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;
    private JObject InputAudioTracks;

    protected override void OnCreate()
    {
      Subscribe<InputAudioTracksChangedEventArgs>(it =>
      {
        InputName = it.InputName;
        InputAudioTracks = it.InputAudioTracks;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
    [DataOutput(98)]
    [Label("Input Audio Tracks")]
    public JObject InputAudioTracksOutput() => InputAudioTracks;
  }
}