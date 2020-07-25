using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Behave : MonoBehaviour {
    public GameObject target;
    public GameObject hideeTarget;
    public GameObject playerForced;
    private void Start () {
        hideeTarget.SetActive (false);
    }
    private void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.tag == "Player") {
            hideeTarget.SetActive (true);
            Rigidbody rb = playerForced.GetComponent<Rigidbody> ();
            int power = 10000;
            Vector3 force = new Vector3 (0, 15, 30); // 力を設定
            rb.AddForce (force * power, ForceMode.Force); // 力を加える
        }
    }
}