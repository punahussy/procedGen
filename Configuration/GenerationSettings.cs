namespace ProcedGenV2.Configuration;

/// <summary>Изменяемые параметры процедурной генерации уровня.</summary>
public class GenerationSettings
{
    /// <summary>Количество тайлов по горизонтали.</summary>
    public int TilesWide { get; set; } = 30;

    /// <summary>Количество тайлов по вертикали.</summary>
    public int TilesHeight { get; set; } = 15;

    /// <summary>Ветвистость уровня: чем меньше значение, тем компактнее уровень.</summary>
    public int Branching { get; set; } = 30;

    /// <summary>Максимальное число сгенерированных тайлов.</summary>
    public int MaxTileCount { get; set; } = 100;

    /// <summary>Вероятность сменить направление, заданная обратной величиной (1 из BoolChance).</summary>
    public int BoolChance { get; set; } = 4;
}
