using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject LeftTop;
    public GameObject RightTop;
	public GameObject LeftBottom;
	public GameObject RightBottom;

    public Camera cam;

    //三维向量，存储坐标
    Vector3 LTPos;
    Vector3 RTPos;
    Vector3 LBPos;
    Vector3 RBPos;

    Vector3 CurPos;
    float gridWidth = 0;
    float gridHeight = 0;
    //棋盘上每个落子点位置
    Vector2[,] chessPos;
    //棋盘上每个落子点状态，为0无棋，为1黑棋，为-1白棋
    int[,] chessState;
    enum turn {black, white};
    //当前轮到谁下棋
    turn chessTurn;
    //黑白棋及胜利提示图
    public Texture2D white;
    public Texture2D black;
    public Texture2D bwin;
    public Texture2D wwin;
    int winner = 0;
    bool on = true;
    float mindis;

    // Start is called before the first frame update
    void Start()
    {
        //存储每个落子点的位置
        chessPos = new Vector2[16,16];
        chessState = new int[16,16];
        chessTurn = turn.black;
    }
    //计算两点间距离
    float Dis(Vector3 cPos, Vector2 chPos)
    {
        return Mathf.Sqrt(Mathf.Pow(cPos.x - chPos.x,2)+Mathf.Pow(cPos.y - chPos.y,2));
    }
    
    int judge(int m, int n) 
    {
        int flag = 0;
        if(chessTurn == turn.black)
            flag = 1;//轮到黑棋，判断白棋上一步是否胜利
        else
            flag = -1;
        int total = 1;
        int k;
        //同一行左侧连续同样棋子个数
        for(k = m-1; k >= 0; k--) {
            //chessState[k][n]会报错
            if(chessState[k,n] == chessState[m,n])
                total++;
            else
                break;
        }
        //右侧
        for(k = m+1; k <= 15; k++) {
            if(chessState[k,n] == chessState[m,n])
                total++;
            else
                break;
        }
        if(total >= 5)
            return flag;
        else
            total = 1;
        //同一列
        for(k = n-1; k >= 0; k--) {
            if(chessState[m,k] == chessState[m,n])
                total++;
            else
                break;
        }
        for(k = n+1; k <= 15; k++) {
            if(chessState[m,k] == chessState[m,n])
                total++;
            else
                break;
        }
        if(total >= 5)
            return flag;
        else
            total = 1;
        //对角线方向，左下右上
        for(k = 1; k <= n; k++) {
            if (k <= 15-m && k <= 15-n) {
                if(chessState[m+k,n+k] == chessState[m,n])
                    total++;
                else
                    break;
            }
            else
                break;
        }
        for(k = 1; k <= m; k++) {
            if (k <= m && k <= n) {
                if(chessState[m-k,n-k] == chessState[m,n])
                    total++;
                else
                    break;
            }
            else
                break;
        }
        if(total >= 5)
            return flag;
        else
            total = 1;
        
        //另一个对角线方向，左上右下
        for(k = 1; k <= n; k++) {
            if (k <= 15-m && k <= n) {
                if(chessState[m+k,n-k] == chessState[m,n])
                    total++;
                else
                    break;
            }
            else
                break;
        }
        for(k = 1; k <= m; k++) {
            if (k <= 15-n && k <= m) {
                if(chessState[m-k,n+k] == chessState[m,n])
                    total++;
                else
                    break;
            }
            else
                break;
        }
        if(total >= 5)
            return flag;
        else return 0;
    }


    // Update is called once per frame
    void Update()
    {
        //获取目标对象当前世界坐标系位置，将其转换为屏幕坐标系的点
        //定位棋盘四个端点的坐标
        LTPos = cam.WorldToScreenPoint(LeftTop.transform.position);
        RTPos = cam.WorldToScreenPoint(RightTop.transform.position);
        LBPos = cam.WorldToScreenPoint(LeftBottom.transform.position);
        RBPos = cam.WorldToScreenPoint(RightBottom.transform.position);

        //计算出棋盘上每个格子的长宽
        gridWidth = (RTPos.x - LTPos.x) /15;
        gridHeight = (LTPos.y - LBPos.y) /15;
        mindis = gridWidth < gridHeight ? gridWidth :gridHeight;


        for(int i = 0; i <= 15; i++) {
            for(int j = 0; j <= 15; j++) {

                chessPos[i, j] = new Vector2(LBPos.x + gridWidth*i, LBPos.y + gridHeight*j);
            }
        }
        //鼠标点击棋盘下棋
        if(on && Input.GetMouseButtonDown(0)) {
            CurPos = Input.mousePosition;
            for(int i = 0; i <= 15; i++) {
                for(int j = 0; j <= 15; j++) {
                    //判断离鼠标点击位置最近的落子点
                    if(Dis(CurPos, chessPos[i,j]) < mindis/2 && chessState[i,j] == 0) {
                        if(chessTurn == turn.black) {
                            chessState[i,j] = 1;
                        }
                        else {
                            chessState[i,j] = -1;
                        }
                        chessTurn = (chessTurn == turn.black ? turn.white : turn.black);
                        //成功下棋，判断有没有人赢了
                        int ans=judge(i, j);
                        if (ans == 1) {
                            Debug.Log("白赢了");
                            winner = 1;
                            on = false;
                        }
                        else if (ans == -1) {
                            Debug.Log("黑赢了");
                            winner = -1;
                            on = true;
                        }
                        
                    }
                }
            }
        }
        //点击空格键清空棋盘，重新开始游戏
        if (Input.GetKeyDown(KeyCode.Space)) {
            for(int i = 0; i <= 15; i++) {
                for(int j = 0; j <= 15; j++) {
                    chessState[i,j] = 0;
                }
            }
            on = true;
            chessTurn = turn.black;
            winner = 0;
        }
    }
    void OnGUI()
    {

        for(int i = 0; i <= 15; i++) 
        {
            for(int j = 0; j <= 15; j++)
            {
                if(chessState[i,j] == 1)
                {
                    GUI.DrawTexture(new Rect(chessPos[i,j].x-gridWidth/2, Screen.height-chessPos[i,j].y-gridHeight/2,gridWidth,gridHeight),black);
                
                }
                if(chessState[i,j] == -1)
                {
                    GUI.DrawTexture(new Rect(chessPos[i,j].x-gridWidth/2, Screen.height-chessPos[i,j].y-gridHeight/2,gridWidth,gridHeight),white);

                }
            }
        }
        if(winner == -1)
            GUI.DrawTexture(new Rect(Screen.width*0.35f, Screen.height*0.25f, Screen.width*0.3f, Screen.height*0.5f), bwin);
            //坐标，大小
        if(winner == 1)
            GUI.DrawTexture(new Rect(Screen.width*0.35f, Screen.height*0.25f, Screen.width*0.3f, Screen.height*0.5f), wwin);
    
    }
}
