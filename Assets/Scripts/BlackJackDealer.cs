﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum BJDealerState
{
    NeedCard,
    Burst,
    Seventeen
}

public class BlackJackDealer : BlackJackPlayer
{
    private CardInstance hiddenCard;

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
        DOVirtual.DelayedCall(BlackJackManager.animeDuration, () =>
        HiddenPoint()
        );
    }

    /// <summary>
    /// 裏向きでカードを引く。
    /// </summary>
    /// <param name="card"></param>
    private void DrawCardSecret(Card card)
    {
        hiddenCard = deck.DealCardAnime(card, this, true);
        TotalPoint += Mathf.Min(card.number, 10);
        hands.Add(card);
    }

    /// <summary>
    /// ポイントを半分隠す。
    /// </summary>
    private void HiddenPoint()
    {
        pointDisplay.text = $"{Mathf.Min(hands[0].number,10)}+";
    }

    /// <summary>
    /// ディーラーの状態を確認する。
    /// </summary>
    public BJDealerState CheckState()
    {
        if(CheckBurst())
        {
            return BJDealerState.Burst;
        }
        if(Check17orMore())
        {
            return BJDealerState.Seventeen;
        }
        return BJDealerState.NeedCard;
    }

    /// <summary>
    /// 17点以上かどうかをチェック。
    /// </summary>
    /// <param name="dealer"></param>
    /// <returns>17以上ならTrue</returns>
    private bool Check17orMore()
    {
        return TotalPoint >= 17;
    }

    /// <summary>
    /// 裏向きに表示されていたカードを公開し、正しいポイントを表示する。
    /// </summary>
    public void FlipHiddenCard()
    {
        hiddenCard.FlipCard();
        DOVirtual.DelayedCall(BlackJackManager.animeDuration, () =>
        DisplayTotalPoint()
        );
    }
}
