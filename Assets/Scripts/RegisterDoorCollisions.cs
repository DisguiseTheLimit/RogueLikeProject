using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterDoorCollisions : MonoBehaviour
{
    public int boxSize = 5;
    private void OnTriggerEnter2D(Collider2D col)
    {


        float doorAdjust = 3f;
        //Debug.Log("DoorTrigger occured");
        Debug.Log("DoorNum Search" + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        Debug.Log("DoorNum Pos" + col.gameObject.transform.position);
        float posX = col.gameObject.transform.position.x ;
        float posY = col.gameObject.transform.position.y ;
        int x = (int) Mathf.Floor(posX / boxSize);
        int y = (int) Mathf.Floor(posY / boxSize);
        float doorX = posX - x * boxSize;
        float doorY = posY - y * boxSize;
        int doorNum = -1;
        Debug.Log("X Grid: " + x + " doorX :" + doorX);
        Debug.Log("Y Grid: " + y + " doorY :" + doorY);

        float moveX = 0;
        float moveY = 0;

        
        // todo investigate 2nd trigger at (0,0)
        if (posX != 0 && posY != 0)
        {
            
            if (doorX < 2)
            {
                doorNum = 1;
                moveX -= doorAdjust;
            }
            else if (doorX >= 3)
            {
                doorNum = 3;
                moveX += doorAdjust;

            }
            else if (doorY < 2)
            {
                doorNum = 2;
                moveY -= doorAdjust;

            }
            else if (doorY >= 3)
            {
                doorNum = 0;
                moveY += doorAdjust;

            }
            else
            {
                Debug.Log(" unknown Door Number");
            }

            Debug.Log(Time.time+ " DoorNum is : " + doorNum); 
            
            // todo check logical globally
            int adjX = x + (doorNum % 2 == 1 ? doorNum - 2 : 0);
            int adjY = y - (doorNum % 2 == 0 ? doorNum - 1 : 0);

            Debug.Log("adjX : " + adjX + " adjY: " + adjY);
            
            GameObject map1 = GameObject.FindWithTag("MapGen");
 

            MapGenerator map2 = map1.GetComponent<MapGenerator>();
            
           Debug.Log("map: " + map2.numberRooms);

           Box adjBox = map2.GetBox(adjX, adjY);
           Debug.Log("adjBox.roomNumber: " + adjBox?.roomNumber);
           map2.DrawRoom(adjBox.roomNumber);
           
           
           // todo make room number reference starting room variable
           if(!adjBox.isPopulated && adjBox.roomNumber != 0)
           {
                map2.PopulateRoom(adjBox.roomNumber, map2.agents, map2.enemies);
                adjBox.isPopulated = true;
           }
 
           Debug.Log("moveX: " + moveX + " MoveY: " + moveY );
           
           col.gameObject.transform.Translate(moveX,moveY,0, Space.World);

        }

        


    }
 
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("DoorCollision occured");
    }*/

    private void Start()
    {
        Debug.Log("Loaded collision");
    }
}
