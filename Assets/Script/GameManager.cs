using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject chessPiece;
    
    private GameObject[,] boardPosition = new GameObject[9, 7];
    private GameObject[] playerBlue = new GameObject[8];
    private GameObject[] playerRed = new GameObject[8];

    private string currentPlayer = "blue";
    [SerializeField] private bool gameOver = false;

    [SerializeField] private Text endGameText;

    public int playerRedLeft = 8;
    public int playerBlueLeft = 8;

    private void Start()
    {
        playerRed = new GameObject[] {
            chessInit("red_lion", 0, 0, 7), chessInit("red_rat", 2, 0, 1),
            chessInit("red_dog", 1, 1, 3), chessInit("red_leopard", 2, 2, 5),
            chessInit("red_wolf", 2, 4, 4), chessInit("red_cat", 1, 5, 2),
            chessInit("red_elephant", 2, 6, 8), chessInit("red_tiger", 0, 6, 6),
        };
        playerBlue = new GameObject[] {
            chessInit("blue_lion", 8, 6, 7), chessInit("blue_rat", 6, 6, 1),
            chessInit("blue_dog", 7, 5, 3), chessInit("blue_leopard", 6, 4, 5),
            chessInit("blue_wolf", 6, 2, 4), chessInit("blue_cat", 7, 1, 2),
            chessInit("blue_elephant", 6, 0, 8), chessInit("blue_tiger", 8, 0, 6),
        };

        for (int i = 0; i < 8; i++) {
            chessInitPos(playerRed[i]);
            chessInitPos(playerBlue[i]);
        }
    }

    // private void Update() {
    //     // if (gameOver && Input.GetMouseButtonDown(0)) {
    //     //     gameOver = false;
    //     //     SceneManager.LoadScene("Game");
    //     // }


    //     if (gameOver) {
    //         endGame
    //     }
    // }

    public void endGame(string winner) {
        endGameText.text = winner;
    }

    public GameObject chessInit(string name, int x, int y, int chessRank) {
        GameObject newChess = Instantiate(chessPiece, new Vector3(0,0,-1), Quaternion.identity);
        ChessMan chessInfo = newChess.GetComponent<ChessMan>();
        chessInfo.name = name;
        chessInfo.XBoard = x;
        chessInfo.YBoard = y;
        chessInfo.ChessRank = chessRank;
        chessInfo.initialize();

        return newChess;
    }

    public void chessInitPos(GameObject chess) {
        ChessMan chessInfo = chess.GetComponent<ChessMan>();

        boardPosition[chessInfo.XBoard, chessInfo.YBoard] = chess;
    }

    public void setSpaceEmpty (int x, int y) {
        boardPosition[x , y] = null;
    }

    public GameObject GetPosition (int x, int y) {
        return boardPosition[x , y];
    }

    public bool isValidMove(int x, int y) {
        if (x < 0 || y < 0 || x >= 9 || y >= 7) return false;
        return true;
    }

    public string CurrentPlayer {
        get { return currentPlayer; }
    }

    public bool isGameOver {
        get { return gameOver; }
        set { gameOver = value;}
    }

    public void nextTurn() {
        if (currentPlayer == "blue") currentPlayer = "red";
        else currentPlayer = "blue";
    }
    
}
