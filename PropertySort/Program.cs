using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PropertySort {
    class Program {
        static void Main(string[] args) {

            //出力する内容
            var entitys = new List<Entity>() {
                new Entity() { LastName = "Ono", FirstName = "Shinji", Age = 37 },
                new Entity() { LastName = "Honda", FirstName = "Keisuke",  Age = 30 },
                new Entity() { LastName = "Nakayama", FirstName = "Masashi", Age = 49 },
            };


            var csv = new ConvertCsv<Entity>() { Source = entitys };

            Console.WriteLine(csv.ToCsv());
        }
    }

    /// <summary>
    /// List<T>型のオブジェクトをCSV形式の文字列に変換する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConvertCsv<T> {

        private List<T> Entitys;

        /// <summary>
        /// 変換対象ソース
        /// </summary>
        public List<T> Source {
            set {
                this.Entitys = value;
            }
        }

        /// <summary>
        /// CSV形式で出力
        /// </summary>
        /// <returns></returns>
        public string ToCsv() {

            var entityNames = typeof(T).GetProperties().
                                        //SortAttribute属性が設定されているプロパティのみ対象
                                        Where(e => Attribute.IsDefined(e, typeof(SortAttribute))).
                                        //SortAttribute属性のSortIndexプロパティでソート
                                        OrderBy(e => ((SortAttribute)Attribute.GetCustomAttribute(e, typeof(SortAttribute))).SortIndex).
                                        //プロパティ名を取得
                                        Select(e => e.Name);

            var result = new StringBuilder();

            //ヘッダー出力
            result.AppendLine(string.Join(",", entityNames));

            foreach (var entity in this.Entitys) {
                //要素出力
                var element = entityNames.Select(n => typeof(T).GetProperty(n).GetValue(entity));
                result.AppendLine(string.Join(",", element));
            }

            return result.ToString();
        }
    }

}
