using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasControl : MonoBehaviour
{
    public float finishTime = 400.0f;
    public int delay = 30;
    public CanvasGroup menu;
    private float t;
    // Start is called before the first frame update
    void Start()
    {
         t += Time.deltaTime / finishTime;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("malakaAlpha",delay);
    }
    IEnumerator malakaAlpha(){
        yield return new WaitForSeconds(delay);
        menu.alpha = Mathf.Lerp(0,1, Time.time*t);
    }
}
