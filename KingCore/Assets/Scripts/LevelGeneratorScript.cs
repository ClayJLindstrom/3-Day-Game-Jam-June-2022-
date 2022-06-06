using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Pattern of Level Regeneration
// at first, the game will have easy levels to go through, with minimal obstacles to get across
// then at level two, we introduce wall-jumping

public class LevelGeneratorScript : MonoBehaviour
{
    //This is a Vector3 code, so that we can know what levels can connect to which.
    //x refers to the left side, y to the middle, and z to the right;
    //0 means nothing should be in the 2x2 block in the bottom sector of the next level
    //1 means some blocks can be placed there.
    List<(int, Vector3)> levelIndex = new List<(int level, Vector3 code)>{
        //ex: (0, (1,0,0)),
    };

    //we'll access the camera so that we can get it's height.
    private CameraScript cameraScript;
    //these will be the blocks we spawn for the floors and walls.
    private Object wallTile, floorTile;
    //the wall sprites
    private Object wallSprite1, wallSprite2, wallSprite3, wallSprite4;
    //so we know the side length of each tile
    private float tileLength;
    //so we can keep track of how high we have built our tower
    private float buildHeight;
    // Start is called before the first frame update
    void Start()
    {
        cameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraScript>();
        buildHeight = -2f;//cameraScript.ReturnHeight();
        wallTile = Resources.Load("WallTile");
        floorTile = Resources.Load("FloorTile");
        wallSprite1 = Resources.Load("WallSprite1");
        wallSprite2 = Resources.Load("WallSprite2");
        wallSprite3 = Resources.Load("WallSprite3");
        wallSprite4 = Resources.Load("WallSprite4");
        tileLength = 2.5f;

        //starting floor (starts at height = -2)
        MakeStartingFloor();
        //every following floor is built 8 tile lengths higher than the last.
        //FloorM1(Vector3.up *tileLength * 6);
        //FloorE1(Vector3.up *tileLength * 14);
        //FloorM2(Vector3.up *tileLength * 22);
        //FloorE2(Vector3.up *tileLength * 30);
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraScript.ReturnHeight() > buildHeight - (tileLength * 8)){
            GenerateNextFloor();
            cameraScript.IncreaseSpeed();
        }
    }

    public void GenerateNextFloor(){
        //increase the buildHeight (showing how high our building is now)
        buildHeight += 8;
        //pick a random number representing the next level to build.
        int r = Random.Range(1,5);
        
        if(r == 1){
            FloorE1(Vector3.up *tileLength * buildHeight);
        }
        else if(r == 2){
            FloorE2(Vector3.up *tileLength * buildHeight);
        }
        else if(r == 3){
            FloorM1(Vector3.up *tileLength * buildHeight);
        }
        else if(r == 4){
            FloorM2(Vector3.up *tileLength * buildHeight);
        }
        //and then the background
        GenerateBackground(Vector3.up *tileLength * buildHeight);
    }

    //this will make a wall with the base block at "wallLoc" location and the wall "length" blocks high (Works)
    public void MakeWall(Vector3 wallLoc, int length){

        for(int i = 0; i < length; i++){
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
    //next platfo
    public void MakeStartingFloor(){
        //first, we make the floor 6 blocks long and 4 units down from the center
        MakeFloor(Vector3.down * tileLength * 3, 6);
        //then we make two walls on each side of the floor.
        MakeWall(Vector3.right *tileLength * 3.5f
            + Vector3.down * tileLength * 3, 9);
        MakeWall(Vector3.left*tileLength * 3.5f
            + Vector3.down * tileLength * 3, 9);
        //now we add three simple platforms for the player to jump onto
        //one at the bottom right, 3 blocks above the floor,
        MakeFloor(new Vector3(2, -1, 0) * tileLength, 2);
        //one in the middle, 3 blocks above the last
        MakeFloor(new Vector3(-1,1,0) * tileLength, 2);
        //and one more 3 blocks up, but to the left;
        MakeFloor(new Vector3(-2, 3, 0) * tileLength, 2);
        //we'll actually add one more for good measure, 
        MakeFloor(new Vector3(1, 4, 0) * tileLength, 2);
        //now we generate the background
        GenerateBackground(Vector3.down * tileLength * 2);
    }

    public void FloorE1(Vector3 levelLoc){
        //two 8 walls
        MakeWall(levelLoc + Vector3.left * tileLength * 3.5f, 8);
        MakeWall(levelLoc + Vector3.right * tileLength * 3.5f, 8);
        //two floors on the left and right
        MakeFloor(levelLoc + Vector3.left * tileLength * 2.5f, 1);
        MakeFloor(levelLoc + Vector3.right * tileLength * 1.5f, 1);
        //a 1-wall on the left
        MakeFloor(levelLoc + new Vector3(-0.5f, 2, 0) * tileLength, 1);
        //a middle thing
        MakeFloor(levelLoc + new Vector3(0, 3, 0) * tileLength, 2);
        MakeWall(levelLoc + new Vector3(0.5f, 4, 0) * tileLength, 2);
        //another 1-wall on the left
        MakeFloor(levelLoc + new Vector3(-2.5f, 5, 0) * tileLength, 1);
        //and 1 last 1-wall
        MakeFloor(levelLoc + new Vector3(0.5f, 6, 0) * tileLength, 1);
    }

    public void FloorE2(Vector3 levelLoc){
        //two 8 walls
        MakeWall(levelLoc + Vector3.left * tileLength * 3.5f, 8);
        MakeWall(levelLoc + Vector3.right * tileLength * 3.5f, 8);
        //center floor
        MakeFloor(levelLoc, 2);
        //2-floor up and to the left
        MakeFloor(levelLoc + new Vector3(-2.0f, 3,0) * tileLength, 2);
        //2-floor up and to the right
        MakeFloor(levelLoc + new Vector3(2.0f, 4,0) * tileLength, 2);
        //repeat the last two lines of code, but higher
        //2-floor up and to the left
        MakeFloor(levelLoc + new Vector3(-1.5f, 6,0) * tileLength, 1);
        //2-floor up and to the right
        MakeFloor(levelLoc + new Vector3(1.5f, 7,0) * tileLength, 1);
    }

    public void FloorM1(Vector3 levelLoc){
        //two 8 walls
        MakeWall(levelLoc + Vector3.left * tileLength * 3.5f, 8);
        MakeWall(levelLoc + Vector3.right * tileLength * 3.5f, 8);
        //two 5 walls on the inside of these
        MakeWall(levelLoc + new Vector3(-2.5f, 1, 0) * tileLength, 5);
        MakeWall(levelLoc + new Vector3(2.5f, 1, 0) * tileLength, 5);
        //two 4 walls on the inside of that.
        MakeWall(levelLoc + new Vector3(-1.5f, 2, 0) * tileLength, 4);
        MakeWall(levelLoc + new Vector3(1.5f, 2, 0) * tileLength, 4);

    }

    public void FloorM2(Vector3 levelLoc){
        //two 8 walls
        MakeWall(levelLoc + Vector3.left * tileLength * 3.5f, 8);
        MakeWall(levelLoc + Vector3.right * tileLength * 3.5f, 8);
        //a 1-floor to fill in a gap
        MakeFloor(levelLoc + new Vector3(-2.5f, 1, 0) * tileLength, 1);
        //a 3-wall right next to the 1-floor
        MakeWall(levelLoc + new Vector3(-1.5f, 1, 0) * tileLength, 3);
        //a floor to the right
        MakeFloor(levelLoc + new Vector3(2f, 2, 0) * tileLength, 2);
        //a wall on top of that
        MakeWall(levelLoc + new Vector3(1.5f, 3, 0) * tileLength, 4);
        //a floor on top of that!
        MakeFloor(levelLoc + new Vector3(2f, 7, 0) * tileLength, 2);
        //a 2-wall on the top right
        MakeWall(levelLoc + new Vector3(-2.5f, 5, 0) * tileLength, 3);
        //and a floor above that!
        MakeFloor(levelLoc + new Vector3(0.5f, 7, 0) * tileLength, 1);

    }

    public void GenerateBackground(Vector3 levelLoc){
        int r = 1;
        //we make an 8 * 8 layout of the level built of the wall sprites,
        for(int i = 0; i < 8; i++){
            for(int j = 0; j< 8; j++){
                //first, we pick a random tile
                r = Random.Range(1,25);
                if(r == 1){
                    //then, starting from the farthest corner to the left, we instantiate them
                    Instantiate(wallSprite2, levelLoc + new Vector3(-3.5f + j, i, 2) * tileLength, Quaternion.identity);
                }
                else if(r == 2){
                    //then, starting from the farthest corner to the left, we instantiate them
                    Instantiate(wallSprite3, levelLoc + new Vector3(-3.5f + j, i, 2) * tileLength, Quaternion.identity);
                }
                else if(r == 3){
                    //then, starting from the farthest corner to the left, we instantiate them
                    Instantiate(wallSprite4, levelLoc + new Vector3(-3.5f + j, i, 2) * tileLength, Quaternion.identity);
                }
                else if( r == 4){
                    //do nothing
                    //this is just to produce an empty tile.
                }
                else{
                    Instantiate(wallSprite1, levelLoc + new Vector3(-3.5f + j, i, 2) * tileLength, Quaternion.identity);
                }
            }
        }
    }
}
