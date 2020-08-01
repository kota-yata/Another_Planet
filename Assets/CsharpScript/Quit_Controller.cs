using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quit_Controller : MonoBehaviour {
    public Button button_to_be_emulated;
    void Update () {
        if (Input.GetKeyUp (KeyCode.Escape)) {
            button_to_be_emulated.onClick.Invoke ();
        };
        /*if (canvas.enabled) {
            if (Input.GetKeyUp (KeyCode.Y)) OnCallExit ();
            if (Input.GetKeyUp (KeyCode.N)) OnCallCancel ();
        }*/
    }
    public void OnCallExit () {
        UnityEngine.Application.Quit ();
    }
    public void StartOnClick () {
        SceneManager.LoadScene ("Room");
    }
    public void BackToHomeOnClick () {
        SceneManager.LoadScene ("Start");
    }
}