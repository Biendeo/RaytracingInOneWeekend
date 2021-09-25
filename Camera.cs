namespace RaytracingInOneWeekend {
	class Camera {
		private Vec3 Origin { get; init; }
		private Vec3 LowerLeftCorner { get; init; }
		private Vec3 Horizontal { get; init; }
		private Vec3 Vertical { get; init; }

		private double AspectRatio { get; init; }

		private const double ViewportHeight = 2.0;
		private double ViewportWidth => ViewportHeight * AspectRatio;
		private const double FocalLength = 1.0;

		public Camera(int width, int height) {
			AspectRatio = width * 1.0 / height;
			Origin = Vec3.Zero;
			Horizontal = new Vec3(ViewportWidth, 0.0, 0.0);
			Vertical = new Vec3(0.0, ViewportHeight, 0.0);
			LowerLeftCorner = Origin - (Horizontal / 2.0) - (Vertical / 2.0) - new Vec3(0.0, 0.0, FocalLength);
		}

		public Ray3 GetRay(double u, double v) => new Ray3(Origin, LowerLeftCorner + u * Horizontal + v * Vertical - Origin);
	}
}