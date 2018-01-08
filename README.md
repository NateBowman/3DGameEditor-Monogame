# Added Features
Un-inverted Camera (horizontal) pan.

`ITransformable.Name` was renamed to `MeshName`

Prop `Name` was addded to SceneEntity as a hierarchy label.

## Terrain
TerrainEntity: SceneEntity

Terrain uses a .tif with an alpha layer embedded

If a terrain exists in the scene, grid based placement is replaced with terrain based placement of objects.

- Alpha channel is height map
- RGB channels are texture data
- Terrain is locked to the custom Terrain Shader
- Terrain Shader is not selectable by other meshes

## Hierarchy
Scene graph is represented in a TreeView.

- Nodes can be drag/drop parented and will move / scale with parents.
- An Object in the scene can be selected in the tree.
- Selection root (first selected) in the scene is selected in the TreeView
- Right click context menu (delete)
- Deletion of a parent will delete children

## Drawing Mode
Drawing Mode can be switched between wireframe and shaded with a dropdown button.

## Pivot Mode
Gizmo pivot mode can be selected with a drop down button.

## Grid toggle
Grid can be toggled on/off with a toggle button on the toolbar.

## Snap toggle
Translate/Scale snapping can be toggled on/off with a toggle button on the toolbar.

## Mesh/Terrain Asset import
Meshes (fbx/png) / Terrains (tif) can be imported/deleted with the import box in the bottom right of the editor.

- Mesh / Terrains can be imported.
- Mesh / Terrains can be deleted (removes all dependant scene objects)
- Right click context menu (delete)

## Mesh Info
Detailed mesh/meshpart info is currently available in the bottom right for a selected mesh (part of an unfinished feature)

## Save / Load
Save load works for custom terrains assets

Models have their root bone scaled by 0.1f and rotations reset (origin rotation should be frozen by a 3D artist pre-import)

## Property Grid
Readonly tags were added to non-editable options

SceneEntites implement `INotifyPropertyChanged` for some properties. 