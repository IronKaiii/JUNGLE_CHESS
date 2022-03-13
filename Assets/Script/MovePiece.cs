using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{
    public GameObject controller;
    private GameObject reference = null;
    private int coordX;
    private int coordY;

    private int[] bluePlayerDen = new int[2] {8,3};
    private int[] redPlayerDen = new int[2] {0,3};

    public bool takingDown = false;

    public void Start() {
        if (takingDown) gameObject.GetComponent<SpriteRenderer>().color = new Color(0,1f,1f,1f);
    }

    public void OnMouseUp() {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (takingDown) {
            GameObject chessPiece = controller.GetComponent<GameManager>().GetPosition(coordX, coordY);
            
            Destroy(chessPiece);
            if (controller.GetComponent<GameManager>().CurrentPlayer == "red") {
                controller.GetComponent<GameManager>().playerBlueLeft--;
            } else if (controller.GetComponent<GameManager>().CurrentPlayer == "blue") {
                controller.GetComponent<GameManager>().playerRedLeft--;
            }
            if (controller.GetComponent<GameManager>().playerBlueLeft == 0) {
                controller.GetComponent<GameManager>().isGameOver = true;
                controller.GetComponent<GameManager>().endGame("RED");
            } else if (controller.GetComponent<GameManager>().playerRedLeft == 0) {
                controller.GetComponent<GameManager>().isGameOver = true;
                controller.GetComponent<GameManager>().endGame("BLUE");
            }
        }

        controller.GetComponent<GameManager>().setSpaceEmpty(reference.GetComponent<ChessMan>().XBoard,
        reference.GetComponent<ChessMan>().YBoard);

        reference.GetComponent<ChessMan>().XBoard = coordX;
        reference.GetComponent<ChessMan>().YBoard = coordY;
        reference.GetComponent<ChessMan>().SetTheBoard();
        
        if (coordX == redPlayerDen[0] && coordY == redPlayerDen[1]) {
            controller.GetComponent<GameManager>().isGameOver = true;
            controller.GetComponent<GameManager>().endGame("BLUE");
        } else if (coordX == bluePlayerDen[0] && coordY == bluePlayerDen[1]) {
            controller.GetComponent<GameManager>().isGameOver = true;
            controller.GetComponent<GameManager>().endGame("RED");
        }

        controller.GetComponent<GameManager>().chessInitPos(reference);
        controller.GetComponent<GameManager>().nextTurn();
        reference.GetComponent<ChessMan>().DestroyMovePiece();
    }

    public void SetCoord (int x, int y) {
        coordX = x;
        coordY = y;
        
    }

    public GameObject Reference {
        get { return reference; }
        set { reference = value; }
    }

}
