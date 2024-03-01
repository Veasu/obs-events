using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentProfileChanged"/>
    /// </summary>
    public class CurrentProfileChangedEventArgs : Warudo.Core.Events.Event
    {
        /// <summary>
        /// Name of the new profile
        /// </summary>
        public string ProfileName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="profileName">The profile name</param>
        public CurrentProfileChangedEventArgs(string profileName)
        {
            ProfileName = profileName;
        }
    }
}