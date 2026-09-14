class HW{
    public static void TwoSum(int target){
        List<int> nums = new List<int> {2,7,11,15};
        bool found = false;
        for (int i = 0; i < nums.Count; i++){
            int complement = target - nums[i];
            for ( int j = i; j < nums.Count; j++){
                if (complement == nums[j]){
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

    public static void TwoSumDict(int target){
        List<int> nums = new List<int>{2,7,11,15};
        Dictionary<int, int> dict = new Dictionary<int, int>();
        for (int i = 0; i < nums.Count; i++){
            int need = target - nums[i];
            if (dict.ContainsKey(need)){
                Console.WriteLine($"{dict[need]}, {i}");
                break;
            }
            else {
                dict[nums[i]] = i;
            }
        }
    }
}