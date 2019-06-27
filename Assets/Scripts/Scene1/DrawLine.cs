using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    private static DrawLine _instance;
    public static DrawLine Instance { get { return _instance; } }

    [HideInInspector] public List<Vector3> linePos = new List<Vector3>();
    [HideInInspector] public LineRenderer lineRenderer;

    private float timing = 0;
    public float interval;

    private void Awake()
    {
        _instance = this;
        lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.startColor = Color.blue;
        //lineRenderer.endColor = Color.red;
        lineRenderer.material.color = Color.white;
        //lineRenderer.SetColors(Color.red, Color.blue);
        lineRenderer.SetWidth(0.05f, 0.05f);
    }

    private void Update()
    {
        if(SnakeControl.Instance.autoFindFood)
        {
            timing += Time.deltaTime;
            if (timing > interval) 
            {
                timing = 0;
                changeColor();
            }
        }
    }

    void changeColor()
    {
        lineRenderer.startColor = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
        lineRenderer.endColor = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
    }

    public void Draw()
    {
        linePos.RemoveAt(linePos.Count - 2);
        lineRenderer.positionCount = linePos.Count;
        lineRenderer.SetPositions(linePos.ToArray());
    }


    public void SetVector3Pos(List<Vector2> positions)
    {
        linePos.Clear();
        for (int i = 0; i < positions.Count; i++)
        {
            linePos.Add(new Vector3(positions[i].x, positions[i].y, 0));
        }
        linePos.Add(SnakeControl.Instance.snake[0].GetCurrentVector3Pos());
        lineRenderer.positionCount = linePos.Count;
        lineRenderer.SetPositions(linePos.ToArray());
    }

    private Vector3[] ListToArray(List<Vector3> list)
    {
        Vector3[] array = new Vector3[list.Count];
        for (int i = 0; i < list.Count; i++) 
        {
            array[i] = list[i];
        }
        return array;
    }

}
