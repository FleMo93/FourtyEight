using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Extractor : MonoBehaviour, I_IClickable, I_IDamagable
{
    public enum Resources { None, Iron, Stone, Coal, Crystal_Dida, Crystal_Gale }

    [SerializeField]
    private so_DataSet _Stats;
    [SerializeField]
    private so_DataSetGlobal _StatsGlobal;
    [SerializeField]
    public Resources ResourceToTake = Resources.None;

    private so_DataSet.Attribute ressourcePerSecond;
    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private so_DataSet.Attribute energyCost;
    private Animator animator;

    private float timeLeft = 0;

    void Awake ()
    {
        scr_Attributes.Attribute ressourcePerScondEnum = scr_Attributes.Attribute.Coal_per_second;

        switch(ResourceToTake)
        {
            case Resources.Coal:
                ressourcePerScondEnum = scr_Attributes.Attribute.Coal_per_second;
                break;
            case Resources.Crystal_Dida:
                ressourcePerScondEnum = scr_Attributes.Attribute.Dida_per_second;
                break;

            case Resources.Crystal_Gale:
                ressourcePerScondEnum = scr_Attributes.Attribute.Gale_per_second;
                break;

            case Resources.Iron:
                ressourcePerScondEnum = scr_Attributes.Attribute.Iron_per_second;
                break;

            case Resources.Stone:
                ressourcePerScondEnum = scr_Attributes.Attribute.Stone_per_second;
                break;

            case Resources.None:
                return;
        }

        ressourcePerSecond = _Stats.Attributes.Find(x => x.Name == ressourcePerScondEnum);
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);
        health = GetComponent<scr_DataSet>().Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        energyCost = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Cost_Energy);

        health.Value = healthMax.Value;
        timeLeft = 1;

        animator = GetComponent<Animator>();
        _StatsGlobal.Energy -= (int)energyCost.Value;
        
    }

    bool lastEnergyState = false;
	void Update ()
    {
        if(health.Value <= 0)
        {
            Destroy(this.gameObject);
        }

        if(ResourceToTake == Resources.None)
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
            switch(ResourceToTake)
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

    private void OnDestroy()
    {
        _StatsGlobal.Energy += (int)energyCost.Value;
    }
}
