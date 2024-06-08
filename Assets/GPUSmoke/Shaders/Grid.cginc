#define __GRID_TEXTURE(PREFIX) u##PREFIX##Texture
#define __GRID_TEXTURE_RW(PREFIX) u##PREFIX##Texture_RW
#define __GRID_BOUND_MIN(PREFIX) float3(u##PREFIX##BoundMin)
#define __GRID_SIZE(PREFIX) int3(u##PREFIX##GridSize)
#define __GRID_CELL_SIZE(PREFIX) float(u##PREFIX##CellSize)

// Cell Converts
#define _GRID_CELL_INT(PREFIX, CELL) int3(CELL)
#define _GRID_CELL_INT_HIGH(PREFIX, CELL) (int3(CELL) + 1)
#define _GRID_CELL_INT_FLOOR(PREFIX, CELL) int3(floor(CELL))
#define _GRID_CELL_INT_FLOOR_HIGH(PREFIX, CELL) (int3(floor(CELL)) + 1)
#define _GRID_CELL_INT_CLAMP(PREFIX, CELL) clamp(_GRID_CELL_INT(PREFIX, CELL), int3(0, 0, 0), __GRID_SIZE(PREFIX) - 1)
#define _GRID_CELL_FLT(PREFIX, CELL) float3(CELL)
#define _GRID_CELL_FLT_CENTER(PREFIX, CELL) (float3(_GRID_CELL_INT(PREFIX, CELL)) + 0.5)
#define _GRID_CELL_FLT_FLOOR_CENTER(PREFIX, CELL) (float3(_GRID_CELL_INT_FLOOR(PREFIX, CELL)) + 0.5)
#define _GRID_CELL_FLT_CLAMP(PREFIX, CELL) clamp(_GRID_CELL_FLT(PREFIX, CELL), float3(0, 0, 0), float3(__GRID_SIZE(PREFIX)))

// Cell <-> World <-> UVW
#define _GRID_CELL2WORLD(PREFIX, CELL_F, CELL) float3(float3(__GRID_CELL_##CELL_F(PREFIX, CELL)) * __GRID_CELL_SIZE(PREFIX) + __GRID_BOUND_MIN(PREFIX))
#define _GRID_WORLD2CELL(PREFIX, CELL_F, WORLD) __GRID_CELL_##CELL_F(PREFIX, (float3(WORLD) - __GRID_BOUND_MIN(PREFIX)) / __GRID_INV_CELL_SIZE(PREFIX))
#define _GRID_CELL2UVW(PREFIX, CELL_F, CELL) float3(float3(__GRID_CELL_##CELL_F(PREFIX, CELL)) / float3(__GRID_SIZE(PREFIX)))
#define _GRID_UVW2CELL(PREFIX, CELL_F, UVW) __GRID_CELL_##CELL_F(PREFIX, float3(UVW) * float3(__GRID_SIZE(PREFIX)))
#define _GRID_UVW2WORLD(PREFIX, UVW) (float3(UVW) * float3(__GRID_SIZE(PREFIX)) * __GRID_CELL_SIZE(PREFIX) + __GRID_BOUND_MIN(PREFIX))
#define _GRID_WORLD2UVW(PREFIX, WORLD) ((float3(WORLD) - __GRID_BOUND_MIN(PREFIX)) / (float3(__GRID_SIZE(PREFIX)) * __GRID_CELL_SIZE(PREFIX)))
