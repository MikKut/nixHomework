using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

/// <summary>
/// Start point of the app.
/// </summary>
public static class Program
{
    private const int MinNumberInArray = 1;
    private const int MaxNumberInArray = 26;
    private static readonly char[] LettersThatMustBeInUppercase = { 'a', 'e', 'i', 'd', 'h', 'j' };
    private static readonly char[] Alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

    /// <summary>
    /// Entry point to the application.
    /// </summary>
    public static void Main()
    {
        try
        {
            int result;
            if (!int.TryParse(Console.ReadLine(), out result))
            {
                throw new ArgumentException("it is not an integer value!");
            }
            else
            {
                DoTask(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Create an array with N elements, where N is specified from the console line.
    /// Fill it with random numbers from 1 to 26 inclusive. Create 2 arrays, where in 1 array there will be only even values, and in the second array there will be odd values.
    /// Replace the numbers in array 1 and 2 with letters of the English alphabet.
    /// The values ​​of the cells of these arrays are equal to the serial number of each letter in the alphabet.
    /// If the letter is one of the list (a, e, i, d, h, j), then it must be in upper case.
    /// Display on the screen the result of which of the arrays will have more uppercase letters.
    /// Display both arrays on the screen. Each of the arrays should be displayed with 1 line, where its values ​​will be separated by a space.
    /// </summary>
    /// <param name="arraySize">quantity of elements in initial array.</param>
    public static void DoTask(int arraySize)
    {
        if (!(arraySize > 0))
        {
            throw new ArgumentException("The array size is lower then 1");
        }

        var initialArray = CreateArrayWithRandomNumbersInRange(MinNumberInArray, MaxNumberInArray, arraySize);
        var splitedArrays = SplitArrayIntoTwoByParity(initialArray);
        var oddLetters = ConvertNumbersToLettersByThierPositionInAlphabet(splitedArrays.OddNumbers);
        var quantityOfOddUppercaseLetters = MakeSomeLettersInUppercaseAndReturnTheirQuantity(ref oddLetters);
        var evenLetters = ConvertNumbersToLettersByThierPositionInAlphabet(splitedArrays.EvenNumbers);
        var quantityOfEvenUppercaseLetters = MakeSomeLettersInUppercaseAndReturnTheirQuantity(ref evenLetters);
        DisplayWhereMoreUppercaseLetters(quantityOfEvenUppercaseLetters, quantityOfOddUppercaseLetters);
        DisplayArray(evenLetters);
        DisplayArray(oddLetters);
    }

    private static void DisplayWhereMoreUppercaseLetters(int quantityOfEvenUppercaseLetters, int quantityOfOddUppercaseLetters)
    {
        if (quantityOfEvenUppercaseLetters > quantityOfOddUppercaseLetters)
        {
            Console.WriteLine("There is more even letters there");
        }
        else if (quantityOfEvenUppercaseLetters < quantityOfOddUppercaseLetters)
        {
            Console.WriteLine("There is more odd letters there");
        }
        else
        {
            Console.WriteLine("Quantity of odd and even uppercase letters is equal");
        }

        Console.WriteLine();
    }

    private static void DisplayArray(char[] letters)
    {
        Console.WriteLine(string.Join(' ', letters));
    }

    private static int MakeSomeLettersInUppercaseAndReturnTheirQuantity(ref char[] letters)
    {
        int quantityOfUpercaseLetters = 0;
        for (int i = 0; i < letters.Length; i++)
        {
            foreach (var letter in LettersThatMustBeInUppercase)
            {
                if (letter == letters[i])
                {
                    letters[i] = char.ToUpper(letter);
                    quantityOfUpercaseLetters++;
                }
            }
        }

        return quantityOfUpercaseLetters;
    }

    private static char[] ConvertNumbersToLettersByThierPositionInAlphabet(int[] numberArray)
    {
        var charArray = new char[numberArray.Length];
        for (int i = 0; i < numberArray.Length; i++)
        {
            charArray[i] = Alphabet[numberArray[i] - 1];
        }

        return charArray;
    }

    private static int[] CreateArrayWithRandomNumbersInRange(int min, int max, int arraySize)
    {
        var randomNumbers = new int[arraySize];
        var randomNumbersGenerator = new Random();
        for (int i = 0; i < arraySize; i++)
        {
            randomNumbers[i] = randomNumbersGenerator.Next(min, max + 1);
        }

        return randomNumbers;
    }

    private static (int[] OddNumbers, int[] EvenNumbers) SplitArrayIntoTwoByParity(int[] array)
    {
        int oddArraySize = GetQuantityOfOddElements(array), evenArraySize = array.Length - oddArraySize;
        var oddNumbers = new int[oddArraySize];
        var evenNumbers = new int[evenArraySize];
        for (int mainArrayIndex = 0, oddNumbersIndex = 0, evenNumbersIndex = 0; mainArrayIndex < array.Length; mainArrayIndex++)
        {
            if (array[mainArrayIndex] % 2 == 1)
            {
                oddNumbers[oddNumbersIndex++] = array[mainArrayIndex];
            }
            else
            {
                evenNumbers[evenNumbersIndex++] = array[mainArrayIndex];
            }
        }

        return (oddNumbers, evenNumbers);
    }

    private static int GetQuantityOfOddElements(int[] array)
    {
        int oddArraySize = 0;
        foreach (var number in array)
        {
            if (number % 2 == 1)
            {
                oddArraySize++;
            }
        }

        return oddArraySize;
    }
}