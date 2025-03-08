using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LayoutConst
{
    // update values when we agree on a standard, use to simplify calculations
    public const double TileH = 1.0; // height of tiles for stage layout
    public const double TileInnerW = 2.0; // width of middle (traversable) tile for stage layout
    public const double TileOuterW = 1.0; // width of outer (wall) tiles for stage layout
}
