using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ブラックジャックのプレイヤー。
/// </summary>
public class BlackJackPlayer : MonoBehaviour
{
    /// <summary>
    /// カードの合計点数の表示UI
    /// </summary>
    [SerializeField] protected Text pointDisplay = default;

    [SerializeField] protected Deck deck = default;

    /// <summary>
    /// 手札にあるカードのリスト。
    /// </summary>
    public List<Card> Hands { get; protected set; }

    /// <summary>
    /// 有効な（11として扱っている）エースの枚数。
    /// </summary>
    private int ace = 0;

    /// <summary>
    /// カードの合計点
    /// </summary>
    public int TotalPoint { get; protected set; }

    /// <summary>
    /// カードを配置する位置リスト。
    /// </summary>
    [SerializeField] private List<Transform> cardPositions = default;

    /// <summary>
    /// 新たなハンドを作る。2枚カードを引く。
    /// </summary>
    /// <param name="deck"></param>
    public virtual void NewHands()
    {
        DiscardHand();

        Card pickCard = deck.PickCard();
        DrawCard(pickCard);
        pickCard = deck.PickCard();
        DrawCard(pickCard);
        DOVirtual.DelayedCall(BlackJackManager.animeDuration, () =>
        DisplayTotalPoint()
        );
    }

    /// <summary>
    /// カードを1枚引く。
    /// </summary>
    /// <param name="card"></param>
    public void DrawCard(Card card)
    {
        if(card.Number == 1)
        {
            AddAce();
        }

        deck.DealCardAnime(card, this, false);
        TotalPoint += Mathf.Min(card.Number, 10);
        Hands.Add(card);
    }

    /// <summary>
    /// Aを引いたなら、それを記憶して11として扱う。
    /// </summary>
    protected void AddAce()
    {
        ace++;
        TotalPoint += 10;
    }

    /// <summary>
    /// 合計ポイントをUIに表示。
    /// </summary>
    public void DisplayTotalPoint()
    {
        if(CheckBurst())
        {
            pointDisplay.color = BlackJackManager.burstRed;
        }
        pointDisplay.text = TotalPoint.ToString();
    }

    /// <summary>
    /// 手札を破棄する。
    /// </summary>
    protected void DiscardHand()
    {
        Hands = new List<Card>();
        TotalPoint = 0;
        ace = 0;
        pointDisplay.color = BlackJackManager.lightWhite;
        DisplayTotalPoint();
    }

    /// <summary>
    /// 次にカードを配る座標を取得
    /// </summary>
    /// <returns></returns>
    public Vector2 GetDealPosition()
    {
        return cardPositions[Hands.Count].position;
    }

    /// <summary>
    /// バーストしたかを確認。
    /// </summary>
    /// <returns>バーストしたならTrue</returns>
    public bool CheckBurst()
    {
        if (TotalPoint > 21)
        {
            if (ace <= 0)
            {
                return true;
            }
            else
            {
                ace--;
                TotalPoint -= 10;
                return CheckBurst();
            }
        }
        else
        {
            return false;
        }
    }
}
