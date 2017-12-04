#ifndef	__DYNAMICMEMORY_H__
#define	__DYNAMICMEMORY_H__

//!				
//!	@brief	���I����������N���X
//!				
//!	@details	���I�ȃ������̑�����s���܂��B
//!				
//!	@since	2016/09/06	Takano	�V�K�쐬
//!				
template<class VALUE>class CDynamicMemory
{
public:
	//!				
	//!	@brief	�R���X�g���N�^
	//!				
	//!	@since	2016/09/06	Takano	�V�K�쐬
	//!				
	CDynamicMemory()
	{
		Dealloc();
	}
	
	//!				
	//!	@brief	�R���X�g���N�^
	//!				
	//!	@details	�������̈�̊m�ۂƑΏۂƂȂ�C���X�^���X�̃R�s�[�����킹�čs���܂��B
	//!	
	//!	@param[in]	data	�R�s�[�Ώ�
	//!	
	//!	@since	2016/09/06	Takano	�V�K�쐬
	//!				
	CDynamicMemory( LPVOID data )
	{
		Alloc( 1 );
		CopyMemory( m_pObject, data, sizeof( VALUE ) );
	}
	//!				
	//!	@brief	�f�X�g���N�^
	//!				
	//!	@since	2016/09/06	Takano	�V�K�쐬
	//!				
	virtual	~CDynamicMemory()
	{
		Dealloc();
	}

	//!				
	//!	@brief	�������̓��I���蓖��
	//!				
	//!	@details	�����̃T�C�Y�������̃��������m�ۂ��܂��B
	//!				���Ƀ����������蓖�Ă�ꂽ��ԂŌĂяo���ꂽ�ꍇ�́A�m�ۂ���Ă��郁��������U�j�����A�ēx���������m�ۂ��܂��B
	//!	
	//!	@param[in]	size	�m�ۂ������������T�C�Y(�z��T�C�Y)
	//!	
	//!	@retval	���蓖�Ă�ꂽ�������z��̐擪�A�h���X�|�C���^
	//!	
	//!	@since	2016/09/06	Takano	�V�K�쐬
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
	//!	@brief	�������̉��
	//!				
	//!	@details	�m�ۂ��ꂽ��������������܂��B
	//!				�������̊m�ۂ����s����Ă��Ȃ��ꍇ�͏��������s���܂���B
	//!	
	//!	@since	2016/09/06	Takano	�V�K�쐬
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
	//!	@brief	�������̏�����
	//!				
	//!	@details	�m�ۂ��ꂽ�������̈�� 0 �ŏ��������܂��B
	//!	
	//!	@since	2016/09/06	Takano	�V�K�쐬
	//!				
	void Clear()
	{
		ZeroMemory( m_pObject, sizeof( VALUE ) * m_size );
	}

	//!				
	//!	@brief	�������T�C�Y�擾
	//!				
	//!	@details	�m�ۂ��ꂽ�z�񐔂��擾���܂��B
	//!	
	//!	@retval	�m�ۂ��ꂽ�z��
	//!	
	//!	@since	2016/09/06	Takano	�V�K�쐬
	//!				
	__declspec(property(get= GetLength )) DWORD Length;
	DWORD GetLength()
	{
		return m_size;
	}
	
	//!				
	//!	@brief	�擪�A�h���X�擾
	//!				
	//!	@details	�m�ۂ��ꂽ�������o�b�t�@�̐擪�A�h���X���擾���܂��B
	//!	
	//!	@retval	�m�ۂ��ꂽ�������o�b�t�@�̐擪�A�h���X
	//!	
	//!	@since	2016/09/06	Takano	�V�K�쐬
	//!				
	__declspec(property(get=GetPointer)) VALUE* Pointer;
	VALUE* GetPointer()
	{
		return m_pObject;
	}
	//!				
	//!	@brief	�擪�̎Q�Ǝ擾
	//!				
	//!	@details	�m�ۂ��ꂽ�������o�b�t�@�̐擪�̎Q�Ƃ��擾���܂��B
	//!	
	//!	@retval	�m�ۂ��ꂽ�������o�b�t�@�̐擪�̎Q��
	//!	
	//!	@since	2016/09/06	Takano	�V�K�쐬
	//!				
	__declspec( property( get = GetEntity ) ) VALUE& Entity;
	VALUE& GetEntity()
	{
		return *m_pObject;
	}

private:
	VALUE*	m_pObject;	//	�m�ۂ����o�b�t�@�̃|�C���^
	DWORD	m_size;		//	�m�ۂ��郁�����T�C�Y
};

#endif/*__DYNAMICMEMORY_H__*/
