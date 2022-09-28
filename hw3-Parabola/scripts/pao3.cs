using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//根据与目标点距离，实时设置小球的旋转角度
public class pao3 : MonoBehaviour
{
    public GameObject end;
    public GameObject ball;
    public float speed = 10;
    private float distance;
    private bool move = true;
    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(ball.transform.position, end.transform.position);
        StartCoroutine(shoot());
        //协同程序
    }
    IEnumerator shoot()
    {
        while (move)
        {
            Vector3 endpos = end.transform.position;
            ball.transform.LookAt(endpos);
            float angle = Mathf.Min(1, Vector3.Distance(ball.transform.position, endpos)/distance)*45;
            ball.transform.rotation = ball.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42,42),0,0);
            float curdis = Vector3.Distance(ball.transform.position, end.transform.position);
            if(curdis < 0.5f)
                move = false;
            ball.transform.Translate(Vector3.forward*Mathf.Min(speed*Time.deltaTime,curdis));
            GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = ball.transform.position;
            yield return null;
            //暂缓一帧，在下一帧接着往下处理
        }
    }

}
