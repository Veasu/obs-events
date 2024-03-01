using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputmutestatechanged", Title = "On Input Mute State Changed", Category = "OBS Events")]
  public class OnInputMuteStateChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;
    private bool InputMuted;

    protected override void OnCreate()
    {
      Subscribe<InputMuteStateChangedEventArgs>(it =>
      {
        InputName = it.InputName;
        InputMuted = it.InputMuted;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
    [DataOutput(98)]
    [Label("Input Muted")]
    public bool InputMutedOutput() => InputMuted;
  }
}