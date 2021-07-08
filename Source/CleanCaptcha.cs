using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CleanCaptcha
{
    public static void Start(string input, string output)
    {
        Bitmap bm;
        Start(new Bitmap(input), out bm);
        bm.Save(output);
    }

    public static void Start(Bitmap input, out Bitmap output)
    {
        var bitmap = CreateNonIndexedImage(input);
        Bitmap CreateNonIndexedImage(Image src)
        {
            Bitmap newBmp = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics gfx = Graphics.FromImage(newBmp))
            {
                gfx.DrawImage(src, 0, 0);
            }

            return newBmp;
        }

        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                if (bitmap.GetPixel(x, y) != Color.FromArgb(255, Color.Black)) continue;
                if (foodfill_parts.Any(a => a.points.Any(a2 => a2.X == x && a2.Y == y))) continue;

                foodfill_parts.Add(new FloodFillParts());
                BreakParts(x, y, foodfill_parts.Count - 1);
                foodfill_parts[foodfill_parts.Count - 1].x_min = foodfill_parts[foodfill_parts.Count - 1].points.Min(m => m.X);
                foodfill_parts[foodfill_parts.Count - 1].x_max = foodfill_parts[foodfill_parts.Count - 1].points.Max(m => m.X);
                foodfill_parts[foodfill_parts.Count - 1].id = Guid.NewGuid();
            }
        }
        void BreakParts(int x, int y, int part_index)
        {
            if (x < 0 || x >= bitmap.Width) return;
            if (y < 0 || y >= bitmap.Height) return;
            if (bitmap.GetPixel(x, y) != Color.FromArgb(255, Color.Black)) return;
            if (foodfill_parts[part_index].points.Any(a => a.X == x && a.Y == y)) return;

            foodfill_parts[part_index].points.Add(new Point(x, y));

            BreakParts(x + 1, y, part_index);
            BreakParts(x, y + 1, part_index);
            BreakParts(x - 1, y, part_index);
            BreakParts(x, y - 1, part_index);
        }

        List<Guid> index_toremove = new List<Guid>();
        foreach (var part in foodfill_parts)
        {
            if (part.points.Count > 420) continue;
            foreach (var point in part.points)
            {
                bitmap.SetPixel(point.X, point.Y, Color.FromArgb(255, 238, 238, 238));
            }
            index_toremove.Add(part.id);
        }
        foodfill_parts.RemoveAll(ra => index_toremove.Contains(ra.id));

        foodfill_parts = foodfill_parts.OrderBy(o => o.x_min).ToList();
        index_toremove = new List<Guid>();
        for (int i = 0; i < foodfill_parts.Count - 1; i++)
        {
            if (foodfill_parts[i + 1].x_min - foodfill_parts[i].x_max <= 5) continue;

            foreach (var point in foodfill_parts[i].points)
            {
                bitmap.SetPixel(point.X, point.Y, Color.FromArgb(255, 238, 238, 238));
            }
            index_toremove.Add(foodfill_parts[i].id);
        }
        foodfill_parts.RemoveAll(ra => index_toremove.Contains(ra.id));

        index_toremove = new List<Guid>();
        foreach (var part in foodfill_parts)
        {
            if (!part.points.Any(a => a.X == 0 || a.Y == 0 || a.X == bitmap.Width - 1 || a.Y == bitmap.Height - 1)) continue;
            if (part.points.Count > 600) continue;

            foreach (var point in part.points)
            {
                bitmap.SetPixel(point.X, point.Y, Color.FromArgb(255, 238, 238, 238));
            }
            index_toremove.Add(part.id);
        }
        foodfill_parts.RemoveAll(ra => index_toremove.Contains(ra.id));
        
        index_toremove = new List<Guid>();
        for (int i = Math.Min(foodfill_parts.Count, 5); i < foodfill_parts.Count; i++)
        {
            foreach (var point in foodfill_parts[i].points)
            {
                bitmap.SetPixel(point.X, point.Y, Color.FromArgb(255, 238, 238, 238));
            }
            index_toremove.Add(foodfill_parts[i].id);
        }
        foodfill_parts.RemoveAll(ra => index_toremove.Contains(ra.id));

        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                if (x < 10 || y < 10 || x > bitmap.Width - 10 || y > bitmap.Height - 10)
                {
                    bitmap.SetPixel(x, y, Color.FromArgb(255, 238, 238, 238));
                }
            }
        }

        output = bitmap;
    }


    public static List<FloodFillParts> foodfill_parts = new List<FloodFillParts>();
    public class FloodFillParts
    {
        public Guid id;
        public List<Point> points = new List<Point>();
        public int x_min;
        public int x_max;
    }
}