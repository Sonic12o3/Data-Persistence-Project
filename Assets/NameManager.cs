using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour
{
    public static NameManager Instance;
    [SerializeField] public static string playerName = "Name";


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

    public static void ChangeName(string newName)
    {
        playerName = newName;
        Debug.Log(playerName);
    }

    public void PRINTSOMETHING()
    {
        Debug.Log("HEY I DID A THING");
    }

}
