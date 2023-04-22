using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

// A* 알고리즘을 이용한 길찾기
namespace pathFinder2D
{
    // 노드 클래스
    // 우선 순위 큐에 사용가능한 구조 
    class Node : IComparable<Node>
    {
        public bool closed;        // 닫힌 상태인지
        public int valueF;         // F값
        public int valueG;         // G값
        public int valueH;         // H값
        public Point pos;          // 자기의 위치
        public Node parent;    // 연결 관계에서 부모

        public void Init(Point pos) {
            this.closed = false;
            this.valueF = Int32.MaxValue;
            this.valueG = Int32.MaxValue;
            this.valueH = Int32.MaxValue;
            this.pos = pos;
            parent = null;
        }

        public int CompareTo(Node other) {
            return this.valueF - other.valueF;
        }

        public string GetPosAsStr() {
            string str = "";
            return str + this.pos.X + "," + this.pos.Y;
        }
    }

    class PathFinder
    {

        // 4각 타일 노드에서 이동할 위치와 거리값(대각선 포함)
        // 계산속도를 빠르게 하기위하여 int형으로 계산
        int[] dirY = new int[] { -1, 0, 1, 0, -1, 1, 1, -1 };
        int[] dirX = new int[] { 0, -1, 0, 1, -1, -1, 1, 1 };
        int[] cost = new int[] { 10, 10, 10, 10, 14, 14, 14, 14 };

        Point mapSize;      // 지도 크기
        Node[,] nodes;      // 닫힌 목록인지 판별값
        Point start;        // 시작 지점
        Point target;       // 목표 지점

        // A* 에서 Open된 노드리스트
        MinHeap<Node> openlist = new MinHeap<Node>();

        // 찾은 길에 대한 배열
        List<Node> pathList = new List<Node>();

        public void Init(Point mapSize, Point start, Point target, List<Point> listObstacle)
        {
            this.mapSize = mapSize;
            this.nodes = new Node[mapSize.Y, mapSize.X];

            // 노드 초기화
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    nodes[y, x] = new Node();
                    nodes[y, x].Init(new Point(x, y));
                }
            }

            // 장애물 추가
            foreach (Point pos in listObstacle) {
                nodes[pos.Y, pos.X].closed = true;
            }

            // 위치 설정
            this.start = start;
            this.target = target;

            // open 초기화
            openlist = new MinHeap<Node>();

            // 경로 초기화
            this.pathList.Clear();
        }

        // H값을 구하기 위한 함수 - 맨하탄 거리 함수
        int CalcManhatanDist(Node node) {
            return (Math.Abs(node.pos.X - this.target.X) + Math.Abs(node.pos.Y - this.target.Y));
        }

        // A* 알고리즘을 이용하여 길찾기
        public List<Node> Find() {

            // 시작점을 Open에 추가하고 길찾기 시작
            Node startNode = nodes[start.Y, start.X];
            startNode.valueG = 0;
            startNode.valueH = CalcManhatanDist(startNode);
            startNode.valueF = startNode.valueG + startNode.valueH;
            openlist.Add(startNode);

            // F값 계산 및 부모 노드 설정
            while (openlist.Count > 0) 
            {
                // F값 가장 작은 노드를 꺼냄
                Node openNode = this.openlist.ExtractDominating();

                // 방문한 노드는 패스
                if (openNode.closed) {
                    continue;
                }

                openNode.closed = true;

                // 목적지까지 왔다면 완료
                if (openNode.pos.X == target.X && openNode.pos.Y == target.Y) {
                    break;
                }

                // 8방향으로 진행하면서 거리값을 평가함
                for (int i = 0; i < dirX.Length; i++) 
                {
                    int x = openNode.pos.X + dirX[i];
                    int y = openNode.pos.Y + dirY[i];

                    // 맵의 유효범위 체크
                    if (x < 0 || x >= mapSize.X ||
                        y < 0 || y >= mapSize.Y)
                    {
                        continue;
                    }

                    Node curNode = nodes[y, x];

                    // 이동 가능성 체크
                    if (curNode.closed) {
                        continue;
                    }

                    // 현재 노드의 F, G, H 값을 계산
                    // 부모노드 설정
                    // OpenList에 추가
                    int g = openNode.valueG + this.cost[i];
                    int h = CalcManhatanDist(curNode);
                    if (curNode.valueF < (g + h))
                    {
                        continue;
                    }

                    curNode.valueG = g;
                    curNode.valueH = h;
                    curNode.valueF = g+h;
                    curNode.parent = openNode;

                    this.openlist.Add(curNode);
                }

            }   // end of while

            // 경로 설정            
            Node node = nodes[target.Y, target.X];
            while (true) {

                pathList.Add(node);

                Node parentNode = node.parent;

                if (parentNode.pos.X == start.X && parentNode.pos.Y == start.Y) {
                    break;
                }
                
                node = parentNode;
            }

            // 역방향으로 길을 찾았으므로 바낻로 
            pathList.Reverse();

            // test leon 목록 출력
            for(int i = 0; i < pathList.Count; i++) {                
                Trace.WriteLine( "path " + i + " : " + pathList[i].pos );
            }
            // end test

            return pathList;
        }

        public List<Node> GetPath() 
        {
            return pathList;    
        }
    }
}
