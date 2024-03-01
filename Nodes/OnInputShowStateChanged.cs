using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputshowstatechanged", Title = "On Input Show State Changed", Category = "OBS Events")]
  public class OnInputShowStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;
    private bool VideoShowing;

    protected override void OnCreate()
    {
      Subscribe<InputShowStateChangedEventArgs>(it =>
      {
        InputName = it.InputName;
        VideoShowing = it.VideoShowing;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
    [DataOutput(98)]
    [Label("Video Showing")]
    public bool VideoShowingOutput() => VideoShowing;
  }
}