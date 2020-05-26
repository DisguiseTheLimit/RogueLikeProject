using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
//using Unity.Mathematics;

public enum WallType
{
    None,
    Wall,
    Door
}
public class MapGenerator : MonoBehaviour
{


    

    [Range(0,100)]
    public int boxSize = 5;
    
    [Range(0,100)]
    public int wallChance = 80;
    
    [Range(0,100)]
    public int doorChance = 20;

    public int minRooms = 10;
    public int maxRooms = 20;
    public int maxAttempts = 10000;
    public Tilemap floor;
    public Tilemap walls;
    public Tilemap doors;
    public Tilemap agents;
    public Tile floorTile;
    public Tile wallTile;
    public Tile doorTile;
    public Tile openDoorTile;
    public GameObject doorCollider;
    public GameObject[] enemies;
    public GameObject player;
    public int startingRoom = 0;
    
    public int numberRooms = 0;
    private List<Box> boxes = new List<Box>();
    //public Vector3Int tileMapSize;
    //private int width = tileMapSize.x;
   // private int height = tileMapSize.y;

    
    
    [SerializeField]
    public Box test;
   // int mapHeight;
   //int mapWidth;

   
   

   private void Start()
   {
       if(FilterMap())
       {
           SpawnPlayer(agents, player, startingRoom);
           //DrawMap();
           DrawRoom(startingRoom);
           Debug.Log("Map drawn");
           
       }
       else
       {
           Debug.Log("no valid map found");
       }

   }

   bool FilterMap()
   {
       CreateMap();
       bool maxAttemptsReached = false;
       int i = 0;
       
           while (i < maxAttempts && (numberRooms < minRooms || numberRooms > maxRooms))
           {
               ResetMap();
               CreateMap();
               Debug.Log("The current attempt: " + i + " number of rooms: "+ numberRooms);
               i++;
           }
           Debug.Log(" Attempt reached: " + i + " number of rooms: "+ numberRooms);
           return i < maxAttempts;
   }

   void CreateMap()
   {
       numberRooms = 0;
       boxes = new List<Box>();
       CreateRoom();
   }

   void ResetMap()
   {
       walls.ClearAllTiles();
       floor.ClearAllTiles();
   }

   void DrawMap()
   {
       //todo temp code to show entire map
       for (int i = 0; i < numberRooms; i++)
       {
           DrawRoom(i);
           if (i != startingRoom)
           {
               PopulateRoom(i, agents , enemies);
           }

       }
   }

   public Box CreateBox(int roomNumber, int X, int Y )
   {
       //Debug.Log("Start Create Box with param room: " +roomNumber +" at ("+ X+","+Y+")" );
       Box boxNew= new Box();
       boxes.Add(boxNew);
      // Debug.Log("New box added, number of boxes =:" + boxes.Count);
       boxNew.map = this;
       boxNew.X = X;
       boxNew.Y = Y;
       boxNew.size = boxSize;
       boxNew.roomNumber = roomNumber;
       //Debug.Log("New box has X:" + boxNew.X + " Y:" + boxNew.Y +  "roomNumber:" + boxNew.roomNumber);

       // Todo Fix This
       boxNew.walls = new WallType[4];
       for (int i = 0; i < 4; i++)
       {
           int adjX = X + (i % 2 == 1 ? i - 2 : 0);
           int adjY = Y + (i % 2 == 0 ? i - 1 : 0);
           if (RandomChance(wallChance))
           {    
               // Add wall to this side
               boxNew.walls[i] = WallType.Wall;
               // Debug.Log("");
           }
           else
           {
               // check adjacent box is populated 

               if ( !AvailableRoom(adjX , adjY ) )
               {
                   // if populated or fringe of map, place wall 
                   boxNew.walls[i] = WallType.Wall;
               }
               else
               {
                   // No wall this side
                   boxNew.walls[i] = WallType.None;
                  // Debug.Log("Spawn Create Box with param room: " +roomNumber +" at ("+adjX+","+adjY+")" );
                   // As there is no wall on this side, need to create an adjacent box
                   Box boxNext = CreateBox(roomNumber,adjX ,adjY );
                   //Make sure new box has no wall on adjacent side, adjacent wall is i + 2 cloockwise
                   boxNext.walls[(i + 2) % 4] = WallType.None;
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
 
   public Box GetBox (int X, int Y)
   {
       // todo add boundary check
       Box result = boxes.FirstOrDefault(box => box.X == X && box.Y == Y ) ;

       return result;
   }
   
   public bool RandomChance(int chance, int max = 100,int X = 0, int Y = 0)
   {
       float rand = Random.value;
       bool result =  rand * max < chance;
      // Debug.Log(" RandomChance chance=" + chance + " max =" + max + "at ("+ X+","+Y+") result: " + result);
       return result;

   }


  public void DrawRoom(int roomNumber )
   {
       // Get all boxes in room
       List<Box> roomBoxes = boxes.FindAll(box => box.roomNumber == roomNumber);

       // For each box, call draw
       foreach (Box roomBox in roomBoxes)
       {
           roomBox.Draw(walls, wallTile, floor, floorTile, doorTile,doors, openDoorTile);
       }
       
       
   }

  public void AddRoomDoor(int roomNumber)
   {
       // Find all the boxes within a room (roomNumber). 
       List<Box> roomBoxes = boxes.FindAll(box => box.roomNumber == roomNumber);
       int numBoxes = roomBoxes.Count;
       
       // Reduce the door chance for larger rooms
       int roomDoorChance = (numBoxes > 5 ? doorChance / (numBoxes - 1) : doorChance);
       // Add doors for each box in the room
       foreach (Box roomBox in roomBoxes)
       {
           roomBox.AddDoors(roomDoorChance);
       }
   }

  void CreateRoom()
   {
      // Debug.Log("CreateRoom Start");

      // Debug.Log("walls is: " + walls);
        
        
       Vector3Int location = Vector3Int.zero;
     //  Debug.Log("location is: " + location);
       
       Box newBox = CreateBox(numberRooms, 0, 0);
       numberRooms++;
       AddRoomDoor(0);
      
       
       //newBox.draw(walls ,wallTile);
       
   }

  public void PopulateRoom(int roomNumber, Tilemap tilelayer, GameObject[] Population)
  {
      //todo Making enemy spawn variability
      
      // Get all boxes in room
      List<Box> roomBoxes = boxes.FindAll(box => box.roomNumber == roomNumber);

      // For each box, spaw enemy
      foreach (Box roomBox in roomBoxes)
      {
            int chosenItem = Random.Range(0, Population.Length);
         
              GameObject item = Population[chosenItem];
              roomBox.SpawnItem(agents, item);
          

      }
      
  }

  void SpawnPlayer(Tilemap tilelayer, GameObject player, int startingRoom)
  {
      
      
      Box startingBox = boxes.FirstOrDefault(box => box.roomNumber == startingRoom);
      
      StageManager.Instance.Player = startingBox.SpawnItem(tilelayer, player).GetComponent<PlayerController>();

  }

  
}


public class Box
{

    public bool isPopulated = false;
    public int roomNumber;
    public int X;
    public int Y;
    public int size;
    public WallType[] walls;
    public MapGenerator map;
    bool[] doors = new bool[4];

    public void Draw(Tilemap tilelayer, Tile wallTile, Tilemap floorlayer, Tile floorTile, Tile doorTile, Tilemap doorLayer, Tile openDoorTile)
    {
        for (int i = 0; i < 4; i++)
        {
            if (walls[i] != WallType.None)
            {
                Vector3Int lineStartLocation = new Vector3Int(X * size + (i == 3 ? size - 1 : 0),
                    Y * size + (i == 2 ? size - 1 : 0), 0);
                CreateLine(tilelayer, lineStartLocation, wallTile, i % 2, size); 
                if (doors[i])
                {
                    CreateLine(tilelayer, lineStartLocation, openDoorTile, i % 2, size, 1, 2);
                    CreateLine(doorLayer, lineStartLocation, doorTile, i % 2, size, 1, 2);
                }


            }
        }
        // Draw floor
        for (int i = 0; i < size; i++)
        {
            Vector3Int lineStartLocation = new Vector3Int(X * size ,
                Y * size + i, 0);
            CreateLine(floorlayer, lineStartLocation, floorTile, 0, size);
        }
    }
       
    Vector3Int CreateLine(Tilemap tilelayer, Vector3Int startLocation, Tile placeTile, int orientation, int length, int step = 1, int skip = 0)
    {
        //Debug.Log("Start createLine, orientation:" + orientation + " length :"+ length + " step:"+ step);
        Vector3Int tileLocation = new Vector3Int(startLocation.x,startLocation.y,startLocation.z );

        int tilePoint = startLocation[orientation] + skip;
        int endPoint = length + startLocation[orientation] - skip;
        //Debug.Log("Pre loop tilePoint:" + tilePoint + " endPoint:"+endPoint);
        for ( ; tilePoint < endPoint; tilePoint+=step)
        {
            //Debug.Log("tilePoint:" + tilePoint + " endPoint:"+endPoint);
            tileLocation[orientation] = tilePoint;
            PlaceTile(tilelayer,tileLocation,placeTile);
            if (skip > 0)
            {
                //Instantiate(map.doorCollider, tileLocation, quaternion.identity);
            }
       
        }

        return tileLocation;
    }
       
    void PlaceTile(Tilemap tilemap, Vector3Int tileLocation, Tile placeTile)
    {
        tilemap.SetTile(tileLocation, placeTile);
      //  Debug.Log("tile is: " + placeTile);
       // Debug.Log("the location is: " + tileLocation);

    }

    public bool AddDoors(int doorChance)
    {
        // for each side of box
        for (int i = 0; i < 4; i++)
        {
            // Find the adjacent box to the door.
            
            
            // Door 1 = left, 3 = right
            int adjX = X + (i % 2 == 1 ? i - 2 : 0);
            // Door 0 = Top, 2 = Bottom
            int adjY = Y + (i % 2 == 0 ? i - 1 : 0);
            // If wall & no door
            if (walls[i] != WallType.None && doors[i] != true)
            {
                
                doors[i] = false;
                //random door chance
                if(map.RandomChance(doorChance))
                {
                    // if AvailableRoom
                    if (map.AvailableRoom(adjX, adjY))
                    {
                        // create new  room
                        
                        // get next available room number
                        int newRoomNum = map.numberRooms;
                        // Create room box(s)
                        Box newBox = map.CreateBox(newRoomNum, adjX, adjY);
                        map.numberRooms++;
                        // Adds door to wall at position i, for the boxes
                        AddDoor(i, newBox);
                        map.AddRoomDoor(newRoomNum);
                        //newBox.addDoors(doorChance)

                    }
                    else  // existing room
                    { 
                        // get adjacent box to door
                        Box newBox = map.GetBox(adjX, adjY);
                        
                        if (newBox is null)
                        {
                           Debug.Log("Map boundary reached");
                        }
                        else
                        {
                            // Check to see if we are not adding the door to the same  room
                            if (newBox.roomNumber != roomNumber)
                            {
                                // Add an adjacent doors
                                AddDoor(i, newBox);
                            }
                            
                        }
                    }
                }
            }
        }
        return true;
    }

    void AddDoor(int sideNumber, Box adjBox )
    {
        // create matching doors
        int adjSideNumber = (sideNumber + 2) % 4;
        doors[sideNumber] = true;
        adjBox.doors[adjSideNumber] = true;
        
    }

    public GameObject SpawnItem(Tilemap tileLayer,  GameObject item)
    {
       
        int posX = X * size + (int) Mathf.Ceil(size/2);
        int posY = Y * size + (int) Mathf.Ceil(size/2);
        Vector3Int spawnLocation = new Vector3Int (posX, posY, 0);

        return GameObject.Instantiate(item, spawnLocation, Quaternion.identity);
        
    }
    
}




