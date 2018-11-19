Console.WriteLine("Hello KKK World!");

// 1. 단축키(HotKey) : (단축키를 사용할 경우 프로세스의 키입력을 무시한다.)
Keyboard.HotKey(Keys.A,  () => { Console.WriteLine("a"); });

Keyboard.HotKey(Keys.A | Keys.Control, () => 
{
    Console.WriteLine("ctrl + a");
    // 현재 화면 캡쳐 및 자동 저장 (파일명: 현재 날짜)
    Camera.Capture().Save();
});

Keyboard.HotKey(Keys.A | Keys.Alt, () => 
{
    Console.WriteLine("alt + a");
    // 콘솔창 토글
    Command.visible = !Command.visible;
});

// 2. 키보드 전역 훅 : (다른 프로세스에서 키보드 입력을 해도 실행된다.)
// 2.1 키를 누를 때
Keyboard.OnKeyDown += (key) => 
{
    switch (key)
    {
        case Keys.B: Console.WriteLine("B가 눌렸다."); break;
        case Keys.C: Console.WriteLine("C가 눌렸다."); break;
    }
};

// 2.2 키를 땔 때
Keyboard.OnKeyUp += (key) => {
    Console.WriteLine(key);
    /*switch (key)
    {
        case Keys.E: Console.WriteLine("E가 때졌다."); break;
        case Keys.F: Console.WriteLine("F가 때졌다."); break;
    }*/
};

// 3. 테스트 함수
Test();
private void Test()
{
    Console.WriteLine("This is Test function..");
}

// 4. 마우스 전역 훅 : (다른 프로세스에서 마우스 입력을 해도 실행된다.)
Mouse.OnMouseDown += (x, y) =>
{
    Console.WriteLine(string.Format("click (x: {0}, y: {1})", x, y));
}