using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputvolumechanged", Title = "On Input Volume Changed", Category = "OBS Events")]
  public class OnInputVolumeChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private InputVolume Volume;

    protected override void OnCreate()
    {
      Subscribe<InputVolumeChangedEventArgs>(it =>
      {
        Volume = it.Volume;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Volume")]
    public InputVolume VolumeOutput() => Volume;
  }
}