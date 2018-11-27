/* 키보드 예제 */
Console.WriteLine("Keyboard Example");

// 단축키
Keyboard.HotKey(Keys.J, () => 
{
    Console.Write("단축키: J");
});

Keyboard.HotKey(Keys.Control | Keys.J, () => 
{
    /// Logic
});


// 글로벌 훅 (모든 키)
Keyboard.OnKeyDown += (key) => 
{
    /// Logic
};

Keyboard.OnKeyDown += (key) =>
{
   /// Logic
};

// 글로벌 훅 (특정 키)
Keyboard.DownKey(Keys.A | Keys.Control, () =>
{
    // 특정 키 눌러져 있는지 검사 가능
    if(Keyboard.IsKeyDown(Keys.B))
    {
        /// Logic
    }
});

Keyboard.UpKey(Keys.A | Keys.Control, () =>
{
    /// Logic
});

// 글로벌 훅 (키 조합)
Keyboard.ComboKey(Keys.LControlKey, Keys.D, Keys.G, () =>
{
    /// Logic
});

