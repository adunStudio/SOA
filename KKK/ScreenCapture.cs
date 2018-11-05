﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace KKK
{
    class ScreenCapture
    {
        public static Image Capture()
        {
            return CaptureWindow();
        }

        private static Image CaptureWindow()
        {
            return PrintScreen(Screen.PrimaryScreen);
        }

        private static Bitmap PrintScreen(Screen screen)
        {
            Rectangle bounds = screen.Bounds;

            // 디스플레이 범위 / 작업영역
            // 작업 영역은 작업 표시줄, 도킹된 창 및 도킹된 도구 모음을 제외한 디스플레이의 데스크톱 영역 
            if (screen.Bounds.Width / screen.WorkingArea.Width > 1 ||
                screen.Bounds.Height / screen.WorkingArea.Height > 1)
            {
                bounds = new Rectangle(
                    x: 0,
                    y: 0,
                    width: screen.Bounds.Width + screen.WorkingArea.X,
                    height: screen.Bounds.Height + screen.WorkingArea.Y
                    );
            }

            PixelFormat pixelFormat = new Bitmap(1, 1, Graphics.FromHwnd(IntPtr.Zero)).PixelFormat;
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, pixelFormat);

            // System.Drawing.Graphics FromImage
            // https://docs.microsoft.com/ko-kr/dotnet/api/system.drawing.graphics.fromimage?view=netframework-4.7.2
            // 지정된 Image에서 새 Graphics을 만든다.
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(
                    sourceX: bounds.X, 
                    sourceY: bounds.Y,
                    destinationX: 0,
                    destinationY: 0,
                    blockRegionSize: bounds.Size,
                    copyPixelOperation: CopyPixelOperation.SourceCopy);
            }

            return bitmap;
        }
    }
}
