using Microsoft.Xna.Framework.Input;

namespace GDLibrary.Components
{
    public class FirstPersonCameraController : Controller
    {
        private Keys forward;
        private Keys backward;

        public FirstPersonCameraController(Keys forward, Keys backward)
        {
            this.forward = forward;
            this.backward = backward;
        }
        public override void Update()
        {
            if (Input.Keys.IsPressed(forward))
            {
                transform.Translate(transform.Forward * Time.Time.Instance.DeltaTime / 1000.0f);
            }
            else if (Input.Keys.IsPressed(backward))
            {
                transform.Translate(-transform.Forward * Time.Time.Instance.DeltaTime / 1000.0f);
            }

            //A/D - transform.Rotate(0,1,0);

            base.Update();
        }
    }
}