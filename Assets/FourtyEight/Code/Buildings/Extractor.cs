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

    private so_DataSet.Attribute ressourcePerSecond;
    private so_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;


    private float timeLeft = 0;

    void Start ()
    {
        string ressource = string.Empty;

        switch(resourceToTake)
        {
            case Resources.Coal:
                ressource = "Coal";
                break;
            case Resources.Crystal_Dida:
                ressource = "Dida";
                break;

            case Resources.Crystal_Gale:
                ressource = "Gale";
                break;

            case Resources.Iron:
                ressource = "Iron";
                break;

            case Resources.Stone:
                ressource = "Stone";
                break;
        }

        ressourcePerSecond = _Stats.Attributes.Find(x => x.Name == ressource + " per second");
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
            switch(resourceToTake)
            {
                case Resources.Coal:
                    _StatsGlobal.Iron += (int)ressourcePerSecond.Value;
                    break;
                case Resources.Crystal_Dida:
                    _StatsGlobal.CrystelDida += (int)ressourcePerSecond.Value;
                    break;

                case Resources.Crystal_Gale:
                    _StatsGlobal.CrystelGale += (int)ressourcePerSecond.Value;
                    break;

                case Resources.Iron:
                    _StatsGlobal.Iron += (int)ressourcePerSecond.Value;
                    break;

                case Resources.Stone:
                    _StatsGlobal.Stone += (int)ressourcePerSecond.Value;
                    break;
            }
        }
    }
}
