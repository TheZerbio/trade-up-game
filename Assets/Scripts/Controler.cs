using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 20; i++)
        {
            Trader t = new Trader();
            t.print();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
