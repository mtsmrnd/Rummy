namespace Rummy;

public enum Rank
{
    Ace,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Joker
}

public enum Suit
{
    Heart,
    Diamond,
    Club,
    Spade,
    Joker
}

public enum TurnPhase
{
    Draw,
    Play,
    Discard
}

public enum MeldType
{
    Set,
    Straight
}

public enum ObjectiveStatus
{
    NotCompleted,
    CompletedThisTurn,
    Active
}

public enum MeldSide
{
    Left,
    Right
}