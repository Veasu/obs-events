using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sourcefilterenablestatechanged", Title = "On Source Filter Enable State Changed", Category = "OBS Events")]
  public class OnSourceFilterEnableStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SourceName;
    private string FilterName;
    private bool FilterEnabled;

    protected override void OnCreate()
    {
      Subscribe<SourceFilterEnableStateChangedEventArgs>(it =>
      {
        SourceName = it.SourceName;
        FilterName = it.FilterName;
        FilterEnabled = it.FilterEnabled;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Source Name")]
    public string SourceNameOutput() => SourceName;
    [DataOutput(98)]
    [Label("Filter Name")]
    public string FilterNameOutput() => FilterName;
    [DataOutput(97)]
    [Label("Filter Enabled")]
    public bool FilterEnabledOutput() => FilterEnabled;
  }
}