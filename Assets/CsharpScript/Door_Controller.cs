using System.Collections;
using UnityEngine;

public class Door_Controller : MonoBehaviour {
    public bool isOpened = false;
    public bool isEnter = false;

    void Update () {
        Quaternion quaternion = this.transform.rotation;
        float currentRotation = quaternion.eulerAngles.y;
        if (Input.GetKeyDown (KeyCode.F)) {
            if (isEnter && !isOpened) {
                transform.Rotate (0, 90, 0);
                isOpened = true;
                Debug.Log (currentRotation);
            } else if (isEnter && isOpened) {
                transform.Rotate (0, -90, 0);
                isOpened = false;
                //Debug.Log (currentRotation);
            }
        }
    }

    private void OnTriggerStay (Collider collider) {
        if (collider.gameObject.tag == "Player") {
            isEnter = true;
        }
    }
    private void OnTriggerExit (Collider collider) {
        if (collider.gameObject.tag == "Player") {
            isEnter = false;
        }
    }
}