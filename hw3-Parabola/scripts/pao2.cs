using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//利用球形插值slerp实现
public class pao2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject t1;
    public GameObject t2;
    public GameObject ball;//要放在start外面
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = (t1.transform.position + t2.transform.position)*0.5f;
        print(center);
        center -= new Vector3(0,1,0);
        print(center);
        Vector3 start = t1.transform.position - center;
        Vector3 end = t2.transform.position - center;
        print(start);
        print(end);
        //两向量直接进行Slerp球形插值时能直接产生一个曲线弧形的轨迹
        
        ball.transform.position = Vector3.Slerp(start,end,Time.time);
        ball.transform.position += center;
        GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = ball.transform.position;
    }
}


