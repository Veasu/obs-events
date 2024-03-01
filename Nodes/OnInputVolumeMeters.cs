using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputvolumemeters", Title = "On Input Volume Meters", Category = "OBS Events")]
  public class OnInputVolumeMetersNode : Node
  {
    [FlowOutput]
    public Continuation Exit;



    protected override void OnCreate()
    {
      Subscribe<InputVolumeMetersEventArgs>(it =>
      {

        InvokeFlow("Exit");
      }, true);
    }


  }
}