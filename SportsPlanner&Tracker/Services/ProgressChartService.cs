using SkiaSharp;
using SportsPlanner_Tracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SportsPlanner_Tracker.Services
{
    public class ProgressChartService
    {
        public byte[] GenerateProgressChart(List<UserProgress> progress)
        {
            if (progress == null || progress.Count == 0)
                return new byte[0];

            int width = 800;
            int height = 400;
            int margin = 60;

            using (var bitmap = new SKBitmap(width, height))
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.White);

                var axisPaint = new SKPaint
                {
                    Color = SKColors.Black,
                    StrokeWidth = 3,
                    Style = SKPaintStyle.Stroke
                };

                var textPaint = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = 18
                };

                var gridPaint = new SKPaint
                {
                    Color = SKColors.LightGray,
                    StrokeWidth = 1,
                    Style = SKPaintStyle.Stroke
                };

                var linePaint = new SKPaint
                {
                    Color = SKColors.DarkRed, // Jasnija boja linije
                    StrokeWidth = 4,
                    Style = SKPaintStyle.Stroke
                };

                var pointPaint = new SKPaint
                {
                    Color = SKColors.DarkBlue,
                    StrokeWidth = 10,
                    Style = SKPaintStyle.Fill
                };

                // Određivanje minimalne i maksimalne vrijednosti za Y osu (težina)
                float minY = (float)progress.Min(p => p.Weight) - 2;
                float maxY = (float)progress.Max(p => p.Weight) + 2;

                // Priprema koordinata
                float xStep = (width - 2 * margin) / Math.Max(progress.Count - 1, 1);
                float yScale = (height - 2 * margin) / Math.Max(maxY - minY, 1);

                // Crtanje X i Y ose
                canvas.DrawLine(margin, height - margin, width - margin, height - margin, axisPaint); // X-os
                canvas.DrawLine(margin, height - margin, margin, margin, axisPaint); // Y-os

                // Crtanje horizontalne mreže (grid)
                for (float y = minY; y <= maxY; y += 2)
                {
                    float yCoord = height - margin - (y - minY) * yScale;
                    canvas.DrawLine(margin, yCoord, width - margin, yCoord, gridPaint);
                    canvas.DrawText(y.ToString("F1"), 10, yCoord + 5, textPaint);
                }

                // Crtanje grafikona
                var points = progress.Select((p, i) => new SKPoint(margin + i * xStep, height - margin - (float)((p.Weight - minY) * yScale))).ToArray();
                canvas.DrawPoints(SKPointMode.Polygon, points, linePaint);

                // Dodavanje točaka na liniji za bolju vidljivost
                foreach (var point in points)
                {
                    canvas.DrawCircle(point, 5, pointPaint);
                }

                // Dodavanje datuma ispod X-ose
                for (int i = 0; i < progress.Count; i++)
                {
                    float x = margin + i * xStep;
                    canvas.DrawText(progress[i].Date.ToShortDateString(), x - 25, height - 5, textPaint);
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    return data.ToArray();
                }
            }
        }
    }
}
