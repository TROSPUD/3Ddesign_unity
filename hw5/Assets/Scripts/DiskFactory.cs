using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFO;
// 工厂模式
public class DiskFactory : MonoBehaviour {
    private List<Disk> toDelete = new List<Disk>();
    private List<Disk> toUse = new List<Disk>();
    // 可选6种颜色
    public Color[] colors = {Color.white, Color.yellow, Color.red, Color.blue, Color.green, Color.black};
    //生产飞碟，从空闲队列里查找有没有可用飞碟，如果没有则新建一个飞碟
    public GameObject GetDisk(int round) {  //根据回合数对飞碟设置属性并返回
        GameObject newDisk = null;
        if (toUse.Count > 0) {
            newDisk = toUse[0].gameObject;
            toUse.Remove(toUse[0]);
        } else {
            newDisk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UFO"), Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<Disk>();
        }
        // 飞碟的速度为 round * 3
        newDisk.GetComponent<Disk>().speed = 3.0f * round;
        // 飞碟随 round 越来越小
        newDisk.GetComponent<Disk>().size = (1 - round * 0.05f);
        // 飞碟颜色随机
        int color = UnityEngine.Random.Range(0, 6);
        newDisk.GetComponent<Disk>().color = colors[color];

        // 飞碟的发射方向: -1，0则为负方向，1，2则为正方向
        float RanX = UnityEngine.Random.Range(-1, 3) < 1 ? -1 : 1;
        newDisk.GetComponent<Disk>().direction = new Vector3(-RanX, UnityEngine.Random.Range(-2f, 2f), 0);
        // 飞碟的初始位置
        newDisk.GetComponent<Disk>().position = new Vector3(RanX * 13, UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-1f, 1f));

        toDelete.Add(newDisk.GetComponent<Disk>());
        newDisk.SetActive(false);
        newDisk.name = newDisk.GetInstanceID().ToString();
        return newDisk;
    }
    //飞碟回收，将不使用的飞碟从使用队列放到空闲队列里
    public void FreeDisk(GameObject disk) {
        Disk cycledDisk = null;
        foreach (Disk toCycle in toDelete) {
            if (disk.GetInstanceID() == toCycle.gameObject.GetInstanceID()) {
                cycledDisk = toCycle;
            }
        }
        if (cycledDisk != null) {
            cycledDisk.gameObject.SetActive(false);
            toUse.Add(cycledDisk);
            toDelete.Remove(cycledDisk);
        }
    }
}
