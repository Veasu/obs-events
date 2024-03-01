﻿using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputRemoved"/>
    /// </summary>
    public class InputRemovedEventArgs : Warudo.Core.Events.Event
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        public InputRemovedEventArgs(string inputName)
        {
            InputName = inputName;
        }
    }
}