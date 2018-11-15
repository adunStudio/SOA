Console.WriteLine("Hello World");


Keyboard.HotKey(Keys.A,  () => { Console.WriteLine("a"); });

// 캡처, 저장
Keyboard.HotKey(Keys.A | Keys.Control, () => { Console.WriteLine("ctrl + a");
    Camera.Capture().Save();
    // save() -> 현재 날짜 파일명으로 저장
});

// 콘솔창 토글
Keyboard.HotKey(Keys.A | Keys.Alt, () => { Console.WriteLine("alt + a");
    Command.visible = !Command.visible;
});

Test();

private void Test()
{
    Console.WriteLine("Test");
}

