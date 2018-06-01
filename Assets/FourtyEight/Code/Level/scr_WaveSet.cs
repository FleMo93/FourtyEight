using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WaveSet", menuName = "ScriptabeObjects/New wave set")]
public class scr_WaveSet : ScriptableObject
{
    public scr_Wave[] Waves;


    [System.Serializable]
    public class scr_Wave
    {
        public float SpawnAfterSeconds;
        public scr_Group[] Groups;
    }

    [System.Serializable]
    public class scr_Group
    {
        public GameObject EnemyToSpawnPrefab;
        public int Count = 1;
    }
}
