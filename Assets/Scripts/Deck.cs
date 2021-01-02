using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

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
    /// カードの表示用の実体。
    /// </summary>
    [SerializeField] private GameObject cardPrefab = default;

    /// <summary>
    /// カードを生成する初期位置
    /// </summary>
    [SerializeField] private Transform CardInitPoint = default;

    /// <summary>
    /// 生成したカード実体のリスト。
    /// </summary>
    private List<GameObject> cardInstances;

    /// <summary>
    /// 山札を構築
    /// </summary>
    public void NewDeck()
    {
        deck = new List<Card>();
        cardInstances = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                deck.Add(new Card(j, (Suit)i));
            }
        }
        ShuffleDeck();
    }

    /// <summary>
    /// 山札の並びをランダムにシャッフルする。
    /// </summary>
    /// <param name="deck"></param>
    /// <returns></returns>
    public void ShuffleDeck()
    {
        deck = deck.OrderBy(a => Guid.NewGuid()).ToList();
        usedCardCount = 0;
    }

    /// <summary>
    /// カードの実体を全て破棄。
    /// </summary>
    public void ResetInstances()
    {
        foreach (GameObject instance in cardInstances)
        {
            Destroy(instance);
        }
        cardInstances.Clear();
    }

    /// <summary>
    /// カードを一枚引く。
    /// </summary>
    /// <returns></returns>
    public Card PickCard()
    {
        Card pickCard = deck[usedCardCount];
        usedCardCount++;
        return pickCard;
    }

    /// <summary>
    /// カード実体を生成し、配るアニメーションを再生する。
    /// </summary>
    /// <param name="card"></param>
    /// <param name="dealTarget"></param>
    /// <param name="isReverse"></param>
    /// <returns>実体</returns>
    public CardInstance DealCardAnime(Card card, BlackJackPlayer dealTarget, bool isReverse)
    {
        Vector3 rotate = new Vector3(0.0f, 0.0f, 170.0f);
        if(isReverse)
        {
            rotate.y = 180.0f;
            rotate.z = 190.0f;
        }

        GameObject cardInstanceObject = Instantiate(cardPrefab, CardInitPoint.position, Quaternion.Euler(rotate), transform);
        CardInstance cardInstance = cardInstanceObject.GetComponent<CardInstance>();
        cardInstance.SetCardInfo(card);
        cardInstances.Add(cardInstanceObject);
        cardInstance.CardMove(dealTarget.GetDealPosition());

        return cardInstance;
    }
}
