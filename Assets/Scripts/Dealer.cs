using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dealer : Player
{
    /// <summary>
    /// 新たなハンドを作る。ただし2枚目は裏向きで。
    /// </summary>
    public override void NewHands()
    {
        DiscardHand();

        Card pickCard = deck.PickCard();
        DrawCard(pickCard);
        pickCard = deck.PickCard();
        DrawCardSecret(pickCard);
        DOVirtual.DelayedCall(1.0f, () =>
        DisplayHiddenPoint()
        );
    }

    /// <summary>
    /// 裏向きでカードを引く。
    /// </summary>
    /// <param name="card"></param>
    private void DrawCardSecret(Card card)
    {
        deck.DealCardAnime(card, this, true);
        totalPoint += Mathf.Min(card.number, 10);
        hands.Add(card);
    }

    /// <summary>
    /// 半分隠されたポイントを表示する。
    /// </summary>
    private void DisplayHiddenPoint()
    {
        pointDisplay.text = $"{Mathf.Min(hands[0].number,10)}+";
    }

    /// <summary>
    /// 次にカードを配る座標を取得
    /// </summary>
    /// <returns></returns>
    public override Vector2 GetDealPosition()
    {
        float x = 7.7f + (-2.2f * hands.Count);
        return new Vector2(x, transform.position.y);
    }
}
