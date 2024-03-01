using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.mediainputplaybackended", Title = "On Media Input Playback Ended", Category = "OBS Events")]
  public class OnMediaInputPlaybackEndedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;

    protected override void OnCreate()
    {
      Subscribe<MediaInputPlaybackEndedEventArgs>(it =>
      {
        InputName = it.InputName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
  }
}