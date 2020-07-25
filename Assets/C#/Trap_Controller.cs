using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Controller : MonoBehaviour {
    private int maxX = -10;
    private int minX = -50;
    private float transition = 0.05f;
    private bool isPlayerOn = false;
    public GameObject target;
    private void Update () {
        if (isPlayerOn) {
            var currentX = target.transform.position.x;
            if (currentX > maxX) {
                transition = -transition;
            } else if (currentX < minX) {
                transition = -transition;
            }
            target.transform.Translate (transition, 0, 0);
        }
    }
    private void OnTriggerStay (Collider collider) {
        if (collider.gameObject.tag == "Player") {
            isPlayerOn = true;
        }
    }
    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            isPlayerOn = false;
        }
    }
}