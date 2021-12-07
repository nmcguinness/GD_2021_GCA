﻿using GDLibrary;
using GDLibrary.Components;
using GDLibrary.Core;
using JigLibX.Collision;

namespace GDApp
{
    public class MyHeroCollider : CharacterCollider
    {
        public MyHeroCollider(float accelerationRate, float decelerationRate,
       bool isHandlingCollision = true, bool isTrigger = false)
            : base(accelerationRate, decelerationRate, isHandlingCollision, isTrigger)
        {
        }

        protected override void HandleResponse(GameObject parentGameObject)
        {
            if (parentGameObject.GameObjectType == GameObjectType.Consumable)
            {
                System.Diagnostics.Debug.WriteLine(parentGameObject?.Name);

                object[] parameters = { parentGameObject };
                EventDispatcher.Raise(new EventData(EventCategoryType.GameObject,
                    EventActionType.OnRemoveObject, parameters));

                object[] parameters1 = { "health", 1 };
                EventDispatcher.Raise(new EventData(EventCategoryType.UI,
                    EventActionType.OnHealthDelta, parameters1));

                // EventDispatcher.Raise(new EventData(EventCategoryType.Inventory,
                //  EventActionType.OnAddInventory, parameters1));
            }

            base.HandleResponse(parentGameObject);
        }
    }
}