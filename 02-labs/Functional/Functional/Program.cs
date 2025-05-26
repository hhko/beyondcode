// See https://aka.ms/new-console-template for more information
using ConsoleApp1.Adapters;
using ConsoleApp1.Adapters.Sys.Live;

using LanguageExt;

Console.WriteLine($"Hello, World! {Thread.CurrentThread.ManagedThreadId}");


await SideEffect_04_Sys_Main
    .AskUserAsync<MyRuntime>()
    .RunAsync(new MyRuntime(new MyRuntimeEnv()));

int z = 0;