using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class VoiceClipManager : MonoBehaviour
{
    public VoiceClip[] vcs;

    private const string VoiceClipPATH = "VoiceClips";

    public class VoiceClip
    {
        public int voiceType;
        public bool firstCall;
        public float philanthropy;
        public float knowledge;
        public AudioClip audioClip;

        public VoiceClip(int voiceType, bool firstCall, float philanthropy, float knowledge, AudioClip audioClip)
        {
            this.voiceType = voiceType;
            this.firstCall = firstCall;
            this.philanthropy = philanthropy;
            this.knowledge = knowledge;
            this.audioClip = audioClip;
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading VoiceClips");
        vcs = LoadVoiceClips();
        Debug.Log(vcs.Length + " VoiceClips loaded");
    }

    // philantropy = Random.Range(0.5f, 1.5f);         // philantropy = Random.Range(-1.0F, 1.0F);
    // knowledge = Random.Range(0.1f, 0.4f);           // knowledge = Random.Range(-1.0F, 1.0F);


    public void PlayVoiceLine(Trader trader)
    {
        AudioSource source = trader.audioSource;

        // converting schenanigans
        float phil = trader.philantropy*2 - 2.0f; // 0.5,1.5 -> -1,1   
        float know = trader.knowledge*2 - 0.2f; // 0.1,0.4 -> 0,1

        float[] distances = new float[vcs.Length];

        float min = float.MaxValue;

        int minIndex = 0;

        for (int i = 0; i < vcs.Length; i++)
        {
            // filter by firstCall & voiceType
            if (vcs[i].firstCall == trader.firstCall && vcs[i].voiceType == trader.voiceType)
            {
                float phildiff = phil - vcs[i].philanthropy;
                float knowdiff = know - vcs[i].knowledge;

                distances[i] = MathF.Sqrt(MathF.Pow(phildiff, 2) + MathF.Pow(knowdiff, 2));

                if (distances[i] < min)
                {
                    minIndex = i;
                    min = distances[i];
                }
            }
        }

        VoiceClip bestVoiceClip = vcs[minIndex];

        Debug.Log("Stimme: " + trader.voiceType + " Erster Satz: " + trader.firstCall);
        Debug.Log("Interest T/V: " + phil + "/" + bestVoiceClip.philanthropy + " Knowledge T/V: " + know + "/" + bestVoiceClip.knowledge);

        source.clip = bestVoiceClip.audioClip;
        source.Play();
    }

    // This method should get called EARLY to load-in all the sound-files (could take some time)
    VoiceClip[] LoadVoiceClips()
    {
        AudioClip[] acs = Resources.LoadAll<AudioClip>(VoiceClipPATH);

        VoiceClip[] voiceClips = new VoiceClip[acs.Length];

        for (int i = 0; i < acs.Length; i++)
        {
            // acs.name Beispiel: "p1_s2_-0.3_0.9.wav"
            string temp = acs[i].name.Replace(".wav", "");

            string[] temp2 = temp.Split('_');

            // by god lets just hope this works
            int voiceType = Int32.Parse(temp2[0].Substring(1));

            bool firstCall = (Int32.Parse(temp2[1].Substring(1)) == 1);

            float philantrophy = float.Parse(temp2[2], CultureInfo.InvariantCulture);
            float knowledge = float.Parse(temp2[3], CultureInfo.InvariantCulture);

            VoiceClip vc = new VoiceClip(voiceType, firstCall, philantrophy, knowledge, acs[i]);

            voiceClips[i] = vc;
        }

        return voiceClips;
    }
}
