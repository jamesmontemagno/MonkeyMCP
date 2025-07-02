using System.Text.Json.Serialization;

namespace MonkeyMCPShared;

public class MonkeyLocationService
{
    private readonly Random random = new();
    private readonly Dictionary<string, MonkeyTypeConfig> monkeyConfigs;

    public MonkeyLocationService()
    {
        monkeyConfigs = InitializeMonkeyConfigs();
    }

    public MonkeyJourney GenerateMonkeyJourney(Monkey monkey)
    {
        var baseLocation = new GeoLocation(monkey.Latitude, monkey.Longitude);
        var config = GetMonkeyConfig(monkey.Name ?? "Unknown");
        
        var journey = new MonkeyJourney
        {
            MonkeyName = monkey.Name ?? "Unknown",
            StartLocation = baseLocation,
            StartTime = DateTime.UtcNow.AddHours(-random.Next(1, 12)),
            PathPoints = GeneratePathPoints(baseLocation, config),
            Activities = new List<MonkeyActivity>(),
            HealthStats = GenerateHealthStats(config)
        };

        journey.Activities = GenerateActivities(journey.PathPoints, config);
        journey.EndTime = journey.StartTime.AddMinutes(journey.PathPoints.Count * random.Next(5, 15));

        return journey;
    }

    private List<PathPoint> GeneratePathPoints(GeoLocation startLocation, MonkeyTypeConfig config)
    {
        var pathPoints = new List<PathPoint> { new() { Location = startLocation, Timestamp = DateTime.UtcNow.AddHours(-random.Next(1, 12)) } };
        
        var currentLat = startLocation.Latitude;
        var currentLon = startLocation.Longitude;
        var numPoints = random.Next(5, 15);

        for (int i = 1; i <= numPoints; i++)
        {
            // Generate movement based on monkey type behavior
            var moveDistance = config.MaxMovementRadius * random.NextDouble();
            var moveAngle = random.NextDouble() * 2 * Math.PI;
            
            // Add some variation based on monkey type
            if (config.PreferredTerrain == "Forest")
            {
                moveDistance *= 0.7; // Forest monkeys move shorter distances
            }
            else if (config.PreferredTerrain == "Mountain")
            {
                moveDistance *= 1.2; // Mountain monkeys cover more ground
            }

            currentLat += moveDistance * Math.Cos(moveAngle) * 0.001; // Convert to rough lat/lon degrees
            currentLon += moveDistance * Math.Sin(moveAngle) * 0.001;

            pathPoints.Add(new PathPoint
            {
                Location = new GeoLocation(currentLat, currentLon),
                Timestamp = pathPoints[0].Timestamp.AddMinutes(i * random.Next(10, 30))
            });
        }

        return pathPoints;
    }

    private List<MonkeyActivity> GenerateActivities(List<PathPoint> pathPoints, MonkeyTypeConfig config)
    {
        var activities = new List<MonkeyActivity>();
        var activityChance = 0.4; // 40% chance at each point

        for (int i = 1; i < pathPoints.Count; i++)
        {
            if (random.NextDouble() < activityChance)
            {
                var activity = SelectRandomActivity(config);
                activities.Add(new MonkeyActivity
                {
                    Type = activity.Type,
                    Description = activity.Description,
                    Location = pathPoints[i].Location,
                    Timestamp = pathPoints[i].Timestamp,
                    Duration = TimeSpan.FromMinutes(random.Next(activity.MinDuration, activity.MaxDuration + 1)),
                    EnergyChange = activity.EnergyChange + random.Next(-5, 6)
                });
            }
        }

        return activities;
    }

    private MonkeyHealthStats GenerateHealthStats(MonkeyTypeConfig config)
    {
        return new MonkeyHealthStats
        {
            Energy = random.Next(config.BaseEnergy - 20, config.BaseEnergy + 21),
            Happiness = random.Next(60, 101),
            Hunger = random.Next(20, 80),
            Social = random.Next(config.BaseSocial - 15, config.BaseSocial + 16),
            Stress = random.Next(10, 50),
            Health = random.Next(80, 101)
        };
    }

    private ActivityTemplate SelectRandomActivity(MonkeyTypeConfig config)
    {
        var weightedActivities = new List<ActivityTemplate>();
        
        // Add common activities
        weightedActivities.AddRange(GetCommonActivities());
        
        // Add species-specific activities with higher weight
        for (int i = 0; i < 3; i++)
        {
            weightedActivities.AddRange(config.PreferredActivities);
        }

        return weightedActivities[random.Next(weightedActivities.Count)];
    }

    private List<ActivityTemplate> GetCommonActivities()
    {
        return new List<ActivityTemplate>
        {
            new() { Type = "Foraging", Description = "Found some delicious fruits and ate them", MinDuration = 5, MaxDuration = 20, EnergyChange = 10 },
            new() { Type = "Resting", Description = "Took a well-deserved nap under a tree", MinDuration = 15, MaxDuration = 45, EnergyChange = 15 },
            new() { Type = "Exploring", Description = "Investigated an interesting rock formation", MinDuration = 5, MaxDuration = 15, EnergyChange = -5 },
            new() { Type = "Drinking", Description = "Found a water source and had a refreshing drink", MinDuration = 2, MaxDuration = 8, EnergyChange = 5 },
            new() { Type = "Climbing", Description = "Climbed a tall tree to get a better view", MinDuration = 5, MaxDuration = 15, EnergyChange = -10 }
        };
    }

    private MonkeyTypeConfig GetMonkeyConfig(string monkeyName)
    {
        // Try to match by partial name for known monkey types
        foreach (var config in monkeyConfigs)
        {
            if (monkeyName.Contains(config.Key, StringComparison.OrdinalIgnoreCase))
            {
                return config.Value;
            }
        }
        
        // Default config for unknown types
        return monkeyConfigs["Default"];
    }

    private Dictionary<string, MonkeyTypeConfig> InitializeMonkeyConfigs()
    {
        return new Dictionary<string, MonkeyTypeConfig>
        {
            {
                "Baboon",
                new MonkeyTypeConfig
                {
                    MaxMovementRadius = 2.0,
                    PreferredTerrain = "Savanna",
                    BaseEnergy = 80,
                    BaseSocial = 90,
                    PreferredActivities = new List<ActivityTemplate>
                    {
                        new() { Type = "Grooming", Description = "Spent time grooming another baboon's fur", MinDuration = 10, MaxDuration = 30, EnergyChange = 5 },
                        new() { Type = "Socializing", Description = "Had an animated conversation with the troop", MinDuration = 15, MaxDuration = 25, EnergyChange = 0 },
                        new() { Type = "Foraging", Description = "Dug up some tasty roots and tubers", MinDuration = 10, MaxDuration = 25, EnergyChange = 15 }
                    }
                }
            },
            {
                "Howler",
                new MonkeyTypeConfig
                {
                    MaxMovementRadius = 1.5,
                    PreferredTerrain = "Forest",
                    BaseEnergy = 70,
                    BaseSocial = 75,
                    PreferredActivities = new List<ActivityTemplate>
                    {
                        new() { Type = "Howling", Description = "Let out a mighty roar to communicate with distant howlers", MinDuration = 2, MaxDuration = 5, EnergyChange = -5 },
                        new() { Type = "Leaf Eating", Description = "Munched on some fresh, tender leaves", MinDuration = 15, MaxDuration = 35, EnergyChange = 12 },
                        new() { Type = "Territory Marking", Description = "Marked territory with scent to ward off intruders", MinDuration = 3, MaxDuration = 8, EnergyChange = -3 }
                    }
                }
            },
            {
                "Japanese Macaque",
                new MonkeyTypeConfig
                {
                    MaxMovementRadius = 1.8,
                    PreferredTerrain = "Mountain",
                    BaseEnergy = 85,
                    BaseSocial = 85,
                    PreferredActivities = new List<ActivityTemplate>
                    {
                        new() { Type = "Hot Spring Bath", Description = "Relaxed in a natural hot spring", MinDuration = 20, MaxDuration = 45, EnergyChange = 20 },
                        new() { Type = "Snow Play", Description = "Made and threw snowballs for fun", MinDuration = 10, MaxDuration = 20, EnergyChange = -8 },
                        new() { Type = "Potato Washing", Description = "Carefully washed sweet potatoes in a stream", MinDuration = 5, MaxDuration = 12, EnergyChange = 8 }
                    }
                }
            },
            {
                "Proboscis",
                new MonkeyTypeConfig
                {
                    MaxMovementRadius = 1.2,
                    PreferredTerrain = "Mangrove",
                    BaseEnergy = 75,
                    BaseSocial = 70,
                    PreferredActivities = new List<ActivityTemplate>
                    {
                        new() { Type = "Swimming", Description = "Took a refreshing swim across a river", MinDuration = 8, MaxDuration = 20, EnergyChange = -5 },
                        new() { Type = "Nose Flexing", Description = "Showed off impressive nose size to potential mates", MinDuration = 3, MaxDuration = 8, EnergyChange = -2 },
                        new() { Type = "Mangrove Foraging", Description = "Found delicious mangrove fruits and seeds", MinDuration = 12, MaxDuration = 25, EnergyChange = 14 }
                    }
                }
            },
            {
                "Golden Snub-Nosed",
                new MonkeyTypeConfig
                {
                    MaxMovementRadius = 2.5,
                    PreferredTerrain = "Mountain",
                    BaseEnergy = 80,
                    BaseSocial = 80,
                    PreferredActivities = new List<ActivityTemplate>
                    {
                        new() { Type = "High Altitude Foraging", Description = "Searched for lichens and bark in the treetops", MinDuration = 15, MaxDuration = 30, EnergyChange = 10 },
                        new() { Type = "Huddling", Description = "Huddled with family for warmth in the cold mountain air", MinDuration = 20, MaxDuration = 40, EnergyChange = 8 },
                        new() { Type = "Acrobatic Display", Description = "Performed impressive acrobatic moves between branches", MinDuration = 5, MaxDuration = 15, EnergyChange = -10 }
                    }
                }
            },
            {
                "Default",
                new MonkeyTypeConfig
                {
                    MaxMovementRadius = 1.5,
                    PreferredTerrain = "Forest",
                    BaseEnergy = 75,
                    BaseSocial = 75,
                    PreferredActivities = new List<ActivityTemplate>
                    {
                        new() { Type = "Banana Finding", Description = "Discovered a perfectly ripe banana and enjoyed every bite", MinDuration = 3, MaxDuration = 8, EnergyChange = 12 },
                        new() { Type = "Swinging", Description = "Swung gracefully from vine to vine", MinDuration = 5, MaxDuration = 15, EnergyChange = -5 },
                        new() { Type = "Mutual Grooming", Description = "Groomed a friend and got groomed in return", MinDuration = 10, MaxDuration = 25, EnergyChange = 8 }
                    }
                }
            }
        };
    }
}

public class MonkeyJourney
{
    public string MonkeyName { get; set; } = string.Empty;
    public GeoLocation StartLocation { get; set; } = new();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<PathPoint> PathPoints { get; set; } = new();
    public List<MonkeyActivity> Activities { get; set; } = new();
    public MonkeyHealthStats HealthStats { get; set; } = new();
    public TimeSpan TotalDuration => EndTime - StartTime;
    public double TotalDistanceKm => CalculateTotalDistance();

    private double CalculateTotalDistance()
    {
        double total = 0;
        for (int i = 1; i < PathPoints.Count; i++)
        {
            total += PathPoints[i - 1].Location.DistanceTo(PathPoints[i].Location);
        }
        return total;
    }
}

public class PathPoint
{
    public GeoLocation Location { get; set; } = new();
    public DateTime Timestamp { get; set; }
}

public class MonkeyActivity
{
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public GeoLocation Location { get; set; } = new();
    public DateTime Timestamp { get; set; }
    public TimeSpan Duration { get; set; }
    public int EnergyChange { get; set; }
}

public class MonkeyHealthStats
{
    public int Energy { get; set; } // 0-100
    public int Happiness { get; set; } // 0-100
    public int Hunger { get; set; } // 0-100 (higher = more hungry)
    public int Social { get; set; } // 0-100
    public int Stress { get; set; } // 0-100 (higher = more stressed)
    public int Health { get; set; } // 0-100
}

public class GeoLocation
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public GeoLocation() { }
    
    public GeoLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double DistanceTo(GeoLocation other)
    {
        // Haversine formula for distance calculation
        const double R = 6371; // Earth's radius in kilometers
        var dLat = (other.Latitude - Latitude) * Math.PI / 180;
        var dLon = (other.Longitude - Longitude) * Math.PI / 180;
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(Latitude * Math.PI / 180) * Math.Cos(other.Latitude * Math.PI / 180) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }
}

internal class MonkeyTypeConfig
{
    public double MaxMovementRadius { get; set; }
    public string PreferredTerrain { get; set; } = string.Empty;
    public int BaseEnergy { get; set; }
    public int BaseSocial { get; set; }
    public List<ActivityTemplate> PreferredActivities { get; set; } = new();
}

internal class ActivityTemplate
{
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinDuration { get; set; }
    public int MaxDuration { get; set; }
    public int EnergyChange { get; set; }
}

[JsonSerializable(typeof(MonkeyJourney))]
[JsonSerializable(typeof(List<MonkeyJourney>))]
[JsonSerializable(typeof(MonkeyActivity))]
[JsonSerializable(typeof(MonkeyHealthStats))]
[JsonSerializable(typeof(GeoLocation))]
[JsonSerializable(typeof(PathPoint))]
public partial class MonkeyLocationContext : JsonSerializerContext
{
}
