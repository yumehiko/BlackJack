using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームのプレイヤー。
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private Text pointDisplay = default;

    private List<Card> hands = new List<Card>();
    private int totalPoint;

    /// <summary>
    /// 新たなハンドを作る。2枚カードを引く。
    /// </summary>
    /// <param name="deck"></param>
    public virtual void NewHands(Deck deck)
    {
        DrawCard(deck);
        DisplayTotalPoint();
        DrawCard(deck);
        DisplayTotalPoint();
    }

    /// <summary>
    /// カードを1枚引く。
    /// </summary>
    /// <param name="deck"></param>
    public void DrawCard(Deck deck)
    {
        Card pickCard = deck.PickCard();
        totalPoint += Mathf.Min(pickCard.number, 10);
        hands.Add(pickCard);
    }

    /// <summary>
    /// 合計ポイントを表示。
    /// </summary>
    protected void DisplayTotalPoint()
    {
        pointDisplay.text = totalPoint.ToString();
    }
}
