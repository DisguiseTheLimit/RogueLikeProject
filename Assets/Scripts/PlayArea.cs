using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
public class PlayArea : MonoBehaviour
{
    
    private const string LeftBottomCorner = "Roguelike_Tileset_concept_8";
    
    private void Start()
    {
        Debug.Log("Start" );

       // var wallMap = GetComponentsInChildren<Tilemap>()[0];
        
        var wallMap = GetComponentsInChildren<Tilemap>();
        Debug.Log("wallMap is: " + wallMap);

        
        var LeftBottomCornerTile = TileByName(LeftBottomCorner);
        
        var location = Vector3Int.zero;
        Debug.Log("location is: " + location);

        Debug.Log("The Length is: " + wallMap.Length);
        wallMap[0].SetTile(location, LeftBottomCornerTile);
        
        
        
        //StartUpTiles();

    }
    
    
    private static Tile  TileByName (string name)
    {
        
        Debug.Log("TileByName" + name );

        var myTile = (Tile) Resources.Load( name, typeof(Tile) );

        Debug.Log(myTile);

        return myTile;
    }
    
    
    
    
    /*
    private void StartUpTiles()
    {
        var basicLevel = GetComponentsInChildren<Tilemap>()[0];
        
        var localTilePositions = new List<Vector3Int>();

        foreach (var position in basicLevel.cellBounds.allPositionsWithin)
        {
            Vector3Int localPosition = new Vector3Int(position.x, position.y,position.z);
            localTilePositions.Add(localPosition);
        }
    
        
    }
    */


    /*
     *
     * private void CreateRoom(  List<Vector3Int> positions,  int height, int width, string direction)
     
     {
         switch (direction)
         {
             case "Bottom":
                 for (int i = 0; i <= width - 1; i++)
                 {
                     if (i == 0)
                     {
                         var LeftBottomCornerTile = TilesResourceLoader.GetLeftBottomCornerTile();
                     }
                     else if (0 < i && i < width - 1)
                     {
                         var HorizontalWallTile = TilesResourceLoader.GetHorizontalWallTile();
                     }
                     else if (i == width - 1)
                     {
                         var RightBottomCornerTile = TilesResourceLoader.GetRightBottomCornerTile();
                     }
                     else
                     {
                         Debug.Log("Had problem with case: Bottom");
                     }
                 }
                 
             
         }
         
     }
     */
}
