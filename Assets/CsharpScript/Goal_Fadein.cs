using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal_Fadein : MonoBehaviour {
    float alfa = 1;
    public float speed = 0.005f;
    float red, green, blue;
    void Start () {
        red = this.gameObject.GetComponent<Image> ().color.r;
        green = this.gameObject.GetComponent<Image> ().color.g;
        blue = this.gameObject.GetComponent<Image> ().color.b;
    }
    void Update () {
        this.gameObject.GetComponent<Image> ().color = new Color (red, green, blue, alfa);
        Debug.Log (alfa);
        if(alfa > 0){
            alfa -= speed;
        }
    }
}