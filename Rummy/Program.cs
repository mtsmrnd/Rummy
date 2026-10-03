using Rummy;


Game game = new Game();

Console.WriteLine($"Round: {game.CurrentRound.RoundNumber}");
Console.WriteLine($"Current player: {game.CurrentPlayer.Name}");
Console.WriteLine($"Phase: {game.CurrentTurnPhase}");
Console.WriteLine($"Cards in hand: {game.CurrentPlayer.Hand.Count}");
Console.WriteLine($"Top discard: {game.TopDiscard}");

Console.WriteLine("\nDrawing from deck...");
game.DrawFromDeck();

Console.WriteLine($"Phase: {game.CurrentTurnPhase}");
Console.WriteLine($"Cards in hand: {game.CurrentPlayer.Hand.Count}");

Console.WriteLine("\nEnding play phase...");
game.EndPlayPhase();

Console.WriteLine($"Phase: {game.CurrentTurnPhase}");

var cardToDiscard = game.CurrentPlayer.Hand.Cards[0];

Console.WriteLine($"\nDiscarding: {cardToDiscard}");
game.DiscardFromHand(cardToDiscard);

Console.WriteLine($"Next player: {game.CurrentPlayer.Name}");
Console.WriteLine($"Phase: {game.CurrentTurnPhase}");
Console.WriteLine($"Top discard: {game.TopDiscard}");