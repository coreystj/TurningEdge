using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TurningEdge.Math.Structs;
using TurningEdge.Modeling.Common.Factories;
using TurningEdge.Modeling.Common.Graphics.DataTypes;
using TurningEdge.Modeling.Common.Graphics.Structs;
using TurningEdge.Modeling.Models.Meshes.Concretes;
using TurningEdge.Modeling.Models.Readers.Concretes;

namespace TurningEdge.Modeling.Pipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create new stopwatch.
            var stopwatch = new Stopwatch();



            string name = "Model";
            var material = MaterialType.Opaque;
            var vertices = new List<Vector3>();
            var uvs = new List<Vector2>();
            var triangles = new List<int>();
            var colors = new List<Color>();

            int samples = 1000;

            for (int i = 0; i < samples; i++)
            {
                vertices.Add(new Vector3(99,99,99));
                uvs.Add(new Vector2(99,9));
                triangles.Add(99);
                colors.Add(new Color(1,1,1,1));
            }

            var meshOutput = new Mesh(name, material, vertices, uvs, triangles, colors);

            var reader = ReaderFactory.Create<MeshReader>();

            // Begin timing.
            stopwatch.Start();
            Console.WriteLine("Saving: " + meshOutput);
            string outputPath = reader.Write(meshOutput, @"E:\DotNET\TurningEdge\TurningEdge.Modeling\TurningEdge.Modeling.Pipeline\bin\Debug");
            Console.WriteLine("Saved mesh to: " + outputPath);
            stopwatch.Stop();

            Console.WriteLine("Finished saving in: " + stopwatch.Elapsed.TotalSeconds.ToString("F2") + " Second(s).");

            stopwatch.Stop();
            stopwatch.Reset();
            stopwatch.Start();
            Mesh meshInput = reader.Read(outputPath);
            Console.WriteLine("Finished reading from: " + outputPath + " " + meshInput);
            stopwatch.Stop();

            Console.WriteLine("Finished reading in: " + stopwatch.Elapsed.TotalSeconds.ToString("F2") + " Second(s).");
        }
    }
}
