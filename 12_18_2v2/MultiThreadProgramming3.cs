using System;
using System.Threading;
using System.Threading.Tasks;

class TestThread
{
	/// <summary>
	/// THREAD_NUM 個のスレッドを立てる。
	/// それぞれのスレッドの中で num を ROOP_NUM 回インクリメントする。
	/// </summary>
	static void Main()
	{
		const int ThreadNum = 20;
		const int LoopNum = 20;
		int num = 0; // 複数のスレッドから同時にアクセスされる。

		var syncObject = new object();

		//0から20個のスレッドを繰り返し並列動作させます。そのときiをデリゲートとして宣言
		Parallel.For(0, ThreadNum, i =>
		{
			for (int j = 0; j < LoopNum; j++)
			{
				bool lockTaken = false;
				try
				{
					Monitor.Enter(syncObject, ref lockTaken); // ロック取得

					//↓クリティカルセクション
					int tmp = num;
					Thread.Sleep(1);
					num = tmp + 1;
					//↑クリティカルセクション
				}
				//例外が発生したとしても実行したいコード
				finally
				{
					if (lockTaken)
						Monitor.Exit(syncObject); // ロック解放
				}
			}
		});

		Console.Write("{0} ({1})\n", num, ThreadNum * LoopNum);
		// num と THREAD_NUM * ROOP_NUM は一致するはずなんだけど・・・
	}
}