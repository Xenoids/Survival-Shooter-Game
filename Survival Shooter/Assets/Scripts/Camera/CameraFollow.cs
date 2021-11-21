using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float ngesmooth = 5f;
    Vector3 offset;

    // Start is called before the first frame update
    private void Start()
    {
        // Mendapatkan offset antara target dan cam
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Mendapatkan pos utk cam
        Vector3 targetCamPos = target.position + offset;

        // set pos cam dgn smoothing
        transform.position = Vector3.Lerp(transform.position, targetCamPos, ngesmooth * Time.deltaTime);
    }
}
