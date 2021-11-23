using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GDLibrary.Components.UI
{
    public class UIProgressBarController : UIController
    {
        #region Fields

        private int currentValue;
        private int maxValue;
        private int startValue;
        private Rectangle sourceRectangle;

        #endregion Fields

        #region Properties

        public int CurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = ((value >= 0) && (value <= maxValue)) ? value : 0;
            }
        }

        public int MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                maxValue = (value >= 0) ? value : 0;
            }
        }

        public int StartValue
        {
            get
            {
                return startValue;
            }
            set
            {
                //       if(startValue != value)
                startValue = (value >= 0) ? value : 0;
            }
        }

        public UIProgressBarController(int startValue,
            int maxValue, int currentValue)
        {
            StartValue = startValue;
            MaxValue = maxValue;
            CurrentValue = currentValue;

            sourceRectangle = (uiObject as UITextureObject).SourceRectangle;
            sourceRectangle.Width = (int)(sourceRectangle.Width * 0.5f);
        }

        public override void Update()
        {
            HandleInputs();
        }

        protected override void HandleInputs()
        {
            HandleKeyboardInput();
        }

        protected override void HandleKeyboardInput()
        {
            if (Input.Keys.WasJustPressed(Keys.Up))
            {
                //increment current value
                CurrentValue++;
            }
            else if (Input.Keys.WasJustPressed(Keys.Down))
            {
                //decrement current value
                CurrentValue--;
            }
        }

        protected override void HandleMouseInput()
        {
            throw new System.NotImplementedException();
        }

        protected override void HandleGamepadInput()
        {
            throw new System.NotImplementedException();
        }

        #endregion Properties

        //to do...Equals, GetHashCode, Clone
    }
}