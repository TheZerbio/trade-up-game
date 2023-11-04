using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetUserName : MonoBehaviour
{
    private string username;

    // Start is called before the first frame update
    void Start()
    {
        username = System.Environment.UserName;
        this.GetComponent<TMP_Text>().text = username;

        RandomOfferGenerator.GenerateItemValue(10, 5, 1, 0.2f);
    }
}
