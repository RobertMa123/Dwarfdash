using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Transactions;
using UnityEditor;
using UnityEngine;

public class tileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    
    public List<GameObject> tiles = new List<GameObject>();

    private int randomNumberToSpawn = 0;
    [SerializeField]
    private int sizeOfEachPiece;

    private List<GameObject> piecesOnScreen = new List<GameObject>();

    [SerializeField]
    private int maxScreenPieceCount;

    private int amountOnScreenCount;

    private void Start()
    {
        randomNumberToSpawn = tiles.Count;
        initiateTiles();
    }

    private void Update()
    {
        Vector2 playerPos = playerObject.transform.position;
        if (playerPos.y > (piecesOnScreen[0].transform.position.y - (sizeOfEachPiece/2)))
        {
            spawnNewTile();
        }
    }

    private void spawnNewTile()
    {
        GameObject newSpawnedObject = Instantiate(tiles[Random.Range(0, randomNumberToSpawn)], new Vector2(-1.8f, piecesOnScreen[0].transform.position.y + sizeOfEachPiece), Quaternion.identity);
        piecesOnScreen.Insert(0, newSpawnedObject);

        if (piecesOnScreen.Count >= maxScreenPieceCount)
        {
            Destroy(piecesOnScreen[piecesOnScreen.Count - 1]);
            piecesOnScreen.RemoveAt(piecesOnScreen.Count - 1);
        }
    }

    private void initiateTiles()
    {
        for (int i = 0; i < maxScreenPieceCount; i++)
        {
            GameObject newObject = Instantiate(tiles[Random.Range(0, randomNumberToSpawn)], new Vector2(-1.8f, sizeOfEachPiece * i), Quaternion.identity);
            piecesOnScreen.Insert(0, newObject);
        }
    }
}
