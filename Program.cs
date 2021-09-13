using System;
using System.Drawing;

namespace RaytracingInOneWeekend {
	class Program {
		static void Main(string[] args) {
			var scene = new RaytracedScene();

			var bitmap = scene.RenderScene(1024, 1024);

			BitmapDisplayer.WriteToFile(bitmap);
		}
	}
}
