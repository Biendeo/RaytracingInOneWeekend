using System;
using System.Drawing;

namespace RaytracingInOneWeekend {
	class Program {
		static void Main(string[] args) {
			var scene = new RaytracedScene();

			var bitmap = scene.RenderScene(1280, 720);

			BitmapDisplayer.WriteToFile(bitmap);
		}
	}
}
