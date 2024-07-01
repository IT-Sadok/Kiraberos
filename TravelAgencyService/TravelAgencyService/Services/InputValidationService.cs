namespace TravelAgencyService.Services;

public class InputValidationService
{
    public int GetValidatedIntegerInput(string message)
    {
        string? input;
        int result;
        
        do
        {
            Console.Write(message);
            input = Console.ReadLine();
        } 
        while (!IsValidInteger(input, out result));

        return result;
    }
    
    private bool IsValidInteger(string? input, out int result)
    {
        result = 0;
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        return input.All(char.IsDigit) && int.TryParse(input, out result);
    }
}