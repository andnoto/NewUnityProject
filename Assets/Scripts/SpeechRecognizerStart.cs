using UnityEngine;
using UnityEngine.UI;


public class SpeechRecognizerStart : MonoBehaviour

{

    AndroidJavaClass javaClass;
    //

    public Text displayText;

    //
    private string editText;
    // public GameObject semaforo;
    public GameObject redLight;
    public GameObject greenLight;
    // 'ActionRequired.StartAction' is called.
    public ActionRequired actionRequired;



    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
#if UNITY_EDITOR
            DisplayText("Mouse " + gameObject.name);
#elif UNITY_ANDROID
//
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass androidSystem = new AndroidJavaClass("com.example.myapplicationlibrary.SpeechRecognizerManagement");
         // debug    AndroidJavaClass androidSystem = new AndroidJavaClass("com.example.myapplicationlibrary.UnitySendMessageTest");
            context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                androidSystem.CallStatic(
                    "startSpeechRecognizer",
         // debug           "startUnitySendMessageTest",
                    context,
                    gameObject.name, "ReceiveResult", "ReceiveError", "ReceiveReady", "ReceiveBegin"
                );
            }));
//


#endif
        }

    }


    // debug
    void ReceiveDebug(string message)
    {
        if (displayText != null)
            displayText.text = message.ToString();
    }

    //Microphone standby state
    void ReceiveReady(string message)
    {
        redLight.SetActive(false);
        greenLight.SetActive(true);
    }

    //Microphone begin speech recognization state
    void ReceiveBegin(string message)
    {
        if (displayText != null)
            displayText.text = message.ToString();
        redLight.SetActive(false);
        greenLight.SetActive(true);
    }

    //Receive the result when speech recognition succeed.
    void ReceiveResult(string editText)
    {
        // Callback when recognization success
        //
        if (string.IsNullOrEmpty(editText))
            return;

        string[] words = editText.Split('\n');
        DisplayText(words);
        redLight.SetActive(true);
        greenLight.SetActive(false);
        actionRequired.StartAction(words);
    }

    //Receive the error when speech recognition fail.
    void ReceiveError(string message)
    {
        redLight.SetActive(true);
        greenLight.SetActive(false);
    }

    //Display text string (and for reading)
    public void DisplayText(object message)
    {
        if (displayText != null)
            displayText.text = message.ToString();
    }
}

