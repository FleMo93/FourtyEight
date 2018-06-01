using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI_Player : MonoBehaviour {

    public so_DataSetGlobal GlobalDataSet;
    public so_DataSet DataSet;
    public Slider Healthbar;
    public Text Healthtext;
    public Text GameTime;
    public Text NextSpawnTime;
    so_DataSet.Attribute AttributeHealth;
    scr_DataSet.Attribute LocalAttributeHealth;

    GameObject player;
    float pullTime = 1;
    float pullTimer = 1;

    scr_DataSet LocalDataSet;

	// Use this for initialization
	void Start () {
        AttributeHealth = DataSet.Attributes.Where(x => x.Name == scr_Attributes.Attribute.Health).First();
    }
	
	// Update is called once per frame
	void Update () {
        if (player == null)
            pullTimer -= Time.deltaTime;

        if (pullTimer <= 0 && player == null)
            if (GameObject.FindGameObjectsWithTag(scr_Tags.Player) != null)
                player = GameObject.FindGameObjectsWithTag(scr_Tags.Player)[0];
            else
                pullTimer = pullTime;

        if (player == null)
            return;

        Healthbar.maxValue = AttributeHealth.Value;

        if (AttributeHealth.TakeFromLocalDataSet && LocalAttributeHealth == null)
        {
            LocalAttributeHealth = player.GetComponent<scr_DataSet>().Attributes.Where(x => x.Name == scr_Attributes.Attribute.Health).First();
        }

        Healthbar.value = LocalAttributeHealth.Value;
        Healthtext.text = LocalAttributeHealth.Value.ToString();

        GameTime.text = ((int)GlobalDataSet.time_Game).ToString();
        NextSpawnTime.text = ((int)GlobalDataSet.time_forNextWave).ToString();
    }
}
