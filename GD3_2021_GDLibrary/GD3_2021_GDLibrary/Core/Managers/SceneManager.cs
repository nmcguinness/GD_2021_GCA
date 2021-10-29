﻿using GDLibrary.Components;
using System.Collections.Generic;

namespace GDLibrary.Core
{
    /// <summary>
    /// SceneManager stores all scenes and calls update on currently active scene
    /// </summary>
    public class SceneManager
    {
        #region Fields

        private static readonly int DEFAULT_SCENE_COUNT_AT_START = 4;
        private List<Scene> scenes;
        private int sceneToLoad;
        private int activeSceneIndex;

        #endregion Fields

        #region Properties & Indexer

        /// <summary>
        /// Allows us to access a scene in the SceneManager using [] notation e.g. sceneManager[0]
        /// </summary>
        /// <param name="index">Integer index of required scene</param>
        /// <returns>Instance of scene at the index specified</returns>
        public Scene this[int index]
        {
            get
            {
                return scenes[index];
            }
        }

        /// <summary>
        /// Returns the active scene
        /// </summary>
        public Scene ActiveScene
        {
            get { return activeSceneIndex >= 0 && activeSceneIndex < scenes.Count ? scenes[activeSceneIndex] : null; }
        }

        /// <summary>
        /// Count of scenes held in the manager scene list
        /// </summary>
        public int Count
        {
            get
            {
                return scenes.Count;
            }
        }

        #endregion Properties & Indexer

        #region Constructor

        public SceneManager()
        {
            scenes = new List<Scene>(DEFAULT_SCENE_COUNT_AT_START);
            sceneToLoad = -1;
            activeSceneIndex = -1;
        }

        #endregion Constructor

        public void Update()
        {
            //if no active scene and no scene to load then exit
            if (activeSceneIndex == -1 && sceneToLoad == -1)
                return;

            //if scene to load and its not the current scene
            if (sceneToLoad > -1)
            {
                //unload current scene
                if (activeSceneIndex > -1)
                    scenes[activeSceneIndex].Unload();

                //set scene to load as new scene
                activeSceneIndex = sceneToLoad;

                //TODO - nullify camera and disable UI

                //set scene as current in globally accessible static in Scene
                Scene.Current = scenes[activeSceneIndex];

                //reset to -1 to show no scene is waiting to load
                sceneToLoad = -1;

                //initialize the new scene
                scenes[activeSceneIndex].Initialize();
            }

            //update the scene (either new, or the same scene from last update)
            scenes[activeSceneIndex].Update();
        }

        #region Actions - Add, Remove, Load

        /// <summary>
        /// Add a new scene to the manager which by default is not the active scene
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="isActive"></param>
        public void Add(Scene scene, bool isActive = false)
        {
            if (!scenes.Contains(scene))
            {
                scenes.Add(scene);

                if (isActive)
                    sceneToLoad = scenes.Count - 1;
            }
        }

        /// <summary>
        /// Remove a scene from the manager. Notice that you can't remove the default scene.
        /// </summary>
        /// <param name="scene">The scene to remove.</param>
        public void Remove(Scene scene)
        {
            var index = scenes.IndexOf(scene);

            if (index > 0)
            {
                if (activeSceneIndex == index)
                    activeSceneIndex = scenes.Count - 1;

                //call unload to remove any scene resources
                scenes[index].Unload();

                //remove from the list
                scenes.RemoveAt(index);
            }
        }

        public void LoadScene(Scene scene)
        {
            if (scene != null)
            {
                var index = scenes.IndexOf(scene);

                if (index == -1)
                {
                    scenes.Add(scene);
                    index = scenes.Count - 1;
                }

                sceneToLoad = index;
            }
        }

        public void LoadScene(string name)
        {
            var index = scenes.FindIndex(scene => scene.Name.Equals(name));
            LoadScene(index);
        }

        public void LoadScene(int index)
        {
            if (index >= 0 && index < scenes.Count)
                sceneToLoad = index;
        }

        #endregion Actions - Add, Remove, Load
    }
}