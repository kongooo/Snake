using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeKind : MonoBehaviour
{
    // Start is called before the first frame update
    public Button mButton;
    public int SnakeKindOrder;
    public GameObject snakekind;
    //private SnakeKind snakekind;
    void Start()
    {
        Button btnMount = mButton.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TaskOnClick()
    {
        //Loading Scene1
        snakekind.GetComponent<DontDestroyTool>().orderSet(SnakeKindOrder);
        Debug.Log(1);
    }

}
