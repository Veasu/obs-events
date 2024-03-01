using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.profilelistchanged", Title = "On Profile List Changed", Category = "OBS Events")]
  public class OnProfileListChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;



    protected override void OnCreate()
    {
      Subscribe<ProfileListChangedEventArgs>(it =>
      {

        InvokeFlow("Exit");
      }, true);
    }


  }
}