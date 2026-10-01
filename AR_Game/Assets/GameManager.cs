using System;
using System.Collections.Generic;
using UnityEngine;
using uVegas.Core.Cards;
using static GameManager;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.PostProcessing.HistogramMonitor;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private GameObject gamePanel;      
    [SerializeField] private GameObject placeHint;      
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private List<Card> cards = new();

    public enum GameState
    { PlayerTurn, DealerTurn, Win, Lose, EndOfRound, Bet}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class Deck
{
    private List<Card> cards_ = new();
    public int GetCardsCount() { return cards_.Count; }
    public Deck() 
    {
        //Cerate deck
        foreach (Suit s in Enum.GetValues(typeof(Suit))) 
        {
            for (int i = 0; i < 13; i++) 
            {
                cards_.Add(new Card(s, (Rank)i));
            }
        }

        //Shuffle w fisher-yates
        for (int i = cards_.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (cards_[i], cards_[j]) = (cards_[j], cards_[i]);
        }

    }
}
public class Hand
{
    public List<Card> cards = new();

    int GetValue() 
    {
        int total = 0;
        bool hasAce = false;

        foreach (Card card in cards) 
        {
            total += (int)card.rank;
            if ((int)card.rank == 1) 
            {
                hasAce = true; 
            }
            if (hasAce && total + 10 <= 21) total += 10; //Ace is only worth eleven when it will make a blackjack
            return total;

        }
        return total;
    }

    public bool IsBust() { return GetValue() > 21; }
    public bool IsBlackJack() { return cards.Count == 2 && GetValue() == 21; ; }

}
public class Game 
{
    public int startChips = 100;
    public int targetChips = 500;
    public int betValue = 10;

    public int chips;
    public int bet;
    public GameState State;

    public Hand Player = new();
    public Hand Dealer = new();

    private Deck _deck = new();

    public void Reset()
    {
        _deck = new Deck();
        chips = startChips;
        bet = betValue;
        Player.cards.Clear();
        Dealer.cards.Clear();
        State = GameState.PlayerTurn;
    }
    public void Deal()
    {
        if (State != GameState.Bet || bet > chips) return;

        if (_deck.GetCardsCount() < 15) _deck = new Deck();
        Player.cards.Clear();
        Dealer.cards.Clear();


    }


}