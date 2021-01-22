using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasControl : MonoBehaviour
{
    public float finishTime = 400.0f;
    public CanvasGroup menu;
    private float t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / finishTime;
        menu.alpha = Mathf.Lerp(0,1, Time.time*t);
    }
}
