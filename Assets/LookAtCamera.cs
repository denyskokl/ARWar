using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    private Transform mainCamera;

    void Start(){
        mainCamera = Camera.main.transform;
    }

    void LateUpdate() {
        transform.rotation = mainCamera.rotation;
    }
}
