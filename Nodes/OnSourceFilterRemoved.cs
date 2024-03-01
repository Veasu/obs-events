using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sourcefilterremoved", Title = "On Source Filter Removed", Category = "OBS Events")]
  public class OnSourceFilterRemovedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SourceName;
    private string FilterName;

    protected override void OnCreate()
    {
      Subscribe<SourceFilterRemovedEventArgs>(it =>
      {
        SourceName = it.SourceName;
        FilterName = it.FilterName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Source Name")]
    public string SourceNameOutput() => SourceName;
    [DataOutput(98)]
    [Label("Filter Name")]
    public string FilterNameOutput() => FilterName;
  }
}