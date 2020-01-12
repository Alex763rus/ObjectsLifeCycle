if db_id('ObjectsLifeCycle') is null
	Create database ObjectsLifeCycle
go

use ObjectsLifeCycle
go

-- ���:
if object_id('tag') is null
	create table tag(
		  tagId numeric(15,0) identity
	    , tagNum varchar(50)
		, dateInstall smalldatetime
		primary key (tagId)
	)

-- ������ ���������:
if object_id('strength') is null
create table strength(
	  strengthId numeric(15,0) identity
	, [name] varchar(50)
	primary key (strengthId)
)

-- ����������� �����:
if object_id('standardLen') is null
create table standardLen(
	  standardLenId numeric(15,0) identity
	, lengthVal float
	primary key (standardLenId)
)

-- ��� �����:
if object_id('pipeType') is null
create table pipeType(
	  pipeTypeId numeric(15,0) identity
	, pipeTypeVal varchar(20)
	primary key (pipeTypeId)
)

-- �������:
if object_id('pipeDiameter') is null
create table pipeDiameter(
	  pipeDiameterId numeric(15,0) identity
	, pipeDiameterVal float
	primary key (pipeDiameterId)
)

-- ������������ ��������:
if object_id('intercoating') is null
create table intercoating(
	  intercoatingId numeric(15,0) identity
	, brief varchar(25)
	, discript varchar(100)
	primary key (intercoatingId)
)

-- �����:
if object_id('firm') is null
create table firm(
	  firmId numeric(15,0) identity
	, brief varchar(25)
	, [name] varchar(50)
	primary key (firmId)
)

-- ������:
if object_id('protection') is null
create table protection(
	  protectionId numeric(15,0) identity
	, brief varchar(25)
	primary key (protectionId)
)

-- ����������:
if object_id('certificate') is null
create table [certificate](
	  certificateId numeric(15,0) identity
	, patch varchar(100)
	primary key (certificateId)
)

-- �����:
if object_id('coupling') is null
create table coupling(
	  couplingId numeric(15,0) identity
    , batchNum numeric(15,0)    -- ����� ������
	, smeltingNum numeric(15,0) -- ����� ������

	, pipeDiameterId numeric(15,0)
	, protectionId numeric(15,0)
	, strengthId numeric(15,0)
	primary key (couplingId)
	, FOREIGN KEY (pipeDiameterId) REFERENCES pipeDiameter(pipeDiameterId)
	, FOREIGN KEY (protectionId) REFERENCES protection(protectionId)
	, FOREIGN KEY (strengthId) REFERENCES strength(strengthId)
)

-- �����:
if object_id('pipe') is null
create table pipe(
	  pipeId numeric(15,0) identity
    , pipeNum numeric(15,0)
	, factoryNum numeric(15,0)  -- ��������� �����
	, batchNum numeric(15,0)    -- ����� ������
	, smeltingNum numeric(15,0) -- ����� ������
    , gostThCon varchar(50)     -- ���� ��
	, packageNum varchar(50)    -- ����� ������
	, releaseDate smalldatetime -- ���� �������
	, outerCoating int default 0 -- ������� �������� (������� 1/0 ����/���)

	, certificateId numeric(15,0) -- ����������
	, strengthId numeric(15,0)    -- ������ ���������
	, standardLenId numeric(15,0) -- ����������� �����
	, pipeTypeId numeric(15,0)    -- ��� �����
	, pipeDiameterId numeric(15,0) -- ������� �����
	, couplingId numeric(15,0)     -- �����
	, tagId numeric(15,0)          -- �����
	, intercoatingId numeric(15,0) 
	  
	  primary key (pipeId)
	, FOREIGN KEY (certificateId) REFERENCES certificate(certificateId)
	, FOREIGN KEY (strengthId) REFERENCES strength(strengthId)
	, FOREIGN KEY (standardLenId) REFERENCES standardLen(standardLenId)
	, FOREIGN KEY (pipeTypeId) REFERENCES pipeType(pipeTypeId)
	, FOREIGN KEY (pipeDiameterId) REFERENCES pipeDiameter(pipeDiameterId)
	, FOREIGN KEY (couplingId) REFERENCES coupling(couplingId)
	, FOREIGN KEY (tagId) REFERENCES tag(tagId)
	, FOREIGN KEY (intercoatingId) REFERENCES intercoating(intercoatingId)

)

--================��������� ��������� �������:
-- ���:
if not exists(select 1 from tag where tagNum = 'E2003A33D5297889349F9AA6')
	insert into tag(tagNum, dateInstall) select 'E2003A33D5297889349F9AA6', '20200112'

-- ������ ���������:
if not exists(select 1 from strength where [name] = '�')
	insert into strength([name]) select '�'
if not exists(select 1 from strength where [name] = '�(�)')
	insert into strength([name]) select '�(�)'
if not exists(select 1 from strength where [name] = '�(�)')
	insert into strength([name]) select '�(�)'

-- ����������� �����:
if not exists(select 1 from standardLen where lengthVal = 9.5)
	insert into standardLen(lengthVal) select 9.5
if not exists(select 1 from standardLen where lengthVal = 6)
	insert into standardLen(lengthVal) select 6
if not exists(select 1 from standardLen where lengthVal = 7)
	insert into standardLen(lengthVal) select 7

-- ��� �����:
if not exists(select 1 from pipeType where pipeTypeVal = '���')
	insert into pipeType(pipeTypeVal) select '���'
if not exists(select 1 from pipeType where pipeTypeVal = '����')
	insert into pipeType(pipeTypeVal) select '����'

-- �������:
if not exists(select 1 from pipeDiameter where pipeDiameterVal = 60)
	insert into pipeDiameter(pipeDiameterVal) select 60
if not exists(select 1 from pipeDiameter where pipeDiameterVal = 73)
	insert into pipeDiameter(pipeDiameterVal) select 73

-- ������������ ��������:
if not exists(select 1 from intercoating where brief = '�����-1')
	insert into intercoating(brief, discript) select '�����-1', '��������� ��������� ����� ������� � ����� ������� �������'

-- �����:
if not exists(select 1 from firm where brief = 'MajorCrack')
	insert into firm(brief, [name]) select 'MajorCrack', 'MSPLAG17'

-- ������:
if not exists(select 1 from protection where brief = '��������������')
	insert into protection(brief) select '��������������'

-- ����������:
if not exists(select 1 from [certificate] where patch = 'D:\Resources\')
	insert into [certificate](patch) select 'D:\Resources\'

-- �����:
if not exists(select 1 from coupling where couplingId = 1)
	insert into coupling(batchNum, smeltingNum, pipeDiameterId, protectionId, strengthId) 
	select 9812, 739111, 1,1,1

-- �����:
if not exists(select 1 from pipe where pipeId = 1)
	insert into pipe(pipeNum, factoryNum, batchNum, smeltingNum, gostThCon, packageNum, releaseDate, certificateId, strengthId, standardLenId, pipeTypeId, pipeDiameterId, couplingId, tagId, intercoatingId ) 
	select pipeNum = 123456789
	     , factoryNum = 204187
		 , batchNum = 32605
		 , smeltingNum = 1825846
		 , gostThCon = '633-80'
		 , packageNum = '�39035'
		 , releaseDate = '20181026'
		 , certificateId = 1
		 , strengthId = 1
		 , standardLenId = 1
		 , pipeTypeId = 1
		 , pipeDiameterId = 1
		 , couplingId = 1
		 , tagId = 1
		 , intercoatingId = 1

/*
select * from tag
select * from strength
select * from standardLen
select * from pipeType
select * from pipeDiameter
select * from intercoating
select * from firm
select * from protection
select * from [certificate]
select * from coupling
select * from pipe
*/
select * from tag
select * from strength
select * from standardLen
select * from pipeType
select * from pipeDiameter
select * from intercoating
select * from firm
select * from protection
select * from [certificate]
select * from coupling
select * from pipe
 --drop database ObjectsLifeCycle






