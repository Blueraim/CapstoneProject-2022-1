using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRank : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject RankPanel;

    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
        RankPanel.SetActive(true);
    }

    public void ShowHelp() // 
    {
        StartCoroutine(DelayTime(1));
        RankPanel.SetActive(true);
    }

    public void HelpExit() //
    {
        RankPanel.SetActive(false);

    }
}
