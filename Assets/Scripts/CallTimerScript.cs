using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallTimerScript : MonoBehaviour
{
    public Text callTimerText;
    public Player player;

    private float counter = 0;
    private bool startCountdown = false;

    public void OnEnable()
    {
        if(!player.activeOffer && (player.currentCallIndex + 1) >= player.traderPrefabList.Count)
        {
            callTimerText.text = "";
            return;
        }
        counter = player.callDuration;
        startCountdown = true;
    }

    public void OnDisable()
    {
        callTimerText.text = "";
        counter = 0;
    }

    public void Update()
    {
        if (startCountdown)
        {
            counter -= Time.deltaTime;
            callTimerText.text = counter.ToString("F0");

            if (counter <= 0)
            {
                startCountdown = false;
                counter = player.callDuration;
            }
        }      
    }
}
