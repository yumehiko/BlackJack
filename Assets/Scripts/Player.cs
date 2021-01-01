using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ゲームのプレイヤー。
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// カードの合計点数の表示UI
    /// </summary>
    [SerializeField] protected Text pointDisplay = default;

    [SerializeField] protected Deck deck = default;

    /// <summary>
    /// 手札にあるカードのリスト。
    /// </summary>
    protected List<Card> hands = new List<Card>();

    /// <summary>
    /// カードの合計点
    /// </summary>
    public int totalPoint { get; protected set; }

    /// <summary>
    /// カードを配る最初の位置。
    /// </summary>
    protected float dealFirstPosition = -7.7f;

    /// <summary>
    /// カードの間隔。
    /// </summary>
    protected float dealMargin = 2.2f;

    

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
        DOVirtual.DelayedCall(1.0f, () =>
        DisplayTotalPoint()
        );
    }

    /// <summary>
    /// カードを1枚引く。
    /// </summary>
    /// <param name="card"></param>
    public void DrawCard(Card card)
    {
        deck.DealCardAnime(card, this, false);
        totalPoint += Mathf.Min(card.number, 10);
        hands.Add(card);
    }

    /// <summary>
    /// 合計ポイントをUIに表示。
    /// </summary>
    public void DisplayTotalPoint()
    {
        pointDisplay.text = totalPoint.ToString();
    }

    /// <summary>
    /// 手札を破棄する。
    /// </summary>
    protected void DiscardHand()
    {
        hands.Clear();
        totalPoint = 0;
        DisplayTotalPoint();
    }

    /// <summary>
    /// 次にカードを配る座標を取得
    /// </summary>
    /// <returns></returns>
    public virtual Vector2 GetDealPosition()
    {
        float x = -7.7f + (2.2f * hands.Count);
        return new Vector2(x, transform.position.y);
    }
}
