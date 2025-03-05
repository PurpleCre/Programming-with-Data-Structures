public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Problem 1 Start

        // create an empty array varible to store the returned values
        var multiples = new double[length];
        // create a loop that runs as many times as the length parameter
        // we instantiate i as 1 to make it easier when multiplying
        for (int i = 1; i <= length; i++)
        {
            // multiply the given number with the iterator and store result in multiple variable
            var multiple = number * i;
            // add multiple to multiples array using index method, here we subtract 1 from the iterator since index is 0 based
            multiples[i-1] = multiple;
        }
        // return multiples array
        return multiples;

        // Problem 1 End
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Problem 2 Start
 
        // get the elements to be rotated from the data list and store them in a temp list
        var temp = data.GetRange(data.Count - amount, amount);
        // remove the elements to be rotated from the data list
        data.RemoveRange(data.Count - amount, amount);
        // add the temp list to the beginning of the data list
        data.InsertRange(0, temp);

        // Problem 2 End
    }
}
