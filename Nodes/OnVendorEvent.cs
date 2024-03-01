using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.vendorevent", Title = "On Vendor Event", Category = "OBS Events")]
  public class OnVendorEventNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string VendorName;
    private string EventType;
    private JObject eventData;

    protected override void OnCreate()
    {
      Subscribe<VendorEventArgs>(it =>
      {
        VendorName = it.VendorName;
        EventType = it.EventType;
        eventData = it.eventData;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Vendor Name")]
    public string VendorNameOutput() => VendorName;
    [DataOutput(98)]
    [Label("Event Type")]
    public string EventTypeOutput() => EventType;
    [DataOutput(97)]
    [Label("event Data")]
    public JObject eventDataOutput() => eventData;
  }
}