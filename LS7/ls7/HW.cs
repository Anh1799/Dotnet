class HW
{
    public static void TwoSum(int target)
    {
        List<int> nums = new List<int> { 2, 7, 11, 15 };
        bool found = false;
        for (int i = 0; i < nums.Count; i++)
        {
            int complement = target - nums[i];
            for (int j = i; j < nums.Count; j++)
            {
                if (complement == nums[j])
                {
                    Console.WriteLine($"{i}, {j}");
                    found = true;
                    break;
                }
            }
            if (found) break;
        }
        if (!found)
            Console.WriteLine("Not found");
    }

    public static void TwoSumDict(int target)
    {
        List<int> nums = new List<int> { 2, 7, 11, 15 };
        Dictionary<int, int> dict = new Dictionary<int, int>();
        for (int i = 0; i < nums.Count; i++)
        {
            int need = target - nums[i];
            if (dict.ContainsKey(need))
            {
                Console.WriteLine($"{dict[need]}, {i}");
                break;
            }
            else
            {
                dict[nums[i]] = i;
            }
        }
    }

    // #3 : Input n people and input their scores -> print list and calculate the avg score -> find min and max

    public static void StudentList()
    {
        double n;
        bool isValid;
        do
        {
            Console.WriteLine("Enter total of students: ");
            isValid = double.TryParse(Console.ReadLine(), out n);
        }
        while (!isValid);
        List<Dictionary<string, object>> listStudents = new List<Dictionary<string, object>>();

        for (int i = 1; i <= n; i++)
        {
            Console.WriteLine($"Enter the name of student #{i}");
            string? name = Console.ReadLine();
            double score;
            do
            {
                Console.WriteLine($"Enter the score of {name}");
            }
            while (!double.TryParse(Console.ReadLine(), out score) ||
                                                        score < 0 ||
                                                        score > 10);
            Dictionary<string, object> student = new Dictionary<string, object>();
            student["name"] = name;
            student["score"] = score;
            listStudents.Add(student);
        }
        double totalScore = 0;
        double minScore = (double)listStudents[0]["score"];
        double maxScore = (double)listStudents[0]["score"];
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("|           List of students and scores:           |");
        Console.WriteLine("----------------------------------------------------");
        foreach (var student in listStudents)
        {
            Console.WriteLine("" + student["name"] + " : " + student["score"]);
            totalScore += (double)student["score"];
            var s = (double)student["score"];
            if (s > maxScore)
            {
                maxScore = s;
            }
            if (s < minScore)
            {
                minScore = s;
            }
        }
        double averageScore = totalScore / n;
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine($"Average socre: {averageScore}");
        Console.WriteLine("----------------------------------------------------");

        // find min and max
        Console.WriteLine($"the highest score: {maxScore}");
        Console.WriteLine($"the lowesr score: {minScore}");


    }

    public static void BuyandSellStock()
    {
        List<int> prices = new List<int>() { 1, 2, 5, 3, 6, 4 };
        int n = prices.Count();
        int minPrice = prices[0];
        int res = 0;
        int bestPrice = prices[0];
        int sellPrice = prices[0];

        for (int i = 0; i < n; i++)
        {
            int profit = prices[i] - minPrice;
            if (profit > res)
            {
                res = profit;

                bestPrice = minPrice;
                sellPrice = prices[i];
            }
            minPrice = Math.Min(minPrice, prices[i]);
        }
        Console.WriteLine($" Buy at {bestPrice}, sell at {sellPrice}");
        Console.WriteLine($"Profit: {res}");
    }


}