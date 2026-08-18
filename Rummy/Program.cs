using Rummy;
using System.Transactions;

Game game = new Game();

////for (int i = 0; i < 4; i++) {
////    Console.WriteLine($"{game.Players[i].Name} hand is: ");
////    int count = game.Players[i].Hand.Cards.Count;
////    for (int j = 0; j < count; j++) {
////        Console.WriteLine($"{game.Players[i].Hand.Cards[j]} ");
////    }
////        Console.WriteLine("---");
////}
////Console.WriteLine($"{game.Deck.Count} cards remaining in deck");
////Console.WriteLine($"Starter player: {game.CurrentPlayer.Name}");
////for (int i = 0; i < 10; i++)
////{
////    game.AdvanceTurn();
////    Console.WriteLine($"Current player: {game.CurrentPlayer.Name}");
////}

//Console.WriteLine($"Initial player: {game.CurrentPlayer.Name}");
//Console.WriteLine($"Intial phase: {game.CurrentTurnPhase}");
//Console.WriteLine($"Discard pile top: {game.TopDiscard}");
//Console.WriteLine($"Cards in discard pile: {game.DiscardPile.Count}");
//Console.WriteLine($"Cards in hand: {game.CurrentPlayer.Hand.Count}");
////Console.WriteLine($"Intial hand: {game.CurrentPlayer.Hand.ToString()}");
////game.DrawFromDiscardPile();
////Console.WriteLine($"DrawnCard: {game.CurrentPlayer.Hand.Cards[12]}");
////Console.WriteLine($"Cards in discard pile: {game.DiscardPile.Count}");
////Console.WriteLine($"New hand: {game.CurrentPlayer.Hand.ToString()}");
////Console.WriteLine($"Cards in hand: {game.CurrentPlayer.Hand.Count}");
////Console.WriteLine($"Current phase: {game.CurrentTurnPhase}");
////game.EndPlayPhase();
////Console.WriteLine($"Ended Play phase");
////Console.WriteLine($"Current phase: {game.CurrentTurnPhase}");
////Console.WriteLine($"Cards: {game.CurrentPlayer.Hand.ToString()}");
////Console.Write("Select the number of the card you want to discard: ");
////int indexToDiscard = int.Parse(Console.ReadLine());
////Card cardToDiscard = game.CurrentPlayer.Hand.Cards[indexToDiscard - 1];
////game.DiscardFromHand(cardToDiscard);
////Console.WriteLine($"discarded: {cardToDiscard}");
////Console.WriteLine($"Cards in hand: {game.CurrentPlayer.Hand.Count}");
////Console.WriteLine($"Cards: {game.CurrentPlayer.Hand.ToString()}");

//game.DrawFromDeck();
//Console.WriteLine("Drawn from deck");
//Console.WriteLine($"Current phase: {game.CurrentTurnPhase}");
//Console.WriteLine($"Cards in hand: {game.CurrentPlayer.Hand.Count}");
//Console.WriteLine($"New hand: {game.CurrentPlayer.Hand.ToString()}");
//List<Card> cardsToMeld = new List<Card> { game.CurrentPlayer.Hand.Cards[0], game.CurrentPlayer.Hand.Cards[1], game.CurrentPlayer.Hand.Cards[2] };
//game.PlayMeld(MeldType.Set, cardsToMeld);
//Meld meld = game.CurrentRound.Melds[0];
//Console.Write("New meld is : ");
//foreach (Card card in meld.Cards)
//{
//    Console.Write(card);
//    Console.Write(" ");
//}
//Console.WriteLine();
//Console.WriteLine($"New hand: {game.CurrentPlayer.Hand.ToString()}");
//Console.WriteLine($"Cards in hand: {game.CurrentPlayer.Hand.Count}");

Hand check = new Hand();

check.AddCard(new Card(Rank.Ace, Suit.Club));
check.AddCard(new Card(Rank.Two, Suit.Club));
check.AddCard(new Card(Rank.Three, Suit.Club));
check.AddCard(new Card(Rank.Four, Suit.Club));
//check.AddCard(new Card(Rank.Five, Suit.Club));
//check.AddCard(new Card(Rank.Six, Suit.Club));
//check.AddCard(new Card(Rank.Seven, Suit.Club));
//check.AddCard(new Card(Rank.Eight, Suit.Club));
//check.AddCard(new Card(Rank.Nine, Suit.Club));
//check.AddCard(new Card(Rank.Ten, Suit.Club));
//check.AddCard(new Card(Rank.Jack, Suit.Club));
//check.AddCard(new Card(Rank.Queen, Suit.Club));
//check.AddCard(new Card(Rank.King, Suit.Club));
//check.AddCard(new Card(Rank.Joker, Suit.Joker));
//check.AddCard(new Card(Rank.Joker, Suit.Joker));
//Console.WriteLine($"{game.IsValidStraight(check.Cards)}");
//check.AddCard(new Card(Rank.King, Suit.Club));
//check.AddCard(new Card(Rank.King, Suit.Club));
//check.AddCard(new Card(Rank.King, Suit.Club));
//check.AddCard(new Card(Rank.Queen, Suit.Club));
//check.AddCard(new Card(Rank.Queen, Suit.Club));
//check.AddCard(new Card(Rank.Jack, Suit.Club));
//game.CurrentPlayer.Hand = check;

//game.DrawFromDeck();

//Meld? meld1 = game.CreateMeldIfValid(game.CurrentRound.MeldRequirements[0].MeldType, new List<Card> { check.Cards[0], check.Cards[1], check.Cards[2] });
//Meld? meld2 = game.CreateMeldIfValid(game.CurrentRound.MeldRequirements[0].MeldType, new List<Card> { check.Cards[3], check.Cards[4], check.Cards[5] });

//if (meld2 == null)
//{
//    Console.WriteLine("HELPPP");
//}
//game.TryPlayObjective(new List<Meld> { meld1, meld2 });

//Console.WriteLine("Hand: ");
//Console.WriteLine($"{game.CurrentPlayer.Hand}");
//Console.WriteLine("Melds: ");
//foreach (Meld meld in game.CurrentRound.Melds)
//{

//    Console.WriteLine($"{meld}");
//}

//Console.WriteLine($"Objective: {game.CurrentPlayer.RoundObjectiveCompleted}");


Meld meld = new Meld(MeldType.Straight, game.CurrentPlayer, new List<Card> { check.Cards[0], check.Cards[1], check.Cards[2] });

Console.WriteLine("Cards in Meld: ");
foreach (Card card in meld.Cards)
{
    Console.WriteLine($"{card}");
}
meld.AddCard(check.Cards[3], MeldSide.Left);
Console.WriteLine("Cards in Meld: ");
foreach (Card card in meld.Cards)
{
    Console.WriteLine($"{card}");
}