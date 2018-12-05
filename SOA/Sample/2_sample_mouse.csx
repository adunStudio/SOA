/* 마우스 예제 */
Console.WriteLine("Mouse Example");

// 마우스 시뮬레이터
Keyboard.HotKey(Keys.T, () =>
{
    // 클릭 시뮬레이션
    //Mouse.Click(MouseButtons.Left);

    // 더블 클릭 시뮬레이션 
    //Mouse.DoubleClick(MouseButtons.Left);

    // 절대 이동 시뮬레이션
    //Mouse.MoveTo(1900, 1000);

    // 상대 이동 시뮬레이션
    //Mouse.MoveBy(10, 0);

    // 휠 시뮬레이션 default: (isVertical: true)
    //Mouse.Wheel(-10);

    // 드래그 시뮬레이션
    Mouse.Drag(Mouse.X, Mouse.Y, Mouse.X + 100, Mouse.Y + 100);
});

Mouse.Drag(Mouse.X, Mouse.Y, Mouse.X + 100, Mouse.Y + 100);


// 마우스 다운
Mouse.OnMouseDown += (x, y, b, d) =>
{
    Console.WriteLine(string.Format("{0} [Down] (x: {1}, y: {2})", b, x, y));
};

// 마우스 업
Mouse.OnMouseUp += (x, y, b, d) =>
{
    Console.WriteLine(string.Format("{0} [Up] (x: {1}, y: {2})", b, x, y));
};

// 마우스 클릭
Mouse.OnMouseClick += (x, y, b, d) =>
{
    Console.WriteLine(string.Format("{0} [Click] (x: {1}, y: {2})", b, x, y));
};

// 마우스 더블 클릭
Mouse.OnMouseDoubleClick += (x, y, b, d) =>
{
    Console.WriteLine(string.Format("{0} [DoubleClick] (x: {1}, y: {2})", b, x, y));
};

// 마우스 휠
Mouse.OnMouseWheel += (x, y, b, d) =>
{
    Console.WriteLine(string.Format("{0} [Wheel] (x: {1}, y: {2}, delta: {3})", b, x, y, d));
};

// 마우스 드래그 스타트
Mouse.OnMouseDragStart += (x, y, b, d) =>
{
    Console.WriteLine(string.Format("{0} [DragStart] (x: {1}, y: {2})", b, x, y));
};

// 마우스 드래그 엔드
Mouse.OnMouseDragEnd += (x, y, b, d) =>
{
    Console.WriteLine(string.Format("{0} [DragEnd] (x: {1}, y: {2})", b, x, y));
};

// 마우스 이동
Mouse.OnMouseMove += (x, y, b, d) =>
{
    // 마우스 드래그 확인
    if(Mouse.IsDragMode())
    {
        Console.WriteLine(string.Format("{0} [Move] (x: {1}, y: {2})", b, x, y));
    }
};

