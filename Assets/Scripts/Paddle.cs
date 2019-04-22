using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
  
  [SerializeField] float screenWidthInUnits = 16f;
  [SerializeField] float minX = 1f;
  [SerializeField] float maxX = 15f;
  
  // cached refernces
  GameStatus theGameSession;
  Ball theBall;
  
  void Start () {
    theGameSession = FindObjectOfType<GameStatus>();
    theBall = FindObjectOfType<Ball>();
  }
  
  void Update ()
  {
    Vector2 paddlePos = new Vector2 (transform.position.x, transform.position.y);
    paddlePos.x = Mathf.Clamp(GetXPos(), minX,maxX);
    transform.position = paddlePos;
  }
  
  private float GetXPos()
  {
    if(FindObjectOfType<GameStatus>().IsAutoPlayEnabled())
    {
      return FindObjectOfType<Ball>().transform.position.x;
    }
    else
    {
      return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
  }
  
}