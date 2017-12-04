#ifndef	__DYNAMICMEMORY_H__
#define	__DYNAMICMEMORY_H__

//!				
//!	@brief	動的メモリ操作クラス
//!				
//!	@details	動的なメモリの操作を行います。
//!				
//!	@since	2016/09/06	Takano	新規作成
//!				
template<class VALUE>class CDynamicMemory
{
public:
	//!				
	//!	@brief	コンストラクタ
	//!				
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	CDynamicMemory()
	{
		Dealloc();
	}
	
	//!				
	//!	@brief	コンストラクタ
	//!				
	//!	@details	メモリ領域の確保と対象となるインスタンスのコピーをあわせて行います。
	//!	
	//!	@param[in]	data	コピー対象
	//!	
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	CDynamicMemory( LPVOID data )
	{
		Alloc( 1 );
		CopyMemory( m_pObject, data, sizeof( VALUE ) );
	}
	//!				
	//!	@brief	デストラクタ
	//!				
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	virtual	~CDynamicMemory()
	{
		Dealloc();
	}

	//!				
	//!	@brief	メモリの動的割り当て
	//!				
	//!	@details	引数のサイズ分だけのメモリを確保します。
	//!				既にメモリを割り当てられた状態で呼び出された場合は、確保されているメモリを一旦破棄し、再度メモリを確保します。
	//!	
	//!	@param[in]	size	確保したいメモリサイズ(配列サイズ)
	//!	
	//!	@retval	割り当てられたメモリ配列の先頭アドレスポインタ
	//!	
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	VALUE* Alloc( DWORD size )
	{
		Dealloc();
//		try{
			m_size = size;
			m_pObject = new VALUE[m_size];
			Clear();
//		}catch( CMemoryException e ){
//			if( true == report ){
//				e.ReportError();
//			}
//			e.Delete();
//			return NULL;
//		};
		return m_pObject;
	}

	//!				
	//!	@brief	メモリの解放
	//!				
	//!	@details	確保されたメモリを解放します。
	//!				メモリの確保が実行されていない場合は処理を実行しません。
	//!	
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	void Dealloc()
	{
		if( NULL != m_pObject ){
			delete []	m_pObject;
		}
		m_pObject = NULL;
		m_size= 0;
	}

	//!				
	//!	@brief	メモリの初期化
	//!				
	//!	@details	確保されたメモリ領域を 0 で初期化します。
	//!	
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	void Clear()
	{
		ZeroMemory( m_pObject, sizeof( VALUE ) * m_size );
	}

	//!				
	//!	@brief	メモリサイズ取得
	//!				
	//!	@details	確保された配列数を取得します。
	//!	
	//!	@retval	確保された配列数
	//!	
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	__declspec(property(get= GetLength )) DWORD Length;
	DWORD GetLength()
	{
		return m_size;
	}
	
	//!				
	//!	@brief	先頭アドレス取得
	//!				
	//!	@details	確保されたメモリバッファの先頭アドレスを取得します。
	//!	
	//!	@retval	確保されたメモリバッファの先頭アドレス
	//!	
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	__declspec(property(get=GetPointer)) VALUE* Pointer;
	VALUE* GetPointer()
	{
		return m_pObject;
	}
	//!				
	//!	@brief	先頭の参照取得
	//!				
	//!	@details	確保されたメモリバッファの先頭の参照を取得します。
	//!	
	//!	@retval	確保されたメモリバッファの先頭の参照
	//!	
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	__declspec( property( get = GetEntity ) ) VALUE& Entity;
	VALUE& GetEntity()
	{
		return *m_pObject;
	}

private:
	VALUE*	m_pObject;	//	確保されるバッファのポインタ
	DWORD	m_size;		//	確保するメモリサイズ
};

#endif/*__DYNAMICMEMORY_H__*/
