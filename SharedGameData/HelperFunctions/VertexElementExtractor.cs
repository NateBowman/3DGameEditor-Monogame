using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharedGameData.HelperFunctions
{
    public static class VertexElementExtractor
    {
        public static BoundingBox UpdateBoundingBox(Model model, Matrix worldTransform)
        {
            // Initialize minimum and maximum corners of the bounding box to max and min values
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            // For each mesh of the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // Vertex buffer parameters
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;

                    // Get vertex data as float
                    float[] vertexData = new float[vertexBufferSize / sizeof(float)];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    // Iterate through vertices (possibly) growing bounding box, all calculations are done in world space
                    for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                    {
                        Vector3 transformedPosition = Vector3.Transform(new Vector3(
                            vertexData[i], vertexData[i + 1], vertexData[i + 2]), transforms[mesh.ParentBone.Index] * worldTransform);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
            }
            // Create and return bounding box
            return new BoundingBox(min, max);
        }

        public static BoundingBox UpdateBoundingBoxFromVertexList(Model model, Matrix worldTransform, List<float[]> vertexData)
        {
            // Initialize minimum and maximum corners of the bounding box to max and min values
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            int counter = 0;
            // For each mesh of the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // Vertex buffer parameters
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;



                    // Iterate through vertices (possibly) growing bounding box, all calculations are done in world space
                    for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                    {
                        Vector3 transformedPosition = Vector3.Transform(new Vector3(
                            vertexData[counter][i], vertexData[counter][i + 1], vertexData[counter][i + 2]), transforms[mesh.ParentBone.Index] * worldTransform);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                    counter++;
                }
            }
            // Create and return bounding box
            return new BoundingBox(min, max);
        }

        public static List<float[]> GetVertexData(Model model)
        {
            List<float[]> listVertices = new List<float[]>();
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            // For each mesh of the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // Vertex buffer parameters
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;

                    // Get vertex data as float
                    float[] vertexData = new float[vertexBufferSize / sizeof(float)];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    listVertices.Add(vertexData);
                }
            }
            // Create and return bounding box
            return listVertices;
        }

        //public static Vector3[] GetVertexElement(ModelMeshPart meshPart, VertexElementUsage usage)
        //{
        //    VertexDeclaration vd = meshPart.VertexBuffer.VertexDeclaration;
        //    VertexElement[] elements = vd.GetVertexElements();

        //    Func<VertexElement, bool> elementPredicate = ve => ve.VertexElementUsage == usage && ve.VertexElementFormat == VertexElementFormat.Vector3;
        //    if (!elements.Any(elementPredicate))
        //        return null;

        //    VertexElement element = elements.First(elementPredicate);

        //    Vector3[] vertexData = new Vector3[meshPart.NumVertices];
        //    meshPart.VertexBuffer.GetData((meshPart.VertexOffset * vd.VertexStride) + element.Offset,
        //        vertexData, 0, vertexData.Length, vd.VertexStride);

        //    return vertexData;
        //}

        //private static BoundingBox? GetBoundingBox(ModelMeshPart meshPart, Matrix transform)
        //{
        //    if (meshPart.VertexBuffer == null)
        //        return null;

        //    Vector3[] positions = VertexElementExtractor.GetVertexElement(meshPart, VertexElementUsage.Position);
        //    if (positions == null)
        //        return null;

        //    Vector3[] transformedPositions = new Vector3[positions.Length];
        //    Vector3.Transform(positions, ref transform, transformedPositions);

        //    return BoundingBox.CreateFromPoints(transformedPositions);
        //}

        //public static BoundingBox CreateBoundingBox(Model model)
        //{
        //    Matrix[] boneTransforms = new Matrix[model.Bones.Count];
        //    model.CopyAbsoluteBoneTransformsTo(boneTransforms);

        //    BoundingBox result = new BoundingBox();
        //    foreach (ModelMesh mesh in model.Meshes)
        //        foreach (ModelMeshPart meshPart in mesh.MeshParts)
        //        {
        //            BoundingBox? meshPartBoundingBox = GetBoundingBox(meshPart, boneTransforms[mesh.ParentBone.Index]);
        //            if (meshPartBoundingBox != null)
        //                result = BoundingBox.CreateMerged(result, meshPartBoundingBox.Value);
        //        }
            
        //    return result;
        //}



    }
}
