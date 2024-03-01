using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputactivestatechanged", Title = "On Input Active State Changed", Category = "OBS Events")]
  public class OnInputActiveStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;
    private bool VideoActive;

    protected override void OnCreate()
    {
      Subscribe<InputActiveStateChangedEventArgs>(it =>
      {
        InputName = it.InputName;
        VideoActive = it.VideoActive;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
    [DataOutput(98)]
    [Label("Video Active")]
    public bool VideoActiveOutput() => VideoActive;
  }
}