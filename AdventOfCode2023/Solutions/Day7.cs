namespace AdventOfCode2023.Solutions;

public class Day7
{
    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n")
            .Select(x => x.Trim().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .ToList();


        Console.WriteLine(Part1(input));
        Console.WriteLine(Part2());
    }

    private string Part1(List<string[]> input)
    {
        var res = "";
        var lines = new List<Line>(input.Count);
        foreach (var line in input)
        {
            lines.Add(new Line()
            {
                Bid = int.Parse(line[1]),
                Hand = new Hand(line[0])
            });
        }

        var rank = 1;
        foreach (var line in lines.ToLookup(x => x.Hand.Type).OrderBy(x => x.Key))
        {
            if (line.Count() == 1)
            {
                line.Single().Hand.Rank = rank++;
                continue;
            }

            var hands = line.ToList();
            hands.Sort(new HandComparer());

            foreach (var hand in hands)
            {
                hand.Hand.Rank = rank++;
            }
        }
        
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }

        res = lines.Sum(x => x.CalculateLine()).ToString();
        return $"Part 1: {res}";
    }

    private string Part2()
    {
        var res = "";

        return $"Part 2: \n{res}";
    }

    class Line
    {
        public int Bid { get; set; }
        public Hand Hand { get; set; } = null!;

        public override string ToString()
        {
            return $"{Bid} {Hand}";
        }

        public long CalculateLine() => Bid * Hand.Rank;
    }

    class HandComparer : IComparer<Line>
    {
        public int Compare(Line? x, Line? y)
        {
            // ret > 0 -> x > y
            return x!.Hand.IsGreaterThan(y!.Hand) ? 1 : -1;
        }
    }

    class Hand
    {
        private static readonly string[] _cards =
            new[] { "A", "K", "Q", "J", "T", "9", "8", "7", "6", "5", "4", "3", "2" }.Reverse()
                .ToArray();

        public string[] Cards { get; private set; }
        public HandType Type { get; private set; }
        public int Rank { get; set; }

        public Hand(string cards)
        {
            Cards = cards.ToCharArray().Select(x => x.ToString()).ToArray();
            CalculateRank();
        }

        public bool IsGreaterThan(Hand hand)
        {
            if (Type == hand.Type)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    var myCard = Array.IndexOf(_cards, Cards[i]);
                    var otherCard = Array.IndexOf(_cards, hand.Cards[i]);

                    if (myCard == otherCard) continue;

                    return myCard > otherCard;
                }
            }

            return Type > hand.Type;
        }

        private void CalculateRank()
        {
            var dict = new Dictionary<string, int>();

            foreach (var card in Cards)
            {
                dict.TryAdd(card, Cards.Count(x => x == card));
            }

            if (dict.Keys.Count == Cards.Length)
            {
                Type = HandType.HighCard;
                return;
            }

            if (dict.Keys.Count == 1)
            {
                Type = HandType.FiveKind;
                return;
            }

            if (dict.Keys.Count == 2)
            {
                Type = dict.Values.Contains(4) ? HandType.FourKind : HandType.FullHouse;
                return;
            }

            if (dict.Keys.Count == 3)
            {
                Type = dict.Values.Contains(3) ? HandType.ThreeKind : HandType.TwoPair;
                return;
            }

            Type = HandType.OnePair;
        }

        public override string ToString()
        {
            return $"{string.Join(string.Empty, Cards)}, type: {Type}, rank: {Rank}";
        }
    }

    enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeKind,
        FullHouse,
        FourKind,
        FiveKind
    }
}