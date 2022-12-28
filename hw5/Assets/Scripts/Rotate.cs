using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    public float xangle, yangle, zangle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axis = new Vector3(xangle, yangle, zangle);
        this.transform.RotateAround(new Vector3(0, 0, 0), axis, speed * Time.deltaTime);
        this.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }
}
