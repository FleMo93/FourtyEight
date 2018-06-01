﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Wavespawner : MonoBehaviour {

    public scr_WaveSet WaveSet;
    public int nextWave = 0;



    public Vector2 TopLeft_BoxPoint_Position = new Vector2 ();
    public Vector2 BotRight_BoxPoint_Position = new Vector2 ();

    public Vector2 RandomIntegeredPosition;
    // Update is called once per frame
    void Update () {

        if (scr_LevelManager.GetLevelTime() > WaveSet.Waves[nextWave].SpawnAfterSeconds)
        {
            for (int i = 0; i < WaveSet.Waves[nextWave].Groups.Length; i++)
            {
                for (int j = 0; j < WaveSet.Waves[nextWave].Groups[i].Count; j++)
                {
                    RandomIntegeredPosition = new Vector2((int)Random.Range(TopLeft_BoxPoint_Position.x, BotRight_BoxPoint_Position.x),
                        (int)Random.Range( BotRight_BoxPoint_Position.y,TopLeft_BoxPoint_Position.y));
                    Instantiate(WaveSet.Waves[nextWave].Groups[i].EnemyToSpawnPrefab, new Vector3 (RandomIntegeredPosition.x, 0.814f, RandomIntegeredPosition.y), Quaternion.identity);
                }
            }


            nextWave++;
        }

        if (nextWave >= WaveSet.Waves.Length) enabled = false;
	}
}
