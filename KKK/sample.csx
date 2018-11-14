Console.WriteLine("Hello World");
Console.WriteLine(GetSize());
Keyboard.HotKey(Keys.A,                () => { Console.WriteLine("a"); });
Keyboard.HotKey(Keys.A | Keys.Control, () => { Console.WriteLine("a + ctrl"); });
Keyboard.HotKey(Keys.A | Keys.Alt,     () => { Console.WriteLine("a + alt"); });