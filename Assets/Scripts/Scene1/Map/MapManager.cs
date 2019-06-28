using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public GameObject[] propPrefabs;
    public GameObject[] wallPrefabs;
    public List<GameObject> foods = new List<GameObject>();
    public GameObject wallParentPrefab, propParentPrefab;
    public int mapScale, badGrassCount, foodCount, sheildCount, boomCount, mushCount, energyCount, smartGrassCount;
    public int wallCountAverage;
    public Grid[,] Grids;
    private List<Wall>[] Walls;
    [HideInInspector] public Vector2 endPos = Vector2.zero;
    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    public GameObject Temp;
    private List<GameObject> RoadTemps = new List<GameObject>();

    void Awake()
    {
        _instance = this;
        if (LevelData.Instance != null && LevelData.Instance.StartScene)
            InitStartData();
        else
            InitScene1Data();
        initWalls();
        initGrids();
        SetCenterGridsState(false);
        RandomWalls();
        RandomProps();
        SetCenterGridsState(true);         
    }

    void Start()
    {
        SnakeControl.Instance.startMove = true;
    }

    void InitStartData()
    {
        badGrassCount = LevelData.Instance.BadGrass;
        foodCount = LevelData.Instance.Food;
        sheildCount = LevelData.Instance.Sheild;
        boomCount = LevelData.Instance.Boom;
        mushCount = LevelData.Instance.Mush;
        energyCount = LevelData.Instance.Energy;
        smartGrassCount = LevelData.Instance.SmartGrass;
        wallCountAverage = LevelData.Instance.Wall;
    }

    void InitScene1Data()
    {
        badGrassCount = LevelData1.Instance.BadGrass;
        foodCount = LevelData1.Instance.Food;
        sheildCount = LevelData1.Instance.Sheild;
        boomCount = LevelData1.Instance.Boom;
        mushCount = LevelData1.Instance.Mush;
        energyCount = LevelData1.Instance.Energy;
        smartGrassCount = LevelData1.Instance.SmartGrass;
        wallCountAverage = LevelData1.Instance.Wall;
    }

    public void updatePoints()
    {
        GameObject nearestFood = foods[0];
        for (int i = 1; i < foods.Count; i++)
        {
            if (GetDisFromSnake(foods[i]) < GetDisFromSnake(nearestFood))
                nearestFood = foods[i];
        }
        points.Clear();
        SetWayPoints(SnakeControl.Instance.snake[0].GetCurrentPos(), new Vector2(nearestFood.transform.position.x, nearestFood.transform.position.y));
        SnakeControl.Instance.posIndex = points.Count - 1;
        SnakeControl.Instance.movePos = points[points.Count - 1];
        DrawLine.Instance.SetVector3Pos(points);
    }

    float GetDisFromSnake(GameObject prop)
    {
        Vector2 twoPos = new Vector2(prop.transform.position.x, prop.transform.position.y);
        return (SnakeControl.Instance.snake[0].GetCurrentPos() - twoPos).magnitude;
    }

    void SetWayPoints(Vector2 stt, Vector2 end)
    {
        points = FindWay.Instance.GetWay(Grids[(int)stt.x + 40, (int)stt.y + 40], Grids[(int)end.x + 40, (int)end.y + 40]);
        //testRoad();
        shortenWay(1.2f);
        shortenWay(2.1f);
        shortenWay(3.1f);

    }

    void testRoad()
    {
        for (int i = 0; i < RoadTemps.Count; i++)
            Destroy(RoadTemps[i]);
        RoadTemps.Clear();
        for (int i = 0; i < points.Count; i++)
        {
            GameObject temp = Instantiate(Temp, points[i], Quaternion.identity);
            RoadTemps.Add(temp);
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
        int LeftX = (int)points[centerIndex].x + 40 - 1, DownY = (int)points[centerIndex].y + 40 - 1;
        if (LeftX >= 0 && LeftX < 2 * mapScale - 3 && DownY >= 0 && DownY < 2 * mapScale - 3)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!Grids[LeftX + i, DownY + j].GetUseFul())
                        return false;
                }
            }
        }
        return true;
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
        RandomProp(smartGrassCount, propPrefabs[6]);
    }

    public void RandomProp(int count, GameObject prop)
    {
        Vector2 propPos;
        for (int i = 0; i < count; i++) 
        {
            propPos = new Vector2(Random.Range(-mapScale + 2, mapScale - 2), Random.Range(-mapScale + 2, mapScale - 2));
            while (!Grids[(int)propPos.x + 40,(int)propPos.y + 40].GetUseFul())
                propPos = new Vector2(Random.Range(-mapScale + 2, mapScale - 2), Random.Range(-mapScale, mapScale));
            Grids[(int)propPos.x + 40, (int)propPos.y + 40].SetUseFul(false);
            GameObject newProp = GameObject.Instantiate(prop, propPos, Quaternion.identity);
            newProp.transform.SetParent(propParentPrefab.transform, false);
            if (prop == propPrefabs[2])
            {
                foods.Add(newProp);
                Grids[(int)propPos.x + 40, (int)propPos.y + 40].food = true;
            }
                
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
                if ((x > 0 && x < (mapScale * 2 - 2) && y > 0 && y < (mapScale * 2 - 2)) && (x != grid.indexX || y != grid.indexY))
                {
                    if (x == grid.indexX || y == grid.indexY)
                        neighborGrid.Add(Grids[x, y]);
                    else
                    {
                        if (x == grid.indexX - 1 && y == grid.indexY - 1)
                        {
                            if (Grids[x, y + 1].GetUseFul() && Grids[x + 1, y].GetUseFul())
                                neighborGrid.Add(Grids[x, y]);
                        }
                        if (x == grid.indexX + 1 && y == grid.indexY - 1)
                        {
                            if (Grids[x - 1, y].GetUseFul() && Grids[x, y + 1].GetUseFul())
                                neighborGrid.Add(Grids[x, y]);
                        }
                        if (x == grid.indexX - 1 && y == grid.indexY + 1)
                        {
                            if (Grids[x, y - 1].GetUseFul() && Grids[x + 1, y].GetUseFul())
                                neighborGrid.Add(Grids[x, y]);
                        }
                        if (x == grid.indexX + 1 && y == grid.indexY + 1)
                        {
                            if (Grids[x - 1, y].GetUseFul() && Grids[x, y - 1].GetUseFul())
                                neighborGrid.Add(Grids[x, y]);
                        }

                    }
                }                    
            }

        return neighborGrid;
    }
}
