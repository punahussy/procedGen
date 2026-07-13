namespace ProcedGenV2.Configuration;

/// <summary>Параметры процедурной генерации уровня.</summary>
public static class GenerationSettings
{
    /// <summary>Количество тайлов по горизонтали.</summary>
    public static int TilesWide => 30;

    /// <summary>Количество тайлов по вертикали.</summary>
    public static int TilesHeight => 15;

    /// <summary>Ветвистость уровня: чем меньше значение, тем компактнее уровень.</summary>
    public static int Branching => 30;

    /// <summary>Максимальное число сгенерированных тайлов.</summary>
    public static int MaxTileCount => 100;

    /// <summary>Вероятность вернуть true, заданная обратной величиной (1 из BoolChance).</summary>
    public static int BoolChance => 4;
}
