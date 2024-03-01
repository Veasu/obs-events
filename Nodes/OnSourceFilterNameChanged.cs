using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sourcefilternamechanged", Title = "On Source Filter Name Changed", Category = "OBS Events")]
  public class OnSourceFilterNameChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SourceName;
    private string OldFilterName;
    private string FilterName;

    protected override void OnCreate()
    {
      Subscribe<SourceFilterNameChangedEventArgs>(it =>
      {
        SourceName = it.SourceName;
        OldFilterName = it.OldFilterName;
        FilterName = it.FilterName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Source Name")]
    public string SourceNameOutput() => SourceName;
    [DataOutput(98)]
    [Label("Old Filter Name")]
    public string OldFilterNameOutput() => OldFilterName;
    [DataOutput(97)]
    [Label("Filter Name")]
    public string FilterNameOutput() => FilterName;
  }
}