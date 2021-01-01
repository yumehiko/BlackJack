using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// トランプのスート。
/// </summary>
public enum Suit
{
    Spade,
    Heart,
    Club,
    Dia
}

/// <summary>
/// トランプのカード。
/// </summary>
public class Card
{
    public int number { get; private set; }
    public Suit suit { get; private set; }

    public Card(int number, Suit suit)
    {
        this.number = number;
        this.suit = suit;
    }
}

/// <summary>
/// Cardを52枚保持し、管理する。
/// </summary>
public class Deck : MonoBehaviour
{
    /// <summary>
    /// 山札。52枚のカード。
    /// </summary>
    [NonSerialized] public List<Card> deck;

    /// <summary>
    /// 使用済みのカード枚数。
    /// </summary>
    private int usedCardCount = 0;

    /// <summary>
    /// 山札を構築
    /// </summary>
    public void NewDeck()
    {
        deck = new List<Card>();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                deck.Add(new Card(j, (Suit)i));
            }
        }
        deck = ShuffleDeck(deck);
    }

    /// <summary>
    /// 山札の並びをランダムにシャッフルする。
    /// </summary>
    /// <param name="deck"></param>
    /// <returns></returns>
    public List<Card> ShuffleDeck(List<Card> deck)
    {
        deck = deck.OrderBy(a => Guid.NewGuid()).ToList();
        return deck;
    }

    /// <summary>
    /// カードを一枚引く。
    /// </summary>
    /// <returns></returns>
    public Card PickCard()
    {
        Card pickCard = deck[usedCardCount];
        usedCardCount++;
        Debug.Log($"{pickCard.number} : {pickCard.suit}");
        return pickCard;
    }
}
