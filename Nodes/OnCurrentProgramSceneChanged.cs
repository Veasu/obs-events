using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet.Types;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;
using Newtonsoft.Json.Linq;

namespace veasu.obsevents
{
  [NodeType(Id = "com.veasu.obsevents.currentprogramscenechanged", Title = "On Current Scene Changed", Category = "OBS Events")]
  public class OnCurrentProgramSceneChangedNode : Node
  {
    [FlowOutput]
    public Continuation Exit;

    private string SceneName;

    protected override void OnCreate()
    {
      Subscribe<ProgramSceneChangedEventArgs>(it =>
      {
        SceneName = it.SceneName;
        InvokeFlow("Exit");
      }, true);
    }

    [DataOutput(99)]
    [Label("Scene Name")]
    public string SceneNameOutput() => SceneName;
  }
}