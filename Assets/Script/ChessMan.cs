using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMan : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePiece;

    private int xBoard = -1;
    private int yBoard = -1;
    private int chessRank = 0;

    private int[,] redPlayerTrap = new int[3,2] { {0, 2}, {0, 4}, {1,3} };
    private int[,] bluePlayerTrap = new int[3,2] { {8, 2}, {8, 4}, {7,3} };
    private int[] bluePlayerDen = new int[2] {8,3};
    private int[] redPlayerDen = new int[2] {0,3};
    private int[,] river = new int[12,2] {{3,1}, {4,1}, {5,1}, {3,2}, {4,2}, {5,2}, {3,4}, {4,4}, {5,4}, {3,5}, {4,5}, {5,5} };
    private int[,] JumpThreeCoord = new int[8,2] { {2,1}, {2,2}, {2,4}, {2,5}, {6,1}, {6,2}, {6,4}, {6,5}};
    private int[,] JumpTwoCoord = new int[9,2] { {3,0}, {4,0}, {5,0}, {3,3}, {4,3}, {5,3}, {3,6}, {4,6}, {5,6} };

    private string player;

    public Sprite blue_cat, blue_dog, blue_elephant, blue_leopard, 
    blue_lion, blue_rat, blue_tiger, blue_wolf;

    public Sprite red_cat, red_dog, red_elephant, red_leopard, 
    red_lion, red_rat, red_tiger, red_wolf;

    public void initialize() {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetTheBoard();

        switch (this.name) 
        {
            case "blue_cat": this.GetComponent<SpriteRenderer>().sprite = blue_cat; player = "blue"; break;
            case "blue_dog": this.GetComponent<SpriteRenderer>().sprite = blue_dog; player = "blue"; break;
            case "blue_elephant": this.GetComponent<SpriteRenderer>().sprite = blue_elephant; player = "blue";  break;
            case "blue_leopard": this.GetComponent<SpriteRenderer>().sprite = blue_leopard; player = "blue";  break;
            case "blue_lion": this.GetComponent<SpriteRenderer>().sprite = blue_lion; player = "blue";  break;
            case "blue_rat": this.GetComponent<SpriteRenderer>().sprite = blue_rat; player = "blue";  break;
            case "blue_tiger": this.GetComponent<SpriteRenderer>().sprite = blue_tiger; player = "blue";  break;
            case "blue_wolf": this.GetComponent<SpriteRenderer>().sprite = blue_wolf; player = "blue";  break;

            case "red_cat": this.GetComponent<SpriteRenderer>().sprite = red_cat; player = "red"; break;
            case "red_dog": this.GetComponent<SpriteRenderer>().sprite = red_dog; player = "red"; break;
            case "red_elephant": this.GetComponent<SpriteRenderer>().sprite = red_elephant; player = "red"; break;
            case "red_leopard": this.GetComponent<SpriteRenderer>().sprite = red_leopard; player = "red"; break;
            case "red_lion": this.GetComponent<SpriteRenderer>().sprite = red_lion; player = "red"; break;
            case "red_rat": this.GetComponent<SpriteRenderer>().sprite = red_rat; player = "red"; break;
            case "red_tiger": this.GetComponent<SpriteRenderer>().sprite = red_tiger; player = "red"; break;
            case "red_wolf": this.GetComponent<SpriteRenderer>().sprite = red_wolf; player = "red"; break;
        }
    }

    public void SetTheBoard() {
        float x = xBoard;
        float y = yBoard;

        x *= 1.3f;
        y *= 1.305f;

        x += -5.2f;
        y += -3.9f;

        this.transform.position = new Vector3(x,y,-1f);
    }

    public int XBoard {
        get { return xBoard; }
        set { xBoard = value; }
    }

    public int YBoard {
        get { return yBoard; }
        set { yBoard = value; }
    }

    public int ChessRank {
        get { return chessRank; }
        set { chessRank = value; }
    }
    private void OnMouseUp() {
        if (!controller.GetComponent<GameManager>().isGameOver
        && controller.GetComponent<GameManager>().CurrentPlayer == player) {
            DestroyMovePiece();
            initMovePiece();
        }      
    }

    public void DestroyMovePiece() {
        GameObject[] movePieces = GameObject.FindGameObjectsWithTag("MovePiece");
        for (int i = 0; i < movePieces.Length; i++) {
            Destroy(movePieces[i]);
        }
    }

    public void initMovePiece() {
        // only lion can jump 3
        if (chessRank == 7 && readyToJumpThree(xBoard, yBoard)) JumpThreeMove();

        // only lion and tiger can jump2
        if ((chessRank == 7 || chessRank == 6) && readyToJumpTwo(xBoard, yBoard)) JumpTwoMove();

        normalMove();
    }

    public void JumpThreeMove() {
        if (xBoard == 2) {
            MoveChoice(xBoard + 4, yBoard);
        } else if (xBoard == 6) {
            MoveChoice(xBoard - 4, yBoard);
        }
    }

    public void JumpTwoMove() {
        switch(yBoard) {
            case 0: MoveChoice(xBoard, yBoard+3); break;
            case 3: 
                GameManager selectPiece = controller.GetComponent<GameManager>();
                
                GameObject ratChessPosUpper1 = selectPiece.GetPosition(xBoard, yBoard + 1);
                GameObject ratChessPosUpper2 = selectPiece.GetPosition(xBoard, yBoard + 2);
                GameObject ratChessPosLower1 = selectPiece.GetPosition(xBoard, yBoard - 1);
                GameObject ratChessPosLower2 = selectPiece.GetPosition(xBoard, yBoard - 2);
                if ((ratChessPosUpper1 != null || ratChessPosUpper2 != null) && (ratChessPosLower1 != null || ratChessPosLower2 != null)) {
                    break;
                } else if (ratChessPosUpper1 != null || ratChessPosUpper2 != null) {
                    MoveChoice(xBoard, yBoard-3);
                    break;
                } else if (ratChessPosLower1 != null || ratChessPosLower2 != null) {
                    MoveChoice(xBoard, yBoard+3);
                    break;
                }  
                MoveChoice(xBoard, yBoard+3);
                MoveChoice(xBoard, yBoard-3);
                break;
            case 6: MoveChoice(xBoard, yBoard-3); break;
        }
    }

    public void normalMove() {
        MoveChoice(xBoard + 1, yBoard);
        MoveChoice(xBoard - 1, yBoard);
        MoveChoice(xBoard, yBoard + 1);
        MoveChoice(xBoard, yBoard - 1);
    }

    public bool readyToJumpThree(int x, int y) {
        for (int i = 0; i < JumpThreeCoord.GetLength(0); i++) {
            if (x == JumpThreeCoord[i,0] && y == JumpThreeCoord[i,1]) {
                // if rat block the river
                GameManager selectPiece = controller.GetComponent<GameManager>();
                for (int j = 1; j < 4; j++) {
                    if (x == 2) {
                        GameObject ratChessPos = selectPiece.GetPosition(x + j, y);
                        if (ratChessPos != null) return false; // rat block the jump
                    } else if (x == 6) {
                        GameObject ratChessPos = selectPiece.GetPosition(x - j, y);
                        if (ratChessPos != null) return false; // rat block the jump
                    }
                }
                return true; 
            }
        }
        return false;
    }

    public bool readyToJumpTwo(int x, int y) {
        for (int i = 0; i < JumpTwoCoord.GetLength(0); i++) {
            if (x == JumpTwoCoord[i,0] && y == JumpTwoCoord[i,1]) {
                // if rat block the river
                GameManager selectPiece = controller.GetComponent<GameManager>();
                for (int j = 1; j < 3; j++) {
                    if (y == 0) {
                        GameObject ratChessPos = selectPiece.GetPosition(x, y + j);
                        if (ratChessPos != null) return false; // rat block the jump
                    } else if (y == 6) {
                        GameObject ratChessPos = selectPiece.GetPosition(x, y - j);
                        if (ratChessPos != null) return false; // rat block the jump
                    } // when the jump occurs on the middle, need to check from other function
                }
                return true; 
            } 
        }
        return false;
    }

    public bool isRiver(int x, int y) {
        for (int i = 0; i < river.GetLength(0); i++) {
            if (x == river[i,0] && y == river[i,1]) return true; 
        }
        return false;
    }

    public bool redIsInTrap(int x, int y) {
        for (int i = 0; i < bluePlayerTrap.GetLength(0); i++) {
            if (x == bluePlayerTrap[i,0] && y == bluePlayerTrap[i,1]) return true; 
        }
        return false;
    }

    public bool blueIsInTrap(int x, int y) {
        for (int i = 0; i < redPlayerTrap.GetLength(0); i++) {
            if (x == redPlayerTrap[i,0] && y == redPlayerTrap[i,1]) return true; 
        }
        return false;
    }

    public bool redDenCheck(int x, int y) {
        if (x == redPlayerDen[0] && y == redPlayerDen[1]) return true;
        return false;
    }

    public bool blueDenCheck(int x, int y) {
        if (x == bluePlayerDen[0] && y == bluePlayerDen[1]) return true;
        return false;
    }
    
    public void MoveChoice(int x, int y) {
        GameManager selectPiece = controller.GetComponent<GameManager>();
        if (selectPiece.isValidMove(x,y)) {
            GameObject newChessPos = selectPiece.GetPosition(x,y);

            // if the destination is empty space
            if (newChessPos == null) {
                // only rat can enter the river
                if (isRiver(x,y)) {
                    if (chessRank == 1) triggerMovePiece(x, y);
                } // cant enter its own den
                else if ((player == "red" && !redDenCheck(x, y)) ||
                (player == "blue" && !blueDenCheck(x, y))) {
                    triggerMovePiece(x, y);
                }
            } // if there is a chess piece of enemy team
            else if (newChessPos.GetComponent<ChessMan>().player != player) {
                // if chess is in the enemy trap, all animal can take it down
                if (newChessPos.GetComponent<ChessMan>().player == "red" &&
                    redIsInTrap(x,y)) {
                    triggerTakeDown(x, y);
                } else if (newChessPos.GetComponent<ChessMan>().player == "blue" &&
                    blueIsInTrap(x,y)) {
                    triggerTakeDown(x, y);
                }

                // if the rat is selected, it can take down elephant
                if (chessRank == 1 && newChessPos.GetComponent<ChessMan>().chessRank == 8 && !isRiver(xBoard, yBoard)) {
                    triggerTakeDown(x, y);
                }
                // compare the rank, cant kill the rat in river
                if (chessRank >= newChessPos.GetComponent<ChessMan>().chessRank) {
                    if (chessRank == 8 && newChessPos.GetComponent<ChessMan>().chessRank == 1) return;
                    else if (!isRiver(x,y)) triggerTakeDown(x, y);
                    else if (chessRank == 1) triggerTakeDown(x, y);
                }
            }
        }
    }

    public void triggerMovePiece(int xBoardOfChess, int yBoardOfChess) {
        float x = xBoardOfChess;
        float y = yBoardOfChess;

        x *= 1.3f;
        y *= 1.305f;

        x += -5.2f;
        y += -3.9f;

        GameObject MoveChoiceHighlight = Instantiate(movePiece, new Vector3(x, y, -3f), Quaternion.identity);
        MovePiece moveAction = MoveChoiceHighlight.GetComponent<MovePiece>();
        moveAction.Reference = gameObject;
        moveAction.SetCoord(xBoardOfChess, yBoardOfChess);
    }

    public void triggerTakeDown(int xBoardOfChess, int yBoardOfChess) {
        float x = xBoardOfChess;
        float y = yBoardOfChess;

        x *= 1.3f;
        y *= 1.305f;

        x += -5.2f;
        y += -3.9f;

        GameObject MoveChoiceHighlight = Instantiate(movePiece, new Vector3(x, y, -3f), Quaternion.identity);
        MovePiece moveAction = MoveChoiceHighlight.GetComponent<MovePiece>();
        moveAction.takingDown = true;
        moveAction.Reference = gameObject;
        moveAction.SetCoord(xBoardOfChess, yBoardOfChess);
    }
}
