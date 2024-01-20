namespace Studio.Settings
{
    public enum SceneNames
    {
        Unknown,

        Splash,
        Menu,
        Game,
        Loading,
        Empty,
    }

    public enum GameStates
    {
        Unknown,

        Splash,
        Menu,
        Game,
    }

    public enum SpreadsheetDataType
    {
        Localization,
    }

    public enum CacheType
    {
        UserLocalData,
        AppSettingsData,
        PurchaseData,
    }

    public enum PurchasingType
    {
        Unknown = -1,

        RemoveAds = 0,
    }

    public enum Languages
    {
        Ukrainian = 38,
        Russian = 30,
        English = 10,
    }

    public enum LogTypes
    {
        Unknown,

        Info,
        Warning,
        Error,
        Debug,
    }

    public enum Sounds
    {
        Unknown,

        ButtonDown,
        ButtonUp,
        ButtonClick,
        ShowView,
        HideView,
        MenuBackground,
        GameBackground,
        LoadingBackground,
    }
}