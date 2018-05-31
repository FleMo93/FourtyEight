using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    private enum Resources { Iron, Stone, Coal, Crystal_Dida, Crystal_Gale }

    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;
    [SerializeField]
    private Resources resourceToTake = Resources.Coal;

    private so_DataSet.Attribute ironPerSecond;
    private so_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;


    private float timeLeft = 0;

    void Start ()
    {
        
        ironPerSecond = _Stats.Attributes.Find(x => x.Name == "Iron per second");
        health = _Stats.Attributes.Find(x => x.Name == "Health");
        healthMax = _Stats.Attributes.Find(x => x.Name == "Maximum Health");
        timeLeft = 1;
    }
	
	void Update ()
    {
        if(_StatsGlobal.Energy < 0 || health.Value <= 0)
        {
            return;
        }

        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0)
        {
            _StatsGlobal.Iron += (int)ironPerSecond.Value;
        }
    }
}
