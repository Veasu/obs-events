using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentSceneTransitionChanged"/>
    /// </summary>
    public class CurrentSceneTransitionChangedEventArgs : Warudo.Core.Events.Event
    {
        /// <summary>
        /// Name of the new transition
        /// </summary>
        public string TransitionName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="transitionName">The transition name</param>
        public CurrentSceneTransitionChangedEventArgs(string transitionName)
        {
            TransitionName = transitionName;
        }
    }
}