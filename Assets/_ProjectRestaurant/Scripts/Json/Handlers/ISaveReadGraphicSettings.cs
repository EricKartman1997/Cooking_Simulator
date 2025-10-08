public interface ISaveReadGraphicSettings
{
    void SaveGraphicSettings(bool fullScreen, int qualityIndex, int resolutionIndex);
    
    GraphicSettings ReadOnlyGraphicSettings { get; }
    
}
