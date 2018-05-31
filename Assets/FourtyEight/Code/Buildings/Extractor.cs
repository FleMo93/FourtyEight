using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;

    private so_DataSet.Attribute IronToGain;
    private so_DataSet.Attribute TickUntilGain;


    private float timeLeft = 0;

    void Start ()
    {
        IronToGain = _Stats.Attributes.Find(x => x.Name == "Iron to gain");
        TickUntilGain = _Stats.Attributes.Find(x => x.Name == "Tick until gain");

        timeLeft = TickUntilGain.Value;
    }
	
	void Update ()
    {
        if(_StatsGlobal.Energy < 0)
        {
            return;
        }

        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0)
        {
            _StatsGlobal.Iron += (int)IronToGain.Value;
        }
    }
}
