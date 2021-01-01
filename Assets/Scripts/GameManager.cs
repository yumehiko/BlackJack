using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ゲームの状態を示す。
/// </summary>
public enum GameState
{
    PrevStart,
    Wait,
    HitOrStand,
    YouWin,
    YouLose
}

/// <summary>
/// ゲーム進行を管理。
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 現在のゲーム状態。
    /// </summary>
    private GameState gameState = GameState.PrevStart;

    [SerializeField] private Deck deck = default;
    [SerializeField] private Player player = default;
    [SerializeField] private Dealer dealer = default;
    [SerializeField] private CenterUI centerUI = default;

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
                NewGame();
            }
        }
    }

    /// <summary>
    /// 新しいゲームを開始。
    /// </summary>
    public void NewGame()
    {
        deck.ResetInstances();
        deck.ShuffleDeck();
        ChangeState(GameState.Wait);

        player.NewHands();
        dealer.NewHands();

        DOVirtual.DelayedCall(1.0f, () => ChangeState(GameState.HitOrStand));
    }

    /// <summary>
    /// ゲームマネージャーの状態を変更。
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(GameState newState)
    {
        gameState = newState;
        centerUI.ChangeState(gameState);
    }

    /// <summary>
    /// 対象がバーストしたかチェック。
    /// </summary>
    /// <returns>バーストしたならTrue</returns>
    private bool CheckBurst(Player target)
    {
        return target.totalPoint > 21;
    }

    /// <summary>
    /// Hitをコール。
    /// </summary>
    public void CallHit()
    {
        Card card = deck.PickCard();
        player.DrawCard(card);
        ChangeState(GameState.Wait);

        DOVirtual.DelayedCall(1.0f, () =>
        player.DisplayTotalPoint()
        );
        if (CheckBurst(player))
        {
            DOVirtual.DelayedCall(1.0f, () =>
            ChangeState(GameState.YouLose)
            );
        }
        else
        {
            DOVirtual.DelayedCall(1.0f, () =>
            ChangeState(GameState.HitOrStand)
            );
        }
    }

    /// <summary>
    /// Standをコール。
    /// </summary>
    public void CallStand()
    {

    }

    /// <summary>
    /// タイトル画面へ戻る。
    /// </summary>
    public void ReturnTitle()
    {
        ChangeState(GameState.PrevStart);
    }
}
