using System;
using System.Threading;

public class Program
{
	static void Main()
	{
		// 非同期処理を開始。
		BeginAsyncWork(Callback);

		// 同時に別の処理もする。
		for (int i = 0; i < 7; i++)
		{
			// 0.8秒おきにメッセージ表示。
			System.Threading.Thread.Sleep(800);
			Console.WriteLine("メイン処理 {0}", i);
		}
	}
	static void BeginAsyncWork(AsyncCallback callback)
	{
		Action async = AsyncWork;
		async.BeginInvoke(callback, null);
	}
	static void AsyncWork()
	{
		for (int i = 0; i < 5; i++)
		{
			// 1秒おきにメッセージ表示。
			System.Threading.Thread.Sleep(1000);
			Console.WriteLine("非同期処理 {0}", i);
		}
	}
	static void Callback(IAsyncResult r)
	{
		Console.WriteLine("終了！");
	}
}