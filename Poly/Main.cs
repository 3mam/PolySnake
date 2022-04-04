using System.Runtime.InteropServices;
using Poly;

Console.WriteLine("Hello, World!");

//Hide the console on Windows.
if (Environment.OSVersion.Platform == PlatformID.Win32NT)
{
  [DllImport("kernel32.dll")]
  static extern IntPtr GetConsoleWindow();

  [DllImport("user32.dll")]
  static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

  var handle = GetConsoleWindow();
  ShowWindow(handle, 0);
}

using var window = Window.Create();
window.Run();