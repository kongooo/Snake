using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change : MonoBehaviour
{
    public int type;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetSkin);
    }

    public void SetSkin()
    {
        DontDestroyTool.Instance.orderSet(type);
    }
}
