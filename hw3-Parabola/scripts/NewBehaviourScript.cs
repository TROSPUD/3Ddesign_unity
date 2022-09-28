using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//运用物理公式直接计算出小球位置
public class NewBehaviourScript : MonoBehaviour
{
    public GameObject ball;
    public float volocity = 10;
	public float angle = 60;
	float volocityY = 0;
	float volocityX = 0;
	float time = 0;

	void Start()
    {
		volocityY = Mathf.Sin(Mathf.Deg2Rad * angle) * volocity;
		volocityX = Mathf.Cos(Mathf.Deg2Rad * angle) * volocity;
    }

	//Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		//水平方向匀速运动
		float x = volocityX * time;

		//垂直方向重力加速度下落运动
		float y = volocityY * time - 9.8f * 0.5f * Mathf.Pow(time, 2);

		Vector3 pos = new Vector3(x, y, 0);
		if (pos.y >= 0)
		{
			//记录小球轨迹
			GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = pos;
			ball.transform.position = pos;
		}
	}
}
