using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int cards;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DrawDiscard());
    }

    IEnumerator DrawDiscard()
    {
        Board board = new Board();
        List<Card> draws;
        while (true)
        {
            draws = board.DrawCards(cards);
            board.DiscardCards(draws);
            Debug.Log(board);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
