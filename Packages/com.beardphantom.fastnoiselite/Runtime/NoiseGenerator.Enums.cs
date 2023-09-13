namespace FastNoise
{
    public enum NoiseType
    {
        OpenSimplex2,
        OpenSimplex2S,
        Cellular,
        Perlin,
        ValueCubic,
        Value
    }

    public enum RotationType3D
    {
        None,
        ImproveXYPlanes,
        ImproveXZPlanes
    }

    public enum FractalType
    {
        None,
        FBm,
        Ridged,
        PingPong,
        DomainWarpProgressive,
        DomainWarpIndependent
    }

    public enum CellularDistanceFunction
    {
        Euclidean,
        EuclideanSq,
        Manhattan,
        Hybrid
    }

    public enum CellularReturnType
    {
        CellValue,
        Distance,
        Distance2,
        Distance2Add,
        Distance2Sub,
        Distance2Mul,
        Distance2Div
    }

    public enum DomainWarpType
    {
        OpenSimplex2,
        OpenSimplex2Reduced,
        BasicGrid
    }

    internal enum TransformType3D
    {
        None,
        ImproveXYPlanes,
        ImproveXZPlanes,
        DefaultOpenSimplex2
    }
}