using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class VoiceClipManager : MonoBehaviour
{
    public VoiceClip[] vcs;

    private const string VoiceClipPATH = "VoiceClips";

    public class TupleList<T1, T2> : List<Tuple<T1,T2>> where T1 : IComparable
    {
        public void Add(T1 Item1, T2 Item2)
        {
            Add(new Tuple<T1, T2>(Item1, Item2));
        }
        public new void Sort()
        {
            Comparison<Tuple<T1, T2>> c = (a, b) => a.Item1.CompareTo(b.Item1);
            base.Sort(c);
        }
    }


    public class VoiceClip
    {
        public int voiceType;
        public int stage;
        public float philanthropy;
        public float knowledge;
        public AudioClip audioClip;

        public VoiceClip(int voiceType, int stage, float philanthropy, float knowledge, AudioClip audioClip)
        {
            this.voiceType = voiceType;
            this.stage = stage;
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

        TupleList<float,VoiceClip> bestNVCs = ChooseBestVoiceClips(trader);

        VoiceClip currentVC = bestNVCs[trader.stage].Item2;

        Debug.Log("Stimme: " + trader.voiceType + " Stage: " + trader.stage);
        Debug.Log("Interest T/V: " + trader.philantropy + "/" + currentVC.philanthropy + " Knowledge T/V: " + trader.knowledge + "/" + currentVC.knowledge);
        Debug.Log("Distanz: " + bestNVCs[trader.stage].Item1);

        source.clip = currentVC.audioClip;
        source.Play();
    }

    public TupleList<float, VoiceClip> ChooseBestVoiceClips(Trader trader)
    {
        AudioSource source = trader.audioSource;

        // converting schenanigans
        float phil = trader.philantropy * 2 - 2.0f; // 0.5,1.5 -> -1,1   
        float know = trader.knowledge * 2 - 0.2f; // 0.1,0.4 -> 0,1

        //float[] distances = new float[vcs.Length];
        //int[] indices = new int[vcs.Length];

        TupleList<float, VoiceClip> list = new TupleList<float, VoiceClip>();

        VoiceClip[] bestVCs = new VoiceClip[vcs.Length];

        //SortedList<float, VoiceClip> sorted = new SortedList<float, VoiceClip>();

        //List<VoiceClip> vcList = new List<VoiceClip>();

        for (int i = 0; i < vcs.Length; i++)
        {
            var distance = float.MaxValue;

            int currStage = 1;
            if (trader.stage >= 2)
            {
                currStage = 2;
            } 

            // filter by stage & voiceType
            if (vcs[i].stage == currStage && vcs[i].voiceType == trader.voiceType)
            {
                float phildiff = phil - vcs[i].philanthropy;
                float knowdiff = know - vcs[i].knowledge;

                distance = MathF.Sqrt(MathF.Pow(phildiff, 2) + MathF.Pow(knowdiff, 2));
            }
            list.Add(distance, vcs[i]);
        }

        list.Sort();

        return list;
    }

    // This method should get called EARLY to load-in all the sound-files (could take some time)
    public VoiceClip[] LoadVoiceClips()
    {
        AudioClip[] acs = Resources.LoadAll<AudioClip>(VoiceClipPATH);

        VoiceClip[] voiceClips = new VoiceClip[acs.Length];

        for (int i = 0; i < acs.Length; i++)
        {
            // acs.name Beispiel: "p1_s2_-0.3_0.9.wav"
            string temp = acs[i].name.Replace(".wav", "");

            string[] temp2 = temp.Split('_');

            Debug.Log("Loading.." + temp);

            // by god lets just hope this works

            int voiceType = Int32.Parse(temp2[0].Substring(1));

            int stage = Int32.Parse(temp2[1].Substring(1));

            float philantrophy = float.Parse(temp2[2], CultureInfo.InvariantCulture);
            float knowledge = float.Parse(temp2[3], CultureInfo.InvariantCulture);

            VoiceClip vc = new VoiceClip(voiceType, stage, philantrophy, knowledge, acs[i]);

            voiceClips[i] = vc;
        }

        return voiceClips;
    }
}
