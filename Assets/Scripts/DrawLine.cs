using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    private static DrawLine _instance;
    public static DrawLine Instance { get { return _instance; } }

    [HideInInspector] public List<Vector3> linePos = new List<Vector3>();
    [HideInInspector] public LineRenderer lineRenderer;

    private void Awake()
    {
        _instance = this;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.SetColors(Color.black, Color.black);
        lineRenderer.SetWidth(0.05f, 0.05f);
    }

    public void Draw()
    {
        Debug.Log("draw");
        linePos.RemoveAt(linePos.Count - 2);
        lineRenderer.positionCount = linePos.Count;
        lineRenderer.SetPositions(linePos.ToArray());
    }


    public void SetVector3Pos(List<Vector2> positions)
    {
        linePos.Clear();
        Debug.Log("clear");
        for (int i = 0; i < positions.Count; i++)
        {
            linePos.Add(new Vector3(positions[i].x, positions[i].y, 0));
        }
        linePos.Add(SnakeControl.Instance.snake[0].GetCurrentVector3Pos());
        linePos.ForEach(p => Debug.Log(p));
        lineRenderer.positionCount = linePos.Count;
        lineRenderer.SetPositions(linePos.ToArray());
        Debug.Log("end");
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
