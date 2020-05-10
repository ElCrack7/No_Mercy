using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTarget : MonoBehaviour
{
    public Transform target;

    public float followTime;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iTween.MoveUpdate(this.gameObject, iTween.Hash("position", target.position + offset, "time", followTime,  "easytype", iTween.EaseType.easeInOutSine));
    }
}
