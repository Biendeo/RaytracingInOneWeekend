using System;
using System.Collections.Generic;
using System.Linq;

namespace RaytracingInOneWeekend {
	class HittableList : IHittable {
		public List<IHittable> Objects { get; init; }

		public HittableList() {
			Objects = new();
		}

		public HittableList(List<IHittable> objects) {
			Objects = objects;
		}

		public bool Hit(Ray3 ray3, double tMin, double tMax, out HitRecord record) {
			record = new HitRecord(Vec3.Zero, Vec3.Zero, 0.0, false);
			bool hasHitAnything = false;
			double closestSoFar = tMax;

			foreach (var o in Objects) {
				if (o.Hit(ray3, tMin, closestSoFar, out HitRecord objectRecord)) {
					hasHitAnything = true;
					closestSoFar = objectRecord.T;
					record = objectRecord;
				}
			}

			return hasHitAnything;
		}

		public override string ToString() => $"HL: {string.Join(", ", Objects.Select(o => o.ToString()))}";
	}
}