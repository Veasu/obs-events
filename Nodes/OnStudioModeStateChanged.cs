using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.studiomodestatechanged", Title = "On Studio Mode State Changed", Category = "OBS Events")]
  public class OnStudioModeStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private bool StudioModeEnabled;

    protected override void OnCreate()
    {
      Subscribe<StudioModeStateChangedEventArgs>(it =>
      {
        StudioModeEnabled = it.StudioModeEnabled;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Studio Mode Enabled")]
    public bool StudioModeEnabledOutput() => StudioModeEnabled;
  }
}