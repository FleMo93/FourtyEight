using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Level : MonoBehaviour
{
    // 0-10 RESERVED
    // 0 = air
    // 11 = Earth/Ground
    // 12 = Mountain

    int TerrainHeight = 32;
    int TerrainWidth = 64;

    Color32 groundGreen = new Color32(98, 176, 70, 255);
    Color32 groundGray = new Color32();
    Color32 groundDarkGreen = new Color32();
    Color32 groundBlack = new Color32();

    byte[,] above; // layer with Mountain and Buildings
    byte[,] ground;// (unminable Layer)
    Transform[,] above_trs; // layer with Mountain and Buildings
    Transform[,] ground_trs;// (unminable Layer) 
    MeshFilter[,] above_msh; // layer with Mountain and Buildings
    MeshFilter[,] ground_msh;// (unminable Layer)
    BoxCollider[,] above_col; // layer with Mountain and Buildings
    BoxCollider[,] ground_col;// (unminable Layer)
    Renderer[,] above_ren; // layer with Mountain and Buildings
    Renderer[,] ground_ren;// (unminable Layer)

    scr_LevelManager lvlManager;

    internal void InitLevel(int withd, int height, GameObject baseTile, GameObject parentElement, scr_LevelManager lMng, Texture2D inputLayout = null)
    {
        TerrainHeight = height;
        TerrainWidth = withd;
        lvlManager = lMng;

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
                    ground_trs[j, i] = Instantiate(baseTile, new Vector3(j, -1, i), Quaternion.identity).transform;
                    ground_msh[j, i] = ground_trs[j, i].GetComponent<MeshFilter>();
                    ground_col[j, i] = ground_trs[j, i].GetComponent<BoxCollider>();
                    ground_ren[j, i] = ground_trs[j, i].GetComponent<Renderer>();
                    ground_trs[j, i].parent = parentElement.transform;
                }
                if (above_trs[j, i] == null)
                {
                    above_trs[j, i] = Instantiate(baseTile, new Vector3(j, 0, i), Quaternion.identity).transform;
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

    internal void SetMapToTextureLayout(Texture2D inputTex)
    {
        Color32[] imgCol = inputTex.GetPixels32();


        for (int i = 0; i < TerrainHeight; i++)
        {
            for (int j = 0; j < TerrainWidth; j++)
            {
                above_ren[j,i].material.color = imgCol[j + i * TerrainWidth];
                ground_ren[j, i].material.color = new Color32(139, 69, 19,0);
                if (imgCol[j + i * TerrainWidth].isSameAs(groundGreen))
                {
                    ground_ren[j, i].material.color = groundGreen;
                    above_col[j, i].enabled = false;
                    above_msh[j, i].mesh = null;
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
        if (isAbove)
        {
            above[x, y] = Input;
            UpdateMesh(x, y);
        }
        else
        {
            ground[x, y] = Input;
            UpdateMesh(x, y, false);
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

        if (targetPlattfomr[x, y] == 0)
        {
            above_msh[x, y].mesh = null;
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
