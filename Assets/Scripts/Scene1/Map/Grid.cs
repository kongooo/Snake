using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private Vector2 pos;
    private bool useFul;
    public bool food = false;
    public int Scost, Ecost, indexX, indexY;
    public Grid parentGrid;

    public Grid(Vector2 pos)
    {
        this.useFul = true;
        this.pos = pos;
        indexX = (int)pos.x + 40;
        indexY = (int)pos.y + 40;
    }

    public Vector2 GetPos()
    {
        return this.pos;
    }

    public void SetPos(Vector2 pos)
    {
        this.pos = pos;
    }

    public bool GetUseFul()
    {
        return this.useFul;
    }

    public void SetUseFul(bool useFul)
    {
        this.useFul = useFul;
    }

    public int GetCost()
    {
        return Scost + Ecost;
    }

    public void SetEcost(Grid sGrid, Grid eGrid)
    {
        Ecost = (int)((sGrid.pos-eGrid.pos).magnitude) * 14;
    }

}
