using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLoading : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject Ready;
    public GameObject Fight;

    // Start is called before the first frame update
    void Start()
    {
        LoadingScreen.SetActive(true);
        Ready.SetActive(true);
        Fight.SetActive(false);
        StartCoroutine(FightDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FightDelay()
    {
        yield return new WaitForSeconds(3);
        Ready.SetActive(false);
        Fight.SetActive(true);
        yield return new WaitForSeconds(2);
        LoadingScreen.SetActive(false);
    }

}
