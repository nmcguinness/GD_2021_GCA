using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary.Managers
{
    /// <summary>
    /// Stores a dictionary of MENU-SPECIFIC ui scenes and updates and draws the currently active scene
    /// </summary>
    public class UIMenuManager : UISceneManager
    {
        public UIMenuManager(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }
    }
}