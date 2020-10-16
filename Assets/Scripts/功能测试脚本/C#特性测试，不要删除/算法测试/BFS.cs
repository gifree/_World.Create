using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BFS:MonoBehaviour
{
     private void Start()
    {
        int i, j, startx, starty, head, tail, flag, tx, ty;
        //初始化迷宫信息5*4
        n = 5;
        m = 4;
        for (i = 1; i <= n; i++)
        {
            a[i] = new int[5];
            book[i] = new int[5];
            for (j = 1; j <= m; j++)
            {
                //(1,3),(3,3),(4,2),(5,4)处为障碍物
                if ((i == 1 && j == 3) || (i == 3 && j == 3) || (i == 4 && j == 2) || (i == 5 && j == 4))
                {
                    a[i][j] = 1;
                }
                else
                {
                    a[i][j] = 0;
                }
            }
        }
        //终点为(4,3)
        p = 4;
        q = 3;
        //队列初始化
        head = tail = 1;
        //将起点坐标加入到队列
        startx = starty = 1;
        que[tail] = new note()
        {
            x = startx,
            y = starty,
            f = 0,
            s = 0
        };
        tail++;
        book[startx][starty] = 1;
        flag = 0;   //用来标记是否到达目的点，0表示暂时还没到达
                    //当队列不为空时循环
        while (head < tail)
        {
            //遍历四个方向
            for (int k = 0; k < next.Length; k++)
            {
                //计算下一个点的坐标
                tx = que[head].x + next[k][0];
                ty = que[head].y + next[k][1];
                //判断是否越界
                if (tx < 1 || tx > n || ty < 1 || ty > m)
                {
                    continue;
                }
                //判断该点是否为障碍物或者已经在路径中
                if (a[tx][ty] == 0 && book[tx][ty] == 0)
                {
                    //把这个点标记为已经走过
                    book[tx][ty] = 1;
                    //把新的点加到队列中
                    que[tail].x = tx;
                    que[tail].y = ty;
                    que[tail].f = head; //这一步是从head处扩展的
                    que[tail].s = que[head].s + 1;  //步数是上一步的步数+1
                    tail++;
                }
                //如果找到终点，停止扩展，任务结束，退出循环
                if (tx == p && ty == q)
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                break;
            }
            head++;//当一个点扩展结束后，head++才能对后面的点再进行扩展
        }
        //tail指向的是队列队尾的下一个位置，所以此处需要-1
        print("最短步数为：" + que[tail - 1].s);
        //Console.ReadKey();
    }
    static int n, m, p, q;
    static int[][] a = new int[6][]; //记录障碍物
    static int[][] book = new int[6][];//记录走过的路径
    static note[] que = new note[25];

    static int[][] next = new int[][]
    {
            new int[] { 0, 1 }, //向右
            new int[] { 1, 0 }, //向下
            new int[] { 0, -1 },//向左
            new int[] { -1, 0 } //向上
    };

    struct note
    {
        public int x;  //横坐标
        public int y;  //纵坐标
        public int f;  //上一步在队列中的编号
        public int s;  //步数
    }
}