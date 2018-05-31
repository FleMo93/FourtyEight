using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour, I_IClickable
{
    private enum Resources { None, Iron, Stone, Coal, Crystal_Dida, Crystal_Gale }

    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;
    [SerializeField]
    private Resources resourceToTake = Resources.None;

    private so_DataSet.Attribute ressourcePerSecond;
    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private Animator animator;

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

            case Resources.None:
                return;
        }

        ressourcePerSecond = _Stats.Attributes.Find(x => x.Name == ressource + " per second");
        health = GetComponent<scr_DataSet>().Attributes.Find(x => x.Name == "Health");
        healthMax = _Stats.Attributes.Find(x => x.Name == "Maximum Health");
        timeLeft = 1;

        animator = GetComponent<Animator>();

        
    }

    bool lastEnergyState = false;
	void Update ()
    {
        if(health.Value <= 0)
        {
            Destroy(this.gameObject);
        }

        if(resourceToTake == Resources.None)
        {
            return;
        }

        if(_StatsGlobal.Energy < 0 || lastEnergyState)
        {
            animator.Play("Idle");
        }

        if(_StatsGlobal.Energy > 0 || !lastEnergyState)
        {
            animator.Play("Drill");
        }

        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0)
        {
            switch(resourceToTake)
            {
                case Resources.Coal:
                    _StatsGlobal.Coal += (int)ressourcePerSecond.Value;
                    break;
                case Resources.Crystal_Dida:
                    _StatsGlobal.CrystalDida += (int)ressourcePerSecond.Value;
                    break;

                case Resources.Crystal_Gale:
                    _StatsGlobal.CrystalGale += (int)ressourcePerSecond.Value;
                    break;

                case Resources.Iron:
                    _StatsGlobal.Iron += (int)ressourcePerSecond.Value;
                    break;

                case Resources.Stone:
                    _StatsGlobal.Stone += (int)ressourcePerSecond.Value;
                    break;
            }

            timeLeft = 1;
        }
    }

    public so_DataSet GetSoDataSet()
    {
        return _Stats;
    }

    public scr_DataSet GetScrDataSet()
    {
        return GetComponent<scr_DataSet>();
    }
}
