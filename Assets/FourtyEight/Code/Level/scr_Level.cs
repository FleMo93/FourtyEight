using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Level : MonoBehaviour
{

    int TerrainHeight = 32;
    int TerrainWidth = 64;

    int BorderHeight = 6;

    Color32 Color_Grass = new Color32(98, 176, 70, 255);
    Color32 Color_Dirt = new Color32(233, 191, 128, 255);
    Color32 Color_DarkGreen = new Color32(0, 117, 62, 255);
    Color32 Color_Black = new Color32(0, 0, 0, 255);

    Color32 Color_Ore_Stone = new Color32(73, 73, 73, 255);
    Color32 Color_Ore_Coal = new Color32(39, 39, 39, 255);
    Color32 Color_Ore_Iron = new Color32(86, 52, 0, 255);
    Color32 Color_Ore_Dida = new Color32(255, 0, 255, 255);
    Color32 Color_Ore_Gale = new Color32(255, 255, 0, 255);

    /// <summary>
    /// 0-10 RESERVED
    /// 0 = air
    /// 11 = Grass (walkable)
    /// 12 = Earth/Ground (the Dirt we walk upon) like grass
    /// 13 = Mountain (minable blocks)
    /// 14 = Ore_Stone
    /// 15 = Ore_Coal
    /// 16 = Ore_Iron
    /// 17 = Ore_Crystal_Dida
    /// 18 = Ore_Crystal_Gale
    /// </summary>
    byte[,] above; // layer with Mountain and Buildings

    /// <summary>
    /// 0-10 RESERVED
    /// 0 = air
    /// 11 = Grass (walkable)
    /// 12 = Earth/Ground (the Dirt we walk upon) like grass
    /// 13 = Mountain (minable blocks)
    /// 14 = Ore_Stone
    /// 15 = Ore_Coal
    /// 16 = Ore_Iron
    /// 17 = Ore_Crystal_Dida
    /// 18 = Ore_Crystal_Gale
    /// </summary>
    byte[,] ground;// (unminable Layer)

    // holds the bases for Mountaing
    Transform[,] above_trs; // layer with Mountain and Buildings
    Transform[,] ground_trs;// (unminable Layer) 
    MeshFilter[,] above_msh; // layer with Mountain and Buildings
    MeshFilter[,] ground_msh;// (unminable Layer)
    BoxCollider[,] above_col; // layer with Mountain and Buildings
    BoxCollider[,] ground_col;// (unminable Layer)
    Renderer[,] above_ren; // layer with Mountain and Buildings
    Renderer[,] ground_ren;// (unminable Layer)

    scr_LevelManager lvlManager;
    GameObject baseTile;

    internal void InitLevel(int withd, int height,GameObject pBaseTile, GameObject parentElement, scr_LevelManager lMng, Texture2D inputLayout = null)
    {
        TerrainHeight = height;
        TerrainWidth = withd;
        lvlManager = lMng;
        baseTile = pBaseTile;

        ground = new byte[TerrainWidth, TerrainHeight];
        above = new byte[TerrainWidth, TerrainHeight];
        ground_trs = new Transform[TerrainWidth, TerrainHeight];
        above_trs = new Transform[TerrainWidth, TerrainHeight];
        ground_msh = new MeshFilter[TerrainWidth, TerrainHeight];
        above_msh = new MeshFilter[TerrainWidth, TerrainHeight];
        ground_col = new BoxCollider[TerrainWidth, TerrainHeight];
        above_col = new BoxCollider[TerrainWidth, TerrainHeight];
        ground_ren = new Renderer[TerrainWidth, TerrainHeight];
        above_ren = new Renderer[TerrainWidth, TerrainHeight];

        for (int i = 0; i < TerrainHeight; i++)
        {
            for (int j = 0; j < TerrainWidth; j++)
            {
                ground[j, i] = 11;
                above[j, i] = 0;

                if (ground_trs[j, i] == null)
                {
                    ground_trs[j, i] = Instantiate(baseTile, new Vector3(j - (TerrainWidth / 2), -1, i - (TerrainHeight / 2)), Quaternion.identity).transform;
                    ground_msh[j, i] = ground_trs[j, i].GetComponent<MeshFilter>();
                    ground_col[j, i] = ground_trs[j, i].GetComponent<BoxCollider>();
                    ground_ren[j, i] = ground_trs[j, i].GetComponent<Renderer>();
                    ground_trs[j, i].parent = parentElement.transform;
                }
                if (above_trs[j, i] == null)
                {
                    above_trs[j, i] = Instantiate(baseTile, new Vector3(j - (TerrainWidth / 2), 0, i - (TerrainHeight / 2)), Quaternion.identity).transform;
                    above_msh[j, i] = above_trs[j, i].GetComponent<MeshFilter>();
                    above_col[j, i] = above_trs[j, i].GetComponent<BoxCollider>();
                    above_ren[j, i] = above_trs[j, i].GetComponent<Renderer>();
                    above_trs[j, i].parent = parentElement.transform;
                }
            }
        }


        if (inputLayout != null)
        {
            SetMapToTextureLayout(inputLayout);
        }
    }

    internal void ConstructBorder()
    {
        Collider[] myCols = GetComponents<Collider>();
        int count = myCols.Length;
        for (int i = 0; i < count; i++)
        {
            Destroy(myCols[i]);
        }
        BoxCollider Border_left = gameObject.AddComponent<BoxCollider>();
        BoxCollider Border_top = gameObject.AddComponent<BoxCollider>();
        BoxCollider Border_right = gameObject.AddComponent<BoxCollider>();
        BoxCollider Border_bottom = gameObject.AddComponent<BoxCollider>();

        Border_left.center = new Vector3(-(TerrainWidth / 2 + 1), 0, 0);
        Border_left.size = new Vector3(1, BorderHeight, TerrainHeight + 2);

        Border_top.center = new Vector3(0, 0, TerrainHeight / 2 + 1);
        Border_top.size = new Vector3(TerrainWidth + 2, BorderHeight, 1);

        Border_right.center = new Vector3(TerrainWidth / 2 + 1, 0, 0);
        Border_right.size = new Vector3(1, BorderHeight, TerrainHeight + 2);

        Border_bottom.center = new Vector3(0, 0, -(TerrainHeight / 2 + 1));
        Border_bottom.size = new Vector3(TerrainWidth + 2, BorderHeight, 1);

    }

    internal void SetMapToTextureLayout(Texture2D inputTex)
    {
        Color32[] imgCol = inputTex.GetPixels32();


        for (int i = 0; i < TerrainHeight; i++)
        {
            for (int j = 0; j < TerrainWidth; j++)
            {
                above_ren[j, i].material.color = imgCol[j + i * TerrainWidth];
                ground_ren[j, i].material.color = new Color32(139, 69, 19, 0);
                // if color is green or brown, make dirt or grass
                if (imgCol[j + i * TerrainWidth].isSameAs(Color_Grass) ||
                    imgCol[j + i * TerrainWidth].isSameAs(Color_Dirt))
                {
                    above[j, i] = 0;
                    ground[j, i] = 11;
                    if (imgCol[j + i * TerrainWidth].isSameAs(Color_Dirt))
                    {
                        ground[j, i] = 12;
                    }
                    ground_ren[j, i].material.color = Color_Grass;

                    Destroy(above_trs[j, i].gameObject);
                    above_col[j, i] = null;
                    above_ren[j, i] = null;
                    above_msh[j, i] = null;
                }
                else if (imgCol[j + i * TerrainWidth].isSameAs(Color_Ore_Stone))
                {
                    above[j, i] = 14;
                    ground[j, i] = 14;

                    Transform tempT = above_trs[j, i];
                    Destroy(tempT.gameObject);
                    above_trs[j, i] = Instantiate(lvlManager.TransList_Stone[0]);
                    above_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    above_msh[j, i] = null;// above_trs[j, i].GetComponent<MeshFilter>();
                    above_col[j, i] = above_trs[j, i].GetComponent<BoxCollider>();
                    above_ren[j, i] = null;//above_trs[j, i].GetComponent<Renderer>();

                    Transform tempT_g = ground_trs[j, i];
                    Destroy(tempT_g.gameObject);
                    ground_trs[j, i] = Instantiate(lvlManager.TransList_Stone_ground[0]);
                    ground_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    ground_msh[j, i] = null;//ground_trs[j, i].GetComponent<MeshFilter>();
                    ground_col[j, i] = ground_trs[j, i].GetComponent<BoxCollider>();
                    ground_ren[j, i] = null;//ground_trs[j, i].GetComponent<Renderer>();

                }
                else if (imgCol[j + i * TerrainWidth].isSameAs(Color_Ore_Coal))
                {
                    above[j, i] = 15;
                    ground[j, i] = 15;

                    Transform tempT = above_trs[j, i];
                    Destroy(tempT.gameObject);
                    above_trs[j, i] = Instantiate(lvlManager.TransList_Coal[0]);
                    above_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    above_msh[j, i] = null;// above_trs[j, i].GetComponent<MeshFilter>();
                    above_col[j, i] = above_trs[j, i].GetComponent<BoxCollider>();
                    above_ren[j, i] = null;//above_trs[j, i].GetComponent<Renderer>();

                    Transform tempT_g = ground_trs[j, i];
                    Destroy(tempT_g.gameObject);
                    ground_trs[j, i] = Instantiate(lvlManager.TransList_Coal_ground[0]);
                    ground_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    ground_msh[j, i] = null;//ground_trs[j, i].GetComponent<MeshFilter>();
                    ground_col[j, i] = ground_trs[j, i].GetComponent<BoxCollider>();
                    ground_ren[j, i] = null;//ground_trs[j, i].GetComponent<Renderer>();

                }
                else if (imgCol[j + i * TerrainWidth].isSameAs(Color_Ore_Iron))
                {
                    above[j, i] = 16;
                    ground[j, i] = 16;

                    Transform tempT = above_trs[j, i];
                    Destroy(tempT.gameObject);
                    above_trs[j, i] = Instantiate(lvlManager.TransList_Iron[0]);
                    above_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    above_msh[j, i] = null;// above_trs[j, i].GetComponent<MeshFilter>();
                    above_col[j, i] = above_trs[j, i].GetComponent<BoxCollider>();
                    above_ren[j, i] = null;//above_trs[j, i].GetComponent<Renderer>();

                    Transform tempT_g = ground_trs[j, i];
                    Destroy(tempT_g.gameObject);
                    ground_trs[j, i] = Instantiate(lvlManager.TransList_Iron_ground[0]);
                    ground_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    ground_msh[j, i] = null;//ground_trs[j, i].GetComponent<MeshFilter>();
                    ground_col[j, i] = ground_trs[j, i].GetComponent<BoxCollider>();
                    ground_ren[j, i] = null;//ground_trs[j, i].GetComponent<Renderer>();

                }
                else if (imgCol[j + i * TerrainWidth].isSameAs(Color_Ore_Dida))
                {
                    above[j, i] = 17;
                    ground[j, i] = 17;

                    Transform tempT = above_trs[j, i];
                    Destroy(tempT.gameObject);
                    above_trs[j, i] = Instantiate(lvlManager.TransList_Dida[0]);
                    above_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    above_msh[j, i] = null;// above_trs[j, i].GetComponent<MeshFilter>();
                    above_col[j, i] = above_trs[j, i].GetComponent<BoxCollider>();
                    above_ren[j, i] = null;//above_trs[j, i].GetComponent<Renderer>();

                    Transform tempT_g = ground_trs[j, i];
                    Destroy(tempT_g.gameObject);
                    ground_trs[j, i] = Instantiate(lvlManager.TransList_Dida_ground[0]);
                    ground_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    ground_msh[j, i] = null;//ground_trs[j, i].GetComponent<MeshFilter>();
                    ground_col[j, i] = ground_trs[j, i].GetComponent<BoxCollider>();
                    ground_ren[j, i] = null;//ground_trs[j, i].GetComponent<Renderer>();

                }
                else if (imgCol[j + i * TerrainWidth].isSameAs(Color_Ore_Gale))
                {
                    above[j, i] = 17;
                    ground[j, i] = 17;

                    Transform tempT = above_trs[j, i];
                    Destroy(tempT.gameObject);
                    above_trs[j, i] = Instantiate(lvlManager.TransList_Gale[0]);
                    above_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    above_msh[j, i] = null;// above_trs[j, i].GetComponent<MeshFilter>();
                    above_col[j, i] = above_trs[j, i].GetComponent<BoxCollider>();
                    above_ren[j, i] = null;//above_trs[j, i].GetComponent<Renderer>();

                    Transform tempT_g = ground_trs[j, i];
                    Destroy(tempT_g.gameObject);
                    ground_trs[j, i] = Instantiate(lvlManager.TransList_Gale_ground[0]);
                    ground_trs[j, i].transform.position = new Vector3((-TerrainWidth / 2) + j, 0, (-TerrainHeight / 2) + i);
                    ground_msh[j, i] = null;//ground_trs[j, i].GetComponent<MeshFilter>();
                    ground_col[j, i] = ground_trs[j, i].GetComponent<BoxCollider>();
                    ground_ren[j, i] = null;//ground_trs[j, i].GetComponent<Renderer>();

                }
            }
        }

    }

    void UpdateFullTerrain()
    {
        //for (int i = 0; i < TerrainHeight; i++)
        //{
        //    for (int j = 0; j < TerrainWidth; j++)
        //    {
        //        UpdateMeshOfTile(ground[j, i], false);
        //        UpdateMeshOfTile(above[j, i]);
        //    }
        //}
    }

    public void SetTileByte(byte Input, int x, int y, bool isAbove = true)
    {
        x = x + TerrainWidth / 2;
        y = y + TerrainHeight / 2;

        if (isAbove)
        {
            UpdateMesh(x, y);
            above[x, y] = Input;
        }
        else
        {
            UpdateMesh(x, y, false);
            ground[x, y] = Input;
        }
    }

    void UpdateMesh(int x, int y, bool isAbove = true)
    {
        byte[,] targetPlattfomr = isAbove ? above : ground;
        Transform[,] targetPlattfomr_trs = isAbove ? above_trs : ground_trs;

        #region conv NullCheckers

        Transform aboveTopLeft = null;
        Transform aboveTopMid = null;
        Transform aboveTopRight = null;
        Transform aboveLeft = null;
        Transform aboveMid = null; // potential target
        Transform aboveRight = null;
        Transform aboveBotLeft = null;
        Transform aboveBotMid = null;
        Transform aboveBotRight = null;

        MeshFilter aboveTopLeft_mesh = null;
        MeshFilter aboveTopMid_mesh = null;
        MeshFilter aboveTopRight_mesh = null;
        MeshFilter aboveLeft_mesh = null;
        MeshFilter aboveMid_mesh = null; // potential target
        MeshFilter aboveRight_mesh = null;
        MeshFilter aboveBotLeft_mesh = null;
        MeshFilter aboveBotMid_mesh = null;
        MeshFilter aboveBotRight_mesh = null;

        Transform groundTopLeft = null;
        Transform groundTopMid = null;
        Transform groundTopRight = null;
        Transform groundLeft = null;
        Transform groundMid = null;  // potential target
        Transform groundRight = null;
        Transform groundBotLeft = null;
        Transform groundBotMid = null;
        Transform groundBotRight = null;

        MeshFilter groundTopLeft_mesh = null;
        MeshFilter groundTopMid_mesh = null;
        MeshFilter groundTopRight_mesh = null;
        MeshFilter groundLeft_mesh = null;
        MeshFilter groundMid_mesh = null; // potential target
        MeshFilter groundRight_mesh = null;
        MeshFilter groundBotLeft_mesh = null;
        MeshFilter groundBotMid_mesh = null;
        MeshFilter groundBotRight_mesh = null;

        #endregion

        //if (isAbove) // if we click on the Above Layer
        //{
        //    aboveMid = above_trs[x, y];
        //    aboveLeft = above_trs[x - 1, y];
        //    aboveBotRight = above_trs[x + 1, y];

        //    aboveBotMid = above_trs[x - 1, y];
        //    aboveBotLeft = above_trs[x - 1, y - 1];
        //    aboveBotRight = above_trs[x - 1, y + 1];

        //    aboveTopMid = above_trs[x + 1, y];
        //    aboveTopLeft = above_trs[x + 1, y - 1];
        //    aboveTopRight = above_trs[x + 1, y + 1];

        //    aboveMid_mesh = above_msh[x, y];
        //    aboveLeft_mesh = above_msh[x - 1, y];
        //    aboveBotRight_mesh = above_msh[x + 1, y];

        //    aboveBotMid_mesh = above_msh[x - 1, y];
        //    aboveBotLeft_mesh = above_msh[x - 1, y - 1];
        //    aboveBotRight_mesh = above_msh[x - 1, y + 1];

        //    aboveTopMid_mesh = above_msh[x + 1, y];
        //    aboveTopLeft_mesh = above_msh[x + 1, y - 1];
        //    aboveTopRight_mesh = above_msh[x + 1, y + 1];
        //}

        //else
        //{
        //    Debug.LogError("CURRENTLY NOT SUPPOERTED");
        //}

            if (targetPlattfomr[x, y] == 0 ||
            targetPlattfomr[x, y] == 14 ||
    targetPlattfomr[x, y] == 15 ||
    targetPlattfomr[x, y] == 16 ||
    targetPlattfomr[x, y] == 17 ||
    targetPlattfomr[x, y] == 18)
            {

                Transform tempT = above_trs[x, y];
                Transform tempTParentEle = tempT.parent;
                Destroy(tempT.gameObject);
                
                above_trs[x, y] = Instantiate(baseTile, new Vector3(x - (TerrainWidth / 2), 0, y - (TerrainHeight / 2)), Quaternion.identity).transform;
                above_msh[x, y] = above_trs[x, y].GetComponent<MeshFilter>();
                above_msh[x, y].mesh = null;
                above_col[x, y] = above_trs[x, y].GetComponent<BoxCollider>();
                above_col[x, y].enabled = false;
                above_ren[x, y] = above_trs[x, y].GetComponent<Renderer>();
                above_trs[x, y].parent = tempTParentEle;

            }

            if (above_msh[x, y] != null)
            {
                if (above_msh[x, y].mesh != null)
                {
                    above_msh[x, y].mesh = null;
                }
            }
        if (above_col[x, y] != null)
        {
            above_col[x, y].enabled = false;
        }

        else
        {


            above_msh[x, y].mesh = lvlManager.MeshList_singles[0].mesh;
            above_col[x, y].enabled = true;
        }

        //if (targetPlattfomr[x,y] == 0)
        //{
        //    aboveMid_mesh.mesh = 
        //}


    }

    //void initWriteBitmapToLevel()
    //{

    //}
}
