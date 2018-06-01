using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_LevelManager_proc : MonoBehaviour {

    // should be Mesh generation, but wont be!

    public static scr_LevelManager_proc inst;

    public GameObject basicEmpty;
    public GameObject baseTile;
    public GameObject baseTile_ground;

    public Texture2D Img_LevelInput;

    const int TERRAINHEIGHT = 32;
    const int TERRAINWITH = 64;

    public scr_Level_proc currentLevel;
    GameObject currentLevelParent;

    public List<MeshFilter> MeshList_singles;
    public List<MeshFilter> MeshList_end;
    public List<MeshFilter> MeshList_bow;
    public List<MeshFilter> MeshList_innerT;
    public List<MeshFilter> MeshList_cross;

    public List<MeshFilter> MeshList_outerCorners;
    public List<MeshFilter> MeshList_outerBows;

    public List<MeshFilter> MeshList_innerCorners;

    public List<MeshFilter> MeshList_full;

    public List<Transform> TransList_Stone;
    public List<Transform> TransList_Stone_ground;
    public List<Transform> TransList_Coal;
    public List<Transform> TransList_Coal_ground;
    public List<Transform> TransList_Iron;
    public List<Transform> TransList_Iron_ground;
    public List<Transform> TransList_Dida; // lila
    public List<Transform> TransList_Dida_ground; // lila
    public List<Transform> TransList_Gale; // gelb
    public List<Transform> TransList_Gale_ground; // gelb



    void Awake(){

        if (inst != null)
        {
            Destroy(this);
            return;
        }
        inst = this;
        currentLevelParent = Instantiate(basicEmpty);
        currentLevel = currentLevelParent.AddComponent<scr_Level_proc>();
        currentLevelParent.name = "Terrain";
        currentLevel.InitLevel(TERRAINWITH, TERRAINHEIGHT, baseTile, currentLevelParent, this, Img_LevelInput);
        currentLevel.ConstructBorder();
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
