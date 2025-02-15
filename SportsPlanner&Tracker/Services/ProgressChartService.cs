using SkiaSharp;
using SportsPlanner_Tracker.Models;
using System;
using System.Collections.Generic;
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
            int leftMargin = 80;  
            int rightMargin = 60;
            int bottomMargin = 60;
            int topMargin = 40;

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

                var font = new SKFont
                {
                    Size = 16
                };

                var textPaint = new SKPaint
                {
                    Color = SKColors.Black,
                    IsAntialias = true
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

                // Određivanje minimalne i maksimalne vrijednosti za Y-os (težina)
                float minY = (float)progress.Min(p => p.Weight) - 2;
                float maxY = (float)progress.Max(p => p.Weight) + 2;

                // Priprema koordinata
                float xStep = (width - leftMargin - rightMargin) / Math.Max(progress.Count - 1, 1);
                float yScale = (height - topMargin - bottomMargin) / Math.Max(maxY - minY, 1);

                // Crtanje X i Y ose
                canvas.DrawLine(leftMargin, height - bottomMargin, width - rightMargin, height - bottomMargin, axisPaint); // X-os
                canvas.DrawLine(leftMargin, height - bottomMargin, leftMargin, topMargin, axisPaint); // Y-os

                // Crtanje horizontalne mreže (grid)
                for (float y = minY; y <= maxY; y += 2)
                {
                    float yCoord = height - bottomMargin - (y - minY) * yScale;
                    canvas.DrawLine(leftMargin, yCoord, width - rightMargin, yCoord, gridPaint);

                    // Prikaz brojeva na Y-osi (poravnanje desno da ne izlaze van slike)
                    canvas.DrawText(y.ToString("F1"), leftMargin - 15, yCoord + 5, SKTextAlign.Right, font, textPaint);
                }

                // Crtanje grafikona
                var points = progress.Select((p, i) => new SKPoint(leftMargin + i * xStep, height - bottomMargin - (float)((p.Weight - minY) * yScale))).ToArray();
                canvas.DrawPoints(SKPointMode.Polygon, points, linePaint);

                // Dodavanje točaka na liniji za bolju vidljivost
                foreach (var point in points)
                {
                    canvas.DrawCircle(point, 5, pointPaint);
                }

                // Dodavanje datuma ispod X-ose
                for (int i = 0; i < progress.Count; i++)
                {
                    float x = leftMargin + i * xStep;
                    canvas.DrawText(progress[i].Date.ToShortDateString(), x - 25, height - 5, SKTextAlign.Center, font, textPaint);
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
