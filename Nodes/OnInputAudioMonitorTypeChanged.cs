using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputaudiomonitortypechanged", Title = "On Input Audio Monitor Type Changed", Category = "OBS Events")]
  public class OnInputAudioMonitorTypeChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;
    private string MonitorType;

    protected override void OnCreate()
    {
      Subscribe<InputAudioMonitorTypeChangedEventArgs>(it =>
      {
        InputName = it.InputName;
        MonitorType = it.MonitorType;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
    [DataOutput(98)]
    [Label("Monitor Type")]
    public string MonitorTypeOutput() => MonitorType;
  }
}