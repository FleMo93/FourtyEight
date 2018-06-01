using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Spawner", menuName = "ScriptabeObjects/DataSet")]
public class scr_WaveSpawner : ScriptableObject
{

}

[System.Serializable]
public class scr_Wave
{
    public scr_Group[] Groups;
}

[System.Serializable]
public class scr_Group
{
    public GameObject EnemyToSpawnPrefab;
    public int Count = 1;
}