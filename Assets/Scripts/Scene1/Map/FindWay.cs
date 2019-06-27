using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWay : MonoBehaviour
{
    private static FindWay _instance;
    public static FindWay Instance { get { return _instance; } }

    private List<Grid> openList = new List<Grid>();
    private List<Grid> closeList = new List<Grid>();

    private void Awake()
    {
        _instance = this;
    }
 
    public List<Vector2> GetWay(Grid sGrid, Grid eGrid)
    {
        Grid grid = sGrid;
        List<Grid> neighborGrids = new List<Grid>();
        List<Vector2> wayPoints = new List<Vector2>();
        bool find = false;
        int cost = 0;
        grid.Scost = 0;
        grid.SetEcost(sGrid, eGrid);
        grid.parentGrid = grid;
        openList.Add(grid);
        while (openList.Count != 0)
        {
            grid = getMinCostGrid(openList);
            if (grid == eGrid)
            {
                find = true;
                break;
            }
            neighborGrids = MapManager.Instance.GetAroundGrids(grid);
            for (int i = 0; i < neighborGrids.Count; i++) 
            {
                if ((neighborGrids[i].GetUseFul() || neighborGrids[i].food) && !closeList.Contains(neighborGrids[i])) 
                {
                    cost = (neighborGrids[i].GetPos() - grid.GetPos()).magnitude == 1 ? 10 : 14;
                    if (openList.Contains(neighborGrids[i]))
                    {
                        if (grid.Scost + cost < neighborGrids[i].Scost)
                        {
                            neighborGrids[i].Scost = grid.Scost + cost;
                            neighborGrids[i].parentGrid = grid;
                        }
                    }
                    else
                    {
                        neighborGrids[i].Scost = grid.Scost + cost;
                        neighborGrids[i].SetEcost(neighborGrids[i], eGrid);
                        neighborGrids[i].parentGrid = grid;
                        openList.Add(neighborGrids[i]);
                    }
                }
            }
            neighborGrids.Clear();
            openList.Remove(grid);
            closeList.Add(grid);
        }

        openList.Clear();
        closeList.Clear();

        if (find) 
        while (grid!= sGrid) 
        {
            wayPoints.Add(grid.GetPos());
            grid = grid.parentGrid;
        }
        else
        {
            Debug.Log("can't find way");
        }
        return wayPoints;
    }


    Grid getMinCostGrid(List<Grid> grids)
    {
        Grid minGrid = grids[0];
        for (int i = 1; i < grids.Count; i++)
        {
            if (grids[i].GetCost() <= minGrid.GetCost())
                minGrid = grids[i];
        }
        return minGrid;
    }


}
