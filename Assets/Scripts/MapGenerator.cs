using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
//using Unity.Mathematics;

public class MapGenerator : MonoBehaviour
{

    [Range(0,100)]
    public int boxSize = 5;
    
    [Range(0,100)]
    public int wallChance = 80;
    
    [Range(0,100)]
    public int doorChance = 20;
    
    public Tilemap floor;
    public Tilemap walls;
    public Tile floorTile;
    public Tile wallTile;

    public int numberRooms = 0;
    private List<box> boxes = new List<box>();
    //public Vector3Int tileMapSize;
    //private int width = tileMapSize.x;
   // private int height = tileMapSize.y;

    
    
    [SerializeField]
    public box test;
   // int mapHeight;
   //int mapWidth;

   private void Update()
   {
       int showroom = 0;
       if (Input.GetKeyDown("space"))
       {
           numberRooms = 0;
           showroom = 0;
           boxes = new List<box>();
           walls.ClearAllTiles();
           floor.ClearAllTiles();
           CreateRoom();
           for (int i = 0; i < numberRooms; i++)
           {
               DrawRoom(i);    
           }

           
           
           
       }

       if (Input.GetKeyDown(KeyCode.UpArrow))
       {
           DrawRoom(showroom);
           showroom++;
       }

   }

   public box CreateBox(int roomNumber, int X, int Y )
   {
       //Debug.Log("Start Create Box with param room: " +roomNumber +" at ("+ X+","+Y+")" );
       box boxNew= new box();
       boxes.Add(boxNew);
      // Debug.Log("New box added, number of boxes =:" + boxes.Count);
       boxNew.map = this;
       boxNew.X = X;
       boxNew.Y = Y;
       boxNew.size = boxSize;
       boxNew.roomNumber = roomNumber;
       //Debug.Log("New box has X:" + boxNew.X + " Y:" + boxNew.Y +  "roomNumber:" + boxNew.roomNumber);

       // Todo Fix This
       boxNew.walls = new char[4];
       for (int i = 0; i < 4; i++)
       {
           int adjX = X + (i % 2 == 1 ? i - 2 : 0);
           int adjY = Y + (i % 2 == 0 ? i - 1 : 0);
           if (RandomChance(wallChance))
           {
               boxNew.walls[i] = 'W';
              // Debug.Log("");
           }
           else
           {
               // check adjacent box is populated 

               if ( !AvailableRoom(adjX , adjY ) )
               {
                   // if populated, place wall 
                   boxNew.walls[i] = 'W';
               }
               else
               {
                   boxNew.walls[i] = 'N';
                  // Debug.Log("Spawn Create Box with param room: " +roomNumber +" at ("+adjX+","+adjY+")" );
                   box boxNext = CreateBox(roomNumber,adjX ,adjY );
                   boxNext.walls[(i + 2) % 4] = 'N';
                   //boxNext.draw(walls ,wallTile);
                  // Debug.Log("End Create BoxSpawned  at ("+ boxNext.X+","+ boxNext.Y+") walls = "+ boxNext.walls[0]+ boxNext.walls[1]+ boxNext.walls[2]+ boxNext.walls[3]);
               }
           }
       }
       // Debug.Log("End Create Box with param room: " +roomNumber +" at ("+ X+","+Y+") walls = "+ boxNew.walls[0]+ boxNew.walls[1]+ boxNew.walls[2]+ boxNew.walls[3]);
       return boxNew;
   }

   public bool AvailableRoom(int X, int Y)
   { 
       // todo add boundary check
     bool result =false;
     if (-10 < X && X < 150 && -10 < Y && Y < 150)
     {
         result = GetBox(X,Y) is null;
     }
     //result = GetBox(X,Y) == null;
     // Debug.Log("available room checking at ("+ X+","+Y+") result: " + result);
     return result;
   }
 
   public box GetBox (int X, int Y)
   {
       // todo add boundary check
       box result = boxes.FirstOrDefault(box => box.X == X && box.Y == Y ) ;

       return result;
   }
   
   public bool RandomChance(int chance, int max = 100,int X = 0, int Y = 0)
   {
       float rand = Random.value;
       bool result =  rand * max < chance;
      // Debug.Log(" RandomChance chance=" + chance + " max =" + max + "at ("+ X+","+Y+") result: " + result);
       return result;

   }


   void DrawRoom(int roomNumber )
   {
       // Get all boxes in room
       List<box> roomBoxes = boxes.FindAll(box => box.roomNumber == roomNumber);

       // For each box, call draw
       foreach (box roomBox in roomBoxes)
       {
           roomBox.draw(walls, wallTile, floor, floorTile);
       }
       
       
   }

  public void AddRoomDoor(int roomNumber)
   {
       List<box> roomBoxes = boxes.FindAll(box => box.roomNumber == roomNumber);
       int numBoxes = roomBoxes.Count;
       int roomDoorChance = (numBoxes > 5 ? doorChance / (numBoxes - 1) : doorChance);
       foreach (box roomBox in roomBoxes)
       {
           roomBox.addDoors(roomDoorChance);
       }
   }

  void CreateRoom()
   {
      // Debug.Log("CreateRoom Start");

      // Debug.Log("walls is: " + walls);
        
        
       Vector3Int location = Vector3Int.zero;
     //  Debug.Log("location is: " + location);
       
       box newBox = CreateBox(numberRooms, 0, 0);
       numberRooms++;
       AddRoomDoor(0);
      
       
       //newBox.draw(walls ,wallTile);
       
   }
}


public class box : MonoBehaviour
{
    
    public int roomNumber;
    public int X;
    public int Y;
    public int size;
    public char[] walls;
    public MapGenerator map;
    bool[] doors = new bool[4];

    public void draw(Tilemap tilelayer, Tile wallTile, Tilemap floorlayer, Tile floorTile)
    {
        for (int i = 0; i < 4; i++)
        {
            if (walls[i] != 'N')
            {
                Vector3Int lineStartLocation = new Vector3Int(X * size + (i == 3 ? size - 1 : 0),
                    Y * size + (i == 2 ? size - 1 : 0), 0);
                if (doors[i])
                {
                    createLine(tilelayer, lineStartLocation, wallTile, i % 2, size );
                    //lineStartLocation[i % 2] = lineStartLocation[i % 2] + 2;
                    //createLine(tilelayer, lineStartLocation, wallTile, i % 2, size/2 -1 );
                }
                else
                {
                    createLine(tilelayer, lineStartLocation, wallTile, i % 2, size);                   
                }

 
            }
        }
        // Draw floor
        for (int i = 0; i < size; i++)
        {
            Vector3Int lineStartLocation = new Vector3Int(X * size ,
                Y * size + i, 0);
            createLine(floorlayer, lineStartLocation, floorTile, 0, size);
        }
    }
       
    Vector3Int createLine(Tilemap tilelayer, Vector3Int startLocation, Tile placeTile, int orientation, int length, int step = 1)
    {
        //Debug.Log("Start createLine, orientation:" + orientation + " length :"+ length + " step:"+ step);
        Vector3Int tileLocation = new Vector3Int(startLocation.x,startLocation.y,startLocation.z );

        int tilePoint = startLocation[orientation];
        int endPoint = length + tilePoint;
        //Debug.Log("Pre loop tilePoint:" + tilePoint + " endPoint:"+endPoint);
        for ( ; tilePoint < endPoint; tilePoint+=step)
        {
            //Debug.Log("tilePoint:" + tilePoint + " endPoint:"+endPoint);
            tileLocation[orientation] = tilePoint;
            PlaceTile(tilelayer,tileLocation,placeTile);
       
        }

        return tileLocation;
    }
       
    void PlaceTile(Tilemap tilemap, Vector3Int tileLocation, Tile placeTile)
    {
        tilemap.SetTile(tileLocation, placeTile);
      //  Debug.Log("tile is: " + placeTile);
       // Debug.Log("the location is: " + tileLocation);

    }

    public bool addDoors(int doorChance)
    {
        // for each side of box
        for (int i = 0; i < 4; i++)
        {
            int adjX = X + (i % 2 == 1 ? i - 2 : 0);
            int adjY = Y + (i % 2 == 0 ? i - 1 : 0);
            
            if (walls[i] != 'N' && doors[i] != true)
            {
                doors[i] = false;
                //random door chance
                if(map.RandomChance(doorChance))
                {
                    // if AvailableRoom
                    if (map.AvailableRoom(adjX, adjY))
                    {
                        // place room
                        int newRoomNum = map.numberRooms;
                        box newBox = map.CreateBox(newRoomNum, adjX, adjY);
                        map.numberRooms++;
                        AddDoor(i, newBox);
                        map.AddRoomDoor(newRoomNum);
                        //newBox.addDoors(doorChance);
                        
                        //place doors
                        
                    }
                    else
                    {
                        box newBox = map.GetBox(adjX, adjY);
                        // else if
                        if (newBox is null)
                        {
                           // Debug.Log("Map boundary reached");
                        }
                        else
                        {
                            AddDoor(i, newBox);
                        }
                    }
                }
            }
        }
        return true;
    }

    void AddDoor(int sideNumber, box adjBox )
    {
        int adjSideNumber = (sideNumber + 2) % 4;
        doors[sideNumber] = true;
        adjBox.doors[adjSideNumber] = true;
        
    }    
    
}


