using Microsoft.Xna.Framework.Media;
using System;

namespace GDLibrary
{
    /// <summary>
    /// Encapsulates the properties of a playable video
    /// </summary>
    public class VideoCue
    {
        #region Fields

        /// <summary>
        /// A unique name for the video
        /// </summary>
        private string name;

        /// <summary>
        /// Unique auto generated id
        /// </summary>
        private string id;

        /// <summary>
        /// A user defined video file in supported format
        /// </summary>
        private Video video;

        /// <summary>
        /// Get/set looping
        /// </summary>
        private bool isLooped;

        /// <summary>
        /// Get/set is muted
        /// </summary>
        private bool isMuted;

        /// <summary>
        /// Time from which to begin playing the video
        /// </summary>
        private TimeSpan playPosition;

        #endregion Fields

        #region Properties

        public string Name { get => name; set => name = value.Length != 0 ? value.Trim() : "Default_Name"; }
        public string Id { get => id; }
        public Video Video { get => video; set => video = value; }
        public bool IsLooped { get => isLooped; set => isLooped = value; }
        public bool IsMuted { get => isMuted; set => isMuted = value; }
        public TimeSpan PlayPosition { get => playPosition; set => playPosition = value; }

        #endregion Properties

        public VideoCue(string name, Video video, TimeSpan playPosition, bool isLooped = false, bool isMuted = false)
        {
            Name = name;
            this.id = $"VC-" + Guid.NewGuid();
            this.video = video;
            this.playPosition = playPosition;
            this.isLooped = isLooped;
            this.isMuted = isMuted;
        }

        //TODO - Clone, Equals, GetHashCode
    }
}