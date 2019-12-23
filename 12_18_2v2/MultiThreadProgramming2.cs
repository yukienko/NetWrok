using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

//マルチスレッドプログラミング2

enum State
{
	ready,		//計算開始前
	running,	//計算真っ最中
	wait,		//計算一時停止中
}

class TestThread
{
	static long sNum;
	static State sThreadState;

	static void Main()
	{
		Thread thread = null;

		Console.Write("素因数分解を行います。\n" +
		  "何か数値を入力してください。\n" +
		  "(計算途中で何かキー入力を行うと処理を中断します。)\n" +
		  "(q と入力するとプログラムを終了します。)\n");

		sThreadState = State.ready;

		while (true)
		{
			Console.Write("> ");
			string line = Console.ReadLine();

			if (sThreadState == State.running)  //計算中
			{
				sThreadState = State.wait;      //計算中断

				//計算を中止するかどうか確認。
				Console.Write("計算を中断しました。\n" +
				  "  c     : 計算中止\n" +
				  "  q     : プログラム終了\n" +
				  "  その他: 計算続行\n" +
				  "# ");

				line = Console.ReadLine();
				if (line.Length != 0)
				{
					if (line[0] == 'c' || line[0] == 'C')
					{
						sThreadState = State.ready;
						thread.Join();
						Console.Write("計算を中止しました。\n");
						continue;
					}
					else if (line[0] == 'q' || line[0] == 'Q')
					{
						return;
					}
				}

				sThreadState = State.running; // 計算再開
			}
			else
			{
				if (line.Length == 0) continue;

				// q が入力されたらプログラム終了。
				if (line[0] == 'q' || line[0] == 'Q') return;

				// 因数分解を開始する。
				//long型が64bitなのでlineをint型に直すときも64bitInt型に戻してあげる
				//tryの中の分を実行してやる
				try { sNum = Int64.Parse(line);}
				//sNumの中に文として間違っているものが入ろうとしたときに実行される。
				catch (FormatException)
				{
					Console.Write("不正な文字列が入力されました。\n"); continue;
				}
				//オーバーフローしたときに実行される。
				catch (OverflowException)
				{
					Console.Write("値が大きすぎます。\n"); continue;
				}

				sThreadState = State.running;
				//スレッドの作成。引数は開始時の実行メソッド
				//実行メソッドは実行するが、そのメソッドとは別に文は進んでいる。これをマルチスレッドプログラミングという。
				thread = new Thread(new ThreadStart(ThreadFunction));
				//スレッドを実行開始
				thread.Start();
			}
		}
	}

	//こいつがめっちゃ時間かかる処理
	static void ThreadFunction()
	{
		Console.Write("素因数分解開始\n");
		IList factors = Factorization(sNum);
		if (factors != null)
		{
			Console.Write("\n素因数分解終了\n");

			foreach (long i in factors)
			{
				if (sThreadState == State.ready) break;
				if (sThreadState == State.wait) continue;
				Console.Write("{0} ", i);
			}
			Console.Write("\n");
		}
		sThreadState = State.ready;
	}

	/// <summary>
	/// 素因数分解を行う。
	/// (馬鹿でかい数字を素因数分解しようとすると非常に重たい。)
	/// </summary>
	/// <param name="n">素因数分解したい数値</param>
	/// <returns>因数のリスト</returns>
	static IList Factorization(long n)
	{
		ArrayList factors = new ArrayList();

		//小数点切り上げ
		long sqrtn = (long)Math.Ceiling(Math.Sqrt(n) + 1);
		long i = 2;

		while (i < sqrtn)
		{
			if (sThreadState == State.ready) break;
			if (sThreadState == State.wait) continue;

			if (n % i == 0)
			{
				factors.Add(i);
				n /= i;
				Console.Write("{0}", i);
			}
			else
			{
				++i;
			}

			Console.Write('.'); // 途中経過を表示
		}

		if (n != 1)
			factors.Add(n);

		return factors;
	}//Factorization
}