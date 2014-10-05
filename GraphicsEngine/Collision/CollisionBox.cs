﻿// CollisionBox.cs
//
// Copyright 2013 Fons van der Plas
// Fons van der Plas, fonsvdplas@gmail.com

using System;
using OpenTK;

namespace GraphicsLibrary.Collision
{
	/* De collisionAABB class is een doos, gedefinieerd met twee hoekpunten lb en rt
	 * AABB: Axis Aligned Bounding Box
	 */
	public class CollisionAABB
	{
		public Vector3 lb, rt;

		public CollisionAABB(Vector3 lb, Vector3 rt)
		{
			this.lb = lb;//min
			this.rt = rt;//max
		}

		/* Deze functie berekent de afstand van een lijn die de doos snijdt
		 */
		public float Intersect(CollisionRay ray)
		{
			// Algorithm: http://i.stack.imgur.com/tfvSJ.png
			Vector3 t = new Vector3();

			if (ray.dir == Vector3.Zero)
			{
				return -1;
			}
			Vector3 r = ray.dir;
			r.NormalizeFast();
			Vector3 _lb = ray.eye - lb;
			Vector3 _rt = ray.eye - rt;

			
			//X
			if (r.X > 0) // Determine candidate plane
			{
				t.X = Max(-1, _lb.X / -r.X); // Save distance to hit point
			}
			else
			{
				t.X = Max(-1, _rt.X / -r.X);
			}
			//Y					 
			if (r.Y > 0)
			{
				t.Y = Max(-1, _lb.Y / -r.Y);
			}
			else
			{
				t.Y = Max(-1, _rt.Y / -r.Y);
			}
			//Z					 
			if (r.Z < 0)
			{
				t.Z = Max(-1, _lb.Z / r.Z);
			}
			else
			{
				t.Z = Max(-1, _rt.Z / r.Z);
			}
			r.Z *= -1; //....
			
			// The largest hit distance must be that of the hit plane
			if (t.X >= t.Y && t.X >= t.Z)
			{
				// test X
				// Check if hit point is inside the box using the other 2 dimensions
				Vector3 hit = ray.eye + Vector3.Multiply(r, t.X);
				if (hit.Y >= lb.Y && hit.Y <= rt.Y && hit.Z >= lb.Z && hit.Z <= rt.Z)
				{
					return t.X;
				}
			}
			else if (t.Y >= t.X && t.Y >= t.Z)
			{
				// test Y
				Vector3 hit = ray.eye + Vector3.Multiply(r, t.Y);
				if (hit.X >= lb.X && hit.X <= rt.X && hit.Z >= lb.Z && hit.Z <= rt.Z)
				{
					return t.Y;
				}
			}
			else if (t.Z >= t.Y && t.Z >= t.X)
			{
				// test Z
				Vector3 hit = ray.eye + Vector3.Multiply(r, t.Z);
				if (hit.Y >= lb.Y && hit.Y <= rt.Y && hit.X >= lb.X && hit.X <= rt.X)
				{
					return t.Z;
				}
			}
			return -1;
		}

		/* Een simpelere functie die alleen kijkt of een bepaald punt in de AABB zit
		 */
		public bool isInside(Vector3 position)
		{
			return position.X >= lb.X && position.X <= rt.X && position.Y >= lb.Y && position.Y <= rt.Y && position.Z >= lb.Z && position.Z <= rt.Z;
		}

		// Float math
		private float Max(float a, float b)
		{
			return Math.Max(a, b);
		}

		// Voorbeeld:

		//CollisionBox collisionBox = new CollisionBox(new Vector3(0, 0, 0), new Vector3(1000, 100, 1000));
		//Vector3 dir = Vector3.Zero;
		//
		//fpsCam.Y = Math.Min(fpsCam.Y, 1.57f);
		//fpsCam.Y = Math.Max(fpsCam.Y, -1.57f);
		//
		//dir.X = (float) Math.Sin(fpsCam.X);
		//dir.Z = (float) Math.Cos(fpsCam.X);
		//dir.Y = (float) Math.Tan(fpsCam.Y);
		//
		//CollisionRay collisionRay = new CollisionRay(Camera.Instance.position, dir);
		//Console.WriteLine(collisionBox.Intersect(collisionRay));
	}
}
