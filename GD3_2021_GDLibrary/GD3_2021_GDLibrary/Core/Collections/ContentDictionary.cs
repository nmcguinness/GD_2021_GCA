using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace GDLibrary.Collections
{
    /// <summary>
    /// Provide generic map to load and store game content and allow Dispose() to be called on all content
    /// </summary>
    /// <typeparam name="V">MonoGame or user-defined class that optionally implements IDisposable</typeparam>
    public class ContentDictionary<V> : IDisposable
    {
        #region Fields

        private string name;
        private Dictionary<string, V> dictionary;
        private ContentManager content;

        #endregion Fields

        #region Properties

        protected Dictionary<string, V> Dictionary
        {
            get
            {
                return dictionary;
            }
        }

        public V this[string key] //indexer
        {
            get
            {
                if (!Dictionary.ContainsKey(key))
                {
                    throw new Exception(key + " resource was not found in dictionary. Have you loaded it in a Main::LoadTextures() type method?");
                }

                return dictionary[key];
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value.Trim();
            }
        }

        #endregion Properties

        public ContentDictionary(string name = "asset dictionary")
        {
            Name = name;
            dictionary = new Dictionary<string, V>();
        }

        public virtual bool Add(string key, string assetPath)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, Application.Content.Load<V>(assetPath));
                return true;
            }
            return false;
        }

        //same as Load() above but uses assetPath to form key string from regex
        public virtual bool Add(string assetPath)  //"Assets/Props/crates", crates
        {
            return Add(assetPath, Utilities.StringUtility.ParseNameFromPath(assetPath));
        }

        public virtual bool Remove(string key)
        {
            if (dictionary.ContainsKey(key))
            {
                //unload from RAM
                Dispose(dictionary[key]);
                //remove from dictionary
                dictionary.Remove(key);
                return true;
            }
            return false;
        }

        public virtual int Count()
        {
            return dictionary.Count;
        }

        /// <summary>
        /// Disposes of the contents of the dictionary.
        /// </summary>
        /// <seealso cref="https://robertgreiner.com/iterating-through-a-dictionary-in-csharp/"/>
        public virtual void Dispose()
        {
            //copy values from dictionary to list
            List<V> list = new List<V>(dictionary.Values);

            for (int i = 0; i < list.Count; i++)
            {
                Dispose(list[i]);
            }

            //empty the list
            list.Clear();

            //clear the dictionary
            dictionary.Clear();
        }

        public virtual void Dispose(V value)
        {
            //if this is a disposable object (e.g. model, sound, font, texture) then call its dispose
            if (value is IDisposable)
            {
                ((IDisposable)value).Dispose();
            }
            //if it's just a user-defined or C# object, then set to null for garbage collection
            else
            {
                value = default(V); //null
            }
        }
    }
}