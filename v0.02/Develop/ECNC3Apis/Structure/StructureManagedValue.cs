using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>加工条件ファイル構造</summary>
	public class StructureManagedValue : ICloneable, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;


        public StructureManagedValue(List<int> values = null)
        {
            _values = new List<int>();
            if (values != null)
            {
                foreach(int temp in values)
                {
                    _values.Add(temp);
                }
            }
            else
            {
                _values.Add(0);
            }
        }
        private List<int> _values = null;
        /// <summary>INDEX</summary>
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                int _index = 0;
                _index = value;
                if (_index >= _values.Count
                    || _index < 0)
                {
                    //インデックスの指定が範囲外の場合
                    Common.ECNC3Log log = new Common.ECNC3Log("StructureManagedValue");
                    _Index = 0;
                    _Value = _values[_Index];
                    log.Error
                        (
                        "InvalidValue[Count= " + _values.Count.ToString() + " , Index= " + _index.ToString() + "] " +
                        "To [" + "Index= 0 , " + "Value= " + _values[_Index].ToString() + "]"
                        );
                    return;
                }
                _Value = _values[_index];
                _Index = _index;
            }
        }
        private int _Index = 0;
        /// <summary>値</summary>
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                int _value = 0;
                _value = value;
                for(int count = 0; count < _values.Count; count++)
                {
                    if(_values[count] == _value)
                    {
                        _Index = count;
                        _Value = _value;
                        return;
                    }
                }
                //リストに値が一致しない場合
                _Index = 0;
                _Value = _values[_Index];
                Common.ECNC3Log log = new Common.ECNC3Log("StructureManagedValue");
                log.Error
                    (
                    "InvalidValue[Value= " + _value.ToString() + "] " +
                    "To [" + "Index= 0 , " + "Value= " + _values[_Index].ToString() + "]"
                    );
            }
        }
        private int _Value = 0;
		/// <summary>インスタンスの破棄</summary>
		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );    //  ファイナライザによるDispose()呼び出しの抑制。
		}
		/// <summary>インスタンスの破棄</summary>
		/// <param name="disposing">呼び出し元の判別
		///     <list type="bullet" >
		///         <item>true=Dispose()関数からの呼び出し。</item>
		///         <item>false=ファイナライザによる呼び出し。</item>
		///     </list>
		/// </param>
		protected void Dispose( bool disposing )
		{
			try {
				if( false == _disposed ) {
					if( true == disposing ) {
						//  マネージリソースの解放
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				//  基底クラスのDispose()を確実に呼び出す。
				;	// base.Dispose( disposing );
			}
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
            StructureManagedValue temp = new StructureManagedValue(_values);
			temp._Index = _Index;
            temp._Value = _values[_Index];
			return temp;
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy(StructureManagedValue source )
		{
            _Index = source._Index;
            _Value = source._Value;
		}
		public int Compare(StructureManagedValue source )
		{
			int ret = 0;
			while( true ) {
				ret = _Index.CompareTo( source._Index);
				if( 0 != ret ) {
					break;
				}
				ret = _Value.CompareTo( source._Value);
				if( 0 != ret ) {
					break;
				}
                break;
			}
			return ret;
		}
	}
}
