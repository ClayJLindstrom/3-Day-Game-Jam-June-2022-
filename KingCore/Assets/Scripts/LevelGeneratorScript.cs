using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour
{

    //these will be the blocks we spawn for the floors and walls.
    private Object wallTile, floorTile;
    //so we know the side length of each tile
    private float tileLength;
    // Start is called before the first frame update
    void Start()
    {
        wallTile = Resources.Load("WallTile");
        floorTile = Resources.Load("FloorTile");
        tileLength = 2.5f;

        //Instantiate(wallTile, Vector3.right, Quaternion.identity);
        //MakeWall(Vector3.left * tileLength * 4 - Vector3.left*tileLength/2, 8);
        MakeFloor(Vector3.down * tileLength * 4, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this will make a wall with the base block at "wallLoc" location and the wall "length" blocks high (Works)
    public void MakeWall(Vector3 wallLoc, int length){

        for(int i = 0; i <= length; i++){
            //place walls 1 above and 1 below the previous ones.
            Instantiate(wallTile, wallLoc + Vector3.up * tileLength * i, Quaternion.identity);
        }
    }

    //This is to make the floor at "floorLoc" location and "length" blocks long
    public void MakeFloor(Vector3 wallLoc, int length){
        //if there is an even number of blocks
        if(length % 2 == 0){
            for(int i = 0; i < (length)/2; i++){
                //place a floor 1 to the right and left of the floor.
                Instantiate(floorTile, wallLoc + Vector3.right * tileLength/2 + Vector3.right * tileLength * i, Quaternion.identity);
                Instantiate(floorTile, wallLoc + Vector3.left * tileLength/2 + Vector3.left * tileLength * i, Quaternion.identity);
            }

        }
        //otherwise, if there is an odd number of blocks
        else{
            //place the middle one first, and then
            Instantiate(floorTile, wallLoc, Quaternion.identity);
            for(int i = 1; i <= (length-1)/2; i++){
                //place a floor 1 to the right and left of the already placed blocks until we have the entire floor.
                Instantiate(floorTile, wallLoc + Vector3.right * tileLength * i, Quaternion.identity);
                Instantiate(floorTile, wallLoc + Vector3.left * tileLength * i, Quaternion.identity);
            }
        }
    }


    //this will make the floor, and three platforms for the player to jump across.
    public void MakeStartingFloor(){

    }
}
