using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sourcefilterlistreindexed", Title = "On Source Filter List Reindexed", Category = "OBS Events")]
  public class OnSourceFilterListReindexedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SourceName;

    protected override void OnCreate()
    {
      Subscribe<SourceFilterListReindexedEventArgs>(it =>
      {
        SourceName = it.SourceName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Source Name")]
    public string SourceNameOutput() => SourceName;
  }
}