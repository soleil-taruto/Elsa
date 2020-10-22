using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	/// <summary>
	/// <para>構造化オブジェクトをラップするクラス</para>
	/// <para>構造化オブジェクトは、以下のいずれかであるものを指す。</para>
	/// <para>1. 全ての要素が構造化オブジェクトである ObjectList</para>
	/// <para>2. 全ての要素の値が構造化オブジェクトである ObjectMap</para>
	/// <para>3. object</para>
	/// <para>4. null</para>
	/// </summary>
	public class ObjectTree
	{
		/// <summary>
		/// <para>色々なオブジェクトを構造化オブジェクトに変換する。</para>
		/// <para>本メソッドの用途は決まっていない。呼び出し側は処理が変更されることを想定すること。</para>
		/// <para>現在のところ、自力で作ったオブジェクトを JsonTools.Encode() に渡せるような構造化オブジェクトに変換することを用途としている。</para>
		/// </summary>
		/// <param name="root">色々なオブジェクト</param>
		/// <returns>構造化オブジェクト</returns>
		public static object Conv(object root)
		{
			// for JsonTools.Encode() --->

			if (root == null)
			{
				return new JsonTools.Word()
				{
					Value = "null",
				};
			}
			if (root.GetType().IsPrimitive)
			{
				return new JsonTools.Word()
				{
					Value = root.ToString().ToLower(),
				};
			}

			// <--- for JsonTools.Encode()

			if (root is string) // string も IEnumerable
				return root;

			if (root is IDictionary) // IDictionary は IEnumerable を継承しているので、先に。
			{
				ObjectMap om = ObjectMap.Create();

				foreach (DictionaryEntry pair in (IDictionary)root)
				{
					om.Add(pair.Key, Conv(pair.Value));
				}
				return om;
			}
			if (root is IEnumerable)
			{
				ObjectList ol = new ObjectList();

				foreach (object element in (IEnumerable)root)
				{
					ol.Add(Conv(element));
				}
				return ol;
			}
			return root;
		}

		/// <summary>
		/// 色々なオブジェクトからインスタンスを生成する。
		/// </summary>
		/// <param name="root">色々なオブジェクト</param>
		/// <returns>インスタンス</returns>
		public static ObjectTree Convert(object root)
		{
			return new ObjectTree(Conv(root));
		}

		private object Root;

		/// <summary>
		/// 構造化オブジェクトからインスタンスを生成する。
		/// </summary>
		/// <param name="root">構造化オブジェクト</param>
		public ObjectTree(object root)
		{
			this.Root = root;
		}

		public ObjectTree this[int index]
		{
			get
			{
				return new ObjectTree(((ObjectList)this.Root)[index]);
			}
		}

		public ObjectTree this[string path]
		{
			get
			{
				return this[StringTools.Tokenize(path, "/")];
			}
		}

		public ObjectTree this[string[] pTkns]
		{
			get
			{
				object node = this.Root;

				foreach (string pTkn in pTkns)
				{
					if (node is ObjectList)
					{
						node = ((ObjectList)node)[int.Parse(pTkn)];
					}
					else if (node is ObjectMap)
					{
						node = ((ObjectMap)node)[pTkn];
					}
					else
					{
						throw new Exception("不明なパストークンです。" + pTkn);
					}
				}
				return new ObjectTree(node);
			}
		}

		public int Count
		{
			get
			{
				if (this.Root is ObjectList)
				{
					return ((ObjectList)this.Root).Count;
				}
				if (this.Root is ObjectMap)
				{
					return ((ObjectMap)this.Root).Count;
				}
				throw new Exception("リスト又はマップではありません。");
			}
		}

		public string[] GetKeys()
		{
			if (this.Root is ObjectList)
			{
				return IntTools.Sequence(((ObjectList)this.Root).Count).Select(index => index.ToString()).ToArray();
			}
			if (this.Root is ObjectMap)
			{
				return ((ObjectMap)this.Root).GetKeys().ToArray();
			}
			throw new Exception("リスト又はマップではありません。");
		}

		public string StringValue
		{
			get
			{
				return this.Root.ToString();
			}
		}

		public override string ToString()
		{
			if (this.Root == null)
			{
				return "[DEBUG] null";
			}
			return "[DEBUG] JSON " + JsonTools.Encode(this.Root);
		}

		public ObjectTree[] ToArray()
		{
			return this.Iterate().ToArray();
		}

		public IEnumerable<ObjectTree> Iterate()
		{
			if (this.Root is ObjectList)
			{
				foreach (object element in ((ObjectList)this.Root).Direct())
				{
					yield return new ObjectTree(element);
				}
			}
			else if (this.Root is ObjectMap)
			{
				foreach (KeyValuePair<string, object> pair in ((ObjectMap)this.Root).GetEntries())
				{
					yield return new ObjectTree(new ObjectList(pair.Key, pair.Value));
				}
			}
			else
			{
				throw new Exception("リスト又はマップではありません。");
			}
		}

		public bool IsList()
		{
			return this.Root is ObjectList;
		}

		public bool IsMap()
		{
			return this.Root is ObjectMap;
		}

		public object Direct()
		{
			return this.Root;
		}
	}
}
