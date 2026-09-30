using UnityEngine;
using uVegas.Core.Cards;
using uVegas.UI;
public class NewEmptyCSharpScript:MonoBehaviour
{
    UICard uiCard;

    private void Start()
    {


        uiCard = GetComponent<UICard>();
        uiCard.currentCard.suit = Suit.Hearts;
        uiCard.currentCard.rank = Rank.Queen;
        uiCard.Init(uiCard.currentCard, uiCard.currentTheme);


    }
}
