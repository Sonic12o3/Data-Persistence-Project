using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public TMP_InputField input;

    //When the Object is generated, if another version exists, then this version destroys itself
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Current object is set to instance
            Instance = this;
            //Game wont destroy this object between scenes;
            DontDestroyOnLoad(gameObject);
        }


    }

    public void loadMain()
    {
        if (input.gameObject != null)
        {
            if (input.text == null)
            {
                NameManager.ChangeName("Name");
            }
            else
            {
                NameManager.ChangeName(input.text);
            }
        }
        SceneManager.LoadScene(1);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(0);
    }
}