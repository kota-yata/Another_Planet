using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Transition : MonoBehaviour {
    public string key;
    public string scene;
    public GameObject targetPanel;
    private bool isKeyDown = false;
    float alfa;
    float speed = 0.01f;
    float red, green, blue;
    void Start () {
        red = targetPanel.GetComponent<Image> ().color.r;
        green = targetPanel.GetComponent<Image> ().color.g;
        blue = targetPanel.GetComponent<Image> ().color.b;
    }
    void Update () {
        if (Input.GetKeyDown (key)) {
            isKeyDown = true;
        }
        if (isKeyDown) {
            targetPanel.GetComponent<Image> ().color = new Color (red, green, blue, alfa);
            if (alfa > 1) {
                SceneManager.LoadScene (scene);
            }
            alfa += speed;
        }
    }
}