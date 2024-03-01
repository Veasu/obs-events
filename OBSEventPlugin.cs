using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using OBSWebsocketDotNet.Communication;
using OBSWebsocketDotNet.Types.Events;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Clients;
using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Events;
using Warudo.Core.Plugins;
using Warudo.Plugins.Core.Mixins;
using WebSocketSharp;

namespace veasu.obsevents {
  [PluginType(Id = "com.veasu.obsevents", Name = "OBS Events", Version = "0.1.0", Author = "Veasu", Description = "OBE Event Nodes", Icon = @"<svg version=""1.0"" xmlns=""http://www.w3.org/2000/svg""
  width=""50.000000pt"" height=""50.000000pt"" viewBox=""0 0 50.000000 50.000000""
  preserveAspectRatio=""xMidYMid meet""><g transform=""translate(0.000000,50.000000) scale(0.100000,-0.100000)""
  fill=""currentColor"">
  <path d=""M123 250 c57 -166 76 -210 89 -210 10 0 18 3 18 7 0 4 -30 99 -67
  210 -61 183 -70 203 -90 203 -22 0 -20 -8 50 -210z""/>
  <path d=""M184 250 c67 -198 72 -210 96 -210 24 0 29 12 96 210 67 199 70 210
  50 210 -19 0 -29 -21 -81 -180 -33 -99 -62 -180 -65 -180 -3 0 -32 81 -65 180
  -52 159 -62 180 -81 180 -20 0 -17 -11 50 -210z""/>
  <path d=""M303 334 c-35 -105 -40 -130 -31 -152 9 -25 14 -15 59 118 27 79 49
  148 49 152 0 5 -8 8 -18 8 -14 0 -27 -28 -59 -126z""/>
  </g></svg>", NodeTypes = new[] {
    typeof(OnCurrentProgramSceneChangedNode),
    typeof(OnSceneListChangedNode),
    typeof(OnSceneItemListReindexedNode),
    typeof(OnSceneItemCreatedNode),
    typeof(OnSceneItemRemovedNode),
    typeof(OnSceneItemEnableStateChangedNode),
    typeof(OnSceneItemLockStateChangedNode),
    typeof(OnCurrentSceneCollectionChangedNode),
    typeof(OnSceneCollectionListChangedNode),
    typeof(OnCurrentSceneTransitionChangedNode),
    typeof(OnCurrentSceneTransitionDurationChangedNode),
    typeof(OnSceneTransitionStartedNode),
    typeof(OnSceneTransitionEndedNode),
    typeof(OnSceneTransitionVideoEndedNode),
    typeof(OnCurrentProfileChangedNode),
    typeof(OnProfileListChangedNode),
    typeof(OnStreamStateChangedNode),
    typeof(OnRecordStateChangedNode),
    typeof(OnReplayBufferStateChangedNode),
    typeof(OnCurrentPreviewSceneChangedNode),
    typeof(OnStudioModeStateChangedNode),
    typeof(OnSceneItemSelectedNode),
    typeof(OnSceneItemTransformChangedNode),
    typeof(OnInputAudioSyncOffsetChangedNode),
    typeof(OnSourceFilterCreatedNode),
    typeof(OnSourceFilterRemovedNode),
    typeof(OnSourceFilterListReindexedNode),
    typeof(OnSourceFilterEnableStateChangedNode),
    typeof(OnInputMuteStateChangedNode),
    typeof(OnInputVolumeChangedNode),
    typeof(OnVendorEventNode),
    typeof(OnMediaInputPlaybackEndedNode),
    typeof(OnMediaInputPlaybackStartedNode),
    typeof(OnMediaInputActionTriggeredNode),
    typeof(OnVirtualcamStateChangedNode),
    typeof(OnCurrentSceneCollectionChangingNode),
    typeof(OnCurrentProfileChangingNode),
    typeof(OnSourceFilterNameChangedNode),
    typeof(OnInputCreatedNode),
    typeof(OnInputRemovedNode),
    typeof(OnInputNameChangedNode),
    typeof(OnInputActiveStateChangedNode),
    typeof(OnInputShowStateChangedNode),
    typeof(OnInputAudioBalanceChangedNode),
    typeof(OnInputAudioTracksChangedNode),
    typeof(OnInputAudioMonitorTypeChangedNode),
    typeof(OnInputVolumeMetersNode),
    typeof(OnReplayBufferSavedNode),
    typeof(OnSceneCreatedNode),
    typeof(OnSceneRemovedNode),
    typeof(OnSceneNameChangedNode)
  })]
  public class OBSEventPlugin : Plugin {
    private const string ToolbarIconConnected = "<svg viewBox=\"0 0 24 24\" role=\"img\" xmlns=\"http://www.w3.org/2000/svg\"><g id=\"SVGRepo_bgCarrier\" stroke-width=\"2.648\"></g><g id=\"SVGRepo_tracerCarrier\" stroke-linecap=\"round\" stroke-linejoin=\"round\"></g><g id=\"SVGRepo_iconCarrier\"><title>OBS Studio icon</title><path fill=\"currentColor\" d=\"M12,24C5.383,24,0,18.617,0,12S5.383,0,12,0s12,5.383,12,12S18.617,24,12,24z M12,1.109 C5.995,1.109,1.11,5.995,1.11,12C1.11,18.005,5.995,22.89,12,22.89S22.89,18.005,22.89,12C22.89,5.995,18.005,1.109,12,1.109z M6.182,5.99c0.352-1.698,1.503-3.229,3.05-3.996c-0.269,0.273-0.595,0.483-0.844,0.78c-1.02,1.1-1.48,2.692-1.199,4.156 c0.355,2.235,2.455,4.06,4.732,4.028c1.765,0.079,3.485-0.937,4.348-2.468c1.848,0.063,3.645,1.017,4.7,2.548 c0.54,0.799,0.962,1.736,0.991,2.711c-0.342-1.295-1.202-2.446-2.375-3.095c-1.135-0.639-2.529-0.802-3.772-0.425 c-1.56,0.448-2.849,1.723-3.293,3.293c-0.377,1.25-0.216,2.628,0.377,3.772c-0.825,1.429-2.315,2.449-3.932,2.756 c-1.244,0.261-2.551,0.059-3.709-0.464c1.036,0.302,2.161,0.355,3.191-0.011c1.381-0.457,2.522-1.567,3.024-2.935 c0.556-1.49,0.345-3.261-0.591-4.54c-0.7-1.007-1.803-1.717-3.002-1.969c-0.38-0.068-0.764-0.098-1.148-0.134 c-0.611-1.231-0.834-2.66-0.528-3.996L6.182,5.99z\"></path></g></svg><i style=\"position: absolute; background: rgb(36,161,72); width: 6px; height: 6px; border-radius: 3px;\"></i>";
    private const string ToolbarIconNotConnected = "<svg viewBox=\"0 0 23.75 23.75\" role=\"img\" xmlns=\"http://www.w3.org/2000/svg\"><g id=\"SVGRepo_bgCarrier\" stroke-width=\"2.648\"></g><g id=\"SVGRepo_tracerCarrier\" stroke-linecap=\"round\" stroke-linejoin=\"round\"></g><g id=\"SVGRepo_iconCarrier\"><title>OBS Studio icon</title><path fill=\"currentColor\" d=\"M12,24C5.383,24,0,18.617,0,12S5.383,0,12,0s12,5.383,12,12S18.617,24,12,24z M12,1.109 C5.995,1.109,1.11,5.995,1.11,12C1.11,18.005,5.995,22.89,12,22.89S22.89,18.005,22.89,12C22.89,5.995,18.005,1.109,12,1.109z M6.182,5.99c0.352-1.698,1.503-3.229,3.05-3.996c-0.269,0.273-0.595,0.483-0.844,0.78c-1.02,1.1-1.48,2.692-1.199,4.156 c0.355,2.235,2.455,4.06,4.732,4.028c1.765,0.079,3.485-0.937,4.348-2.468c1.848,0.063,3.645,1.017,4.7,2.548 c0.54,0.799,0.962,1.736,0.991,2.711c-0.342-1.295-1.202-2.446-2.375-3.095c-1.135-0.639-2.529-0.802-3.772-0.425 c-1.56,0.448-2.849,1.723-3.293,3.293c-0.377,1.25-0.216,2.628,0.377,3.772c-0.825,1.429-2.315,2.449-3.932,2.756 c-1.244,0.261-2.551,0.059-3.709-0.464c1.036,0.302,2.161,0.355,3.191-0.011c1.381-0.457,2.522-1.567,3.024-2.935 c0.556-1.49,0.345-3.261-0.591-4.54c-0.7-1.007-1.803-1.717-3.002-1.969c-0.38-0.068-0.764-0.098-1.148-0.134 c-0.611-1.231-0.834-2.66-0.528-3.996L6.182,5.99z\"></path></g></svg>";
      
    [Markdown(-997, primary: true)]
    public string ConnectedInfo = "Status: Disconnected";

    private static readonly ConcurrentQueue<Action> _actions = new();

    [Mixin(1)]
    public ToolbarItemMixin ToolbarItem;

    [DataInput(-900)]
    [Label("URL")]
    private string URL = "";

    [DataInput(-899)]
    [Label("Port")]
    private string Port = "";

    [Markdown(-801)]
    public string PasswordInfo = "Leave password blank if you have no authentication setup";

    [DataInput(-800)]
    [Label("Password")]
    public string websocketPassword = "";

    [DataInput]
    [Hidden]
    [Disabled]
    private string websocketPasswordHidden = "";

    [Trigger(-600)]
    [HiddenIf(nameof(IsConnected))]
    private void Connect() {
     DoConnect();
    }

    [Trigger(-599)]
    [HiddenIf(nameof(IsDisconnected))]
    private void Disconnect() {
     DoDisconnect();
    }
   
    // [DataInput(-598)]
    // [Label("Auto Reconnect")]
    // [HiddenIf(nameof(IsConnected))]
    // private bool AutoReconnect = false;

    // [Markdown(-597, false, false)]
    // [HiddenIf(nameof(IsConnected))]
    // public string HeartbeatString = "Auto Reconnect Will Attempt To Reconnect Every Minute";

    private bool AttemptFirstLogin = false;

    private void DoConnect() {
       if (!IsConnected()) {
        WebsocketClient.ConnectAsync("ws://" + URL + ":" + Port, websocketPasswordHidden);
        WebsocketClient.Connected += OnConnected;
        WebsocketClient.Disconnected += OnDisconnect;
      }
    }

    private void DoDisconnect() {
       if (IsConnected()) {
        WebsocketClient.Disconnect();
      }
    }
    public OBSWebsocketDotNet.OBSWebsocket WebsocketClient = new();
    
    private bool IsConnected() => this.WebsocketClient.IsConnected;
    private bool IsDisconnected() => this.WebsocketClient.IsConnected == false;

    protected override void OnCreate() {
      base.OnCreate();

      Watch<string>(nameof(websocketPassword), (from, to) => {
        websocketPassword = new string('*', to.Length);
        if (to.Length == 0) {
          websocketPasswordHidden = "";
        }
        else if ((to.Length - websocketPasswordHidden.Length) == 1) {
          websocketPasswordHidden += to[^1];
        } else if ((to.Length - websocketPasswordHidden.Length) > 1) {
          websocketPasswordHidden += to[..];
        } else {
          websocketPasswordHidden = websocketPasswordHidden[..to.Length];
        }
        BroadcastDataInput(nameof(websocketPassword));
      });
      ToolbarItem.SetTooltip("Disconnected From OBS Websocket");
      ToolbarItem.SetIcon(ToolbarIconNotConnected);
      ToolbarItem.OnTrigger = () => Context.Service.NavigateToPlugin(Type.Id, null);
      ToolbarItem.SetEnabled(true);

      SubscribeToEvents(WebsocketClient);

    }

    protected override void OnDestroy() {
        base.OnDestroy();
        WebsocketClient.Disconnect();
        WebsocketClient.Connected -= OnConnected;
        WebsocketClient.Disconnected -= OnDisconnect;
    }
    

    public override void OnUpdate() {
      if (!AttemptFirstLogin && IsDisconnected() && !URL.IsNullOrEmpty() && !Port.IsNullOrEmpty()){
        DoConnect();
        AttemptFirstLogin = true;
      }

      // if (AutoReconnect && IsDisconnected()) {
      //   UniTask.Void(async () => {
      //     await UniTask.Delay(TimeSpan.FromMinutes(1));
      //     DoConnect();
      //   });
      // }

      while(_actions.Count > 0)
      {
          if(_actions.TryDequeue(out var action))
          {
              action?.Invoke();
          }
      }
    }

    public override void OnPreUpdate() {
        base.OnPreUpdate();
    }

    private void OnDisconnect(object sender, ObsDisconnectionInfo info) {
      ConnectedInfo = "Status: Disconnected";
      ToolbarItem.SetTooltip("Disconnected From OBS Websocket");
      ToolbarItem.SetIcon(ToolbarIconNotConnected);
      WebsocketClient.Disconnected -= OnDisconnect;
      WebsocketClient.Connected -= OnConnected;
    }

    private void OnConnected(object sender, EventArgs args) {
      ToolbarItem.SetTooltip("Connected To OBS Websocket");
      ToolbarItem.SetIcon(ToolbarIconConnected);
      ConnectedInfo = "Status: Connected";
      Context.PluginManager.SavePluginData(this);
      Context.Service.Toast(Warudo.Core.Server.ToastSeverity.Success, "Connected To OBS", "Successfully Connected to OBS Websocket");
      Broadcast();
    }

    private static void SubscribeToEvents<T>(T target)
    {
        foreach(var @event in target.GetType().GetEvents())
        {
            var handler = GetHandlerFor(@event);
            @event.AddEventHandler(target, handler);
        }
    }

    static MethodInfo? genericHandlerMethod = typeof(OBSEventPlugin).GetMethod("Handler", BindingFlags.Static | BindingFlags.NonPublic);
    static MethodInfo? genericBroadcast = typeof(OBSEventPlugin).GetMethod("Broadcast", BindingFlags.Static | BindingFlags.NonPublic);

    private static Delegate GetHandlerFor(EventInfo eventInfo)
    {
        var eventArgsType = eventInfo.EventHandlerType?.GetMethod("Invoke")?.GetParameters()[1]?.ParameterType;
        if(eventArgsType is null)
        {
            throw new ApplicationException("Couldn't get event args type from eventInfo.");
        }
        var handlerMethod = genericHandlerMethod?.MakeGenericMethod(eventArgsType);
        if(handlerMethod is null)
        {
            throw new ApplicationException("Couldn't get handlerMethod from genericHandlerMethod.");
        }

        if (eventArgsType.Namespace == "System") {
          return Delegate.CreateDelegate(typeof(EventHandler), handlerMethod);
        }
        return Delegate.CreateDelegate(typeof(EventHandler<>).MakeGenericType(eventArgsType), handlerMethod);
    }

    private static void Handler<e>(object? sender, e args)
    {
      if(args.GetType().IsSubclassOf(typeof(Event)))
      {
        _actions.Enqueue(() => {
          genericBroadcast.MakeGenericMethod(args.GetType()).Invoke(sender, new object[]{args});
        });
      }
    }

    private static void Broadcast<T>(T e) where T : Warudo.Core.Events.Event => Context.EventBus.Broadcast<T>(e);
  }
}
