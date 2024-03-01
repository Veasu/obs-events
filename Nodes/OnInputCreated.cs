using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.inputcreated", Title = "On Input Created", Category = "OBS Events")]
  public class OnInputCreatedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string InputName;
    private string InputKind;
    private string UnversionedInputKind;
    private JObject InputSettings;
    private JObject DefaultInputSettings;

    protected override void OnCreate()
    {
      Subscribe<InputCreatedEventArgs>(it =>
      {
        InputName = it.InputName;
        InputKind = it.InputKind;
        UnversionedInputKind = it.UnversionedInputKind;
        InputSettings = it.InputSettings;
        DefaultInputSettings = it.DefaultInputSettings;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Input Name")]
    public string InputNameOutput() => InputName;
    [DataOutput(98)]
    [Label("Input Kind")]
    public string InputKindOutput() => InputKind;
    [DataOutput(97)]
    [Label("Unversioned Input Kind")]
    public string UnversionedInputKindOutput() => UnversionedInputKind;
    [DataOutput(96)]
    [Label("Input Settings")]
    public JObject InputSettingsOutput() => InputSettings;
    [DataOutput(95)]
    [Label("Default Input Settings")]
    public JObject DefaultInputSettingsOutput() => DefaultInputSettings;
  }
}