using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ゲームの状態を示す。
    /// </summary>
    private enum GameState
    {
        PrevStart,
        Wait,
        HitOrStand,
        Retry
    }

    /// <summary>
    /// 現在のゲーム状態。
    /// </summary>
    private GameState gameState = GameState.PrevStart;

    [SerializeField] private Deck deck = default;
    [SerializeField] private Player player = default;
    [SerializeField] private Dealer dealer = default;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        deck.NewDeck();
    }

    private void Update()
    {
        if(gameState == GameState.PrevStart)
        {
            if(Input.GetMouseButtonDown(0))
            {
                gameState = GameState.Wait;
                NewGame();
            }
        }
    }

    /// <summary>
    /// 新しいゲームを開始。
    /// </summary>
    private void NewGame()
    {
        player.NewHands(deck);
        dealer.NewHands(deck);
    }
}
