using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public PuzzleShuffle puzzleShuffle;
    
    public void TryMoveTile()
    {
        puzzleShuffle.TryMoveTile(this.gameObject);
    }
}
