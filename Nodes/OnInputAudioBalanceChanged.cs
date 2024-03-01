using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputaudiobalancechanged", Title = "On Input Audio Balance Changed", Category = "OBS Events")]
  public class OnInputAudioBalanceChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;
    private double InputAudioBalance;

    protected override void OnCreate()
    {
      Subscribe<InputAudioBalanceChangedEventArgs>(it =>
      {
        InputName = it.InputName;
        InputAudioBalance = it.InputAudioBalance;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
    [DataOutput(98)]
    [Label("Input Audio Balance")]
    public double InputAudioBalanceOutput() => InputAudioBalance;
  }
}