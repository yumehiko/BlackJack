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
public class BlackJackManager : MonoBehaviour
{
    /// <summary>
    /// 赤色。バースト時の文字カラー。
    /// </summary>
    public static Color burstRed = new Color(0.91f, 0.16f, 0.31f);

    /// <summary>
    /// 白色。通常時の文字カラー。
    /// </summary>
    public static Color lightWhite = new Color(0.9f, 1.0f, 0.93f);

    /// <summary>
    /// ゲーム全体のアニメーションの再生速度。
    /// </summary>
    public static float animeDuration = 1.0f;

    /// <summary>
    /// 現在のゲーム状態。
    /// </summary>
    private GameState gameState = GameState.PrevStart;

    [SerializeField] private Deck deck = default;
    [SerializeField] private BlackJackPlayer player = default;
    [SerializeField] private BlackJackDealer dealer = default;
    [SerializeField] private CenterUI centerUI = default;
    [SerializeField] private SoundDirector soundDirector = default;

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

        soundDirector.PlaySound();
        player.NewHands();
        dealer.NewHands();

        DOVirtual.DelayedCall(animeDuration, () => ChangeState(GameState.HitOrStand));
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
    /// プレイヤーとディーラーのスコアを比較し、多い方の勝利。
    /// </summary>
    /// <param name="player"></param>
    /// <param name="dealer"></param>
    /// <returns>プレイヤーが勝利したらTrue</returns>
    private bool CheckWinner()
    {
        return player.TotalPoint > dealer.TotalPoint;
    }

    /// <summary>
    /// ゲームに勝利する。終了処理を行う。
    /// </summary>
    public void WinGame()
    {
        ChangeState(GameState.YouWin);
    }

    /// <summary>
    /// ゲームに敗北する。終了処理を行う。
    /// </summary>
    public void LoseGame()
    {
        ChangeState(GameState.YouLose);
    }

    /// <summary>
    /// Hitをコール。
    /// </summary>
    public void CallHit()
    {
        soundDirector.PlaySound();
        Card card = deck.PickCard();
        player.DrawCard(card);
        ChangeState(GameState.Wait);

        DOVirtual.DelayedCall(animeDuration, () =>
        player.DisplayTotalPoint()
        );

        if (player.CheckBurst())
        {
            DOVirtual.DelayedCall(animeDuration, () =>
            LoseGame()
            );
        }
        else
        {
            DOVirtual.DelayedCall(animeDuration, () =>
            ChangeState(GameState.HitOrStand)
            );
        }
    }

    /// <summary>
    /// Standをコール。
    /// </summary>
    public void CallStand()
    {
        ChangeState(GameState.Wait);
        dealer.FlipHiddenCard();
        soundDirector.PlaySound();

        StartCoroutine(nameof(DealerTurnCo));
    }

    /// <summary>
    /// ディーラーのターンを処理。
    /// </summary>
    private bool DealerTurn()
    {
        BJDealerState dealerState = dealer.CheckState();

        switch (dealerState)
        {
            case BJDealerState.Burst:
                WinGame();
                return false;

            case BJDealerState.NeedCard:
                soundDirector.PlaySound();
                Card card = deck.PickCard();
                dealer.DrawCard(card);
                return true;

            case BJDealerState.Seventeen:
                if (CheckWinner())
                {
                    WinGame();
                    return false;
                }
                else
                {
                    LoseGame();
                    return false;
                }

            default: return false;
        }
    }

    /// <summary>
    /// ディーラーのターン処理をループ。
    /// </summary>
    /// <returns></returns>
    private IEnumerator DealerTurnCo()
    {
        bool isLoop = true;
        while (isLoop)
        {
            yield return new WaitForSeconds(animeDuration);
            dealer.DisplayTotalPoint();
            isLoop = DealerTurn();
        }

    }

    /// <summary>
    /// タイトル画面へ戻る。
    /// </summary>
    public void ReturnTitle()
    {
        ChangeState(GameState.PrevStart);
    }
}
