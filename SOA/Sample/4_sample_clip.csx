Keyboard.HotKey(Keys.C, () =>
{
    Clip.Copy();
});

Keyboard.HotKey(Keys.H, () =>
{
    // 클립보드
    string text = Clip.GetText();
    Keyboard.Send(text);
});

Keyboard.HotKey(Keys.V, () =>
{
    Clip.Paste();
});

Keyboard.HotKey(Keys.G, () => 
{
    Clip.SetText("todo");
})