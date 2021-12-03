﻿using GDLibrary.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GDLibrary.Components.UI
{
    public class UIReticuleBehaviour : UIBehaviour
    {
        private Texture2D originalDefaultTexture;

        public override void Awake()
        {
            originalDefaultTexture = (uiObject as UITextureObject).DefaultTexture;

            Input.Mouse.SetMouseVisible(false);

            EventDispatcher.Subscribe(EventCategoryType.Picking, HandlePickedObject);

            base.Awake();
        }

        private void HandlePickedObject(EventData eventData)
        {
            var uiTextureObject = uiObject as UITextureObject;

            switch (eventData.EventActionType)
            {
                case EventActionType.OnObjectPicked:
                    uiObject.Color = Color.Red;
                    uiTextureObject.DefaultTexture = uiTextureObject.AlternateTexture;
                    break;

                case EventActionType.OnNoObjectPicked:
                    uiObject.Color = Color.White;
                    uiTextureObject.DefaultTexture = originalDefaultTexture;
                    break;

                default:
                    break;
            }

            if (eventData.Parameters != null)
            {
                GameObject picked = eventData?.Parameters[0] as GameObject;
                var dist = Vector3.Distance(Camera.Main.Transform.LocalTranslation, picked.Transform.LocalTranslation);
                System.Diagnostics.Debug.WriteLine(dist);
            }
        }

        protected override void OnDisabled()
        {
            Input.Mouse.SetMouseVisible(true);
            base.OnDisabled();
        }

        public override void Update()
        {
            uiObject.Transform.LocalTranslation = Input.Mouse.Position;
            base.Update();
        }
    }
}