using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solar : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Sun").transform.Rotate(Vector3.up*Time.deltaTime*5);
        //自转
        GameObject.Find("Mercury").transform.Rotate(Vector3.up*Time.deltaTime*5);
        //公转
        GameObject.Find("Mercury").transform.RotateAround(Vector3.zero, new Vector3(0.1f,1,0),47*Time.deltaTime);
        GameObject.Find("Venus").transform.Rotate(Vector3.up*Time.deltaTime*5);
        GameObject.Find("Venus").transform.RotateAround(Vector3.zero, new Vector3(0.1f,1,0),35*Time.deltaTime);
        GameObject.Find("Earth").transform.Rotate(Vector3.up*Time.deltaTime*5);
        GameObject.Find("Earth").transform.RotateAround(Vector3.zero, new Vector3(0.1f,1,0),29*Time.deltaTime);
        GameObject.Find("Mars").transform.Rotate(Vector3.up*Time.deltaTime*5);
        GameObject.Find("Mars").transform.RotateAround(Vector3.zero, new Vector3(0.1f,1,0),24*Time.deltaTime);
        GameObject.Find("Jupiter").transform.Rotate(Vector3.up*Time.deltaTime*5);
        GameObject.Find("Jupiter").transform.RotateAround(Vector3.zero, new Vector3(0.1f,1,0),13*Time.deltaTime);
        GameObject.Find("Saturn").transform.Rotate(Vector3.up*Time.deltaTime*5);
        GameObject.Find("Saturn").transform.RotateAround(Vector3.zero, new Vector3(0.1f,1,0),9*Time.deltaTime);
        GameObject.Find("Uranus").transform.Rotate(Vector3.up*Time.deltaTime*5);
        GameObject.Find("Uranus").transform.RotateAround(Vector3.zero, new Vector3(0.1f,1,0),6*Time.deltaTime);
        GameObject.Find("Neptune").transform.Rotate(Vector3.up*Time.deltaTime*5);
        GameObject.Find("Neptune").transform.RotateAround(Vector3.zero, new Vector3(0.1f,1,0),5*Time.deltaTime);

        //月球绕地球公转
        GameObject.Find("Moon").transform.RotateAround(Vector3.zero, new Vector3(0.1f,1,0),1*Time.deltaTime);
    }
}
