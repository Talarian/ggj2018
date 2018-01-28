using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject normal;
    public GameObject highlight;
    public bool flashOnStart = false;

    void Awake ()
    {
        if(flashOnStart)
        {
            DoFlashing();
        }
    }

    public virtual void DoFlashing()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        normal.SetActive(false);
        highlight.SetActive(true);
        yield return new WaitForSeconds(1f);
        normal.SetActive(true);
        highlight.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        normal.SetActive(false);
        highlight.SetActive(true);
        yield return new WaitForSeconds(1f);
        normal.SetActive(true);
        highlight.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        normal.SetActive(false);
        highlight.SetActive(true);
        yield return new WaitForSeconds(1f);
        normal.SetActive(true);
        highlight.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }


}
