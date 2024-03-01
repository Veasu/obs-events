using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.currentprofilechanged", Title = "On Current Profile Changed", Category = "OBS Events")]
  public class OnCurrentProfileChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string ProfileName;

    protected override void OnCreate()
    {
      Subscribe<CurrentProfileChangedEventArgs>(it =>
      {
        ProfileName = it.ProfileName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Profile Name")]
    public string ProfileNameOutput() => ProfileName;
  }
}