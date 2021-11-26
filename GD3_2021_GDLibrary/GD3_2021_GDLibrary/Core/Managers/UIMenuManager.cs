using GDLibrary.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary.Managers
{
    /// <summary>
    /// Stores a dictionary of MENU-SPECIFIC ui scenes and updates and draws the currently active scene
    /// </summary>
    public class UIMenuManager : UISceneManager
    {
        public UIMenuManager(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
        }

        protected override void HandleEvent(EventData eventData)
        {
            if (eventData.EventCategoryType == EventCategoryType.Menu)
            {
                if (eventData.EventActionType == EventActionType.OnPause)
                    statusType = StatusType.Drawn | StatusType.Updated;
                else if (eventData.EventActionType == EventActionType.OnPlay)
                    statusType = StatusType.Off;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (UIObject uiObject in activeUIScene.UiObjects)
            {
                var btnObject = uiObject as UIButtonObject;

                if (btnObject != null)
                {
                    if (Input.Mouse.Bounds.Intersects(btnObject.Bounds))
                    {
                        if (Input.Mouse.WasJustClicked(Inputs.MouseButton.Left))
                        {
                            System.Diagnostics.Debug.WriteLine("mouse over!!!");
                            EventDispatcher.Raise(new EventData(EventCategoryType.Menu,
                                EventActionType.OnPlay));
                        }
                    }
                }
            }

            //loop through all the ui objects in the active scene

            //test if the mouse is over the ui button object

            //react if mouse over and/or mouse click over ui button

            base.Update(gameTime);
        }
    }
}