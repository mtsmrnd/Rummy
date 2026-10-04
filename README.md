# Rummy

A prototype of a Rummy card game written in C#.

The current version focuses on the game rules and state: turns, meld validation, round objectives, Jokers, scoring, and moving between rounds.

It is not a complete playable game yet. There is no interactive UI, the console program is only there to demonstrate some of the game state and turn flow.

## Features

- Four-player game
- Draw → Play → Discard turn cycle
- Drawing from the deck or discard pile
- Sets and straights
- Jokers as wildcards
- Round-specific objectives
- Extending melds after completing an objective
- Multi-round game flow and scoring
- Tests for the main rules and state transitions

## Game rules

The game is divided into rounds. Each round has an objective made up of one or more melds that the player must complete before they can start adding cards to melds already on the table.

Each player starts a round with 12 cards.

On their turn, a player:

1. Draws a card from either the deck or discard pile.
2. Plays their objective, extends existing melds, or does nothing.
3. Ends the turn by discarding a card.

The goal of a round is to get rid of all the cards in your hand.

The MVP currently has two rounds:

- Round 1: two sets of three cards.
- Round 2: one set of three cards and one straight of four cards.

## Melds

### Sets

A set is three cards of the same rank. One Joker can be used as a wildcard.

For example:

```text
8♣  8♥  8♠
```

or:

```text
8♣  8♥  Joker
```

### Straights

A straight contains cards of the same suit in consecutive rank order. The current implementation supports straights of length 4 or 13.

For four-card straights, the order of the cards represents the player's intention.

For example:

```text
Joker  2♣  3♣  4♣
```

means the Joker is being used as an Ace, while:

```text
2♣  3♣  4♣  Joker
```

means it is being used as a Five.

The game logic preserves this order instead of sorting the cards automatically. An eventual UI would let the player decide this ordering.

## Turn flow

A normal turn has three phases:

```text
Draw → Play → Discard
```

During the Draw phase, the player takes a card from either the deck or the discard pile.

During the Play phase, they can complete their round objective. If they completed their objective on an earlier turn, they can also add cards to melds already on the table.

A player cannot complete their objective and immediately extend other melds during the same turn. That becomes available from their next turn onward.

Finally, the player discards one card and play moves to the next player.

## Rounds and scoring

When a player gets rid of their last card, the round ends.

Each player receives points based on the value of the cards left in their hand. Each card has face value points, except the Ace (20 pts), Joker(30 pts), and Face Cards (10 pts).

After the final round, the standings are ordered from lowest score to highest score.

## Project structure

```text
Rummy/
    Card.cs
    Deck.cs
    Game.cs
    Hand.cs
    Meld.cs
    MeldRequirement.cs
    Player.cs
    Round.cs
    Enums.cs
    Program.cs

Rummy.Tests/
```

`Game` contains the main game flow and coordinates the other domain objects.

Classes such as `Card`, `Hand`, `Meld`, `Player`, and `Round` represent the individual parts of the game.

`Rummy.Tests` contains the xUnit tests for the rules and game-state behaviour.

## Running the project

Requires the .NET 10 SDK.

Run the small console demo with:

```bash
dotnet run --project Rummy
```

Example output:

```text
Round: 1
Current player: Player 2
Phase: Draw
Cards in hand: 12
Top discard: 6♦

Drawing from deck...
Phase: Play
Cards in hand: 13

Ending play phase...
Phase: Discard

Discarding: 8♣
Next player: Player 3
Phase: Draw
Top discard: 8♣
```

The deck and starting player are randomized, so the exact output changes between runs.

Run the tests with:

```bash
dotnet test
```

## Testing

Most of the work in this project is in the game logic, so it is tested independently from the console demo.

The tests cover things such as:

- deck and hand behaviour;
- turn-phase transitions;
- set and straight validation;
- Joker behaviour;
- card ownership and duplicate card use;
- playing and extending melds;
- round objectives;
- scoring;
- round transitions and game-over behaviour.

## Current scope

This is currently a rules prototype rather than a finished game.

There is no interactive interface, AI opponent, networking, or save system. The focus for this version was getting the underlying game rules and state transitions working and tested.