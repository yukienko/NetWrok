//抽象クラスを使うメリット
//プロジェクト名が間違ってます→12_18_1v2(こっちが正しい)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_18_1v2
{
    class Program
    {
        static void Main(string[] args)
        {
            Bird[] b = new Bird[2]; //Birdクラスの変数の配列を生成
            b[0] = new Crow();
            b[1] = new Sparrow();
            for(int i = 0; i < b.Length; i++)
            {
                Console.WriteLine(b[i].Name+" : ");
                b[i].Sing();
            }
        }
    }

    class Crow : Bird
    {
        //コンストラクタ
        public Crow() : base("カラス") { }
        //カラスがなく
        public override void Sing()
        {
            Console.WriteLine("カーカー");
        }
    }
    class Sparrow : Bird
    {
        //コンストラクタ
        public Sparrow() : base("すずめ") { }
        //すずめがなく
        public override void Sing()
        {
            Console.WriteLine("チュンチュン");
        }
    }
    abstract class Bird
    {
        //名前フィールド
        private String name;
        //引数付きコンストラクタ
        public Bird(String name)
        {
            this.name = name;
        }
        //名前を取得
        public String Name
        {
            get
            {
                return name;
            }
        }
        //鳴く（抽象メソッド）
        public abstract void Sing();
    }
}

/*
 * abstract : 抽象メソッド
 */
