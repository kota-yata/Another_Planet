using UnityEngine;

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
    }
}