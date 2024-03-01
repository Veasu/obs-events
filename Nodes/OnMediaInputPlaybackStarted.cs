using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.mediainputplaybackstarted", Title = "On Media Input Playback Started", Category = "OBS Events")]
  public class OnMediaInputPlaybackStartedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;

    protected override void OnCreate()
    {
      Subscribe<MediaInputPlaybackStartedEventArgs>(it =>
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