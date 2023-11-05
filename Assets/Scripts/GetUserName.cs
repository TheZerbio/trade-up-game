using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetUserName : MonoBehaviour
{
    private string username;

    // Start is called before the first frame update
    void Start()
    {
        username = System.Environment.UserName;
        this.GetComponent<Text>().text = username;
    }
}
