using System;
using UnityEngine;

public class PositionTranslator : MonoBehaviour
{
    public Transform chessboard;
    private Vector3 chessboardSize;
    public Transform smallPawn;

    public Transform Batlefield;
    public Transform largePawn;

    public float scalingFactor = 100.0f;

    public Vector2 chessboardPositionX;
    public Vector2 chessboardPositionZ;

    public Transform Corner1;
    public Transform Corner2;
        public Transform Corner3;
        public Transform Corner4;

    private void Start()
    {
        chessboardSize = chessboard.localScale;

        chessboardPositionX.x = chessboard.position.x - chessboardSize.x / 2;
        chessboardPositionX.y = chessboard.position.x + chessboardSize.x / 2;

        chessboardPositionZ.x = chessboard.position.z - chessboardSize.z / 2;
        chessboardPositionZ.y = chessboard.position.z + chessboardSize.z / 2;
        
        //Debug
        Corner1.position = new Vector3(chessboardPositionX.x, chessboard.position.y, Corner1.position.z);
        Corner2.position = new Vector3(chessboardPositionX.y, chessboard.position.y, Corner2.position.z);
        Corner3.position = new Vector3(Corner3.position.x, chessboard.position.y, chessboardPositionZ.x);
        Corner4.position = new Vector3(Corner4.position.x, chessboard.position.y, chessboardPositionZ.y);
        
        Debug.Log($"Min : {chessboardPositionX.x}\nMax : {chessboardPositionX.y}");
    }

    void Update()
    {
        Vector3 pawnPosition = transform.position;


        // Vector3 relativePosition = new Vector3(
        //     pawnPosition.x / (chessboardSize.x / 2),
        //     pawnPosition.y / (chessboardSize.y / 2),
        //     pawnPosition.z / (chessboardSize.z / 2)
        // );

        if (Input.GetKeyDown(KeyCode.A))
            Debug.Log(chessboardSize.x / 2);
    }
}