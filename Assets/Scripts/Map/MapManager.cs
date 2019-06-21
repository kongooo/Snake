using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public GameObject[] propPrefabs;
    public GameObject[] wallPrefabs;
    public GameObject wallParentPrefab, propParentPrefab;
    public int mapScale, badGrassCount, foodCount, sheildCount, boomCount, mushCount, energyCount, smartGrassCount;
    public int wallCountAverage;
    public Grid[,] Grids;
    private List<Wall>[] Walls;
    public GameObject temp, tempEnd, tempInit;
    [HideInInspector] public Vector2 endPos = Vector2.zero;
    [HideInInspector] public List<Vector2> points = new List<Vector2>();

    void Awake()
    {
        _instance = this;
        initWalls();
        initGrids();
        SetCenterGridsState(false);
        RandomWalls();
        RandomProps();
        SetCenterGridsState(true); ;
        
    }

    void Start()
    {
        RandomEndPosTest();
        SnakeControl.Instance.startMove = true;
    }

    void Update()
    {

    }


    public void RandomEndPosTest()
    {
        Vector2 endPos = new Vector2(Random.Range(-mapScale + 2, mapScale - 2), Random.Range(-mapScale + 2, mapScale - 2));
        while (!Grids[(int)endPos.x + 40, (int)endPos.y + 40].GetUseFul())
            endPos = new Vector2(Random.Range(-mapScale + 2, mapScale - 2), Random.Range(-mapScale + 2, mapScale - 2));
        GameObject.Instantiate(tempEnd, endPos, Quaternion.identity);
        GetWayPoints(Vector2.zero, endPos);
    }

    void GetWayPoints(Vector2 stt, Vector2 end)
    {
        points = FindWay.Instance.GetWay(Grids[(int)stt.x + 40, (int)stt.y + 40], Grids[(int)end.x + 40, (int)end.y + 40]);
        for (int i = points.Count - 1; i >= 0; i--)
        {
            GameObject.Instantiate(temp, points[i], Quaternion.identity);
        }
        shortenWay(1.2f);
        shortenWay(2.1f);
        shortenWay(3.1f);
        SnakeControl.Instance.posIndex = points.Count - 1;
        for (int i = points.Count - 1; i >= 0; i--)
        {
            GameObject.Instantiate(tempInit, points[i], Quaternion.identity);
        }
    }

    void shortenWay(float distance)
    {
        for (int i = 1; i < points.Count - 1; i++)
        {
            if ((points[i - 1] - points[i]).magnitude > distance && JudgeAroundGridsUseful(i))
            {
                points.Remove(points[i]);
            }
        }
    }

    bool JudgeAroundGridsUseful(int centerIndex)
    {
        int centerX = (int)points[centerIndex].x + 40, centerY = (int)points[centerIndex].y + 40;
        if (centerX > 0 && centerX < mapScale - 1 && centerY > 0 && centerY < mapScale - 1) 
        if (Grids[centerX + 1, centerY].GetUseFul() && Grids[centerX-1, centerY].GetUseFul() && Grids[centerX, centerY + 1].GetUseFul() && Grids[centerX, centerY - 1].GetUseFul())
            return true;
        return false;
    }

    void RandomWalls()
    {
        bool rotate;
        Vector2 pos;
        for (int i = 0; i < wallPrefabs.Length; i++) 
        {
            for (int j = 0; j < wallCountAverage; j++) 
            {
                rotate = Random.Range(0, 2) == 0 ? false : true;
            repeat: pos = new Vector2(Random.Range(-mapScale + 6, mapScale - 6 + 1), Random.Range(-mapScale + 6, mapScale - 6 + 1));
                if ((pos.x < 8 && pos.x > -8) && (pos.y < 8 && pos.y > -8)) goto repeat;
                Walls[i].Add(new Wall(rotate, pos, wallPrefabs[i], wallParentPrefab));
                Walls[i][j].ShowWall();
                SetWallGridState(Walls[i][j]);
            }            
        }
    }

    void SetWallGridState(Wall wall)
    {
        int gridX, gridY;
        if(wall.GetRotate())
        {
            gridX = (int)wall.GetPos().x + 40;
            gridY = (int)wall.GetPos().y + 40 - wall.GetSize() / 2;
            for (int i = 0; i < wall.GetSize(); i++) 
            {
                if (i == 0) 
                {
                    if (!Grids[gridX - 1, gridY - 1].GetUseFul())
                    {
                        Grids[gridX, gridY - 1].SetUseFul(false);
                        Grids[gridX - 1, gridY].SetUseFul(false);
                    }
                    if (!Grids[gridX + 1, gridY - 1].GetUseFul())
                    {
                        Grids[gridX, gridY - 1].SetUseFul(false);
                        Grids[gridX + 1, gridY].SetUseFul(false);
                    }
                }
                if (i == wall.GetSize() - 1)
                {
                    if (!Grids[gridX + 1, gridY + i + 1].GetUseFul())
                    {
                        Grids[gridX, gridY + i + 1].SetUseFul(false);
                        Grids[gridX + 1, gridY + i].SetUseFul(false);
                    }
                    if (!Grids[gridX - 1, gridY + i + 1].GetUseFul())
                    {
                        Grids[gridX, gridY + i + 1].SetUseFul(false);
                        Grids[gridX - 1, gridY + i].SetUseFul(false);
                    }
                }
                if (gridX >= 0 && gridX < mapScale * 2 && (gridY + i) >= 0 && (gridY + i) < mapScale * 2) 
                    Grids[gridX, gridY + i].SetUseFul(false);
            }
        }
        else
        {
            gridX = (int)wall.GetPos().x + 40 - wall.GetSize() / 2;
            gridY = (int)wall.GetPos().y + 40;
            for (int i = 0; i < wall.GetSize(); i++)
            {
                if (i == 0)
                {
                    if (!Grids[gridX - 1, gridY - 1].GetUseFul())
                    {
                        Grids[gridX, gridY - 1].SetUseFul(false);
                        Grids[gridX - 1, gridY].SetUseFul(false);
                    }
                    if (!Grids[gridX - 1, gridY + 1].GetUseFul())
                    {
                        Grids[gridX, gridY + 1].SetUseFul(false);
                        Grids[gridX - 1, gridY].SetUseFul(false);
                    }
                }
                if (i == wall.GetSize() - 1)
                {
                    if (!Grids[gridX + i + 1, gridY + 1].GetUseFul())
                    {
                        Grids[gridX + i, gridY + 1].SetUseFul(false);
                        Grids[gridX + i + 1, gridY].SetUseFul(false);
                    }
                    if (!Grids[gridX + i + 1, gridY - 1].GetUseFul())
                    {
                        Grids[gridX + i, gridY - 1].SetUseFul(false);
                        Grids[gridX + i + 1, gridY].SetUseFul(false);
                    }
                }
                if (gridX + i >= 0 && gridX + i < mapScale * 2 && gridY >= 0 && gridY < mapScale * 2)
                    Grids[gridX + i, gridY].SetUseFul(false);
            }
        }
    }

    void initWalls()
    {
        Walls = new List<Wall>[wallPrefabs.Length];
        for (int i = 0; i < wallPrefabs.Length; i++)
            Walls[i] = new List<Wall>();
    }

    void initGrids()
    {
        Grids = new Grid[mapScale * 2, mapScale * 2];
        for (int x = 0; x < mapScale * 2; x++)
            for (int y = 0; y < mapScale * 2; y++)
            {
                Grids[x, y] = new Grid(new Vector2(x - mapScale, y - mapScale));
            }
    }

    void RandomProps()
    {
        RandomProp(boomCount, propPrefabs[0]);
        RandomProp(energyCount, propPrefabs[1]);
        RandomProp(foodCount, propPrefabs[2]);
        RandomProp(mushCount, propPrefabs[3]);
        RandomProp(badGrassCount, propPrefabs[4]);
        RandomProp(sheildCount, propPrefabs[5]);
    }

    public void RandomProp(int count, GameObject prop)
    {
        Vector2 propPos;
        for (int i = 0; i < count; i++) 
        {
            propPos = new Vector2(Random.Range(-mapScale, mapScale), Random.Range(-mapScale, mapScale));
            while (!Grids[(int)propPos.x + 40,(int)propPos.y + 40].GetUseFul())
                propPos = new Vector2(Random.Range(-mapScale, mapScale), Random.Range(-mapScale, mapScale));
            Grids[(int)propPos.x + 40, (int)propPos.y + 40].SetUseFul(false);
            GameObject.Instantiate(prop, propPos, Quaternion.identity).transform.SetParent(propParentPrefab.transform);
        }
    }

    void SetCenterGridsState(bool useFul)
    {
        for (int i = 36; i < 44; i++)
            for (int j = 36; j < 44; j++) 
            {
                Grids[i, j].SetUseFul(useFul);
            }
    }

    public List<Grid> GetAroundGrids(Grid grid)
    {
        List<Grid> neighborGrid = new List<Grid>();
        int gridX = grid.indexX - 1, gridY = grid.indexY - 1;
        for (int x = gridX; x < gridX + 3; x++)
            for (int y = gridY; y < gridY + 3; y++)
            {
                if ((x >= 0 && x < (mapScale * 2 - 1) && y >= 0 && y < (mapScale * 2 - 1)) && (x != grid.indexX || y != grid.indexY))
                    neighborGrid.Add(Grids[x, y]);
            }

        return neighborGrid;
    }
}
