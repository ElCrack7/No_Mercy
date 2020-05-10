using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Transform canvas;
    void Awake()
    {
        if (!instance) instance = this;
        canvas = GameObject.Find("Canvas").transform;
    }

    void Update()
    {

    }


}
