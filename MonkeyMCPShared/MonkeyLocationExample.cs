using MonkeyMCPShared;

namespace MonkeyMCPShared.Examples;

public class MonkeyLocationExample
{
    public static void DemonstrateMonkeyJourney()
    {
        var locationService = new MonkeyLocationService();
        
        // Example monkey data
        var baboon = new Monkey
        {
            Name = "Olive Baboon",
            Location = "Kenya",
            Latitude = -1.2921,
            Longitude = 36.8219
        };

        var howler = new Monkey
        {
            Name = "Red Howler Monkey",
            Location = "Amazon Rainforest",
            Latitude = -3.4653,
            Longitude = -62.2159
        };

        var macaque = new Monkey
        {
            Name = "Japanese Macaque",
            Location = "Honshu, Japan",
            Latitude = 36.2048,
            Longitude = 138.2529
        };

        // Generate unique journeys for each monkey
        var baboonJourney = locationService.GenerateMonkeyJourney(baboon);
        var howlerJourney = locationService.GenerateMonkeyJourney(howler);
        var macaqueJourney = locationService.GenerateMonkeyJourney(macaque);

        // Each journey will have:
        // - Unique path points based on monkey type behavior
        // - Species-specific activities (baboons grooming, howlers howling, macaques hot spring bathing)
        // - Different movement patterns (forest vs savanna vs mountain)
        // - Health stats appropriate for each species
        // - Fun random activities like banana finding, swinging, mutual grooming

        System.Console.WriteLine($"Baboon journey: {baboonJourney.Activities.Count} activities over {baboonJourney.TotalDuration.TotalHours:F1} hours");
        System.Console.WriteLine($"Howler journey: {howlerJourney.Activities.Count} activities over {howlerJourney.TotalDuration.TotalHours:F1} hours");
        System.Console.WriteLine($"Macaque journey: {macaqueJourney.Activities.Count} activities over {macaqueJourney.TotalDuration.TotalHours:F1} hours");
    }
}
