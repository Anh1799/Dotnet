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
}