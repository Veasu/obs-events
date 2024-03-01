using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.mediainputactiontriggered", Title = "On Media Input Action Triggered", Category = "OBS Events")]
  public class OnMediaInputActionTriggeredNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;
    private string MediaAction;

    protected override void OnCreate()
    {
      Subscribe<MediaInputActionTriggeredEventArgs>(it =>
      {
        InputName = it.InputName;
        MediaAction = it.MediaAction;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
    [DataOutput(98)]
    [Label("Media Action")]
    public string MediaActionOutput() => MediaAction;
  }
}