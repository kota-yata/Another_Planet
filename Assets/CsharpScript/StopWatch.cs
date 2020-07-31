using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour {
    //テキストオブジェクト
    private GameObject textObject;
    //テキストコンポーネント
    private Text text;
    DateTime dt1 = DateTime.Now;
    // Start is called before the first frame update
    void Start () {
        //textオブジェクトを取得
        textObject = GameObject.Find ("Clt_text");
        //テキストコンポーネントを取得
        text = textObject.GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update () {
        stopWatch ();
    }

    public void stopWatch () {
        TimeSpan limit = new TimeSpan (0, 5, 0);
        DateTime dt2 = DateTime.Now;
        TimeSpan result = limit - (dt2 - dt1);
        text.text = $"{result.ToString(@"mm\:ss")}";
        if (result.Minutes == 0 && result.Seconds == 0) {
            SceneManager.LoadScene ("TimeUp");
        }
    }
}