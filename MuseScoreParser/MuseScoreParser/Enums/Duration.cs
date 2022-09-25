namespace MuseScoreParser.Enums
{
    enum Duration
    {
        N32 = 3,
        N16 = N32 * 2,
        N8 = N16 * 2,
        N4 = N8 * 2,
        N2 = N4 * 2,
        N1 = N2 * 2,
        NDBL = N1 * 2,

        N64TRP = N32 / 3,
        N32TRP = N16 / 3,
        N16TRP = N8 / 3,
        N8TRP = N4 / 3,
        N4TRP = N2 / 3,
        N2TRP = N1 / 3,

        N16DOT = N16 + N32,
        N8DOT = N8 + N16,
        N4DOT = N4 + N8,
        N2DOT = N2 + N4,
        N1DOT = N1 + N2
    }
}