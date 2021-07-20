
using Comfort.Common;
using EFT;

class Utils
{
    public static GameWorld GameWorld { get => Singleton<GameWorld>.Instance; }
    public static bool InGame { get => Singleton<GameWorld>.Instantiated; }
    public static Player LocalPlayer { get => GameWorld.RegisteredPlayers.Find(p => p.IsYourPlayer); }

}
