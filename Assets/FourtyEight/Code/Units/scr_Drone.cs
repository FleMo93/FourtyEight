using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class scr_Drone : MonoBehaviour, I_IDamagable, I_IClickable
{
    [SerializeField]
    private so_DataSet _Stats;

    private scr_DataSet scrStats;
    private scr_DataSet.Attribute health;
    private so_DataSet.Attribute healthMax;
    private so_DataSet.Attribute visibilityRange;
    private so_DataSet.Attribute rotationSpeed;
    private so_DataSet.Attribute movementSpeed;
    private so_DataSet.Attribute fireRate;
    private so_DataSet.Attribute damage;
    private so_DataSet.Attribute attackRange;
    private List<Vector3> path;
    private GameObject actualTargetToAttack;
    private GameObject player;
    private Rigidbody myRigidbody;
    //private List<GameObject> damagables;

    private enum States { None, Attack, Move }
    private States state = States.Move;

    private void Start()
    {
        scrStats = GetComponent<scr_DataSet>();
        health = scrStats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Health);
        healthMax = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Maximum_Health);
        visibilityRange = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Visibility_Range);
        rotationSpeed = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Rotation_speed);
        movementSpeed = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Movement_Speed);
        fireRate = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Fire_rate);
        damage = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Damage);
        attackRange = _Stats.Attributes.Find(x => x.Name == scr_Attributes.Attribute.Range);

        health.Value = healthMax.Value;

        player = GameObject.FindGameObjectWithTag(scr_Tags.Player);
        path = new List<Vector3>();

        myRigidbody = GetComponent<Rigidbody>();

        if(player == null)
        {
            throw new Exception("No player found!");
        }
    }

    private void Update()
    {
        DrawPath();

        if(health.Value <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        CalculatePath();

        Ray ray = new Ray(this.transform.position, 
            (player.transform.position - this.transform.position).normalized);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, attackRange.Value);


        if (path.Count == 0 || 
           ( (Vector3.Distance(this.transform.position, player.transform.position) <= attackRange.Value) && hit.collider != null && hit.collider.gameObject == player))
        {
            state = States.Attack;
        }
        else
        {
            state = States.Move;
        }

        if(state == States.Move)
        {
            Walk();
        }
        else if(state == States.Attack)
        {
            myRigidbody.velocity = Vector3.zero;
            actualTargetToAttack = GetNearesDamagable();

            if(actualTargetToAttack != null)
            {
                RotateTowards(actualTargetToAttack.transform.position);
                Attack(actualTargetToAttack);
            }
        }
    }

    void Walk()
    {
        Vector3? nextPath = GetNexPath();

        if(!nextPath.HasValue)
        {
            return;
        }

        nextPath = new Vector3(nextPath.Value.x,
            this.transform.position.y,
            nextPath.Value.z);

        RotateTowards(nextPath.Value);

        Vector3 direction = (nextPath.Value - this.transform.position).normalized;
        direction.y = 0;

        myRigidbody.velocity = direction * movementSpeed.Value;
    }

    void RotateTowards(Vector3 v3)
    {
        Quaternion actualRot = this.transform.rotation.Copy();
        this.transform.LookAt(v3);
        this.transform.eulerAngles = new Vector3(
            0,
            this.transform.eulerAngles.y,
            0);

        Quaternion targetRot = this.transform.rotation.Copy();
        this.transform.rotation = Quaternion.RotateTowards(actualRot, targetRot, rotationSpeed.Value * Time.deltaTime);
    }

    Vector3? GetNexPath()
    {
        List<Vector3> v3ToDelete = new List<Vector3>();
        Vector3? v3ToReturn = null;

        foreach(Vector3 v3 in path)
        {
            Vector3 point = new Vector3(v3.x, this.transform.position.y, v3.z);

            if (Vector3.Distance(point, this.transform.position) < 0.2f)
            {
                v3ToDelete.Add(v3);
            }
            else
            {
                v3ToReturn = v3;
            }
        }

        foreach(Vector3 v3 in v3ToDelete)
        {
            path.Remove(v3);
        }

        return v3ToReturn;
    }

    void DrawPath()
    {
        for (int i = 0; i < path.Count - 1; i++)
            Debug.DrawLine(path[i], path[i + 1], Color.red);
    }

    float timeToPathCalculation = 0;
    void CalculatePath()
    {
        timeToPathCalculation -= Time.deltaTime;

        if (timeToPathCalculation <= 0)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 targetPos = player.transform.position;
            targetPos.y = 0;
            NavMesh.CalculatePath(this.transform.position, player.transform.position, NavMesh.AllAreas, path);
            this.path.Clear();

            for(int i = path.corners.Length - 1; i >= 0; i--)
            {
                this.path.Add(path.corners[i]);
            }

            timeToPathCalculation = 1;
        }
    }

    GameObject GetNearesDamagable()
    {
        float lastRange = float.MaxValue;
        GameObject lastGo = null;

        foreach(GameObject go in GetDamagables())
        {
            if(go == player)
            {
                return go;
            }

            float range = Vector3.Distance(this.transform.position, go.transform.position);

            if(range < lastRange)
            {
                lastRange = range;
                lastGo = go;
            }
        }

        return lastGo;
    }

    GameObject[] GetDamagables()
    {
        List<GameObject> list = new List<GameObject>();
        RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, attackRange.Value, Vector3.up);

        foreach(RaycastHit hit in hits)
        {
            I_IDamagable damagable = hit.collider.gameObject.GetComponent<I_IDamagable>();

            if(damagable != null)
            {
                list.Add(hit.collider.gameObject);
            }
        }

        return list.ToArray();
    }

    float timeToAttack = 0;
    void Attack(GameObject go)
    {
        timeToAttack -= Time.deltaTime;

        if(timeToAttack <= 0)
        {

            I_IDamagable dmgable = go.GetComponent<I_IDamagable>();

            so_DataSet.Attribute healthSo = dmgable.GetSoDataSet().Attributes.Where(x => x.Name == scr_Attributes.Attribute.Health).First();

            if(healthSo.TakeFromLocalDataSet)
            {
                scr_DataSet.Attribute healthScr = dmgable.GetScrDataSet().Attributes.Where(x => x.Name == scr_Attributes.Attribute.Health).First();
                healthScr.Value -= damage.Value;
            }
            else
            {
                healthSo.Value -= damage.Value;
            }
            

            timeToAttack = fireRate.Value;
        }
    }

    public so_DataSet GetSoDataSet()
    {
        return _Stats;
    }

    public scr_DataSet GetScrDataSet()
    {
        return scrStats;
    }
}
