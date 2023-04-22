using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pathFinder2D
{
    public partial class Form1 : Form
    {
        // 맵의 크기
        private Point mapSize = new Point();

        // 장애물 위치 리스트
        private List<Point> listObstacle = new List<Point>();

        // 시작 위치
        private Point startPos = new Point(5, 5);

        // 목표 위치
        private Point targetPos = new Point(500, 500);

        // 길찾기 객체
        private PathFinder pathFinder = new PathFinder();

        // 맵 배경
        private Image imgMap;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnPathFind_Click(object sender, EventArgs e)
        {
            // 이미지로부터 맵을 읽어와서 장애물 설정
            List<Point> obstacles = new List<Point>();
            ExtractObstacleFromImage("map.png", obstacles);
            this.imgMap = Image.FromFile("map.png");

            this.listObstacle.Clear();
            foreach (Point pos in obstacles)
            {
                this.listObstacle.Add(pos);
            }

            this.pathFinder.Init(mapSize, startPos, targetPos, obstacles);
            this.pathFinder.Find();

            pnlMap.Refresh();

            this.imgMap.Dispose();

        }

        // 이미지를 분석하여 장애물 위치 반환하기
        // 이미지에서 흰색이 아닌부분은 모두 장애물
        private void ExtractObstacleFromImage(string pathImgMap, List<Point> obstacles) 
        {
            obstacles.Clear();
            Bitmap img = new Bitmap(pathImgMap);

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    // 흰색이 아닌곳은 무조건 장애물임 
                    Color pixel = img.GetPixel(i, j);
                    if ((pixel.R != 255) || (pixel.G != 255) || (pixel.B != 255))
                    {
                        obstacles.Add(new Point(i, j));
                    }
                }
            }

            this.mapSize = new Point(img.Width, img.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // 전체 상황을 그리기
        private void Draw(Graphics g, List<Node> path)
        {
            Pen penObs = new Pen(Color.Beige, 1);

            if (this.imgMap != null)
            {
                g.DrawImage(this.imgMap, 0, 0);
            }

            //foreach (Point pos in this.listObstacle)
            //{
            //    Rectangle rectObs = new Rectangle(pos.X, pos.Y, 1, 1);
            //    g.DrawRectangle(penObs, rectObs);
            //}

            Rectangle rect = new Rectangle(startPos.X-5, startPos.Y-5, 10, 10);
            g.FillEllipse(new SolidBrush(Color.Blue), rect);

            rect = new Rectangle(targetPos.X-5, targetPos.Y-5, 10, 10);
            g.FillEllipse(new SolidBrush(Color.Green), rect);

            if (null !=path && path.Count > 2) {

                Pen penPath = new Pen(Color.Blue, 1);

                for (int i = 0; i < path.Count-1; i++)
                {
                    g.DrawLine(penPath, path[i].pos, path[i+1].pos);
                }
            }
        }

        private void pnlMap_Paint(object sender, PaintEventArgs e)
        {
            List<Node> path = this.pathFinder.GetPath();

            Draw(e.Graphics, path);
        }
    }
}
