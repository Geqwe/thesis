using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   /* public GameObject inpmod;
    public GameObject point;

    private void Awake()
    {
        if(inpmod && point)
        {
            inpmod.SetActive(true);
            point.SetActive(true);
        }
        
    }*/
    public void ChooseLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
