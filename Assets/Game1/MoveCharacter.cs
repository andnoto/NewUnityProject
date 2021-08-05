using UnityEngine;
using UnityEngine.UI;

// A screen touch move the sprite left or right.
// Multiple screen touch makes the sprite jump.

public class MoveCharacter : MonoBehaviour

{

    // 
    public ActionRequired actionRequired;
    //
    //This is Main Camera in the Scene
    Camera cam;
    // log
    // public Text logText;
    //
    private string[] words = new string[1];

    void Start()
    {
        //This gets the Main Camera from the Scene
        cam = Camera.main;
    }

    private void Update()
{
        // Transforms gameObject position from world space into screen space (pixel)
        Vector3 screenPosGameObject = cam.WorldToScreenPoint(gameObject.transform.position);
        // log 
        // if (logText != null) logText.text = "screenPos.x =" + screenPos.x;
        // Handle screen touches.
        if (Input.touchCount == 1)
        // A screen touch to the right of the sprite moves the sprite to the right.
        // A screen touch to the left of the sprite moves the sprite to the left.
        {
            Touch touch = Input.GetTouch(0);
            // position of the touch in pixel coordinates 
            Vector2 posTouch = touch.position;
            // log 
            // if (logText != null) logText.text = "pos.x =" + pos.x + "screenPos.x =" + screenPos.x;
            if (posTouch.x > screenPosGameObject.x)
                {
                words[0] = "destra";
                actionRequired.StartAction(words);
                } else
                if (posTouch.x < screenPosGameObject.x)
                    {
                    words[0] = "sinistra";
                    actionRequired.StartAction(words);
                    }
         }
        if (Input.touchCount > 1)
        // Multiple screen touch makes the sprite jump.
        {
            words[0] = "salta";
                actionRequired.StartAction(words);
            }
        }
}



