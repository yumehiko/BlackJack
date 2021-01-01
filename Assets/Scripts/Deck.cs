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
    private List<GameObject> cardInstances = new List<GameObject>();

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
    public void DealCardAnime(Card card, Player dealTarget, bool isReverse)
    {
        Vector3 towardPosition = dealTarget.GetDealPosition();
        towardPosition.z = -5.0f;
        GameObject cardInstance = Instantiate(cardPrefab, CardInitPoint.position, Quaternion.identity, transform);
        cardInstance.GetComponent<CardInstance>().SetCardInfo(card, isReverse);
        cardInstances.Add(cardInstance);
        
        cardInstance.transform.DOMove(towardPosition, 1.0f);
    }
}
