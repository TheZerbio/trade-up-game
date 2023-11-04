using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoiceClipManager : MonoBehaviour
{
    //public AudioSource audioSource;
    //public AudioClip[] callClips;
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
        vcs = LoadVoiceClips();
    }



    public void PlayVoiceLine(Trader trader)
    {
        AudioSource source = trader.audioSource;

        float phil = trader.philantropy; // + standard deviation
        float know = trader.knowledge;

        float[] distances = { };

        float min = float.MaxValue;

        int minIndex = 0;

        for (int i = 0; i < vcs.Length; i++)
        {
            // filter by firstCall & voiceType
            if (vcs[i].firstCall != trader.firstCall) break;
            if (vcs[i].voiceType != trader.voiceType) break;

            float phildiff = phil - vcs[i].philanthropy;
            float knowdiff = know - vcs[i].knowledge;

            distances[i] = MathF.Sqrt(MathF.Pow(phildiff, 2) + MathF.Pow(knowdiff, 2));

            if(distances[i] < min)
            {
                minIndex = i;
                min = distances[i];
            }
        }

        source.clip = vcs[minIndex].audioClip;
        source.Play();
    }

    // This method should get called EARLY to load-in all the sound-files (could take some time)
    VoiceClip[] LoadVoiceClips()
    {
        AudioClip[] acs = Resources.LoadAll<AudioClip>(VoiceClipPATH);

        VoiceClip[] voiceClips = { };

        for (int i = 0; i < acs.Length; i++)
        {
            // acs.name Beispiel: "p1_s2_-0.3_0.9.wav"
            string temp = acs[i].name.Replace(".wav", "");

            string[] temp2 = temp.Split('_');

            // by god lets just hope this works
            int voiceType = Int32.Parse(temp2[0].Substring(1));

            bool firstCall = (Int32.Parse(temp2[1].Substring(1)) == 1);

            float philantrophy = float.Parse(temp2[2]);

            float knowledge = float.Parse(temp2[3]);

            VoiceClip vc = new VoiceClip(voiceType, firstCall, philantrophy, knowledge, acs[i]);

            voiceClips[i] = vc;
        }

        return voiceClips;
    }
}
