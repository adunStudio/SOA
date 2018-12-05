Console.WriteLine("[Capture Program]");

bool captureMode = false;
int start_x = -1;
int start_y = -1;    

// [Global Hook Keyboard Combo] 
Keyboard.ComboKey(Keys.LControlKey, Keys.LShiftKey, Keys.D4, () =>
{
    if (captureMode == true) return;

    captureMode = true;

    Console.WriteLine("[Capture Program] Ready");
});

// [Global Hook Mouse Drag Start] 
Mouse.OnMouseDragStart += (x, y, b, d) =>
{
    if (captureMode == false) return;

    start_x = x;
    start_y = y;

    Console.WriteLine("[Capture Program] Start");
};

// [Global Hook Mouse Drag End] 
Mouse.OnMouseDragEnd += (x, y, b, d) =>
{
    if (captureMode == false) return;

    // 캡쳐해서 바탕화면에 저장
    Camera.Capture(start_x, start_y, x, y).Save(directory: "C:/Users/adunstudio/Desktop");

    captureMode = false;

    Console.WriteLine("[Capture Program] Catpure, End");
};

// [Global Hook Mouse Doubli Click] 
Mouse.OnMouseClick += (x, y, b, d) =>
{
    if (b != MouseButtons.Right) return;
    if (captureMode == false) return;

    captureMode = false;

    Console.WriteLine("[Capture Program] End");
};