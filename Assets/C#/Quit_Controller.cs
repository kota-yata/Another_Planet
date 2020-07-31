using UnityEngine;
<<<<<<< HEAD
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
=======

public class Quit_Controller : MonoBehaviour {
    public Canvas canvas;
    void Start () {
        canvas.enabled = false;
    }
    void Update () {
        if (Input.GetKeyUp (KeyCode.Escape)) {
            if (canvas.enabled) {
                canvas.enabled = false;
            } else {
                canvas.enabled = true;
            }
        };
        if (canvas.enabled) {
            if (Input.GetKeyUp (KeyCode.Y)) OnCallExit ();
            if (Input.GetKeyUp (KeyCode.N)) OnCallCancel ();
        }
    }
    void OnApplicationQuit () {
        if (canvas.enabled == false) Application.CancelQuit ();
        canvas.enabled = true;
    }
    public void OnCallExit () {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit ();
#endif
    }
    public void OnCallCancel () {
        canvas.enabled = false;
>>>>>>> f00aeff2133a3a846b91fd4a87fee47c0f5eccc2
    }
}