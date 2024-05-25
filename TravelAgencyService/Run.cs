namespace TravelAgencyService;

public class Run
{
    private static Execute _programm;

    static void Main()
    {
        _programm = new Execute();
        _programm.Run();
    }
}