using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal_Scene_Transition : MonoBehaviour {
    public GameObject target;
    float alfa;
    float speed = 0.005f;
    float red, green, blue;
    void Start () {
        red = target.GetComponent<Image> ().color.r;
        green = target.GetComponent<Image> ().color.g;
        blue = target.GetComponent<Image> ().color.b;
    }
    void Update () {
        target.GetComponent<Image> ().color = new Color (red, green, blue, alfa);
        if (alfa > 1) {
            SceneManager.LoadScene ("Goal");
        }
        alfa += speed;
    }
}