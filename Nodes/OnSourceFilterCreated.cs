using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.sourcefiltercreated", Title = "On Source Filter Created", Category = "OBS Events")]
  public class OnSourceFilterCreatedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SourceName;
    private JObject DefaultFilterSettings;

    protected override void OnCreate()
    {
      Subscribe<SourceFilterCreatedEventArgs>(it =>
      {
        SourceName = it.SourceName;
        DefaultFilterSettings = it.DefaultFilterSettings;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Source Name")]
    public string SourceNameOutput() => SourceName;
    [DataOutput(98)]
    [Label("Default Filter Settings")]
    public JObject DefaultFilterSettingsOutput() => DefaultFilterSettings;
  }
}