using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_LevelManager : MonoBehaviour {

    // should be Mesh generation, but wont be!

    public static scr_LevelManager inst;

    public GameObject basicEmpty;
    public GameObject baseTile;

    public Texture2D Img_LevelInput;

    const int TERRAINHEIGHT = 32;
    const int TERRAINWITH = 64;

    public scr_Level currentLevel;
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
    
    void Awake(){

        if (inst != null)
        {
            Destroy(this);
            return;
        }
        inst = this;
        currentLevelParent = Instantiate(basicEmpty);
        currentLevel = currentLevelParent.AddComponent<scr_Level>();
        currentLevelParent.name = "Terrain";
        currentLevel.InitLevel(TERRAINWITH, TERRAINHEIGHT, baseTile, currentLevelParent, this, Img_LevelInput);
        currentLevel.ConstructBorder();
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
