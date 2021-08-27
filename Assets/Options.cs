using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public string Url;
    public GameObject Sound, Music, Learn, MainM, Option,gameMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainMenu()
    {
        MainM.SetActive(true);
        Option.SetActive(false);
        gameMenuPanel.SetActive(false);
    }
    public void LearnUrl()
    {
        Application.OpenURL(Url);
    }

}
