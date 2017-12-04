using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models.Common
{
	/// <summary>アセンブリ検索支援</summary>
	/// <typeparam name="T">抽出対象のインターフェース定義</typeparam>
	public class AidAssembly<T>
	{
		/// <summary>コンストラクタ</summary>
		public AidAssembly()
		{
		}
		/// <summary>アプリケーションドメイン上のアセンブリ</summary>
		private List<Assembly> Assemblies
		{
			get
			{
				List<Assembly> result = new List<Assembly>();
				Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
				foreach( Assembly item in asms ) {
					result.Add( item );
				}
				return result;
			}
		}
		/// <summary>アプリケーションドメイン上のインターフェース</summary>
		public List<Type> Interfaces
		{
			get
			{
				List<Type> result = new List<Type>();
				foreach( Assembly asm in Assemblies ) {
					if( null != asm ) {
						result.AddRange( new List<Type>( asm.GetTypes().Where( c => c.GetInterfaces().Any( t => t == typeof( T ) ) ) ) );
					}
				}
				return result;
			}
		}
	}
}
