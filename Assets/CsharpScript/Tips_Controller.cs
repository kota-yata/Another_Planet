using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips_Controller : MonoBehaviour {
    void Update () {
        Animation ();
    }

    void Animation () {
        Vector3 eulerAngle = new Vector3 (0f, 5f, 0f);
        transform.Rotate (eulerAngle);
    }

    [SerializeField]
    public string textFromInspector;
    public GameObject objToRemove;

    public GameObject tipsTarget;
    public GameObject tipsPanel;
    private void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.tag == "Player") {
            tipsPanel.SetActive (true);
            Text tipsText = tipsTarget.GetComponent<Text> ();
            tipsText.text = textFromInspector;
            Destroy (objToRemove, .5f);
        }
    }
    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            tipsPanel.SetActive (false);
            Text tipsText = tipsTarget.GetComponent<Text> ();
            tipsText.text = "";
        }
    }
}