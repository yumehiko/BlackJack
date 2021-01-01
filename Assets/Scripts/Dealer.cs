using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : Player
{
    public override void NewHands(Deck deck)
    {
        DrawCard(deck);
        DisplayTotalPoint();
        DrawCard(deck);
        DisplayTotalPoint();
    }
}
