using System;
using System.Threading.Tasks;
using System.Threading;

//マルチスレッドプログラミング1

namespace ConsoleApplication1
{
	class TaskSample
	{
		static void Main(string[] args)
		{
			const int N = 40;

			Parallel.For(0, N, id => // こう書くだけで、並行して処理が行われる
			{
				//Console.Write("{0}\n", id);

				Random rnd = new Random();

				for (int i = 0; i < 4; ++i)
				{
					Thread.Sleep(rnd.Next(50, 100)); // ランダムな間隔で処理を一時中断
					Console.Write("2_{0}\n", id);

					Console.Write("{0} (ID: {1})\n", i, id);
				}
			});
			// 並行して動かしている処理がすべて終わるまで、自動的に待つ
		}
	}
}