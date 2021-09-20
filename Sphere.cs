using System;

namespace RaytracingInOneWeekend {
	struct Sphere : IHittable {
		public Vec3 Centre { get; set; }
		public double Radius { get; set; }

		public Sphere(Vec3 centre, double radius) {
			Centre = centre;
			Radius = radius;
		}

		public bool Hit(Ray3 ray, double tMin, double tMax, out HitRecord record) {
			record = new HitRecord(Vec3.Zero, Vec3.Zero, 0.0, false);

			Vec3 oc = ray.Origin - Centre;
			double a = ray.Direction.SquaredMagnitude;
			double halfB = Vec3.Dot(oc, ray.Direction);
			double c = oc.SquaredMagnitude - Radius * Radius;

			double discriminant = halfB * halfB - a * c;
			if (discriminant < 0.0) {
				return false;
			}
			double squareRootDiscriminant = Math.Sqrt(discriminant);

			double root = (-halfB - squareRootDiscriminant) / a;
			if (root < tMin || tMax < root) {
				root = (-halfB + squareRootDiscriminant) / a;
				if (root < tMin || tMax < root) {
					return false;
				}
			}

			Vec3 outwardNormal = (ray.At(root) - Centre) / Radius;
			bool isFrontFace = Vec3.Dot(ray.Direction, outwardNormal) < 0;
			Vec3 normal = isFrontFace ? outwardNormal : -outwardNormal;

			record = new HitRecord(ray.At(root), normal, root, isFrontFace);
			return true;
		}

		public override string ToString() => $"S({Centre}, {Radius})";
	}
}