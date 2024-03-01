using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentProgramSceneChanged"/>
    /// </summary>
    public class ProgramSceneChangedEventArgs : Warudo.Core.Events.Event
    {
        /// <summary>
        /// The new scene name
        /// </summary>
        public string SceneName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sceneName">The scene name</param>
        public ProgramSceneChangedEventArgs(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}