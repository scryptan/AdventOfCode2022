namespace AdventOfCode2024.Solutions;

public class Day4
{
    class Card
    {
        public int Id { get; set; }
        public List<int> WinningDeck { get; set; }
        public List<int> CurrentDeck { get; set; }
        public int Count { get; set; }

        public Card(int id, List<int> WinningDeck, List<int> CurrentDeck, int Count = 1)
        {
            this.Id = id;
            this.WinningDeck = WinningDeck;
            this.CurrentDeck = CurrentDeck;
            this.Count = Count;
        }
    }

    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n")
            .Select(x => x.Trim())
            .ToList();

        var cards = new List<Card>();
        foreach (var line in input)
        {
            cards.Add(ParseCard(line));
        }

        Console.WriteLine(Part1(cards));
        Console.WriteLine(Part2(cards));
    }

    private Card ParseCard(string line) // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    {
        var parts = line.Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var id = int.Parse(
            parts[0].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]);
        var deck = parts[1].Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var winningDeck = deck[0].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToList();
        var currentDeck = deck[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToList();
        return new Card(id, winningDeck, currentDeck);
    }

    private long CalculateCountWinningCards(Card card)
        => card.WinningDeck.Count(x => card.CurrentDeck.Contains(x));

    private long CalculateCardScore(Card card)
        => (long) Math.Pow(2, CalculateCountWinningCards(card) - 1);

    private string Part1(List<Card> cards)
    {
        var res = 0L;

        foreach (var card in cards)
        {
            res += CalculateCardScore(card);
        }

        return $"Part 1: {res}";
    }

    private string Part2(List<Card> cards)
    {
        var res = 0L;

        foreach (var card in cards)
        {
            var score = CalculateCountWinningCards(card);
            if (score > 0)
            {
                var cardsToIncrement = cards.Where(x => x.Id > card.Id
                                                        && x.Id <= card.Id + score).ToList();
                foreach (var cardToIncrement in cardsToIncrement)
                {
                    cardToIncrement.Count += 1 * card.Count;
                }
            }
        }

        res = cards.Sum(x => x.Count);

        return $"Part 2: {res}";
    }
}