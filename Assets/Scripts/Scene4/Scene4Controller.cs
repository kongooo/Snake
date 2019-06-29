using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Scene4Controller : MonoBehaviour
{
    private static Scene4Controller _instance;
    public static Scene4Controller Instance { get { return _instance; } }
    public GameObject[] Blocks;
    public TextMeshProUGUI DistanceText, ScoreText;
    public Canvas EndCanvas, UICanvas;
    private float distance = 0;

    private void Awake()
    {
        _instance = this;
        if (LevelData.Instance != null && LevelData.Instance.StartScene)
        {
            InitStartData();
        }
        else
        {
            InitScene4Data();
        }
    }

    private void Start()
    {
        RandomFirstBlock();
        EndCanvas.planeDistance = 0;
    }

    private void Update()
    {
        UpdateDistance();
    }

    void InitStartData()
    {
        SnakeMove4.Instance.Xspeed = LevelData.Instance.ForthSpeed;
    }

    void InitScene4Data()
    {
        SnakeMove4.Instance.Xspeed = LevelData4.Instance.ForthSpeed;
    }

    public void GameOver()
    {
        ScoreText.text = DistanceText.text;
        UICanvas.gameObject.SetActive(false);
        EndCanvas.planeDistance = 1;
        GameObject.Find("Setting").SetActive(false);
    }

    public void GameAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateDistance()
    {
        distance +=  Time.deltaTime * SnakeMove4.Instance.Xspeed;
        DistanceText.text = ((int)distance).ToString();
    }

    void RandomFirstBlock()
    {
        int index = Random.Range(0, Blocks.Length);
        var leftDown = Camera.main.ViewportToWorldPoint(Vector3.zero);
        var rightUp = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        float cameraWidth = (rightUp.x - leftDown.x) / 2;
        float blockX = Blocks[index].GetComponent<BoxCollider2D>().size.x / 2;// - cameraWidth;
        Instantiate(Blocks[index], new Vector2(blockX, 0), Quaternion.identity);
    }
}
